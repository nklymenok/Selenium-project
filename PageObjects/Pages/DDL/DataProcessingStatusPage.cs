using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL
{
    class DataProcessingStatusPage
    {
        private IWebDriver driver;

        public DataProcessingStatusPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[1]")]
        public IList<IWebElement> RequestDT { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[2]")]
        public IList<IWebElement> Client { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[2]")]
        public IList<IWebElement> ClientUser { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[3]")]
        public IList<IWebElement> User { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[3]")]
        public IList<IWebElement> UserRequestType { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[4]")]
        public IList<IWebElement> RequestType { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[4]")]
        public IList<IWebElement> UserDataType { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[5]")]
        public IList<IWebElement> DataType { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[5]")]
        public IList<IWebElement> UserStatus { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[6]")]
        public IList<IWebElement> Status { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[6]")]
        public IList<IWebElement> UserDataset { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[7]")]
        public IList<IWebElement> Dataset { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[8]")]
        public IList<IWebElement> ProcessStartDT { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[9]")]
        public IList<IWebElement> ProcessEndDT { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-office']//td[10]")]
        public IList<IWebElement> ETA { get; set; }

        [FindsBy(How = How.Id, Using = "RefreshRate-button")]
        public IWebElement RefreshRateButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "ui-id-3")]
        public IWebElement FifteenSecRefreshLocator { get; set; }

        [FindsBy(How = How.Id, Using = "DataProcessStatusReportLink")]
        public IWebElement DataProcessStatusReportLocator { get; set; }

        [FindsBy(How = How.Id, Using = "DataProcessStatusPopUp")]
        public IWebElement ReportPopupLocator { get; set; }

        public int DatasetRow(string dataset)
        {
            int row=0;
           
            for(int i=0; i < Dataset.Count; i++)
            {
                if (Dataset[i].Text.Contains(dataset)) row = i;
            }

            return row;
        }

        public int UserDatasetRow(string dataset)
        {
            int row = 0;

            for (int i = 0; i < UserDataset.Count; i++)
            {
                if (UserDataset[i].Text.Contains(dataset)) row = i;
            }

            return row;
        }

        public void CheckRefreshRateToMinimum()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("RefreshRate-button")));
                RefreshRateButtonLocator.ClickEx(driver);
                FifteenSecRefreshLocator.Click();

                Utils.WaitUntilLoadingDisappears(driver);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }   
        }
    }
}
