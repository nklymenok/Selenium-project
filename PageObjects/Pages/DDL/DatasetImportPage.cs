using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages.DDL
{
    class DatasetImportPage
    {
        private IWebDriver driver;

        public DatasetImportPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "DatasetTypeId-button")]
        public IWebElement DatasetTypeListLocator { get; set; }

        [FindsBy(How = How.Id, Using = "LineOfBusiness")]
        public IWebElement LineOfBusinessTextLocator { get; set; }

        [FindsBy(How = How.Id, Using = "Region")]
        public IWebElement RegionTextLocator { get; set; }

        [FindsBy(How = How.Id, Using = "Description")]
        public IWebElement DatasetDescriptiontextboxLocator { get; set; }

        [FindsBy(How = How.Id, Using = "AgeBaseDateYear-button")]
        public IWebElement DatasetBaseYearListLocator { get; set; }

        [FindsBy(How = How.Id, Using = "ClientId-button")]
        public IWebElement DatasetOwnerListLocator { get; set; }

        [FindsBy(How = How.Id, Using = "txtUploadFileDataset")]
        public IWebElement UploadButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "btnStartDatasetImport")]
        public IWebElement StartButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "divUploadFileDataset")]
        public IWebElement UploadedFileTextLocator { get; set; }

        [FindsBy(How = How.Id, Using = "DatasetTypeId-button")]
        public IWebElement SelectDatasetTypeButtonLocator { get; set; }

        [FindsBy(How = How.ClassName, Using = "ui-menu-item")]
        public IList<IWebElement> DatasetTypeLocator { get; set; }

        [FindsBy(How = How.ClassName, Using = "footerAdaptive")]
        public IWebElement FooterLocator { get; set; }

        [FindsBy(How = How.Id, Using = "DataLoadValidationInfoForm")]
        public IWebElement DatasetValidationPopupLocator { get; set; }


        public void SelectDatasetType(string datasetType)
        {
            for (int i = 0; i < DatasetTypeLocator.Count; i++)
            {
                if (DatasetTypeLocator[i].Text == datasetType)
                {
                    DatasetTypeLocator[i].Click();
                }
            }
        }
    }
}
