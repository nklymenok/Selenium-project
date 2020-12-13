using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class DatasetManagementTests
    {
        IWebDriver driver;
        LoginPage loginPage;
        private const string officeName = "Test - Office";


        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://qa.millimanpixel.com";
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            loginPage.LoginToApplication();
        }

        [Test]
        [CustomRetry]
        public void CheckDatasetManagementPageOpensTest()
        {
            var menu = new MenuPage(driver);

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.DatasetManagementMenuLocator.Click();

            Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Administrator/Datasets"),
                "datasetManagement page is not opened");
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}
