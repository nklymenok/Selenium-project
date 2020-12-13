using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages
{
    class DatasetsPage
    {
        private IWebDriver driver;

        public DatasetsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[2]/table/tbody/tr/td[2]")]
        public IList<IWebElement> DatasetNameLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[2]/table/tbody/tr/td[7]/a[1]")]
        public IList<IWebElement> SettingsButtonLocator { get; set; }

        [FindsBy(How = How.ClassName, Using = "footerAdaptive")]
        public IWebElement FooterLocator { get; set; }

        public void SelectDataset(string datasetName)
        {
            for (int i=0; i<DatasetNameLocator.Count; i++)
            {
                if (DatasetNameLocator[i].Text.Equals(datasetName))
                {
                    Utils.Scroll(FooterLocator, driver);

                    SettingsButtonLocator[i].Click();
                }
            }
        }
    }
}
