using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using OpenQA.Selenium.Support.UI;
using Milliman.Pixel.Web.Tests.PageObjects.Pages;
using System.Threading;
using Type = Milliman.Pixel.Web.Tests.PageObjects.Type;
using Extras = SeleniumExtras.WaitHelpers;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class UserDDLtests
    {
        IWebDriver driver;
        LoginPage loginPage;

        WebDriverWait wait;
        WebDriverWait waitQuick;

        static string user = "03_testuser@test.com";

        static string randomString = Utils.RandomString(5);

        static string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString()).ToString()).ToString();

        const string validationPopupDatasetWithoutLatLong = "Mandatory column in table 'Policies' called 'Latitude' is not present.\r\nMandatory column in table 'Policies' called 'Longitude' is not present.";
        const string validationPopupDatasetWithoutPolicies = "Mandatory file with Policies is missing";

        static string fileRates = "Carrier Rates";

        const string adminSharedRates = "AdminShared";
        const string adminClientRates = "AdminLuxoft";
        const string adminTestRates = "AdminTest";
        const string adminSharedLosses = "LossAdminShared";
        const string adminClientLosses = "LossAdminLuxoft";
        const string adminTestLosses = "LossAdminTest";

        [SetUp]
        public void InitializeAdmin()
        {
            driver = new ChromeDriver();
            driver.Url = "https://pixel.com";
           
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10000));
            waitQuick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Order(1)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "ImportDatasetsTestData")]
        public void UploadDatasetByUserTest(string type, string datasetDifference)
        {
            loginPage.LoginToApplication(user, "34566");

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            ImportDataset(datasetDifference, DatasetUserDescription(type), PathToZipUser(type), type);

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(user, dataProcessingStatus.ClientUser[0].Text, "Incorrect User in User column");
                Assert.AreEqual("Add", dataProcessingStatus.UserRequestType[0].Text, "Incorrect Request type");
                Assert.AreEqual("Dataset", dataProcessingStatus.UserDataType[0].Text, "Incorrect Data type");
            });

            wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In queue"));

            wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In progress"));

            if (!dataProcessingStatus.UserStatus[0].Text.Equals("Completed successfully") | !dataProcessingStatus.UserStatus[0].Text.Equals("Failed"))
            {
                wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In queue"));

                wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In progress"));
            }

            Utils.WaitBeforeAssert(driver);

            Assert.AreEqual("Completed successfully", dataProcessingStatus.UserStatus[0].Text, $"Status is not Completed successfully for dataset {datasetName}");
        }

        [Order(2)]
        [CustomRetry]
        [TestCase(Type.Flood_Louisiana_FEMA, "without Policies", validationPopupDatasetWithoutPolicies)]
        //[TestCase(Type.Flood_Louisiana_FEMA, "without LatLong", validationPopupDatasetWithoutLatLong)]
        public void CheckValidationPopupWhileUploadingTest(string type, string datasetDifference, string validationMessage)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.DataserImportMenuLocator.Click();

            var datasetImportPage = new DatasetImportPage(driver);

            UploadDataset($"{PathToZipUser(type)} {datasetDifference}", driver);

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            Assert.True(datasetImportPage.DatasetValidationPopupLocator.Text.Equals(validationMessage));
        }

        [Order(3)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "ImportDatasetsFullCycleTestData")]
        public void CheckUserDatasetOnSourceTest(string type, string datasetDifference)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            var treeView = new UserStoryListTreeViewPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            treeView.openTreeView();

            treeView.Folder[1].ClickEx(driver);

            Utils.WaitUntilLoadingPlaceholderDisappears(driver, secondtToWait: 20);

            treeView.FilterDataset(datasetName);

            Utils.WaitBeforeAssert(driver);

            Assert.True(treeView.Description[0].Text.Contains(datasetName),
                $"Dataset {datasetName} doesn`t display on Source");
        }

        [Order(4)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "ImportDatasetsFullCycleTestData")]
        public void AddRatesByUserTest(string type, string datasetDifference)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            var menu = new MenuPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            menu.DataMenuLocator.ClickEx(driver);

            menu.CarrierRatesAddMenuLocator.Click();

            var carrierAddPage = new CarrierRatesAddPage(driver);

            carrierAddPage.SelectDatasetButtonLocator.Click();

            carrierAddPage.SelectDataset(datasetName);

            Assert.AreEqual(carrierAddPage.SelectDatasetButtonLocator.Text, datasetName, "Dataset is not selected");

            carrierAddPage.CarrierNameTextBoxLocator.SendKeys($"User{randomString}");

            Assert.True(carrierAddPage.EnteredCarrierNameLocator.Text.Equals($"LUXFT-User{randomString}"), "Carrier name is not entered");

            uploadFile(PathToRates(type), driver);

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            Assert.True(carrierAddPage.UploadedFileTextLocator.Text.Contains(fileRates), "File is not loaded");

            Utils.Scroll(carrierAddPage.FooterLocator, driver);

            carrierAddPage.StartButtonLocator.Click();

            waitQuick.Until(ExpectedConditions.ElementToBeClickable(carrierAddPage.ConfirmAddRatesButtonLocator)).Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In queue"));

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In progress"));

            Utils.WaitBeforeAssert(driver);

            Assert.AreEqual("Completed successfully", dataProcessingStatus.UserStatus[0].Text, "Status is not Completed successfully");
        }

        [Order(5)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "ImportDatasetsFullCycleTestData")]
        public void AddLossesByUserTest(string type, string datasetDifference)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            var menu = new MenuPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            menu.DataMenuLocator.ClickEx(driver);

            menu.LossesAddMenuLocator.Click();

            var lossesAddPage = new LossesAddPage(driver);

            lossesAddPage.SelectDatasetButtonLocator.Click();

            lossesAddPage.SelectDataset(datasetName);

            Assert.AreEqual(lossesAddPage.SelectDatasetButtonLocator.Text, datasetName);

            UploadLosses(type, "LossUser1", driver);

            wait.Until(ExpectedConditions.ElementToBeClickable(lossesAddPage.StartButtonLocator)).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(lossesAddPage.ConfirmAddRatesButtonLocator)).Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In queue"));

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In progress"));

            Utils.WaitBeforeAssert(driver);

            Assert.AreEqual("Completed successfully", dataProcessingStatus.UserStatus[0].Text, "Status is not Completed successfully");
        }

        [Order(6)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "AddRatesByAdminTestData")]
        public void AddRatesByAdminTest(string type, string datasetDifference, string carrierName, bool isTypeShared, bool isLuxoftRate)
        {
            loginPage.LoginToApplication();

            var menu = new MenuPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            menu.DataMenuLocator.ClickEx(driver);

            menu.CarrierRatesAddMenuLocator.Click();

            var carrierAddPage = new CarrierRatesAddPage(driver);

            carrierAddPage.SelectDatasetButtonLocator.Click();

            carrierAddPage.SelectDataset(datasetName);

            Assert.AreEqual(carrierAddPage.SelectDatasetButtonLocator.Text, datasetName, "Dataset is not selected");

            carrierAddPage.CarrierNameTextBoxLocator.SendKeys($"{carrierName}{randomString}");

            Assert.True(carrierAddPage.EnteredCarrierNameLocator.Text.Contains($"{carrierName}{randomString}"), "Carrier name is not entered");

            if (!isTypeShared)
            {
                carrierAddPage.CarrierTypeButtonLocator.ClickEx(driver);
                carrierAddPage.ClientTypeLocator.Click();
                Thread.Sleep(500);

                if (isLuxoftRate)
                {
                    carrierAddPage.SelectClient("Luxoft");
                }
                else
                {
                    carrierAddPage.SelectClient("Test");
                }
            }

            uploadFile(PathToRates(type), driver);

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            Assert.True(carrierAddPage.UploadedFileTextLocator.Text.Contains(fileRates), "File is not loaded");

            Utils.Scroll(carrierAddPage.FooterLocator, driver);

            carrierAddPage.StartButtonLocator.Click();

            waitQuick.Until(ExpectedConditions.ElementToBeClickable(carrierAddPage.ConfirmAddRatesButtonLocator)).Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In queue"));

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In progress"));

            Utils.WaitBeforeAssert(driver);

            Assert.AreEqual("Completed successfully", dataProcessingStatus.Status[0].Text, "Status is not Completed successfully");
        }

        [Order(7)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "AddLossesByAdminTestData")]
        public void AddLossesByAdminTest(string type, string datasetDifference, string lossName, bool isTypeShared, bool isLuxoftLoss)
        {
            loginPage.LoginToApplication();

            var menu = new MenuPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            menu.DataMenuLocator.ClickEx(driver);

            menu.LossesAddMenuLocator.Click();

            var lossesAddPage = new LossesAddPage(driver);

            lossesAddPage.SelectDatasetButtonLocator.Click();

            lossesAddPage.SelectDataset(datasetName);

            Assert.AreEqual(lossesAddPage.SelectDatasetButtonLocator.Text, datasetName);

            UploadLosses(type, lossName, driver);

            if (!isTypeShared)
            {
                lossesAddPage.CarrierTypeButtonLocator.ClickEx(driver);
                lossesAddPage.ClientTypeLocator.Click();
                Thread.Sleep(500);
                if (isLuxoftLoss)
                {
                    lossesAddPage.SelectClient("Luxoft");
                }
                else
                {
                    lossesAddPage.SelectClient("Test");
                }
            }

            wait.Until(ExpectedConditions.ElementToBeClickable(lossesAddPage.StartButtonLocator));

            lossesAddPage.ScrollToStartButton();

            lossesAddPage.StartButtonLocator.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(lossesAddPage.ConfirmAddRatesButtonLocator)).Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In queue"));

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In progress"));

            Utils.WaitBeforeAssert(driver);

            Assert.AreEqual("Completed successfully", dataProcessingStatus.Status[0].Text, "Status is not Completed successfully");
        }

        [Order(8)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "ImportDatasetsFullCycleTestData")]
        public void CheckAddedCarriersAndLossesTest(string type, string datasetDifference)
        {
            loginPage.LoginToApplication();

            var menu = new MenuPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.DatasetManagementMenuLocator.Click();

            var datasetsPage = new DatasetsPage(driver);

            Utils.WaitUntilLoadingPlaceholderDisappears(driver);

            datasetsPage.SelectDataset(datasetName);

            var datasetsToClientsPage = new DatasetsToClientsPage(driver);

            Utils.WaitUntilLoadingPlaceholderDisappears(driver);

            datasetsToClientsPage.OpenSettingsForClient("Luxoft");

            var clientToDatasetPage = new ClientToDatasetPage(driver);

            clientToDatasetPage.DefaultStoryButtonLocator.WaitForElementPresentAndEnabled(driver);

            Assert.Multiple(() =>
            {
                Assert.True(clientToDatasetPage.DefaultStoryButtonLocator.Text.Contains("Default (Default)"), "Dafault story isn`t selected");

                Assert.True(clientToDatasetPage.CarrierIsPresent($"LUXFT-User{randomString}"), $"Carrier LUXFT-User{randomString} isn`t present");
                Assert.True(clientToDatasetPage.CarrierIsPresent($"{adminSharedRates}{randomString} (cur)"), $"Carrier {adminSharedRates}{randomString} (cur) isn`t present");
                Assert.True(clientToDatasetPage.CarrierIsPresent($"{adminClientRates}{randomString}"), $"Carrier {adminClientRates}{randomString} isn`t present");

                Assert.True(clientToDatasetPage.CarrierIsDisabled($"{adminTestRates}{randomString}"), $"Carrier {adminTestRates}{randomString} isn`t disabled");

                Assert.True(clientToDatasetPage.CarrierIsAllowedReport($"LUXFT-User{randomString}"), $"Carrier LUXFT-User{randomString} isn`t allowed to report");
                Assert.True(clientToDatasetPage.CarrierIsAllowedReport($"{adminClientRates}{randomString}"), $"Carrier {adminClientRates}{randomString} isn`t allowed to report");
                Assert.True(!clientToDatasetPage.CarrierIsAllowedReport($"{adminSharedRates}{randomString} (cur)"), $"Carrier {adminSharedRates}{randomString} (cur) is allowed to report");

                Assert.True(clientToDatasetPage.CarrierIsAllowedExport($"LUXFT-User{randomString}"), $"Carrier LUXFT-User{randomString} isn`t allowed to export");
                Assert.True(clientToDatasetPage.CarrierIsAllowedExport($"{adminClientRates}{randomString}"), $"Carrier {adminClientRates}{randomString} isn`t allowed to export");
                Assert.True(!clientToDatasetPage.CarrierIsAllowedExport($"{adminSharedRates}{randomString} (cur)"), $"Carrier {adminSharedRates}{randomString} (cur) is allowed to export");

                Assert.True(clientToDatasetPage.LossIsPresent("LossUser1"), "Loss LossUser1 isn`t present");
                Assert.True(clientToDatasetPage.LossIsPresent(adminClientLosses), $"Loss {adminClientLosses} isn`t present");
                Assert.True(clientToDatasetPage.LossIsPresent($"{adminSharedLosses} (cur)"), $"Loss {adminSharedLosses} (cur) isn`t present");

                Assert.True(clientToDatasetPage.LossIsDisabled(adminTestLosses), $"Loss {adminTestLosses} isn`t disabled");

                Assert.True(clientToDatasetPage.LossIsAllowedReport("LossUser1"), "Loss LossUser1 isn`t allowed to report");
                Assert.True(clientToDatasetPage.LossIsAllowedReport(adminClientLosses), $"Loss {adminClientLosses} isn`t allowed to report");
                Assert.True(!clientToDatasetPage.LossIsAllowedReport($"{adminSharedLosses} (cur)"), $"Loss {adminSharedLosses} (cur) is allowed to report");

                Assert.True(clientToDatasetPage.LossIsAllowedExport("LossUser1"), "Loss LossUser1 isn`t allowed to export");
                Assert.True(clientToDatasetPage.LossIsAllowedExport(adminClientLosses), $"Loss {adminClientLosses} isn`t allowed to export");
                Assert.True(!clientToDatasetPage.LossIsAllowedExport($"{adminSharedLosses} (cur)"), $"Loss {adminSharedLosses} (cur) is allowed to export");
            });
        }

        [Order(9)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "DeleteDatasetsTestData")]
        public void CheckDeleteDatasetTest(string type, string datasetDifference)
        {
            loginPage.LoginToApplication();

            var menu = new MenuPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.DatasetArchiveDeleteMenuLocator.Click();

            var datasetDeletePage = new DatasetDeletePage(driver);

            Utils.WaitUntilLoadingPlaceholderDisappears(driver);

            datasetDeletePage.SelectDatasetButtonLocator.Click();

            datasetDeletePage.SelectDataset(datasetName);

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.AreEqual(datasetName, datasetDeletePage.SelectDatasetButtonLocator.Text, $"Dataset {datasetName} is not selected");

            datasetDeletePage.StartButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In queue"));

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In progress"));

            Utils.WaitBeforeAssert(driver);

            Assert.AreEqual("Completed successfully", dataProcessingStatus.Status[0].Text, "Status is not Completed successfully");
        }

        [Order(10)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "DeleteDatasetsTestData")]
        public void CheckDatasetIsDisappeardTest(string type, string datasetDifference)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            var dashboardPage = new DashboardPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            dashboardPage.FilterDataset(datasetName);

            Assert.AreEqual(0, dashboardPage.Description.Count, $"Dataset {datasetName} is not deleted");
        }

        [Order(11)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "RemainingDatasetsTestData")]
        public void AllowAllRatesAndLossesReportAndExportTest(string type, string datasetDifference)
        {
            loginPage.LoginToApplication();

            var menu = new MenuPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.DatasetManagementMenuLocator.Click();

            var datasetsPage = new DatasetsPage(driver);

            Utils.WaitUntilLoadingPlaceholderDisappears(driver);

            datasetsPage.SelectDataset(datasetName);

            var datasetsToClientsPage = new DatasetsToClientsPage(driver);

            Utils.WaitUntilLoadingPlaceholderDisappears(driver);

            datasetsToClientsPage.OpenSettingsForClient("Luxoft");

            var clientToDatasetPage = new ClientToDatasetPage(driver);

            clientToDatasetPage.DefaultStoryButtonLocator.WaitForElementPresentAndEnabled(driver);

            clientToDatasetPage.CheckAllowAll(clientToDatasetPage.RatesAllowedReportAllCheckboxLocator);

            clientToDatasetPage.CheckAllowAll(clientToDatasetPage.LossesAllowedReportAllCheckboxLocator);

            Utils.WaitBeforeAssert(driver);

            Assert.Multiple(() =>
            {
                Assert.True(clientToDatasetPage.CheckRates(clientToDatasetPage.CarrierAllowedReportCheckboxLocator), "Check all is not working correctly");
                Assert.True(clientToDatasetPage.CheckLosses(clientToDatasetPage.LossAllowedReportCheckboxLocator), "Check all is not working correctly");
            });
        }

        [Order(12)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "RemainingDatasetsTestData")]
        public void CheckRatesAndLossesInStoryFilterTest(string type, string datasetDifference)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            var dashboardPage = new DashboardPage(driver);

            var datasetName = $"{DatasetUserDescription(type)} {datasetDifference}";

            dashboardPage.FilterDataset(datasetName);

            Assert.True(dashboardPage.Description.Count > 0, $"Dataset {DatasetUserDescription(type)} {datasetDifference} is not present");

            dashboardPage.FirstDefaultDatasetLocator.ClickEx(driver);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            defaultDatasetPage.ExpandFilterLocator.WaitForElementPresentAndEnabled(driver).Click();

            defaultDatasetPage.GeneralPanelLocator.WaitForElementPresentAndEnabled(driver);

            defaultDatasetPage.ExpandPrimaryCarrierLocator.Click();

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsCarrierDisplayed($"LUXFT-User{randomString}"), $"Carrier LUXFT-User{randomString} isn`t present");
                Assert.True(defaultDatasetPage.IsCarrierDisplayed($"{adminSharedRates}{randomString} (cur)"), $"Carrier {adminSharedRates}{randomString} (cur) isn`t present");
                Assert.True(defaultDatasetPage.IsCarrierDisplayed($"{adminClientRates}{randomString}"), $"Carrier {adminClientRates}{randomString} isn`t present");
                Assert.False(defaultDatasetPage.IsCarrierDisplayed($"{adminTestRates}{randomString}"), $"Carrier {adminTestRates}{randomString} isn`t disabled");

                if (type == Type.DP3_Florida | type == Type.HO6_Florida | type == Type.HO3_South_Carolina) defaultDatasetPage.LossesPanelLocator.Click();

                Assert.True(defaultDatasetPage.IsLossDisplayed("LossUser1"), "Loss LossUser1 isn`t displayed");
                Assert.True(defaultDatasetPage.IsLossDisplayed(adminClientLosses), $"Loss {adminClientLosses} isn`t displayed");
                Assert.True(defaultDatasetPage.IsLossDisplayed($"{adminSharedLosses}"), $"Loss {adminSharedLosses} isn`t displayed");
                Assert.False(defaultDatasetPage.IsLossDisplayed(adminTestLosses), $"Loss {adminTestLosses} is displayed");
            });
        }

        [Order(13)]
        [CustomRetry]
        [TestCaseSource(typeof(DDLTestCases), "ImportDatasetsMandatoryTestData")]
        public void UploadDatasetMandatoryTest(string type)
        {
            loginPage.LoginToApplication(user, "NBV87^yu");

            var datasetName = $"{DatasetUserDescription(type)}";

            ImportDataset("Mandatory", DatasetUserDescription(type), PathToZipMandatoryOnly(type), type);

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            //Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Integration/DataProcessingStatus"),
            //    "Data processing Page is not opened");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(user, dataProcessingStatus.ClientUser[0].Text, "Incorrect User in User column");
                Assert.AreEqual("Add", dataProcessingStatus.UserRequestType[0].Text, "Incorrect Request type");
                Assert.AreEqual("Dataset", dataProcessingStatus.UserDataType[0].Text, "Incorrect Data type");
            });

            wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In queue"));

            wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In progress"));

            if (!dataProcessingStatus.UserStatus[0].Text.Equals("Completed successfully") | !dataProcessingStatus.UserStatus[0].Text.Equals("Failed"))
            {
                wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In queue"));

                wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[5]"), "In progress"));
            }

            Utils.WaitBeforeAssert(driver);

            Assert.AreEqual("Completed successfully", dataProcessingStatus.UserStatus[0].Text, $"Status is not Completed successfully for dataset {datasetName}");
        }

        public static void WriteToFileData(string PathSample, string Path, string Text)
        {
            string content = File.ReadAllText(PathSample);
            content = Text + "\r\n" + content;
            File.WriteAllText(Path, content);
            Thread.Sleep(1000);
        }

        public static void WriteToFileSource(string Path, string Text)
        {
            File.WriteAllText(Path, Text);
        }

        public static void UploadLosses(string type, string lossName, IWebDriver driver)
        {
            By uploadLossesButton = By.Id("txtUploadLosses");
            By uploadLossDataButton = By.Id("txtUploadLossData");

            WriteToFileData($@"{PathToLosses(type)}\Loss data sample.csv",
                $@"{PathToLosses(type)}\Loss data.csv", $"PolicyId|{lossName}");

            WriteToFileSource($@"{PathToLosses(type)}\Loss source.csv",
                $"LossFieldName|LossDisplayName\r\n{lossName}|{lossName}");

            driver.FindElement(uploadLossesButton).SendKeys($@"{PathToLosses(type)}\Loss source.csv");

            driver.FindElement(uploadLossDataButton).SendKeys($@"{PathToLosses(type)}\Loss data.csv");
        }

        public void UploadDataset(string path, IWebDriver driver)
        {
            By uploadButton = By.Id("txtUploadFileDataset");
            driver.FindElement(uploadButton).SendKeys($"{path}.zip");
        }
        public void uploadFile(string path, IWebDriver driver)
        {
            By uploadButton = By.Id("txtUploadFileRate");
            driver.FindElement(uploadButton).SendKeys($"{path}.csv");
        }

        public void ImportDataset(string datasetDifference, string datasetDescription, string pathToZip, string Type)
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.DataserImportMenuLocator.Click();

            var datasetImportPage = new DatasetImportPage(driver);

            datasetImportPage.SelectDatasetTypeButtonLocator.Click();

            datasetImportPage.SelectDatasetType(Type);

            UploadDataset($"{pathToZip} {datasetDifference}", driver);

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            datasetImportPage.DatasetDescriptiontextboxLocator.SendKeys($"{datasetDescription} {datasetDifference}");

            Assert.True(datasetImportPage.UploadedFileTextLocator.Text.Contains($"{datasetDifference}"),
                "dataset is not selected");

            Utils.Scroll(datasetImportPage.FooterLocator, driver);

            datasetImportPage.StartButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);
        }

        public string PathToZipUser(string type)
        {
            return Path.Combine(path, "DDLdatasets", type, type);
        }

        public string PathToZipMandatoryOnly(string type)
        {
            return Path.Combine(path, "DDLdatasets", "MandatoryFields", type);
        }

        public string DatasetUserDescription(string type)
        {
            return $"TestUser{randomString} {type}";
        }

        public string PathToRates(string type)
        {
            return Path.Combine(path, "DDLdatasets", type, fileRates);
        }

        public static string PathToLosses(string type)
        {
            return Path.Combine(path, "DDLdatasets", type);
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}

