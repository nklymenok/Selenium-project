using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class TreeViewTests
    {
        public IWebDriver driver;
        LoginPage loginPage;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://qa.millimanpixel.com";
            driver.Manage().Window.Maximize();

            loginPage = new LoginPage(driver);

            loginPage.LoginToApplication();
        }

        [Test]
        [CustomRetry]
        public void SwitchBetweenTreeViewAndGridViewTest()
        {
            var treeView = new UserStoryListTreeViewPage(driver);

            treeView.openTreeView();

            Assert.True(treeView.FolderTreeLocator.Displayed, "TreeView is not displayed");
        }

        [Test]
        [CustomRetry]
        public void ControlPanelButtonsAreShownTest()
        {
            var treeView = new UserStoryListTreeViewPage(driver);

            treeView.openTreeView();

            Assert.Multiple(() =>
            {
                Assert.True(treeView.NewFolderLocator.Displayed, "New folder button is not displayed");
                Assert.True(treeView.EditFolderDisabledLocator.Displayed, "Edit folder button is not displayed");
                Assert.True(treeView.CutItemLocator.Displayed, "Cut button is not displayed");
                Assert.True(treeView.PasteItemLocator.Displayed, "Paste button is not displayed");
                Assert.True(treeView.MoveDownItemLocator.Displayed, "Move down button is not displayed");
                Assert.True(treeView.MoveUpItemLocator.Displayed, "Move up button is not displayed");
                Assert.True(treeView.DeleteItemLocator.Displayed, "Delete folder button is not displayed");
            });
        }

        [Test]
        [CustomRetry]
        public void PixelRootFolderIsDisplayedTest()
        {
            var treeView = new UserStoryListTreeViewPage(driver);

            treeView.openTreeView();

            Assert.AreEqual("Pixel", treeView.Folder[0].Text, "Pixel root folder is not displayed");
        }

        [CustomRetry]
        [TestCase(0)]
        [TestCase(1)]
        public void CheckFolderProhibitedToRenameTest(int folderIndex)
        {
            var treeView = new UserStoryListTreeViewPage(driver);

            treeView.openTreeView();

            treeView.Folder[folderIndex].ClickEx(driver);

            Assert.IsTrue(treeView.EditFolderDisabledLocator.Displayed, "EditFolder button is enabled");
        }


        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }

    }
}
