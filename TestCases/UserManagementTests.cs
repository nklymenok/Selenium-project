using Milliman.Pixel.Web.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class UserManagementTests
    {
        IWebDriver driver;
        LoginPage loginPage;
        static string random = Utils.RandomString(5);
        private const string roleUser = "User";
        private const string roleSuperAdmin = "Super-Administrator";
        private const string roleOfficeAdmin = "Office-Administrator";
        private const string roleOfficeActuary = "Office-Actuary";
        private const string password = "NBV87^yu";

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://pixel.com";
                    driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            loginPage.LoginToApplication();
        }

        [Test]
        [CustomRetry]
        public void CheckUserManagementPageOpensTest()
        {
            var menu = new MenuPage(driver);

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.UserManagementMenuLocator.Click();

            Assert.True(driver.Url.Equals("https://pixel.com/Administrator/UserManagement"),
                "UserManagement page is not opened");
        }

        [CustomRetry]
        [TestCase(roleUser)]
        [TestCase(roleSuperAdmin)]
        [TestCase(roleOfficeAdmin)]
        [TestCase(roleOfficeActuary)]
        public void AddUserTest(string user, string password = password)
        {
            var menu = new MenuPage(driver);

            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));

            menu.AdministrationMenuLocator.ClickEx(driver);

            menu.UserManagementMenuLocator.Click();

            var userPage = new UserManagementPage(driver);

            userPage.AddNewUser(user, random, password);

            userPage.IdButtonLocator.ClickEx(driver);

            Thread.Sleep(1000);

            wait.Until(ExpectedConditions.TextToBePresentInElement(userPage.Id[0], "1"));

            userPage.IdButtonLocator.Click();

            Thread.Sleep(1000);

            Assert.AreEqual($"{user}{random}@test.com", userPage.Login[0].Text,
                $"User {user} is not added");
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}
