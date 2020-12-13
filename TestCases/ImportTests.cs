using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using OpenQA.Selenium.Support.UI;
using Milliman.Pixel.Web.Tests.PageObjects.Pages;
using Extras = SeleniumExtras.WaitHelpers;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class ImportTests
    {
        IWebDriver driver;
        LoginPage loginPage;

        static string admin = "01_testuserAuto";


        static string randomString = Utils.RandomString(5);

        static string fileZipAdmin = "1933-HO3 Florida Market Basket";

        static string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString()).ToString()).ToString();

        string pathToZipAdmin = Path.Combine(path, fileZipAdmin);

        string datasetDescription = $"Test{randomString} {fileZipAdmin}";


        [SetUp]
        public void InitializeAdmin()
        {
            driver = new ChromeDriver();
            driver.Url = "https://qa.millimanpixel.com";
            //driver.Url = "https://preprod.millimanpixel.com/";

            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            loginPage.LoginToApplication();
        }

        [Order(1)]
        [CustomRetry]
        [TestCase("full")]
        [TestCase("only Policies")]
        [TestCase("without JSON")]
        [TestCase("Carriers and Rates files are empty")]
        public void UploadDatasetTest(string datasetDifference)
        {
            importDataset(datasetDifference, datasetDescription, pathToZipAdmin, "HO3 Florida");

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            //Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Integration/DataProcessingStatus"),
            //    "Data processing Page is not opened");

            dataProcessingStatus.CheckRefreshRateToMinimum();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(admin, dataProcessingStatus.User[0].Text, "Incorrect User in User column");
                Assert.AreEqual("Add", dataProcessingStatus.RequestType[0].Text, "Incorrect Request type");
                Assert.AreEqual("Dataset", dataProcessingStatus.DataType[0].Text, "Incorrect Data type");
            });

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10000));

            wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In queue"));

            wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In progress"));

            if (!dataProcessingStatus.Status[0].Text.Equals("Completed successfully") | !dataProcessingStatus.UserStatus[0].Text.Equals("Failed"))
            {
                wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In queue"));

                wait.Until(Extras.ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In progress"));
            }

            Assert.AreEqual("Completed successfully", dataProcessingStatus.Status[0].Text, "Status is not Completed successfully");
        }

        [Order(2)]
        [CustomRetry]
        [TestCase("full")]
        [TestCase("only Policies")]
        [TestCase("without JSON")]
        [TestCase("Carriers and Rates files are empty")]
        public void CheckDatasetOnSourceTest(string datasetDifference)
        {
            var treeView = new UserStoryListTreeViewPage(driver);

            treeView.openTreeView();

            treeView.Folder[1].ClickEx(driver);

            Utils.WaitUntilLoadingPlaceholderDisappears(driver, secondtToWait: 20);

            treeView.FilterDataset($"{datasetDescription} {datasetDifference}");

            Assert.True(treeView.Description[0].Text.Contains($"{datasetDescription} {datasetDifference}"),
                 $"Dataset {datasetDescription} {datasetDifference} doesn`t display on Source");
        }


        public void uploadDataset(string path, IWebDriver driver)
        {
            By uploadButton = By.Id("txtUploadFileDataset");
            driver.FindElement(uploadButton).SendKeys($"{path}.zip");
        }

        public void importDataset(string datasetDifference, string datasetDescription, string pathToZip, string datasetType = "Flood Louisiana FEMA")
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.DataserImportMenuLocator.Click();

            var datasetImportPage = new DatasetImportPage(driver);

            datasetImportPage.SelectDatasetTypeButtonLocator.Click();

            datasetImportPage.SelectDatasetType(datasetType);

            uploadDataset($"{pathToZip} {datasetDifference}", driver);

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            datasetImportPage.DatasetDescriptiontextboxLocator.SendKeys($"{datasetDescription} {datasetDifference}");

            Assert.True(datasetImportPage.UploadedFileTextLocator.Text.Contains($"{datasetDifference}"),
                "dataset is not selected");

            Utils.Scroll(datasetImportPage.FooterLocator, driver);

            datasetImportPage.StartButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}
