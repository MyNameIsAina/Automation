using Automation.Extensions.Components;
using Automation.Extensions.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automation.Testing
{
    [TestClass]
    public class SeleniumSamples
    {
        // Constants
        const string URL = "https://gravitymvctestapplication.azurewebsites.net/";
        const string DRIVER_PATH = "C:\\Users\\Aina\\Desktop\\Main\\Programmesana\\Selenium\\web-drivers";
        const string CHROME = "chrome";
        const string FIREFOX = "firefox";
        const string IE = "ie";
        const string EDGE = "edge";

        /**
         * 
        [TestMethod]
        public void WebDriverSamples() {
            IWebDriver driver = new ChromeDriver();
            Thread.Sleep(1000);
            driver.Dispose();

            driver = new FirefoxDriver();
            Thread.Sleep(1000);
            driver.Dispose();

            driver = new InternetExplorerDriver();
            Thread.Sleep(1000);
            driver.Dispose();
        }
        
        [TestMethod]
        public void WebElementSamples() {
            var driver = new ChromeDriver();
            // Not always mut most of the time will makde sure that page has been loaded before we start to interact with the page
            // And maximazes the window
            driver.Manage().Window.Maximize();
            // Navigate to url
            driver.Navigate().GoToUrl(URL);
            // Find specific element and click on it
            driver.FindElement(By.XPath("//a[.='Students']")).Click();
            Thread.Sleep(2000);
            driver.Dispose();
        }

        [TestMethod]
        public void SelectElementSamples()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
            driver.FindElement(By.XPath("//a[.='Courses']")).Click();
            // Find the select elemenet, we save it in a value so that we can pass that value for 
            var element = driver.FindElement(By.XPath("//select[@id='SelectedDepartment']"));
            // Initialize SelectElement
            var selectElement = new SelectElement(element);
            // Using SelectElement we can select specific value (can select using Text, Index or Value)
            selectElement.SelectByValue("3");
            Thread.Sleep(2000);
            driver.Dispose();
        }
        */
        /**
        [TestMethod]
        public void WebDriverFactorySample()
        { 
            var driver = new WebDriverFactory(new DriverParams { Driver = EDGE, Binaries = DRIVER_PATH }).Get();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
            driver.FindElement(By.XPath("//a[.='Students']")).Click();
            Thread.Sleep(2000);
            driver.Dispose();
        }

        [TestMethod]
        public void GoToUrlSapmle()
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.Dispose();
        }

        [TestMethod]
        public void GetElementSample() 
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = EDGE, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetElement(By.XPath("//a[.='Students']")).Click();
            // driver.GetElement(By.XPath("//a[.='Students']"), TimeSpan.FromSeconds(20)).Click();
            Thread.Sleep(2000);
            driver.Dispose();
        }

        [TestMethod]
        public void SelectElementSample2()
        {
            var driver = new ChromeDriver();
            driver.GoToUrl(URL);
            driver.GetElement(By.XPath("//a[.='Students']")).Click();
            driver.GetElement(By.XPath("//select[@id='SelectedDepartment']")).AsSelect().SelectByValue("3");
            driver.Dispose();
        }

        [TestMethod]
        public void GetElementsSample()
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            var elements = driver.GetElements(By.XPath("//li[contains(@class, 'nav-item')]/a"));
            driver.Dispose();
        }
        */
        [TestMethod]
        public void GetVisibleElement() 
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetVisibleElement(By.XPath("//a[.='Students']")).Click();
            //driver.GetVisibleElement(By.XPath("//a[.='Students']"), TimeSpan.FromSeconds(20)).Click();
            driver.Dispose();
        }

        [TestMethod]
        public void GetVisibleElements()
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetVisibleElements(By.XPath("//a[.='Students']"));
            //driver.GetVisibleElements(By.XPath("//a[.='Students']"), TimeSpan.FromSeconds(20));
            driver.Dispose();
        }

        [TestMethod]
        public void GetEnabledElement() 
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetElement(By.XPath("//a[.='Students']")).Click();
            driver.GetEnabledElement(By.XPath("//input[@id='SearchString']")).SendKeys("Hello Its a test ");
            //driver.GetEnabledElements(By.XPath("//a[.='Students']"), TimeSpan.FromSeconds(20));
            driver.Dispose();
        }

        [TestMethod]
        public void GetEnabledElements()
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetVisibleElements(By.XPath("//a[.='Students']"));
            //driver.GetVisibleElements(By.XPath("//a[.='Students']"), TimeSpan.FromSeconds(20));
            driver.Dispose();
        }

        [TestMethod]
        public void VerticalWindowScrollSample()
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.Manage().Window.Size = new Size(100, 500);
            driver.VerticalWindowScroll(200);
            driver.Dispose();
        }

        [TestMethod]
        public void ActionSample() 
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetElement(By.XPath("//a[.='Students']")).Actions().Click().Build().Perform();
            driver.Dispose();
        }

        [TestMethod]
        public void ForcedClickSample()
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetElement(By.XPath("//a[.='Students']")).ForceClick();
            driver.Dispose();
        }

        [TestMethod]
        public void SendKeysWithInterval() 
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetElement(By.XPath("//a[.='Students']")).Click();
            driver.GetEnabledElement(By.XPath("//input[@id='SearchString']")).SendKeys("Hello Its a test ", 100);
            driver.Dispose();
        }

        [TestMethod]
        public void ForceClearSample() 
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetElement(By.XPath("//a[.='Students']")).Click();
            var element = driver.GetEnabledElement(By.XPath("//input[@id='SearchString']"));
            element.SendKeys("Hello Its a test ", 100).SendKeys(Keys.Home);
            element.ForceClear();

            driver.Dispose();

        }

        [TestMethod]
        public void SubmitFormSample()
        {
            var driver = new WebDriverFactory(new DriverParams { Driver = CHROME, Binaries = DRIVER_PATH }).Get();
            driver.GoToUrl(URL);
            driver.GetElement(By.XPath("//a[.='Students']")).Click();
            var element = driver.GetEnabledElement(By.XPath("//input[@id='SearchString']"));
            element.SendKeys("Alexander", 100);
            //var formElement = driver.GetElement(By.XPath("//form[@action='/Student']"));
            //first and only form in the page, but in real life should find the form element first

            // dont know how to get the index of an element though
            driver.SubmitForm(0);
            driver.Dispose();

        }
    }
}
