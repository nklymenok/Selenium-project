using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Threading;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages
{
    class ClientToDatasetPage : DatasetsToClientsPage
    {
        private IWebDriver driver;

        public ClientToDatasetPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "k-input")]
        public IWebElement DefaultStoryButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='carrierGrid']/div[2]//td[1]")]
        public IList<IWebElement> CarrierNameLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='carrierGrid']/div[2]//tr")]
        public IList<IWebElement> RowRateLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='lossGrid']/div[2]//tr")]
        public IList<IWebElement> RowLossLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='carrierGrid']//td[2]//input")]
        public IList<IWebElement> CarrierAllowedReportCheckboxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='carrierGrid']//td[3]//input")]
        public IList<IWebElement> CarrierAllowedExportCheckboxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='lossGrid']/div[2]//td[1]")]
        public IList<IWebElement> LossNameLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='lossGrid']//td[2]//input")]
        public IList<IWebElement> LossAllowedReportCheckboxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='lossGrid']//td[3]//input")]
        public IList<IWebElement> LossAllowedExportCheckboxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "headerCarrierIsReportAllowed")]
        public IWebElement RatesAllowedReportAllCheckboxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "headerCarrierIsExportAllowed")]
        public IWebElement RatesAllowedExportAllCheckboxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "headerLossIsReportAllowed")]
        public IWebElement LossesAllowedReportAllCheckboxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "headerLossIsExportAllowed")]
        public IWebElement LossesAllowedExportAllCheckboxLocator { get; set; }

        public void SelectDefaultStory(string storyName = "Default (Default)")
        {
            DefaultStoryButtonLocator.Click();
            driver.FindElement(By.XPath($"//*[@id='defaultSotries-list']//*[contains(text(), '{storyName}')]")).Click();
        }

        public bool CarrierIsPresent(string carrierName)
        {
            bool isPresent = false;

            for (int i=0; i < CarrierNameLocator.Count; i++)
            {
                if (CarrierNameLocator[i].Text.Equals(carrierName))
                {
                    isPresent = true;
                }
            }
            return isPresent;
        }

        public bool LossIsPresent(string lossName)
        {
            bool isPresent = false;

            for (int i = 0; i < LossNameLocator.Count; i++)
            {
                if (LossNameLocator[i].Text.Equals(lossName))
                {
                    isPresent = true;
                }
            }
            return isPresent;
        }

        public bool CarrierIsAllowedReport(string carrierName)
        {
            bool isAllowed = false;

            for (int i = 0; i < CarrierNameLocator.Count; i++)
            {
                if (CarrierNameLocator[i].Text.Equals(carrierName))
                {
                    if (CarrierAllowedReportCheckboxLocator[i].Selected)
                    isAllowed = true;
                }
            }
            return isAllowed;
        }

        public bool CarrierIsAllowedExport(string carrierName)
        {
            bool isAllowed = false;

            for (int i = 0; i < CarrierNameLocator.Count; i++)
            {
                if (CarrierNameLocator[i].Text.Equals(carrierName))
                {
                    if (CarrierAllowedExportCheckboxLocator[i].Selected)
                        isAllowed = true;
                }
            }
            return isAllowed;
        }

        public bool LossIsAllowedReport(string lossName)
        {
            bool isAllowed = false;

            for (int i = 0; i < LossNameLocator.Count; i++)
            {
                if (LossNameLocator[i].Text.Equals(lossName))
                {
                    if (LossAllowedReportCheckboxLocator[i].Selected)
                        isAllowed = true;
                }
            }
            return isAllowed;
        }

        public bool LossIsAllowedExport(string lossName)
        {
            bool isAllowed = false;

            for (int i = 0; i < LossNameLocator.Count; i++)
            {
                if (LossNameLocator[i].Text.Equals(lossName))
                {
                    if (LossAllowedExportCheckboxLocator[i].Selected)
                        isAllowed = true;
                }
            }
            return isAllowed;
        }

        public bool CarrierIsDisabled(string carrierName)
        {
            bool isDisabled = false;

            for (int i = 0; i < CarrierNameLocator.Count; i++)
            {
                if (CarrierNameLocator[i].Text.Equals(carrierName))
                {
                    if (RowRateLocator[i].GetAttribute("style").Contains("opacity: 0.5"))
                        isDisabled = true;
                }
            }
            return isDisabled;
        }

        public bool LossIsDisabled(string lossName)
        {
            bool isDisabled = false;

            for (int i = 0; i < LossNameLocator.Count; i++)
            {
                if (LossNameLocator[i].Text.Equals(lossName))
                {
                    if (RowLossLocator[i].GetAttribute("style").Contains("opacity: 0.5"))
                        isDisabled = true;
                }
            }
            return isDisabled;
        }

        public void CheckAllowAll(IWebElement checkbox)
        {
            if (!checkbox.Selected) checkbox.Click();
            Utils.WaitUntilLoadingDisappears(driver);
        }

        public bool CheckRates(IList<IWebElement> checkboxes)
        {
            bool isAllChecked = true;

            for (int i = 0; i < CarrierNameLocator.Count; i++)
            {
                if (!checkboxes[i].Selected)
                {
                    if (RowRateLocator[i].GetAttribute("style").Contains("opacity: 0.5"))
                    {
                        continue;
                    }
                    else
                    {
                        isAllChecked = false;
                    }
                }
            }
            return isAllChecked;
        }

        public bool CheckLosses(IList<IWebElement> checkboxes)
        {
            bool isAllChecked = true;

            for (int i = 0; i < LossNameLocator.Count; i++)
            {
                if (!checkboxes[i].Selected)
                {
                    if (RowLossLocator[i].GetAttribute("style").Contains("opacity: 0.5"))
                    {
                        continue;
                    }
                    else
                    {
                        isAllChecked = false;
                    }
                }
            }
            return isAllChecked;
        }
    }
}
