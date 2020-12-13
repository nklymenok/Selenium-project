using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;
using Extras = SeleniumExtras.WaitHelpers;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class ErrorHandlingTests
    {
        IWebDriver driver;
        LoginPage loginPage;

        WebDriverWait wait;
        WebDriverWait waitQuick;

        static string user = "03_testuser@test.com";

        static string randomString = Utils.RandomString(5);

        static string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString()).ToString()).ToString();

        const string noSourceIdError = "Errors: Mandatory filter Source_ID in General will not be diplayed due to missing source column Source_ID in Policies file. Add columns Source_ID to Policies file";

        const string contentsDeductibleIncorrectType = "Errors: Table Policies column ContentsDeductible has data which is not compatible with int.";
 
        const string buildingDeductibleIncorrectType = "Errors: Table Policies column BuildingDeductible has data which is not compatible with int.";

        const string noStateCountyId = "Errors: Mandatory filter StateCountyId in Geography will not be diplayed due to missing source column StateCountyId in Policies file. Add columns StateCountyId to Policies file";

        const string stateCountyIdIncorrectType = "Errors: Table Policies column StateCountyId has data which is not compatible with int.";

        const string baseFloodElevationFeetIncorrectType = "Errors: Table Policies column BaseFloodElevationFeet has data which is not compatible with Numeric(20,12).";

        const string withoutSourceSystemId = "Mandatory column in table 'Policies' called 'PolicyId' is not present.";

        const string withoutCounty = "Mandatory column in table 'Policies' called 'County' is not present.";

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

        [CustomRetry]
        [TestCase("ContentsDeductible incorrect type", contentsDeductibleIncorrectType)]
        [TestCase("BuildingDeductible incorrect type", buildingDeductibleIncorrectType)]
        [TestCase("StateCountyId incorrect type", stateCountyIdIncorrectType)]
        [TestCase("BaseFloodElevationFeet incorrect type", baseFloodElevationFeetIncorrectType)]
        public void CheckErrorHandlingTest(string dataset, string validationMessage)
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

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Failed", dataProcessingStatus.UserStatus[0].Text, $"Status is not Failed for dataset {DatasetUserDescription(dataset)}");

                dataProcessingStatus.DataProcessStatusReportLocator.Click();
                Thread.Sleep(1000);

                var a = dataProcessingStatus.ReportPopupLocator.Text;

                Assert.True(dataProcessingStatus.ReportPopupLocator.Text.Contains(validationMessage));
            });
        }

        [CustomRetry]
        [TestCase("Without PolicyId", withoutSourceSystemId)]
        [TestCase("Without County", withoutCounty)]
        public void CheckValidationPopupWhileUploadingTest(string dataset, string validationMessage)
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.DataserImportMenuLocator.Click();

            var datasetImportPage = new DatasetImportPage(driver);

            UploadDataset(PathToZipUser(dataset), driver);

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            Assert.True(datasetImportPage.DatasetValidationPopupLocator.Text.Equals(validationMessage));
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
            return Path.Combine(path, "DDLdatasets", "Error Handling", dataset);
        }

        public string DatasetUserDescription(string dataset)
        {
            return $"ErrorHandling{randomString} {dataset}";
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }


    }
}

