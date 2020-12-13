using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Milliman.Pixel.Web.Tests.PageObjects
{
    public class MenuPage
    {
        private IWebDriver driver;

        public MenuPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='pixel-menu-collapse']//*[text()='ADMINISTRATION']")]
        public IWebElement AdministrationMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Data Management']")]
        public IWebElement DataManagementMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Datasets']")]
        public IWebElement DatasetManagementMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='User Management']")]
        public IWebElement UserManagementMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Client Statistics']")]
        public IWebElement ClientStatisticsMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='pixel-menu-collapse']//*[text()='DATA']")]
        public IWebElement DataMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Processing Status']")]
        public IWebElement ProcessingStatusMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Dataset: Import']")]
        public IWebElement DataserImportMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Dataset: Export']")]
        public IWebElement DatasetExportMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Dataset: Archive/Delete']")]
        public IWebElement DatasetArchiveDeleteMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Carrier Rates: Add']")]
        public IWebElement CarrierRatesAddMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Carrier Rates: Remove']")]
        public IWebElement CarrierRatesRemoveMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Carrier Rates: Export]")]
        public IWebElement CarrierRatesExportMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Losses: Add']")]
        public IWebElement LossesAddMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Losses: Remove']")]
        public IWebElement LossesRemoveMenuLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='dropdown-menu']//*[text()='Losses: Export]")]
        public IWebElement LossesExportMenuLocator { get; set; }
    }
}
