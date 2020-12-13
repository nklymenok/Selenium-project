using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages;
using Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Extras = SeleniumExtras.WaitHelpers;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class StoryTests
    {
        IWebDriver driver;
        LoginPage loginPage;

        WebDriverWait wait;
        WebDriverWait waitQuick;

        static string user = "03_testuser@test.com";

        static string randomString = Utils.RandomString(5);

        const string adminSharedRates = "AdminShared";
        const string adminClientRates = "AdminLuxoft";
        const string adminTestRates = "AdminTest";
        const string adminSharedLosses = "LossAdminShared";
        const string adminClientLosses = "LossAdminLuxoft";
        const string adminTestLosses = "LossAdminTest";

        static string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString()).ToString()).ToString();
        

        [SetUp]
        public void InitializeAdmin()
        {
            driver = new ChromeDriver();
            driver.Url = "https://qa.millimanpixel.com";
            //driver.Url = "https://preprod.millimanpixel.com/";

            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10000));
            waitQuick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            loginPage.LoginToApplication(user, "NBV87^yu");
        }

        [Order(1)]
        [CustomRetry]
        [TestCase("Flood Florida FEMA full")]
        public void UploadDatasetByUserTest(string dataset)
        {          
            ImportDataset(dataset, PathToZipUser(dataset));

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

            Assert.AreEqual("Completed successfully", dataProcessingStatus.UserStatus[0].Text, $"Status is not Completed successfully for dataset {dataset}");
        }

        [Order(2)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void CheckCreateStoryTest(string dataset)
        {
            var dashboardPage = new DashboardPage(driver);

            dashboardPage.FilterDataset($"ReportTesting{randomString} {dataset}");

            Assert.True(dashboardPage.Description.Count > 0, $"Dataset ReportTesting{randomString} {dataset} is not present");

            dashboardPage.OpenDataset($"ReportTesting{randomString} {dataset}", "Default");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            //Assert.True(driver.Url.Contains("https://qa.millimanpixel.com/Dashboard/StoryPage/"),
            //   "default page is not opened");

            defaultDatasetPage.ExpandFilterLocator.WaitForElementPresentAndEnabled(driver, 100);

            defaultDatasetPage.ExpandFilterLocator.WaitForElementPresentAndEnabled(driver).Click();

            defaultDatasetPage.GeneralPanelLocator.WaitForElementPresentAndEnabled(driver);

            Assert.True(defaultDatasetPage.GeneralPanelLocator.Displayed, "filter is not opened");

            defaultDatasetPage.SelectPrimaryCarrier($"{adminClientRates}");

            defaultDatasetPage.SelectSecondaryCarrier("LUXFT-User");

            defaultDatasetPage.SelectLoss(adminClientLosses);

            defaultDatasetPage.ScrollToUpdateResultsButton();

            defaultDatasetPage.UpdateResultsButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.ActionsButtonLocator.Click();

            defaultDatasetPage.SaveAsButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);
   
            new Actions(driver).SendKeys($"TestStory {randomString}").Perform();

            defaultDatasetPage.StoryDescriptionTextBoxLocator.WaitForElementPresentAndEnabled(driver).SendKeys($"TestStory {randomString}");

            defaultDatasetPage.SaveDashboardStoryButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.True(defaultDatasetPage.StoryNameLocator.Text.Equals($"TestStory {randomString}"));
        }

        [Order(3)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void CheckPoliciesWithPremiumReportTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsPoliciesWithPremiumReportExist($"{adminClientRates}", "LUXFT-User"));
                Assert.True(defaultDatasetPage.PoliciesWithPremiumTabLocator.GetAttribute("class").Contains(" activeStoryTable"));
            });
        }

        [Order(4)]
        [CustomRetry]
        public void CheckAveragePremiumReportTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.AveragePremiumTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.AveragePremiumMinimizedLocator.Click();
            }
            else defaultDatasetPage.AveragePremiumTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsAveragePremiumReportExist($"{adminClientRates}", $"LUXFT-User"));
                Assert.True(defaultDatasetPage.AveragePremiumTabLocator.GetAttribute("class").Contains(" activeStoryTable"));
            });
        }

        [Order(5)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void CheckWinRateReportTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.WinRateTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.WinRateMinimizedLocator.Click();
            }
            else defaultDatasetPage.WinRateTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsWinRateReportExist($"{adminClientRates}", "LUXFT-User"));
                Assert.True(defaultDatasetPage.WinRateTabLocator.GetAttribute("class").Contains(" activeStoryTable"));
            });
        }

        [Order(6)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void CheckCarrierByVariableReportTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.CarrierByVariableTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.CarrierByVariableMinimizedLocator.Click();
            }
            else defaultDatasetPage.CarrierByVariableTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Utils.Scroll(new DashboardPage(driver).LogoLocator, driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsTableExist("Adj. Ground Elev. (ft)", $"{adminClientRates}", "LUXFT-User", "\r\nLess than 5 feet\r\n$324\r\n$324\r\n14 to 16 feet\r\n$411\r\n$411\r\n30 to 35 feet\r\n$242\r\n$242\r\nOver 40 feet\r\n$711\r\n$711"), $"Incorrect table result for Adj. Ground Elev. (ft). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Adj. Relative Elev. (ft)", $"{adminClientRates}", "LUXFT-User", "\r\n-10 to -5 feet\r\n$324\r\n$324\r\n0 to 5 feet\r\n$617\r\n$617\r\n5 to 10 feet\r\n$411\r\n$411"), $"Incorrect table result for Adj. Relative Elev. (ft). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Agency", $"{adminClientRates}", "LUXFT-User", "\r\nUnknown (Unknown, #Unknown)\r\n$546\r\n$546"), $"Incorrect table result for Agency. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Barrier Island", $"{adminClientRates}", "LUXFT-User", "\r\nFALSE\r\n$568\r\n$568\r\nTRUE\r\n$411\r\n$411"), $"Incorrect table result for Barrier Island. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Base Flood Elevation", $"{adminClientRates}", "LUXFT-User", "\r\nNot Applicable\r\n$617\r\n$617\r\nLess than 5 feet\r\n$324\r\n$324\r\n10 to 20 feet\r\n$411\r\n$411"), $"Incorrect table result for Base Flood Elevation. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Basement", $"{adminClientRates}", "LUXFT-User", "\r\nFALSE\r\n$546\r\n$546"), $"Incorrect table result for Basement. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Building Deductible", $"{adminClientRates}", "LUXFT-User", "\r\nNone\r\n$705\r\n$705\r\n1500\r\n$324\r\n$324\r\n3000\r\n$242\r\n$242\r\n4000\r\n$432\r\n$432"), $"Incorrect table result for Building Deductible. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Building Ins. to Value", $"{adminClientRates}", "LUXFT-User", "\r\n0 - 20%\r\n$705\r\n$705\r\n20 - 40%\r\n$242\r\n$242\r\n40 - 60%\r\n$378\r\n$378"), $"Incorrect table result for Building Ins. to Value. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Building Limit", $"{adminClientRates}", "LUXFT-User", "\r\n0 to 100K\r\n$546\r\n$546"), $"Incorrect table result for Building Limit. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Building Value", $"{adminClientRates}", "LUXFT-User", "\r\n100K to 150K\r\n$546\r\n$546"), $"Incorrect table result for Building Value. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("City", $"{adminClientRates}", "LUXFT-User", "\r\nUnknown\r\n$546\r\n$546"), $"Incorrect table result for City. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Community Identifier", $"{adminClientRates}", "LUXFT-User", "\r\n120012\r\n$242\r\n$242\r\n120015\r\n$24\r\n$24\r\n120063\r\n$2,344\r\n$2,344\r\n120064\r\n$324\r\n$324\r\n120145\r\n$432\r\n$432\r\n120373\r\n$411\r\n$411\r\n125107\r\n$42\r\n$42"), $"Incorrect table result for Community Identifier. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Construction", $"{adminClientRates}", "LUXFT-User", "\r\nMasonry\r\n$546\r\n$546"), $"Incorrect table result for Construction. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Contents Deductible", $"{adminClientRates}", "LUXFT-User", "\r\n1000\r\n$2,344\r\n$2,344\r\n1500\r\n$253\r\n$253\r\n2000\r\n$142\r\n$142\r\n4000\r\n$432\r\n$432"), $"Incorrect table result for Contents Deductible. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Contents Ins. to Value", $"{adminClientRates}", "LUXFT-User", "\r\n0 - 20%\r\n$1,334\r\n$1,334\r\n20 - 40%\r\n$337\r\n$337\r\n40 - 60%\r\n$227\r\n$227\r\n60 - 80%\r\n$24\r\n$24"), $"Incorrect table result for Contents Ins. to Value. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Contents Limit", $"{adminClientRates}", "LUXFT-User", "\r\n5K to 10K\r\n$324\r\n$324\r\n10K to 20K\r\n$1,006\r\n$1,006\r\n30K to 50K\r\n$227\r\n$227\r\n50K to 75K\r\n$24\r\n$24"), $"Incorrect table result for Contents Limit. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Contents Value", $"{adminClientRates}", "LUXFT-User", "\r\n50K to 75K\r\n$546\r\n$546"), $"Incorrect table result for Contents Value. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("County", $"{adminClientRates}", "LUXFT-User", "\r\nAlachua\r\n$42\r\n$42\r\nBay\r\n$242\r\n$242\r\nBradford\r\n$24\r\n$24\r\nCitrus\r\n$2,344\r\n$2,344\r\nClay\r\n$324\r\n$324\r\nLevy\r\n$422\r\n$422"), $"Incorrect table result for County. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Distance To Coast", $"{adminClientRates}", "LUXFT-User", "\r\n0 to 0.25 miles\r\n$411\r\n$411\r\n0.25 to 0.50 miles\r\n$242\r\n$242\r\n4.0 to 5.0 miles\r\n$1,334\r\n$1,334\r\n20.0 to 25.0 miles\r\n$228\r\n$228\r\n30.0 to 40.0 miles\r\n$42\r\n$42"), $"Incorrect table result for Distance To Coast. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Distance To Ocean", $"{adminClientRates}", "LUXFT-User", "\r\n0 to 0.25 miles\r\n$411\r\n$411\r\n5.0 to 6.0 miles\r\n$242\r\n$242\r\n6.0 to 7.0 miles\r\n$2,344\r\n$2,344\r\n20.0 to 25.0 miles\r\n$324\r\n$324\r\n25.0 to 30.0 miles\r\n$432\r\n$432\r\n40.0 to 50.0 miles\r\n$33\r\n$33"), $"Incorrect table result for Distance To Ocean. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Distance To River", $"{adminClientRates}", "LUXFT-User", "\r\n125 to 250 feet\r\n$324\r\n$324\r\n1,000 to 1,250 feet\r\n$1,184\r\n$1,184\r\n1,250 to 1,500 feet\r\n$42\r\n$42\r\nOver 1,500 feet\r\n$362\r\n$362"), $"Incorrect table result for Distance To River. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Elevated Structure", $"{adminClientRates}", "LUXFT-User", "\r\nUnknown\r\n$546\r\n$546"), $"Incorrect table result for Elevated Structure. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Elevation Above River", $"{adminClientRates}", "LUXFT-User", "\r\n0 to 2 feet\r\n$324\r\n$324\r\n4 to 6 feet\r\n$432\r\n$432\r\n6 to 8 feet\r\n$24\r\n$24\r\n10 to 15 feet\r\n$232\r\n$232\r\n15 to 20 feet\r\n$2,344\r\n$2,344"), $"Incorrect table result for Elevation Above River. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("First Floor Elevation", $"{adminClientRates}", "LUXFT-User", "\r\n0 to 1 feet\r\n$546\r\n$546"), $"Incorrect table result for First Floor Elevation. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Flood Zone", $"{adminClientRates}", "LUXFT-User", "\r\nAE\r\n$324\r\n$324\r\nVE\r\n$411\r\n$411\r\nX\r\n$617\r\n$617"), $"Incorrect table result for Flood Zone. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Flow Allocation (sq km)", $"{adminClientRates}", "LUXFT-User", "\r\nLess than 1.5 sq. km\r\n$333\r\n$333\r\n1.5 to 2 sq. km\r\n$227\r\n$227\r\n2 to 3 sq. km\r\n$24\r\n$24\r\n3 to 5 sq. km\r\n$2,344\r\n$2,344"), $"Incorrect table result for Flow Allocation (sq km). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Foundation", $"{adminClientRates}", "LUXFT-User", "\r\nFill\r\n$368\r\n$368\r\nSlab-on-grade\r\n$617\r\n$617"), $"Incorrect table result for Foundation. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Ground Elevation", $"{adminClientRates}", "LUXFT-User", "\r\nLess than 5 feet\r\n$324\r\n$324\r\n10 to 20 feet\r\n$411\r\n$411\r\n30 to 40 feet\r\n$242\r\n$242\r\n50 to 75 feet\r\n$1,388\r\n$1,388\r\n100 to 150 feet\r\n$24\r\n$24\r\nOver 150 feet\r\n$42\r\n$42"), $"Incorrect table result for Ground Elevation. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Limit to Value", $"{adminClientRates}", "LUXFT-User", "\r\n0 - 50%\r\n$546\r\n$546"), $"Incorrect table result for Limit to Value. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Loss Range", $"{adminClientRates}", "LUXFT-User", "\r\n0 to 50\r\n$765\r\n$765\r\n50 to 100\r\n$24\r\n$24\r\n250 to 500\r\n$368\r\n$368"), $"Incorrect table result for Loss Range. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Number of Stories", $"{adminClientRates}", "LUXFT-User", "\r\n1\r\n$546\r\n$546"), $"Incorrect table result for Number of Stories. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Pixel Region", $"{adminClientRates}", "LUXFT-User", "\r\nCentral West\r\n$2,344\r\n$2,344\r\nGulf\r\n$422\r\n$422\r\nInland 2\r\n$130\r\n$130\r\nWest-Panhandle\r\n$242\r\n$242"), $"Incorrect table result for Pixel Region. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Pre FIRM", $"{adminClientRates}", "LUXFT-User", "\r\nTRUE\r\n$546\r\n$546"), $"Incorrect table result for Pre FIRM. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Primary Premium Range", $"{adminClientRates}", "LUXFT-User", "\r\nLess than 200\r\n$33\r\n$33\r\n200 - 250\r\n$242\r\n$242\r\n300 - 350\r\n$324\r\n$324\r\n400 - 450\r\n$422\r\n$422\r\n700+\r\n$2,344\r\n$2,344"), $"Incorrect table result for Primary Premium Range. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Relative Elevation", $"{adminClientRates}", "LUXFT-User", "\r\n-12 to -8 feet\r\n$324\r\n$324\r\n0 to 2 feet\r\n$237\r\n$237\r\n2 to 4 feet\r\n$133\r\n$133\r\n4 to 8 feet\r\n$2,344\r\n$2,344\r\n8 to 12 feet\r\n$411\r\n$411"), $"Incorrect table result for Relative Elevation. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Site Deductible", $"{adminClientRates}", "LUXFT-User", "\r\nUnknown\r\n$546\r\n$546"), $"Incorrect table result for Site Deductible. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Slope (deg)", $"{adminClientRates}", "LUXFT-User", "\r\nLess than 0.5 degrees\r\n$24\r\n$24\r\n0.5 to 1 degrees\r\n$42\r\n$42\r\n1 to 1.5 degrees\r\n$378\r\n$378\r\n2 to 2.5 degrees\r\n$242\r\n$242\r\n2.5 to 3 degrees\r\n$2,344\r\n$2,344\r\n3.5 to 4 degrees\r\n$411\r\n$411"), $"Incorrect table result for Slope (deg). Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Special Flood Hazard Area", $"{adminClientRates}", "LUXFT-User", "\r\nFALSE\r\n$617\r\n$617\r\nTRUE\r\n$368\r\n$368"), $"Incorrect table result for Special Flood Hazard Area. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Stream Order", $"{adminClientRates}", "LUXFT-User", "\r\nLess than 1\r\n$246\r\n$246\r\n1 to 2\r\n$2,344\r\n$2,344"), $"Incorrect table result for Stream Order. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Total Insured Value", $"{adminClientRates}", "LUXFT-User", "\r\n150K to 200K\r\n$751\r\n$751\r\n200K to 250K\r\n$33\r\n$33"), $"Incorrect table result for Total Insured Value. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("Year Built", $"{adminClientRates}", "LUXFT-User", "\r\n1931 to 1940\r\n$546\r\n$546"), $"Incorrect table result for Year Built. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
                Assert.True(defaultDatasetPage.IsTableExist("ZIP Code", $"{adminClientRates}", "LUXFT-User", "\r\n32058\r\n$24\r\n$24\r\n32065\r\n$324\r\n$324\r\n32401\r\n$242\r\n$242\r\n32601\r\n$42\r\n$42\r\n32625\r\n$411\r\n$411\r\n32696\r\n$432\r\n$432\r\n34429\r\n$2,344\r\n$2,344"), $"Incorrect table result for ZIP Code. Actual: {defaultDatasetPage.CBVTableLocator.Text}");
            });
        }

        [Order(7)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void CheckPremiumRankReportTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.PremiumRankTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.PremiumRankMinimizedLocator.Click();
            }
            else defaultDatasetPage.PremiumRankTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.IsPremiumRankReportExist());
                Assert.True(defaultDatasetPage.PremiumRankTabLocator.GetAttribute("class").Contains(" activeStoryTable"));
            });
        }

        [Order(8)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void Diff2MarketReportCheckDiffMedianTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.DifferenceToMarketTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.DifferenceToMarketMinimizedLocator.Click();
            }
            else defaultDatasetPage.DifferenceToMarketTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.D2MCalculateButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.Diff2MarketTypeLocator.Text.Equals("% Difference"));
                Assert.True(defaultDatasetPage.MeasureButtonLocator.Text.Equals("Median"));
                Assert.True(defaultDatasetPage.LowerBucketLocator.GetAttribute("value").Equals("-100"));
                Assert.True(defaultDatasetPage.UpperBucketLocator.GetAttribute("value").Equals("100"));
                Assert.True(defaultDatasetPage.Diff2MarketNumberOfBucketsLocator.GetAttribute("value").Equals("40"));
                Assert.True(defaultDatasetPage.OutsideOfRangesCheckboxLocator.Selected);
                Assert.True(defaultDatasetPage.DisplayLossRatioCheckboxLocator.Selected);

                Assert.True(defaultDatasetPage.DifferenceToMarketTabLocator.GetAttribute("class").Contains(" activeStoryTable"));

                Assert.True(defaultDatasetPage.D2MChartColumnLocator.Displayed);
                Assert.True(defaultDatasetPage.D2MChartLossRatioLocator.Displayed);
            });
        }

        [Order(9)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void Diff2MarketReportCheckDollarDiffAverageTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.DifferenceToMarketTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.DifferenceToMarketMinimizedLocator.Click();
            }
            else defaultDatasetPage.DifferenceToMarketTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.Diff2MarketTypeLocator.Click();

            defaultDatasetPage.DifferenceDollarTypeLocator.Click();

            defaultDatasetPage.MeasureButtonLocator.Click();

            defaultDatasetPage.AverageMeasureLocator.Click();

            defaultDatasetPage.D2MCalculateButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.Diff2MarketTypeLocator.Text.Equals("$ Difference"));
                Assert.True(defaultDatasetPage.MeasureButtonLocator.Text.Equals("Average"));
                Assert.True(defaultDatasetPage.LowerBucketLocator.GetAttribute("value").Equals("-2000"));
                Assert.True(defaultDatasetPage.UpperBucketLocator.GetAttribute("value").Equals("2000"));
                Assert.True(defaultDatasetPage.Diff2MarketNumberOfBucketsLocator.GetAttribute("value").Equals("40"));
                Assert.True(defaultDatasetPage.OutsideOfRangesCheckboxLocator.Selected);
                Assert.True(defaultDatasetPage.DisplayLossRatioCheckboxLocator.Selected);

                Assert.True(defaultDatasetPage.DifferenceToMarketTabLocator.GetAttribute("class").Contains(" activeStoryTable"));

                Assert.True(defaultDatasetPage.D2MChartColumnLocator.Displayed);
                Assert.True(defaultDatasetPage.D2MChartLossRatioLocator.Displayed);
            });
        }


        [Order(10)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void Diff2MarketReportCheckDistributionMinTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.DifferenceToMarketTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.DifferenceToMarketMinimizedLocator.Click();
            }
            else defaultDatasetPage.DifferenceToMarketTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.Diff2MarketTypeLocator.Click();

            defaultDatasetPage.DistributionTypeLocator.Click();

            defaultDatasetPage.MeasureButtonLocator.Click();

            defaultDatasetPage.MinimumMeasureLocator.Click();

            defaultDatasetPage.D2MCalculateButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.Diff2MarketTypeLocator.Text.Equals("$ Distribution"));
                Assert.True(defaultDatasetPage.MeasureButtonLocator.Text.Equals("Minimum"));
                Assert.True(defaultDatasetPage.LowerBucketLocator.GetAttribute("value").Equals("0"));
                Assert.True(defaultDatasetPage.UpperBucketLocator.GetAttribute("value").Equals("4000"));
                Assert.True(defaultDatasetPage.Diff2MarketNumberOfBucketsLocator.GetAttribute("value").Equals("40"));
                Assert.True(defaultDatasetPage.OutsideOfRangesCheckboxLocator.Selected);
                Assert.True(defaultDatasetPage.DisplayLossRatioCheckboxLocator.Selected);

                Assert.True(defaultDatasetPage.DifferenceToMarketTabLocator.GetAttribute("class").Contains(" activeStoryTable"));

                Assert.True(defaultDatasetPage.D2MChartLossRatioLocator.Displayed);

                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[0].GetAttribute("height").Equals("410"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[1].GetAttribute("height").Equals("0"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[2].GetAttribute("height").Equals("205"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[3].GetAttribute("height").Equals("205"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[4].GetAttribute("height").Equals("410"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[5].GetAttribute("height").Equals("0"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[23].GetAttribute("height").Equals("205"));

                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[0].GetAttribute("height").Equals("410"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[1].GetAttribute("height").Equals("0"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[2].GetAttribute("height").Equals("205"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[3].GetAttribute("height").Equals("205"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[4].GetAttribute("height").Equals("410"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[5].GetAttribute("height").Equals("0"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[23].GetAttribute("height").Equals("205"));
            });
        }

        [Order(11)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void Diff2MarketReportCheckOnlyPrimaryDisplaysTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.DifferenceToMarketTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.DifferenceToMarketMinimizedLocator.Click();
            }
            else defaultDatasetPage.DifferenceToMarketTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.Diff2MarketTypeLocator.Click();

            defaultDatasetPage.DistributionTypeLocator.Click();

            defaultDatasetPage.MeasureButtonLocator.Click();

            defaultDatasetPage.MinimumMeasureLocator.Click();

            defaultDatasetPage.D2MCalculateButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.ScrollToCompetitorLabel();

            defaultDatasetPage.CompetitorLocator.Click();

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.Diff2MarketTypeLocator.Text.Equals("$ Distribution"));
                Assert.True(defaultDatasetPage.MeasureButtonLocator.Text.Equals("Minimum"));
                Assert.True(defaultDatasetPage.LowerBucketLocator.GetAttribute("value").Equals("0"));
                Assert.True(defaultDatasetPage.UpperBucketLocator.GetAttribute("value").Equals("4000"));
                Assert.True(defaultDatasetPage.Diff2MarketNumberOfBucketsLocator.GetAttribute("value").Equals("40"));
                Assert.True(defaultDatasetPage.OutsideOfRangesCheckboxLocator.Selected);
                Assert.True(defaultDatasetPage.DisplayLossRatioCheckboxLocator.Selected);

                Assert.True(defaultDatasetPage.DifferenceToMarketTabLocator.GetAttribute("class").Contains(" activeStoryTable"));

                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[0].GetAttribute("height").Equals("410"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[1].GetAttribute("height").Equals("0"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[2].GetAttribute("height").Equals("205"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[3].GetAttribute("height").Equals("205"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[4].GetAttribute("height").Equals("410"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[5].GetAttribute("height").Equals("0"));
                Assert.True(defaultDatasetPage.D2MChartPrimaryColumnLocator[23].GetAttribute("height").Equals("205"));

                Assert.True(driver.FindElements(By.XPath("//*[@class='highcharts-series highcharts-series-2 highcharts-column-series highcharts-color-2 highcharts-tracker ']")).Count() < 1);
            });
        }

        [Order(12)]
        [TestCase("Flood Florida FEMA full")]
        [CustomRetry]
        public void Diff2MarketReportCheckOnlyCompetitorDisplaysTest(string dataset)
        {
            OpenStory(dataset);

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            if (!defaultDatasetPage.DifferenceToMarketTabLocator.Displayed)
            {
                defaultDatasetPage.StoryAndVisualizationLocator.Click();
                defaultDatasetPage.DifferenceToMarketMinimizedLocator.Click();
            }
            else defaultDatasetPage.DifferenceToMarketTabLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.Diff2MarketTypeLocator.Click();

            defaultDatasetPage.DistributionTypeLocator.Click();

            defaultDatasetPage.MeasureButtonLocator.Click();

            defaultDatasetPage.MinimumMeasureLocator.Click();

            defaultDatasetPage.D2MCalculateButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            defaultDatasetPage.ScrollToCompetitorLabel();


            defaultDatasetPage.PrimaryLocator.Click();

            Assert.Multiple(() =>
            {
                Assert.True(defaultDatasetPage.Diff2MarketTypeLocator.Text.Equals("$ Distribution"));
                Assert.True(defaultDatasetPage.MeasureButtonLocator.Text.Equals("Minimum"));
                Assert.True(defaultDatasetPage.LowerBucketLocator.GetAttribute("value").Equals("0"));
                Assert.True(defaultDatasetPage.UpperBucketLocator.GetAttribute("value").Equals("4000"));
                Assert.True(defaultDatasetPage.Diff2MarketNumberOfBucketsLocator.GetAttribute("value").Equals("40"));
                Assert.True(defaultDatasetPage.OutsideOfRangesCheckboxLocator.Selected);
                Assert.True(defaultDatasetPage.DisplayLossRatioCheckboxLocator.Selected);

                Assert.True(defaultDatasetPage.DifferenceToMarketTabLocator.GetAttribute("class").Contains(" activeStoryTable"));

                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[0].GetAttribute("height").Equals("410"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[1].GetAttribute("height").Equals("0"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[2].GetAttribute("height").Equals("205"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[3].GetAttribute("height").Equals("205"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[4].GetAttribute("height").Equals("410"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[5].GetAttribute("height").Equals("0"));
                Assert.True(defaultDatasetPage.D2MChartSecondaryColumnLocator[23].GetAttribute("height").Equals("205"));

                Assert.True(driver.FindElements(By.XPath("//*[@class='highcharts-series highcharts-series-1 highcharts-column-series highcharts-color-1 highcharts-tracker ']")).Count() < 1);
            });
        }

        public void UploadDataset(string path, IWebDriver driver)
        {
            By uploadButton = By.Id("txtUploadFileDataset");
            driver.FindElement(uploadButton).SendKeys($"{path}.zip");
        }

        public void ImportDataset(string datasetDescription, string pathToZip)
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.DataserImportMenuLocator.Click();

            var datasetImportPage = new DatasetImportPage(driver);

            UploadDataset(pathToZip, driver);

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            datasetImportPage.DatasetDescriptiontextboxLocator.SendKeys($"{DatasetUserDescription(datasetDescription)}");

            Assert.True(datasetImportPage.UploadedFileTextLocator.Text.Contains($"{datasetDescription}"),
                "dataset is not selected");

            Utils.Scroll(datasetImportPage.FooterLocator, driver);

            datasetImportPage.StartButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);
        }

        public string PathToZipUser(string dataset)
        {
            return Path.Combine(path, "DDLdatasets", "ReportTesting", dataset);
        }

        public string DatasetUserDescription(string dataset)
        {
            return $"ReportTesting{randomString} {dataset}";
        }

        public void OpenStory(string dataset)
        {
            var dashboardPage = new DashboardPage(driver);

            dashboardPage.FilterDataset($"ReportTesting{randomString} {dataset}");

            Assert.True(dashboardPage.Description.Count > 0, $"Dataset ReportTesting{randomString} {dataset} is not present");

            dashboardPage.OpenDataset($"ReportTesting{randomString} {dataset}", "TestStory");

            var defaultDatasetPage = new DefaultDatasetsPage(driver);

            //Assert.True(driver.Url.Contains("https://qa.millimanpixel.com/Dashboard/StoryPage/"),
            //   "default page is not opened");

            defaultDatasetPage.ExpandFilterLocator.WaitForElementPresentAndEnabled(driver, 100);
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }


    }
}
