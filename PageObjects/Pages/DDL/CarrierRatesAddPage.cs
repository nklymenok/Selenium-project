using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL
{
    class CarrierRatesAddPage
    {
        private IWebDriver driver;

        public CarrierRatesAddPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "DataSetId-button")]
        public IWebElement SelectDatasetButtonLocator { get; set; }

        [FindsBy(How = How.ClassName, Using = "ui-menu-item")]
        public IList<IWebElement> DatasetLocator { get; set; }

        [FindsBy(How = How.Id, Using = "CarrierName")]
        public IWebElement CarrierNameTextBoxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "AvailabilityType-button")]
        public IWebElement CarrierTypeButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='AvailabilityType-button']//*[text()='Shared']")]
        public IWebElement SharedTypeLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='AvailabilityType-menu']//*[text()='Client-Specific']")]
        public IWebElement ClientTypeLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='btn-file DatasetLoadWizardBtn ui-button ui-widget ui-state-default ui-corner-all']")]
        public IWebElement UploadButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "btnAddRatesEnqueue")]
        public IWebElement StartButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "btnCancel")]
        public IWebElement CancelButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Confirm']")]
        public IWebElement ConfirmAddRatesButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='AddRatesForm']/span")]
        public IWebElement CarrierAddTestLocator { get; set; }

        [FindsBy(How = How.Id, Using = "DataSetTypeName")]
        public IWebElement DatasetTypeTextLocator { get; set; }

        [FindsBy(How = How.Id, Using = "DataSetBaseYear")]
        public IWebElement DatasetBaseYearTextLocator { get; set; }

        [FindsBy(How = How.Id, Using = "ResultingCarrierName")]
        public IWebElement EnteredCarrierNameLocator { get; set; }

        [FindsBy(How = How.Id, Using = "InfoLinkDataSetId")]
        public IWebElement DatasetTipLocator { get; set; }

        [FindsBy(How = How.Id, Using = "DataSetIdAddInfo")]
        public IWebElement DatasetTipTextLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='OK']")]
        public IWebElement TipOkButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "InfoLinkCarrierName")]
        public IWebElement CarrierNameTipLocator { get; set; }

        [FindsBy(How = How.Id, Using = "CarrierNameInfo")]
        public IWebElement CarrierNameTipTextLocator { get; set; }

        [FindsBy(How = How.Id, Using = "InfoLinkRateFile")]
        public IWebElement RateFileTipLocator { get; set; }

        [FindsBy(How = How.Id, Using = "txtUploadFileRateInfo")]
        public IWebElement RateFileTipTextLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ClientsTable2_data_body']/tr")]
        public IList<IWebElement> ClientNameColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='ClientsTable_data_body']/tr")]
        public IList<IWebElement> ClientNameSharedColumnLocator { get; set; }

        [FindsBy(How = How.Id, Using = "divUploadFileRate")]
        public IWebElement UploadedFileTextLocator { get; set; }

        [FindsBy(How = How.ClassName, Using = "footerAdaptive")]
        public IWebElement FooterLocator { get; set; }

        public void SelectDataset(string dataset)
        {
            for (int i = 0; i < DatasetLocator.Count; i++)
            {
                if (DatasetLocator[i].Text == dataset)
                {
                    DatasetLocator[i].Click();
                } 
            }
        }

        public void SelectClient(string clientName)
        {
            for (int i = 0; i < ClientNameColumnLocator.Count; i++)
            {
                if (ClientNameColumnLocator[i].Text == clientName)
                {
                    driver.FindElement(By.XPath($"//*[@id='ClientsTable2_data_body']/tr[{i+1}]//input")).Click();
                }
            }
        }

        public void SelectSharedClient(string clientName, bool isAllowToExport = false)
        {
            for (int i = 0; i < ClientNameSharedColumnLocator.Count; i++)
            {
                if (ClientNameSharedColumnLocator[i].Text == clientName)
                {
                    driver.FindElement(By.XPath($"//*[@id='ClientsTable_data_body']/tr[{i + 1}]//input[1]")).Click();
                    if (isAllowToExport)
                    {
                        driver.FindElement(By.XPath($"//*[@id='ClientsTable_data_body']/tr[{i + 1}]/td[3]/input")).Click();
                    }
                }
            }
        }
    }
}
