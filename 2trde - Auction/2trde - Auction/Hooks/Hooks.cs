using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace FantasyPremierLeague.Hooks
{
    [Binding]
    public class Hooks
    {
        private IWebDriver driver;
        private readonly IConfiguration _configuration;
        private readonly IObjectContainer objectContainer;

        //Load site data and build configuration
        public Hooks(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
            _configuration = new ConfigurationBuilder()
                            .AddJsonFile("SiteDetails.json", optional:false, reloadOnChange:true)
                            .AddEnvironmentVariables()
                            .Build();
        }

        //Create driver and register instance
        [BeforeScenario]
        public void StartUp()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("no-sandbox");
            chromeOptions.AddArguments("start-maximized");
            chromeOptions.AddArguments("disable-gpu");
            chromeOptions.AddArguments("disable-dev-shm-usage");
            driver = new ChromeDriver(chromeOptions);

            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
            objectContainer.RegisterInstanceAs(_configuration);
        }

        //Quit and dispose driver
        [AfterScenario]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
            else throw new System.Exception("There was an error disposing the driver");
        }
    }
}
