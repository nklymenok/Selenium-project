using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Threading;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages
{
    class DatasetManagementPage
    {
        private IWebDriver driver;

        public DatasetManagementPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-clients']/tbody/tr/td[1]")]
        public IList<IWebElement> Id { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-clients']/tbody/tr/td[2]")]
        public IList<IWebElement> Client { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-table-clients']/tbody/tr/td[3]")]
        public IList<IWebElement> Office { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='CDManagementPartial']/form/h4")]
        public IWebElement SelectClientTextLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-cd-assignment']/tbody/tr/td[3]")]
        public IList<IWebElement> Dataset { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-cd-assignment']/tbody/tr/td[4]")]
        public IList<IWebElement> DefaultStory { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-cd-assignment']/tbody/tr/td[5]")]
        public IList<IWebElement> ReportingCarriers { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-cd-assignment']/tbody/tr/td[6]")]
        public IList<IWebElement> ExportingCarriers { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-cd-assignment']/tbody/tr/td[7]")]
        public IList<IWebElement> ReportingLosses { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='admin-cd-assignment']/tbody/tr/td[8]")]
        public IList<IWebElement> ExportingLosses { get; set; }

        public void OpenClient(string clientName)
        {
            for (int i = 0; i < Id.Count; i++)
            {
                if (Client[i].Text == clientName)
                {
                    Utils.Scroll(Client[i+2], driver);
                    Thread.Sleep(2000);

                    Client[i].FindElement(By.LinkText(clientName)).Click();
                }
            }
        }
    } 
}
