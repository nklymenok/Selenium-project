using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL
{
    class CarrierRatesRemovePage
    {
        private IWebDriver driver;

        public CarrierRatesRemovePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "DataSetId-button")]
        public IWebElement SelectDatasetButtonLocator { get; set; }

        [FindsBy(How = How.ClassName, Using = "ui-menu-item")]
        public IList<IWebElement> DatasetLocator { get; set; }

        [FindsBy(How = How.Id, Using = "CarrierId-button")]
        public IWebElement SelectCarrierButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "btnStartRemoveRates")]
        public IWebElement StartButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Confirm']")]
        public IWebElement ConfirmRemoveRatesButtonLocator { get; set; }

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

        public void SelectCarrier(string carrier)
        {
            for (int i = 0; i < DatasetLocator.Count; i++)
            {
                if (DatasetLocator[i].Text.Contains(carrier))
                {
                    DatasetLocator[i].Click();
                }
            }
        }
    }
}
