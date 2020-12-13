using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages
{
    class DataManagementPage
    {
        private IWebDriver driver;

        public DataManagementPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[1]")]
        public IList<IWebElement> Id { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[2]")]
        public IList<IWebElement> Office { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[3]")]
        public IList<IWebElement> TotalClients { get; set; }

        [FindsBy(How = How.Id, Using = "AddOfficeButton")]
        public IWebElement AddOfficeButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "OfficeName")]
        public IWebElement AddOfficeNameTextBoxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Save']")]
        public IWebElement AddOfficeSaveButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='container-fluid']//*[text()='Offices']")]
        public IWebElement OfficesTabLocator { get; set; }

        [FindsBy(How = How.LinkText, Using = "Clients")]
        public IWebElement ClientsTabLocator { get; set; }        

        [FindsBy(How = How.LinkText, Using = "Datasets")]
        public IWebElement DatasetsTabLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//*[text()='ID']")]
        public IWebElement OfficesIDColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//*[text()='Office']")]
        public IWebElement OfficesOfficeColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//*[text()='Total Clients']")]
        public IWebElement OfficesTotalClientsColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-clients']//*[text()='ID']")]
        public IWebElement ClientsIDColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-clients']//*[text()='Client']")]
        public IWebElement ClientsClientColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-clients']//*[text()='Office']")]
        public IWebElement ClientsOfficeColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-clients']//*[text()='Client Short Name']")]
        public IWebElement ClientsClientShortNameColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-clients']//*[text()='Dataset Load From UI']")]
        public IWebElement ClientsDatasetLoadFromUIColumnLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-clients']//*[text()='Edit Client DatasetType']")]
        public IWebElement ClientsEditClientDatasetTypeColumnLocator { get; set; }

        public bool CheckOffice(string officeName)
        {
            Thread.Sleep(1000);

            for (int i=0; i < Id.Count; i++)
            {
                if (Office[i].Text == officeName)
                    return true;    
            }
            return false;
        }

        public void CreateOffice(string officeName)
        {
            if (!CheckOffice(officeName))
            {
                AddOfficeButtonLocator.ClickEx(driver);

                AddOfficeNameTextBoxLocator.WaitForElementPresentAndEnabled(driver).SendKeys(officeName);

                AddOfficeSaveButtonLocator.Click();

                Utils.WaitUntilLoadingDisappears(driver);
            }
        }      
    }   
} 
