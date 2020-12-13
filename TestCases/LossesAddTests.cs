using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    class LossesAddTests
    {
        IWebDriver driver;
        LoginPage loginPage;

        static string randomString = Utils.RandomString(5);

        string datasetDescription = "Flood New Jersey - Milliman Standard";

        static string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString()).ToString()).ToString();

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://pixel.com";
            
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            loginPage.LoginToApplication();
        }

        [CustomRetry]
        [TestCase("AdminShared", ".psv", "|")]
        [TestCase("AdminSharedLuxoft", ".csv", ",")]
        [TestCase("AdminLuxoftExport", ".psv", "|")]
        [TestCase("AdminLuxoft", ".csv", ",", false)]
        public void AddLossesTest(string lossName, string fileFormat, string delimiter, bool isTypeShared = true)
        {
            var menu = new MenuPage(driver);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10000));

            menu.DataMenuLocator.ClickEx(driver);

            menu.LossesAddMenuLocator.Click();

            var lossesAddPage = new LossesAddPage(driver);

            lossesAddPage.SelectDatasetButtonLocator.Click();

            lossesAddPage.SelectDataset(datasetDescription);

            Assert.AreEqual(lossesAddPage.SelectDatasetButtonLocator.Text, datasetDescription);

            UploadLosses(lossName, fileFormat, delimiter, driver);

            if (!isTypeShared)
            {
                lossesAddPage.CarrierTypeButtonLocator.ClickEx(driver);
                lossesAddPage.ClientTypeLocator.Click();
                Thread.Sleep(500);

                lossesAddPage.SelectClient("Luxoft");
            }

            if (lossName.Contains("AdminLuxoftExport"))
            {
                lossesAddPage.SelectSharedClient("Luxoft", true);
            }

            if (lossName.Contains("AdminSharedLuxoft"))
            {
                lossesAddPage.SelectSharedClient("Luxoft");
            }

            wait.Until(ExpectedConditions.ElementToBeClickable(lossesAddPage.StartButtonLocator));

            lossesAddPage.ScrollToStartButton();

            lossesAddPage.StartButtonLocator.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(lossesAddPage.ConfirmAddRatesButtonLocator)).Click();

            Utils.WaitUntilLoadingDisappears(driver, secondtToWait: 500);

            var dataProcessingStatus = new DataProcessingStatusPage(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dataProcessingStatus.CheckRefreshRateToMinimum();

            int row = dataProcessingStatus.DatasetRow(datasetDescription);

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[{row + 1}]/td[6]"), "In queue"));

            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath($"//*[@id='admin-table-office']//tr[{row + 1}]/td[6]"), "In progress"));

            Assert.AreEqual("Completed successfully", dataProcessingStatus.Status[row].Text, "Status is not Completed successfully");
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

        public static void UploadLosses(string lossName, string format, string delimiter, IWebDriver driver)
        {
            By uploadLossesButton = By.Id("txtUploadLosses");
            By uploadLossDataButton = By.Id("txtUploadLossData");

            WriteToFileData($@"{path}\Loss Data Sample{format}",
                $@"{path}\Loss Data{format}", $"PolicyId{delimiter}{lossName}{randomString}");

            WriteToFileSource($@"{path}\Loss Source{format}",
                $"LossFieldName{delimiter}LossDisplayName\r\n{lossName}{randomString}{delimiter}{lossName}{randomString}");

            driver.FindElement(uploadLossesButton).SendKeys($@"{path}\Loss Source{format}");

            driver.FindElement(uploadLossDataButton).SendKeys($@"{path}\Loss Data{format}");
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}
