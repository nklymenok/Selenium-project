using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages
{
    class DatasetsToClientsPage
    {
        private IWebDriver driver;

        public DatasetsToClientsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "DataModuleHeaderLabel")]
        public IWebElement DatasetHeaderLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[2]/table/tbody/tr/td[3]")]
        public IList<IWebElement> ClientNameLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']//tr/td[1]//input")]
        public IList<IWebElement> AssignedCheckboxLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']//tr/td[5]/a")]
        public IList<IWebElement> SettingsButtonLocator { get; set; }

        public void OpenSettingsForClient(string client)
        {
            for (int i = 0; i < ClientNameLocator.Count; i++)
            {
                if (ClientNameLocator[i].Text.Equals(client))
                {
                    ClientNameLocator[i].Click();

                    if (!AssignedCheckboxLocator[i].Selected)
                    {
                        new Actions(driver).DoubleClick(AssignedCheckboxLocator[i]).Perform();
                    }
                    
                    SettingsButtonLocator[i].ClickEx(driver);
                }
            }
        }
    }
}
