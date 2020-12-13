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
    class DashboardTests
    {
        IWebDriver driver;
        LoginPage loginPage;
        private const string password = "Qaz!23";

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

        [Test]
        [CustomRetry]
        public void LogoIsPresentTest()
        {
            var dashboardPage = new DashboardPage(driver);

            Assert.True(dashboardPage.LogoLocator.Displayed, "Logo is not present");
        }

        //[Test]
        [CustomRetry]
        public void CheckLogoNavigationTest()
        {
            var dashboardPage = new DashboardPage(driver);

            var menu = new MenuPage(driver);

            menu.DataMenuLocator.ClickEx(driver);

            menu.ProcessingStatusMenuLocator.ClickEx(driver);
            Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Integration/DataProcessingStatus"),
                "Data processing Page is not opened");

            dashboardPage.LogoLocator.ClickEx(driver);

            Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Dashboard/DashboardGridOpen"),
                "Dashboard grid view is not opened");
        }

        [Test]
        [CustomRetry]
        public void WelcomeUsernameDisplayingTest()
        {
            var dashboardPage = new DashboardPage(driver);

            Assert.AreEqual("01_testuserAuto", dashboardPage.WelcomeUsernameLocator.Text,
                $"{dashboardPage.WelcomeUsernameLocator.Text} displays instead of 01_testuserAuto");
        }

        [Test]
        [CustomRetry]
        public void UsernameFunctionsDisplayingTest()
        {
            var dashboardPage = new DashboardPage(driver);

            dashboardPage.WelcomeUsernameLocator.ClickEx(driver);

            Assert.Multiple(() =>
            {
                Assert.True(dashboardPage.UpdateProfileLocator.Enabled, "Update profile is not displayed");
                Assert.True(dashboardPage.LogOffLocator.Enabled, "Log off is not displayed");
            });
        }

        [Test]
        [CustomRetry]
        public void CheckUpdatePhoneTest()
        {
            string phone = Utils.RandomString(5, "0123456789");
            var dashboardPage = new DashboardPage(driver);

            dashboardPage.WelcomeUsernameLocator.ClickEx(driver);
            dashboardPage.UpdateProfileLocator.Click();

            dashboardPage.UpdatePhoneLocator.WaitForElementPresentAndEnabled(driver).Clear();
            dashboardPage.UpdatePhoneLocator.SendKeys(phone);

            dashboardPage.UpdateButtonLocator.Click();

            dashboardPage.WelcomeUsernameLocator.ClickEx(driver);

            dashboardPage.UpdateProfileLocator.Click();

            var phoneUpdated = dashboardPage.UpdatePhoneLocator.WaitForElementPresentAndEnabled(driver).GetAttribute("value").ToString();

            Assert.AreEqual(phone, phoneUpdated,
                $"{phoneUpdated} displays instead of {phone}");
        }

        [Test]
        [CustomRetry]
        public void CheckUpdatePasswordTest()
        {
            var dashboardPage = new DashboardPage(driver);

            dashboardPage.WelcomeUsernameLocator.ClickEx(driver);
            dashboardPage.UpdateProfileLocator.Click();

            dashboardPage.NewPasswordLocator.WaitForElementPresentAndEnabled(driver).Clear();

            dashboardPage.NewPasswordLocator.SendKeys("Qaz!@3");

            dashboardPage.ConfirmNewPasswordLocator.SendKeys("Qaz!@3");

            dashboardPage.UpdateButtonLocator.Click();

            dashboardPage.WelcomeUsernameLocator.ClickEx(driver);

            dashboardPage.LogOffLocator.Click();

            loginPage.LoginToApplication("01_testuserAuto@test.com", "Qaz!@3");

            //Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Dashboard/DashboardGridOpen"),
            //    "User is not logged in, password are not updated");

            dashboardPage.WelcomeUsernameLocator.ClickEx(driver);
            dashboardPage.UpdateProfileLocator.Click();

            dashboardPage.NewPasswordLocator.WaitForElementPresentAndEnabled(driver).Clear();
            dashboardPage.NewPasswordLocator.SendKeys("Qaz!23");

            dashboardPage.ConfirmNewPasswordLocator.SendKeys("Qaz!23");
            dashboardPage.UpdateButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);
        }

        //[Test]
        [CustomRetry]
        public void CheckLogOffTest()
        {
            var dashboardPage = new DashboardPage(driver);

            dashboardPage.WelcomeUsernameLocator.ClickEx(driver);
            dashboardPage.LogOffLocator.ClickEx(driver);

            Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Account/Login"), "User is not logged off");
        }

        [Test]
        [CustomRetry]
        public void CheckGridViewIsDefaultTest()
        {
            var dashboardPage = new DashboardPage(driver);

            Assert.Multiple(() =>
            {
                //Assert.True(driver.Url.Equals("https://qa.millimanpixel.com/Dashboard/DashboardGridOpen"), "Incorrect URL opens");
                Assert.True(dashboardPage.GridViewRadioButtonLocator.Selected, "GridView radio button is not selected");
                Assert.False(dashboardPage.TreeViewRadioButtonLocator.Selected, "TreeView radio button is selected");
            });
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}
