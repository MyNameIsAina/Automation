using Automation.Extensions.Contracts;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Extensions.Components
{
    public class WebDriverFactory
    {
        private readonly DriverParams driverParams;
        public WebDriverFactory(string driverParamsJson)
            : this(LoadParams(driverParamsJson)) { }

        public WebDriverFactory(DriverParams driverParams)
        {
            this.driverParams = driverParams;
            if (string.IsNullOrEmpty(driverParams.Binaries) || driverParams.Binaries == ".")
            {
                driverParams.Binaries = Environment.CurrentDirectory;
            }
        }

        /*
         * Returns a web driver based on what type of driver Source (Remote/Local Web) has been passed in the driverparams pbject
         * 
         * Returns Web Driver instance
         */
        public IWebDriver Get()
        {
            if (!string.Equals(driverParams.Source, "REMOTE", StringComparison.OrdinalIgnoreCase))
            {
                return GetDriver();
            }
            return GetRemoteDriver();
        }

        // Local Web-drivers
        private IWebDriver GetChrome()
            => new ChromeDriver(driverParams.Binaries);
        private IWebDriver GetFirefox()
            => new FirefoxDriver(driverParams.Binaries);
        private IWebDriver GetInternetExplorer()
            => new InternetExplorerDriver(driverParams.Binaries, new InternetExplorerOptions {}, TimeSpan.FromMinutes(2));
        private IWebDriver GetEdge() 
            => new EdgeDriver(driverParams.Binaries);

        // Web Driver switcher
        private IWebDriver GetDriver() 
        {
            switch (driverParams.Driver.ToUpper())
            {
                case "CHROME": return GetChrome();
                case "FIREFOX": return GetFirefox();
                case "IE": return GetInternetExplorer();
                case "EDGE": return GetEdge();
                default: return GetChrome();
            }
        }

        //Remote Web-Drivers
        private IWebDriver GetReoteChrome() => new RemoteWebDriver(
            new Uri(driverParams.Binaries), new ChromeOptions()
            );
        private IWebDriver GetRemoteFirefox() => new RemoteWebDriver(
            new Uri(driverParams.Binaries), new FirefoxOptions()
            );
        private IWebDriver GetRemoteInternetExplorer() => new RemoteWebDriver(
            new Uri(driverParams.Binaries), new InternetExplorerOptions()
            );
        private IWebDriver GetRemoteEdge() => new RemoteWebDriver(
            new Uri(driverParams.Binaries), new EdgeOptions()
            );

        // Remote Driver switcher
        private IWebDriver GetRemoteDriver()
        {
            switch (driverParams.Driver.ToUpper())
            {
                case "CHROME": return GetReoteChrome();
                case "FIREFOX": return GetRemoteFirefox();
                case "IE": return GetRemoteInternetExplorer();
                case "EDGE": return GetRemoteEdge();
                default: return GetReoteChrome();
            }
        }

        // Loads Json into DriverParams object
        private static DriverParams LoadParams(string driverParamsJson)
        {
            if (string.IsNullOrEmpty(driverParamsJson))
            {
                return new DriverParams { Source = "local", Driver = "Chrome", Binaries = "." };
            }
            return JsonConvert.DeserializeObject<DriverParams>(driverParamsJson);
        }
    }
}
