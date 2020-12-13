using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL
{
    class LossesAddPage : CarrierRatesAddPage
    {

        private IWebDriver driver;

        public LossesAddPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "AvailabilityType-button")]
        public IWebElement CarrierTypeButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='AvailabilityType-button']//*[text()='Shared']")]
        public IWebElement SharedTypeLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='AvailabilityType-menu']//*[text()='Client-Specific']")]
        public IWebElement ClientTypeLocator { get; set; }

        [FindsBy(How = How.Id, Using = "btnStartAddLosses")]
        public IWebElement StartButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Confirm']")]
        public IWebElement ConfirmAddRatesButtonLocator { get; set; }

        public void SelectClient(string clientName)
        {
            for (int i = 0; i < ClientNameColumnLocator.Count; i++)
            {
                if (ClientNameColumnLocator[i].Text == clientName)
                {
                    driver.FindElement(By.XPath($"//*[@id='ClientsTable2_data_body']/tr[{i + 1}]//input")).Click();
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

        public void ScrollToStartButton()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(StartButtonLocator);
            actions.Perform();
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,100)");
        }
    }
}
