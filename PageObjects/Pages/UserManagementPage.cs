using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Milliman.Pixel.Web.Tests.PageObjects
{
    class UserManagementPage
    {
        private IWebDriver driver;

        public UserManagementPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "AddUserButton")]
        public IWebElement AddUserButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "_EmailNew")]
        public IWebElement AddEmailTextBoxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "_CommonNameNew")]
        public IWebElement AddNameTextBoxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "_SecurityGroupIDNew-button")]
        public IWebElement AddSecurityGroupButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "_OfficeIdNew-button")]
        public IWebElement AddOfficeButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='_OfficeIdNew-menu']//*[text()='Milliman - San Francisco']")]
        public IWebElement AddSanFranciscoLocator { get; set; }

        [FindsBy(How = How.Id, Using = "_ClientIdNew-button")]
        public IWebElement AddClientButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='_ClientIdNew-menu']//*[text()='Luxoft']")]
        public IWebElement AddClientLuxoftLocator { get; set; }

        [FindsBy(How = How.Id, Using = "_PasswordNew")]
        public IWebElement AddPasswordTextBoxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "_ConfirmPasswordNew")]
        public IWebElement AddConfirmPasswordTextBoxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Save']")]
        public IWebElement SaveNewUserButtonLocator { get; set; }
      
        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-users']//td[2]")]
        public IList<IWebElement> Login { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-users']//td[1]")]
        public IList<IWebElement> Id { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Cancel']")]
        public IWebElement CancelButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "UserID")]
        public IWebElement IdButtonLocator { get; set; }     


        public void AddNewUser(string user, string random, string password)
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));

            AddUserButtonLocator.ClickEx(driver);

            AddEmailTextBoxLocator.WaitForElementPresentAndEnabled(driver).SendKeys($"{user}{random}@test.com");

            AddNameTextBoxLocator.SendKeys($"{user}{random}");

            AddSecurityGroupButtonLocator.Click();

            driver.FindElement(By.XPath($"//*[@id='_SecurityGroupIDNew-menu']//*[text()='{user}']")).Click();

            Utils.WaitUntilLoadingDisappears(driver);

            AddOfficeButtonLocator.Click();

            AddSanFranciscoLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            AddClientButtonLocator.Click();

            AddClientLuxoftLocator.Click();

            AddPasswordTextBoxLocator.SendKeys(password);

            AddConfirmPasswordTextBoxLocator.SendKeys(password);

            Utils.Scroll(SaveNewUserButtonLocator, driver);

            SaveNewUserButtonLocator.ClickEx(driver);

            Utils.WaitUntilLoadingDisappears(driver);
        }
    }
} 
