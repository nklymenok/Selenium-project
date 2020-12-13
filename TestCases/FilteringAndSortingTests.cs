using Milliman.Pixel.Web.Tests.PageObjects;
using Milliman.Pixel.Web.Tests.PageObjects.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Milliman.Pixel.Web.Tests.TestCases
{
    [TestFixture]
    class FilteringAndSortingTests
    {
        IWebDriver driver;
        LoginPage loginPage;
        private const string password = "Qaz!23";

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://qa.millimanpixel.com";
            //driver.Url = "https://preprod.millimanpixel.com/";
            driver.Manage().Window.Maximize();

            loginPage = new LoginPage(driver);

            loginPage.LoginToApplication();
        }

        [Test]
        [CustomRetry]
        public void FilteringIsPresentTest()
        {
            var dashboardPage = new DashboardPage(driver);

            Assert.Multiple(() =>
            {
                Assert.True(dashboardPage.FilterLabelLocator.Displayed, "Filter label is not displayed");
                Assert.True(dashboardPage.FilterTextBoxLocator.Displayed, "Filter textbox is not displayed");
                Assert.True(dashboardPage.UpdateDateLabelLocator.Displayed, "Update date label is not displayed");
                Assert.True(dashboardPage.DateFromLocator.Displayed, "Date from textbox is not displayed");
                Assert.True(dashboardPage.DateToLocator.Displayed, "Date to textbox is not displayed");
                Assert.True(dashboardPage.ApplyFilterButtonLocator.Displayed, "Apply button is not displayed");
                Assert.True(dashboardPage.ClearFilterButtonLocator.Displayed, "Clear button is not displayed");
            });
        }

        [Test]
        [CustomRetry]
        public void CheckingFilteredFieldTest()
        {
            var dashboardPage = new DashboardPage(driver);

            string filterString = Utils.RandomString(15);

            dashboardPage.FilterTextBoxLocator.SendKeys(filterString);

            Assert.AreEqual(filterString, dashboardPage.FilterTextBoxLocator.GetAttribute("value").ToString(),
                $"{dashboardPage.FilterTextBoxLocator.GetAttribute("value").ToString()} is not equal to {filterString}");
        }

        [Test]
        [CustomRetry]
        public void SpecifyManuallyDataFieldsTest()
        {
            var dashboardPage = new DashboardPage(driver);

            string dataFrom = "07/01/2019";
            string dataTo = "07/16/2019";

            dashboardPage.DateFromLocator.SendKeys(dataFrom);

            dashboardPage.DateToLocator.SendKeys(dataTo);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(dataFrom, dashboardPage.DateFromLocator.GetAttribute("value").ToString(), "Incorrect date from is displayed");
                Assert.AreEqual(dataTo, dashboardPage.DateToLocator.GetAttribute("value").ToString(), "Incorrect date to is displayed");
            });
        }

        [Test]
        [CustomRetry]
        public void ClearFilterTest()
        {
            var dashboardPage = new DashboardPage(driver);

            string filterString = "flood";

            var rowCountBeforeFilter = dashboardPage.Name.Count;

            dashboardPage.FilterTextBoxLocator.SendKeys(filterString);

            dashboardPage.ApplyFilterButtonLocator.ClickEx(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dashboardPage.ClearFilterButtonLocator.Click();

            Utils.WaitUntilLoadingDisappears(driver);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(rowCountBeforeFilter, dashboardPage.Name.Count, "Filter is not cleared");
                Assert.True(dashboardPage.FilterTextBoxLocator.Text == "", "Filter textbox is not empty");
            });
        }

        [Test]
        [CustomRetry]
        public void ClearingFilterAfterReloginTest()
        {
            var dashboardPage = new DashboardPage(driver);

            string filterString = "Patrick";

            var rowCountBeforeFilter = dashboardPage.Name.Count;

            dashboardPage.FilterTextBoxLocator.SendKeys(filterString);

            dashboardPage.ApplyFilterButtonLocator.ClickEx(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dashboardPage.WelcomeUsernameLocator.ClickEx(driver);

            dashboardPage.LogOffLocator.Click();

            loginPage.LoginToApplication("01_testuserAuto@test.com", password);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(rowCountBeforeFilter, dashboardPage.Name.Count, "Filter is not cleared");
                Assert.True(dashboardPage.FilterTextBoxLocator.Text == "", "Filter textbox is not empty");
            });
        }

        [Test]
        [CustomRetry]
        public void CheckIncorrectDatePeriodTest()
        {
            var dashboardPage = new DashboardPage(driver);

            string dateFrom = "07/16/2019";
            string dataTo = "07/07/2019";

            dashboardPage.DateFromLocator.SendKeys(dateFrom);

            dashboardPage.DateToLocator.SendKeys(dataTo);

            dashboardPage.ApplyFilterButtonLocator.ClickEx(driver);

            Assert.True(dashboardPage.FilterAppliedLocator.Text.Equals("The date is incorrect"), "Error message is not displayed");
        }

        [Test]
        [CustomRetry]
        public void FilterClearedIfChangeGridTest()
        {
            var dashboardPage = new DashboardPage(driver);

            string filterString = "Patrick";

            dashboardPage.FilterTextBoxLocator.SendKeys(filterString);

            dashboardPage.ApplyFilterButtonLocator.ClickEx(driver);

            Utils.WaitUntilLoadingDisappears(driver);

            dashboardPage.TreeViewRadioButtonLocator.Click();

            var treeView = new UserStoryListTreeViewPage(driver);

            treeView.FolderTreeLocator.WaitForElementPresentAndEnabled(driver);

            Assert.True(dashboardPage.FilterTextBoxLocator.Text == "", "Filter is not cleared");
        }

        [Test]
        [CustomRetry]
        public void FilterDropdownAppearsTest()
        {
            var dashboardPage = new DashboardPage(driver);

            string filterString = "Flood";

            dashboardPage.FilterTextBoxLocator.SendKeys(filterString);

            Assert.AreEqual(7, dashboardPage.SearchResultsLocator.Count, "Incorrect count of element in Filted drop down");
        }

        [TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }
}
