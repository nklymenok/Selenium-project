using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages
{
    public class DashboardPage
    {
        private IWebDriver driver;

        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='PixelMenu']//img")]
        public IWebElement LogoLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='footerAdaptive']//img")]
        public IWebElement LogoFooterLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='logoutForm']//li[2]//span[1]")]
        public IWebElement WelcomeUsernameLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ProfileUpdateLink']")]
        public IWebElement UpdateProfileLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='logoutForm']//li[2]//li[2]/a")]
        public IWebElement LogOffLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ProfileUpdateForm']//tr[3]//input")]
        public IWebElement UpdatePhoneLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ProfileUpdateForm']//tr[1]//input")]
        public IWebElement NewPasswordLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ProfileUpdateForm']//tr[2]//input")]
        public IWebElement ConfirmNewPasswordLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Update']")]
        public IWebElement UpdateButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Cancel']")]
        public IWebElement CancelButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ModeSelectBlock']//label[2]/input")]
        public IWebElement GridViewRadioButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ModeSelectBlock']//label[1]/input")]
        public IWebElement TreeViewRadioButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='SearchBlock']/label[1]")]
        public IWebElement FilterLabelLocator { get; set; }

        [FindsBy(How = How.Id, Using = "filterText")]
        public IWebElement FilterTextBoxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='SearchBlock']/label[2]")]
        public IWebElement UpdateDateLabelLocator { get; set; }

        [FindsBy(How = How.Id, Using = "datepickerFrom")]
        public IWebElement DateFromLocator { get; set; }

        [FindsBy(How = How.Id, Using = "datepickerTo")]
        public IWebElement DateToLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='SearchBlock']//*[text()='Apply']")]
        public IWebElement ApplyFilterButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='SearchBlock']//*[text()='Clear']")]
        public IWebElement ClearFilterButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='DashboardOpenPart']//td[1]")]
        public IList<IWebElement> Name { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='openDashboardLink'][text()='Default']")]
        public IWebElement FirstDefaultDatasetLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='openDashboardLink']")]
        public IList<IWebElement> DefaultDatasetLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='DashboardOpenPart']//td[2]")]
        public IList<IWebElement> Description { get; set; }

        [FindsBy(How = How.Id, Using = "FilterAppliedLabel")]
        public IWebElement FilterAppliedLocator { get; set; }

        [FindsBy(How = How.Id, Using = "div-loading")]
        public IWebElement LoadingLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='SearchBlock']/span/div/div/div")]
        public IList<IWebElement> SearchResultsLocator { get; set; }

        [FindsBy(How = How.Id, Using = "Last_Page")]
        public IWebElement LastPageLocator { get; set; }

        [FindsBy(How = How.Id, Using = "Previous_Page")]
        public IWebElement PreviousPageLocator { get; set; }

        public bool DatasetRowExist(string dataset)
        {
            bool row = false;

            for (int i = 0; i < Description.Count; i++)
            {
                if (Description[i].Text.Contains(dataset)) row = true;
            }
            if (row == false)
            {
                PreviousPageLocator.Click();
                Utils.WaitUntilLoadingPlaceholderDisappears(driver, secondtToWait: 20);

                for (int i = 0; i < Description.Count; i++)
                {
                    if (Description[i].Text.Contains(dataset)) row = true;
                }
            }
            return row;
        }

        public void FilterDataset(string datasetName)
        {
            FilterTextBoxLocator.SendKeys(datasetName);

            ApplyFilterButtonLocator.ClickEx(driver);

            Utils.WaitUntilLoadingDisappears(driver);
        }

        public void OpenDataset(string dataset, string name)
        {
            for (int i = 0; i < Description.Count; i++)
            {
                if (Description[i].Text.Contains(dataset) & DefaultDatasetLocator[i].Text.Contains(name))
                {
                    DefaultDatasetLocator[i].Click();
                }
            }
        }
    }
}
