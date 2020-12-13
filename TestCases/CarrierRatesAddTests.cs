using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    class CarrierRatesAddTests
    {
        IWebDriver driver;
        LoginPage loginPage;

        WebDriverWait wait;
        WebDriverWait waitQuick;

        static string user = "01_testuserAuto";

        static string randomString = Utils.RandomString(5);

        string datasetDescription = "Flood New Jersey - Milliman Standard";

        static string fileRates = "1556-Flood New Jersey-Milliman Standard-CarrierRate";

        static string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString()).ToString()).ToString();

        string pathToRates = Path.Combine(path, fileRates);

        string datasetTip = "Please select one of your available datasets.Your new carrier rates will be added and your company will be the only one with access to it.";

        string carrierNameTip = "The carrier name should be a unique identifier for these rates which are easily identifiable.You cannot enter a duplicate carrier name nor can you change it once it is added.However, it can be deleted later if necessary";

        string rateFileTip = "The format of the carrier rates file can either be a comma (\",\") or pipe (“|”) delimited file consisting of 2 required columns:PolicyID: unique key identifier of the policy recordTotal: Amount of the premium for the specific policyCurrency amounts should not have a denomination symbol, nor should they have a comma indicating thousands.Also, the decimal symbol should be a period.Example of PSV filePolicyID | Total10001 | 326510002 | 262210003 | 3972Example of CSV filePolicyID , Total10001 , 326510002 , 262210003 , 3972The suffix for the filename must correspond to the delimiter field, for examples filename.csv or filename.psv";

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://qa.millimanpixel.com";
            //driver.Url = "https://preprod.millimanpixel.com/";
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            loginPage.LoginToApplication();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));
            waitQuick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test, Order(1)]
        [CustomRetry]
        public void CarrierRatesAddPageOpensTest()
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.CarrierRatesAddMenuLocator.Click();

            var carrierAddPage = new CarrierRatesAddPage(driver);

            Assert.Multiple(() =>
            {
                //Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/CarrierRates/AddRates"),
                //"failed to open Carrier Rates: Add page");
                Assert.AreEqual("Carrier Rates: Add", carrierAddPage.CarrierAddTestLocator.Text, "Incorrect page is opened");
            });
        }

        [Test, Order(2)]
        [CustomRetry]
        public void CheckCarrierRatesAddPageElementsTest()
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.CarrierRatesAddMenuLocator.Click();

            var carrierAddPage = new CarrierRatesAddPage(driver);

            Assert.Multiple(() =>
            {
                Assert.True(carrierAddPage.SelectDatasetButtonLocator.Displayed);
                Assert.True(carrierAddPage.CarrierNameTextBoxLocator.Displayed);
                Assert.True(carrierAddPage.SharedTypeLocator.Displayed);
                Assert.True(carrierAddPage.UploadButtonLocator.Displayed);
                Assert.True(carrierAddPage.StartButtonLocator.Displayed);
            });
        }

        [Test, Order(3)]
        [CustomRetry]
        public void CheckDatasetTypeAndYearAutomaticallyAreSpecifiedTest()
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.CarrierRatesAddMenuLocator.Click();

            var carrierAddPage = new CarrierRatesAddPage(driver);

            carrierAddPage.SelectDatasetButtonLocator.Click();

            carrierAddPage.SelectDataset(datasetDescription);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Flood New Jersey Mlmn Std", carrierAddPage.DatasetTypeTextLocator.Text,
                    "Incorrect Dataset Type");
                Assert.AreEqual("2019", carrierAddPage.DatasetBaseYearTextLocator.Text,
                    "Incorrect Dataset Base Year");
            });
        }

        [Test, Order(4)]
        [CustomRetry]
        public void CheckCarrierNameFormatAddingByAdminTest()
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.CarrierRatesAddMenuLocator.Click();

            var carrierAddPage = new CarrierRatesAddPage(driver);

            carrierAddPage.CarrierNameTextBoxLocator.SendKeys("Admin");

            Assert.AreEqual("Admin", carrierAddPage.EnteredCarrierNameLocator.Text,
                "Incorrect name displaying");
        }

        [Test, Order(5)]
        [CustomRetry]
        public void CheckTipsContentTest()
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.CarrierRatesAddMenuLocator.Click();

            var carrierAddPage = new CarrierRatesAddPage(driver);

            carrierAddPage.DatasetTipLocator.ClickEx(driver);

            var datasetTipContent = Regex.Replace(carrierAddPage.DatasetTipTextLocator.Text, @"\r\n", String.Empty);

            Assert.True(datasetTip.Equals(datasetTipContent, StringComparison.OrdinalIgnoreCase),
                "Incorrect dataset tip content displaying");

            carrierAddPage.TipOkButtonLocator.Click();

            carrierAddPage.CarrierNameTipLocator.ClickEx(driver);

            var carrierNameTipContent = Regex.Replace(carrierAddPage.CarrierNameTipTextLocator.Text, @"\r\n", String.Empty);

            Assert.True(carrierNameTip.Equals(carrierNameTipContent, StringComparison.OrdinalIgnoreCase),
                "Incorrect carrier name tip content displaying");

            carrierAddPage.TipOkButtonLocator.Click();

            carrierAddPage.RateFileTipLocator.ClickEx(driver);

            var rateFileTipContent = Regex.Replace(carrierAddPage.RateFileTipTextLocator.Text, @"\r\n", String.Empty);

            Assert.True(rateFileTip.Equals(rateFileTipContent, StringComparison.OrdinalIgnoreCase),
                "Incorrect rate file tip content displaying");
        }

        [Order(6)]
        [CustomRetry]
        [TestCase("AdminShared", ".psv")]
        [TestCase("AdminLuxShared", ".csv")]
        [TestCase("AdminExportLuxoft", ".psv")]
        [TestCase("AdminLuxoft", ".csv", false)]
        public void AddRatesTest(string carrierName, string fileFormat, bool isTypeShared = true)
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.CarrierRatesAddMenuLocator.Click();

            var carrierAddPage = new CarrierRatesAddPage(driver);

            carrierAddPage.SelectDatasetButtonLocator.Click();

            carrierAddPage.SelectDataset(datasetDescription);

            Assert.AreEqual(carrierAddPage.SelectDatasetButtonLocator.Text, datasetDescription, "Dataset is not selected");

            carrierAddPage.CarrierNameTextBoxLocator.SendKeys($"{carrierName}{randomString}");

            Assert.True(carrierAddPage.EnteredCarrierNameLocator.Text.Contains(carrierName), "Carrier name is not entered");

            if (!isTypeShared)
            {
                carrierAddPage.CarrierTypeButtonLocator.ClickEx(driver);
                carrierAddPage.ClientTypeLocator.Click();
                Thread.Sleep(500);

                carrierAddPage.SelectClient("Luxoft");
            }

            if (carrierName == "AdminExportLuxoft")
            {
                carrierAddPage.SelectSharedClient("Luxoft", true);
            }

            if (carrierName == "AdminLuxShared")
            {
                carrierAddPage.SelectSharedClient("Luxoft");
            }

            uploadFile(pathToRates, fileFormat, driver);

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

            Assert.AreEqual("Completed successfully", dataProcessingStatus.Status[0].Text, "Status is not Completed successfully");
        }

        [Order(7)]
        [CustomRetry]
        [TestCase("AdminShared")]
        [TestCase("AdminLuxShared")]
        [TestCase("AdminExportLuxoft")]
        [TestCase("AdminLuxoft")]
        public void CheckCarrierRatesRemovingByAdminTest(string carrierName)
        {
            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.CarrierRatesRemoveMenuLocator.Click();

            var carrierRemovePage = new CarrierRatesRemovePage(driver);

            carrierRemovePage.SelectDatasetButtonLocator.Click();

            carrierRemovePage.SelectDataset(datasetDescription);

            carrierRemovePage.SelectCarrierButtonLocator.Click();

            carrierRemovePage.SelectCarrier(carrierName);

            waitQuick.Until(ExpectedConditions.ElementToBeClickable(carrierRemovePage.StartButtonLocator)).Click();

            waitQuick.Until(ExpectedConditions.ElementToBeClickable(carrierRemovePage.ConfirmRemoveRatesButtonLocator)).Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In queue"));

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[1]/td[6]"), "In progress"));

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Completed successfully", dataProcessingStatus.Status[0].Text, "Status is not Completed successfully");
                Assert.AreEqual("Carrier Rates", dataProcessingStatus.DataType[0].Text, "Incorrect dataset type");
                Assert.AreEqual("Delete", dataProcessingStatus.RequestType[0].Text, "Incorrect request type");
            });
        }

        public void uploadFile(string path, string fileFormat, IWebDriver driver)
        {
            By uploadButton = By.Id("txtUploadFileRate");
            driver.FindElement(uploadButton).SendKeys($"{path}{fileFormat}");
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}
