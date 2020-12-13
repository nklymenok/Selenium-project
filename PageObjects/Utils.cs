using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace Milliman.Pixel.Web.Tests.PageObjects
{
    public static class Utils
    {
        private static Random random = new Random();

        public static string RandomString(
            int length = 20,
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
        {
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void Scroll(IWebElement element, IWebDriver driver)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public static T ClickEx<T>(this T item, IWebDriver driver) where T : IWebElement
        {
            item.WaitForElementPresentAndEnabled(driver).Click();
            return item;
        }

        public static IWebElement WaitForElementPresentAndEnabled(this IWebElement locator, IWebDriver driver, int secondsToWait = 20)
        {
            Thread.Sleep(1000);
            new WebDriverWait(driver, new TimeSpan(0, 0, secondsToWait))
               .Until(d => locator.Enabled
                   && locator.Displayed
                   && locator.GetAttribute("aria-disabled") == null);
            Thread.Sleep(1000);
            return locator;
        }

        public static void WaitUntilLoadingDisappears(IWebDriver driver, int secondtToWait = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondtToWait));

            Thread.Sleep(1000);

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("div-loading")));

            Thread.Sleep(1000);
        }

        public static void WaitUntilLoadingPlaceholderDisappears(IWebDriver driver, int secondtToWait = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondtToWait));

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("folderStoriesPlaceHolder")));

            Thread.Sleep(1000);
        }

        public static void WaitUntilCalculatingDisappears(IWebDriver driver, int secondtToWait = 3000)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondtToWait));

            Thread.Sleep(1000);

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//*[@id='div-calculating']/div/div[2]")));

            Thread.Sleep(1000);
        }

        public static void WaitBeforeAssert(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
    }
}
