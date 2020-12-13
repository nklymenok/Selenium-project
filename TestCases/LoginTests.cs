using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Milliman.Pixel.Web.Tests.PageObjects;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class LoginTests
    {
        IWebDriver driver;
        LoginPage loginPage;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://pixel.com";
            driver.Manage().Window.Maximize();

            loginPage = new LoginPage(driver);
        }

        [Test]
        [CustomRetry]
        public void CheckThatPageOpensTest()
        {
            Assert.True(loginPage.LoginButtonLocator.Enabled, "Login button is disabled");
        }

        [Test]
        [CustomRetry]
        public void LoginWithCorrectCredentialsTest()
        {
            loginPage.LoginToApplication();

            Assert.True(driver.Url.Equals("https://pixel.com/Dashboard/DashboardGridOpen"),
                "failed to login");
        }

        [Test]
        [CustomRetry]
        public void LoginWithIncorrectCredentialsTest()
        {
            loginPage.LoginToApplication("10_testuser@test1.com", "3266");

            Assert.AreEqual("Failed Login. Please contact pixelsupport@millimanpixel.com for assistance",
                loginPage.FailedLoginMessageLocator.Text, "Incorrect error message");
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}

