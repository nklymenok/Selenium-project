using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL
{
    class DatasetDeletePage
    {
        private IWebDriver driver;

        public DatasetDeletePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "DatasetId-button")]
        public IWebElement SelectDatasetButtonLocator { get; set; }

        [FindsBy(How = How.ClassName, Using = "ui-menu-item")]
        public IList<IWebElement> DatasetLocator { get; set; }

        [FindsBy(How = How.Id, Using = "btnStartDatasetArchive")]
        public IWebElement StartButtonLocator { get; set; }

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
    }
}
