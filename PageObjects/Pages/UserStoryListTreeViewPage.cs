using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Threading;

namespace Milliman.Pixel.Web.Tests.PageObjects.Pages
{
    class UserStoryListTreeViewPage : DashboardPage
    {
        private IWebDriver driver;
        public UserStoryListTreeViewPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "folderTree")]
        public IWebElement FolderTreeLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='folderTree']/ul/li")]
        public IList<IWebElement> Folder { get; set; }

        [FindsBy(How = How.Id, Using = "NewFolder")]
        public IWebElement NewFolderLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='fa fa-pencil-square-o toolbarIcon toolbarItem']")]
        public IWebElement EditFolderLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='fa fa-pencil-square-o toolbarIcon toolbarItem disabled']")]
        public IWebElement EditFolderDisabledLocator { get; set; }

        [FindsBy(How = How.Id, Using = "CutItem")]
        public IWebElement CutItemLocator { get; set; }

        [FindsBy(How = How.Id, Using = "PasteItem")]
        public IWebElement PasteItemLocator { get; set; }

        [FindsBy(How = How.Id, Using = "MoveDownItem")]
        public IWebElement MoveDownItemLocator { get; set; }

        [FindsBy(How = How.Id, Using = "MoveUpItem")]
        public IWebElement MoveUpItemLocator { get; set; }

        [FindsBy(How = How.Id, Using = "DeleteItem")]
        public IWebElement DeleteItemLocator { get; set; }

        [FindsBy(How = How.Id, Using = "FolderName")]
        public IWebElement FolderNameLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Add']")]
        public IWebElement AddNewFolderButtonLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='ui-dialog-buttonset']//*[text()='Cancel']")]
        public IWebElement CancelNewFolderButtonLocator { get; set; }

        [FindsBy(How = How.Id, Using = "FolderName-error")]
        public IWebElement FolderNameErrorLocator { get; set; }

        [FindsBy(How = How.Id, Using = "folderStoriesPlaceHolder")]
        public IWebElement LoadingHolderLocator { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='SearchBlockTree']//*[text()='Apply']")]
        public IWebElement ApplyFilterButtonLocator { get; set; }

        public bool CheckFolderName(string clientName, string foldername)
        {
            bool isDisplayed = false;

            for (int i=0; i<Folder.Count; i++)
            {
                if (Folder[i].Text == clientName)
                {
                    for (; i < Folder.Count; i++)
                    {
                        if (Folder[i].Text==foldername ? isDisplayed = true : isDisplayed = false);
                    }
                }  
            }
            return isDisplayed;         
        }

        public void CreateFolder(string folderName, IWebDriver driver)
        {
            NewFolderLocator.Click();

            FolderNameLocator.SendKeys(folderName);

            AddNewFolderButtonLocator.Click();

            Thread.Sleep(2000);
        }

        public void openTreeView()
        {
            TreeViewRadioButtonLocator.ClickEx(driver);

            FolderTreeLocator.WaitForElementPresentAndEnabled(driver);
        }

        public void FilterDataset(string datasetName)
        {
            FilterTextBoxLocator.SendKeys(datasetName);

            ApplyFilterButtonLocator.ClickEx(driver);

            Utils.WaitUntilLoadingPlaceholderDisappears(driver);
        }
    }
}
