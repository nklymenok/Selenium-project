using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace Milliman.Pixel.Web.Tests.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement EmailTextBoxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement PasswordTextBoxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='loginForm']//div[4]//input")]
        public IWebElement LoginButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='loginForm']//div[1]//li")]
        public IWebElement FailedLoginMessageLocator { get; set; }

        public void LoginToApplication(string login = "@test.com", string password = "Qaz!**")
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Email")));

            EmailTextBoxLocator.SendKeys(login);
            PasswordTextBoxLocator.SendKeys(password);
            LoginButtonLocator.Submit();
        }
    }
}
