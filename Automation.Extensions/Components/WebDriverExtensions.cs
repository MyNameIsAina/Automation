using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Extensions.Components
{
    public static class WebDriverExtensions
    {
        /**
         * Naviagtes to the given url
         */
        public static IWebDriver GoToUrl(this IWebDriver driver, string url) 
        {
            //actions
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //fluent
            return driver;
        }

        /**
         * Returns element when its found, using default 15 second timeout
         */
        public static IWebElement GetElement(this IWebDriver driver, By by) => GetElement(driver, by, TimeSpan.FromSeconds(15));

        /**
         * Returns element when its found, using explisit timeout value
         */
        public static IWebElement GetElement(this IWebDriver driver, By by, TimeSpan timout)
        {
            var wait = new WebDriverWait(driver, timout);
            //waits untill element is found and then returns it
            return wait.Until(d => d.FindElement(by));
        }

        /**
        * Returns select element 
        */
        public static SelectElement AsSelect(this IWebElement element) => new SelectElement(element);
     
    }
}
