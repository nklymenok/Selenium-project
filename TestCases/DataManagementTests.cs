using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class DataManagementTests
    {
        IWebDriver driver;
        LoginPage loginPage;
        private const string officeName = "Test - Office";


        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://qa.millimanpixel.com";
            //driver.Url = "https://preprod.millimanpixel.com/";
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            loginPage.LoginToApplication();
        }

        //[Test]
        [CustomRetry]
        public void CheckDataManagementPageOpensTest()
        {
            var menu = new MenuPage(driver);

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.DataManagementMenuLocator.Click();

            Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Administrator/TableManagement"),
                "DataManagement page is not opened");
        }

        [Test]
        [CustomRetry]
        public void AddOfficeForTest()
        {
            var menu = new MenuPage(driver);

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.DataManagementMenuLocator.Click();

            var dataPage = new DataManagementPage(driver);

            dataPage.CreateOffice(officeName);

            Assert.True(dataPage.CheckOffice(officeName), "Office is not added");
        }

        [Test]
        [CustomRetry]
        public void CheckAdministrationItemsIndropDownTest()
        {
            var menu = new MenuPage(driver);

            menu.AdministrationMenuLocator.ClickEx(driver);

            Assert.Multiple(() =>
            {
                Assert.True(menu.DataManagementMenuLocator.Displayed, "Data Management item is not displayed");
                Assert.True(menu.DatasetManagementMenuLocator.Displayed, "Dataset  item is not displayed");
                Assert.True(menu.UserManagementMenuLocator.Displayed, "User Management item is not displayed");
                Assert.True(menu.ClientStatisticsMenuLocator.Displayed, "Client Statistics item is not displayed");
            });
        }

        [Test]
        [CustomRetry]
        public void CheckDataManagementElementsTest()
        {
            var menu = new MenuPage(driver);

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.DataManagementMenuLocator.Click();

            var dataPage = new DataManagementPage(driver);

            dataPage.AddOfficeButtonLocator.WaitForElementPresentAndEnabled(driver);

            Assert.Multiple(() =>
            {
                Assert.True(dataPage.OfficesTabLocator.Displayed, "Offices tab is not displayed");
                Assert.True(dataPage.ClientsTabLocator.Displayed, "Clients tab is not displayed");
                Assert.True(dataPage.DatasetsTabLocator.Displayed, "Datasets tab is not displayed");
            });
        }

        [Test]
        [CustomRetry]
        public void CheckOfficeGridLayoutTest()
        {
            var menu = new MenuPage(driver);

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.DataManagementMenuLocator.Click();

            var dataPage = new DataManagementPage(driver);

            dataPage.OfficesTabLocator.ClickEx(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.Multiple(() =>
            {
                Assert.True(dataPage.OfficesIDColumnLocator.Displayed, "ID column is not displayed");
                Assert.True(dataPage.OfficesOfficeColumnLocator.Displayed, "Office column is not displayed");
                Assert.True(dataPage.OfficesTotalClientsColumnLocator.Displayed, "Total clients column is not displayed");
            });
        }

        [Test]
        [CustomRetry]
        public void CheckClientsGridLayoutTest()
        {
            var menu = new MenuPage(driver);

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.DataManagementMenuLocator.Click();

            var dataPage = new DataManagementPage(driver);

            dataPage.ClientsTabLocator.ClickEx(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.Multiple(() =>
            {
                Assert.True(dataPage.ClientsIDColumnLocator.Displayed, "ID column is not displayed");
                Assert.True(dataPage.ClientsClientColumnLocator.Displayed, "Client column is not displayed");
                Assert.True(dataPage.ClientsOfficeColumnLocator.Displayed, "Office column is not displayed");
                Assert.True(dataPage.ClientsClientShortNameColumnLocator.Displayed, "Client Shaort Name column is not displayed");
                Assert.True(dataPage.ClientsDatasetLoadFromUIColumnLocator.Displayed, "Dataset Load From UI column is not displayed");
                Assert.True(dataPage.ClientsEditClientDatasetTypeColumnLocator.Displayed, "Edit Client DatasetType column is not displayed");
            });
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}
