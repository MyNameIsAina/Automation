using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

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
       * Returns select element 
       */
        public static SelectElement AsSelect(this IWebElement element) => new SelectElement(element);

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
        * Returns element when its found, using default 15 second timeout
        */
        public static ReadOnlyCollection<IWebElement> GetElements(this IWebDriver driver, By by) => GetElements(driver, by, TimeSpan.FromSeconds(15));

        /**
        * Returns elements when its found, using explisit timeout value
        */
        public static ReadOnlyCollection<IWebElement> GetElements(this IWebDriver driver, By by, TimeSpan timout)
        {
            var wait = new WebDriverWait(driver, timout);
            //waits untill element is found and then returns it
            return wait.Until(d => d.FindElements(by));
        }

        /**
        * Returns only visible element when its found, using explisit timeout value
        */
        public static IWebElement GetVisibleElement(this IWebDriver driver, By by, TimeSpan timout) {
            var wait = new WebDriverWait(driver, timout);
            //waits untill element is found and then returns it
            return wait.Until(d=>{
                var element = d.FindElement(by);
                //Short for of an if statement => If element is displayed, return it, else return null
                return element.Displayed ? element : null;
            });
        }

        /**
        * Returns only visible element when its found, using default 15 second timeout
        */
        public static IWebElement GetVisibleElement(this IWebDriver driver, By by) => GetVisibleElement(driver, by, TimeSpan.FromSeconds(15));

        /**
        * Returns only visible elements when its found, using explisit timeout value
        */
        public static ReadOnlyCollection<IWebElement> GetVisibleElements(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(d =>
            {
                var elements = d.FindElements(by).Where(i => i.Displayed).ToList();
                return new ReadOnlyCollection<IWebElement>(elements);
            });
        }

        /**
        * Returns only visible elements when its found, using default 15 second timeout
        */
        public static ReadOnlyCollection<IWebElement> GetVisibleElements(this IWebDriver driver, By by) => GetVisibleElements(driver, by, TimeSpan.FromSeconds(15));
        
        /**
        * Returns only enabled element when its found, using explisit timeout value
        */
        public static IWebElement GetEnabledElement(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);

            return wait.Until(d =>
            {
                var element = d.FindElement(by);
                //Short for of an if statement => If element is enabled, return it, else return null
                return element.Enabled ? element : null;
            });
        }

        /**
        * Returns only visible element when its found, using default 15 second timeout
        */
        public static IWebElement GetEnabledElement(this IWebDriver driver, By by) => GetEnabledElement(driver, by, TimeSpan.FromSeconds(15));

        /**
         * Returns only visible elements when its found, using explisit timeout value
         */
        public static ReadOnlyCollection<IWebElement> GetEnabledElements(this IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(d =>
            {
                var elements = d.FindElements(by).Where(i => i.Enabled).ToList();
                return new ReadOnlyCollection<IWebElement>(elements);
            });
        }

        /**
        * Returns only visible element when its found, using default 15 second timeout
        */
        public static ReadOnlyCollection<IWebElement> GetEnabledElements(this IWebDriver driver, By by) => GetEnabledElements(driver, by, TimeSpan.FromSeconds(15));

        /**
         * Scrolls to a point in the page
         */
        public static IWebDriver VerticalWindowScroll(this IWebDriver driver, int scrrollAMount)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scroll(0,{scrrollAMount})");

            return driver;
        }

        /**
         * Set the first action and return it
         */
        public static Actions Actions(this IWebElement element) 
        {
            //To get the driver from the element. This Interface comes from RemoteWebElement and this element implements a lot of interfaces including the IWrapsDriver
            var driver = ((IWrapsDriver)element).WrappedDriver;
            var actions = new Actions(driver);
            //we will return the first action
            return actions.MoveToElement(element);
        }

        /**
         *  Simpified click method
         */
        public static IWebElement ForceClick(this IWebElement element) 
        {
            //Extract the driver from the element
            var driver = ((IWrapsDriver)element).WrappedDriver;
            //Extract JS executor from the driver
            var executor = (IJavaScriptExecutor)driver;
            //Executes js version of the click functionality, whitch is simpler than the Selenium one
            executor.ExecuteScript("arguments[0].click()", element);
            return element;
        }

        /**
         * Send keys with a set inteval between the pressed keys
         * this is an overload for the exiting method that exist under IWebElement
         */
        public static IWebElement SendKeys(this IWebElement element, string text, int interval) 
        {
            foreach (var @char in text)
            {
                element.SendKeys($"{@char}");
                Thread.Sleep(interval);
            }
            return element;
        }

        /**
         * Clears the element suing backspace with 50 ms interval inbetween
         */
        public static IWebElement ForceClear(this IWebElement element) 
        {
            var value = element.GetAttribute("value");
            element.SendKeys(Keys.End);
            for (int i = 0; i < value.Length; i++)
            {
                element.SendKeys(Keys.Backspace);
                Thread.Sleep(50);
            }
            return element;
        }

        /**
         * Submit form by Zero based inxded value
         */
        public static IWebDriver SubmitForm(this IWebDriver driver, int index) 
        {
            var executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript($"document.forms[{index}].submit();");
            return driver;
        }

        /**
        * Submit form by using the ID
        */
        public static IWebDriver SubmitForm(this IWebDriver driver, string id)
        {
            var executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript($"document.forms[\"{id}\"].submit();");
            return driver;
        }
    }
}
