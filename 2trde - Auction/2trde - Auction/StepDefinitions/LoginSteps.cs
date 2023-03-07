using _2trde___Auction.PageFunctions;
using _2trde___Auction.Utils;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using TechTalk.SpecFlow;

namespace _2trde___Auction.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver _driver;
        private readonly Login _login;
        private readonly Dashboard _dashboard;
        private readonly AddNewCar _addNewCar;
        private readonly string _testUrl;
        private readonly SiteDetails _testDetails;

        public LoginSteps(IConfiguration configuration, IWebDriver driver)
        {
            _driver = driver;
            _testUrl = configuration.GetSection("URL").Get<string>();
            _testDetails = configuration.GetSection("UserDetails").Get<SiteDetails>();
            _login = new Login(driver);
            _dashboard = new Dashboard(driver);
            _addNewCar = new AddNewCar(driver);
        }

        [Given(@"I launch the application")]
        public void GivenILaunchTheApplication()
        {
            _login.LaunchUrl(_testUrl);
        }

        [Given(@"I enter '(.*)' login details")]
        public void WhenIEnterLoginDetails(string condition)
        {
            switch (condition)
            {
                case "correct":
                    _login.EnterLoginDetails(_testDetails.email, _testDetails.password);
                    break;
                case "incorrect":
                    _login.EnterLoginDetails(_testDetails.email, _testDetails.incorrectPassword);
                    break;
            }
        }

        [When(@"I click on '(.*)'")]
        public void WhenIClickOn(string name)
        {
            switch (name)
            {
                case "Login":
                    _login.ClickLoginButton();
                    break;
                case "Forgot Password":
                    _login.ClickForgotPasswordLink();
                    break;
                case "Start enrichment":
                    _addNewCar.ClickStartEnrichment();
                    break;
                case "My Auction":
                    //_dashboard.WaitForEnrichmentCompletion();
                    _dashboard.ClickMyAuction();
                    break;
                case "auction details":
                    _dashboard.ClickMyAuctionTab(name);
                    break;
                default:
                    _dashboard.ClickOnLink(name);
                    break;
            }
            Thread.Sleep(2000);
        }

        [When(@"I wait for enrichment completion")]
        public void WhenIWaitForEnrichmentCompletion()
        {
            _dashboard.WaitForEnrichmentCompletion();
        }

        [Then(@"I am on '(.*)' page")]
        public void ThenIAmOnPage(string pageName)
        {
            switch (pageName)
            {
                case "Dashboard":
                    _dashboard.ValidateDashboard();
                    break;
                case "Reset Password":
                    _login.ValidateResetPassword();
                    break;
            }
        }

        [Then(@"I see Incorrect Error message")]
        public void ThenISeeIncorrectErrorMessage()
        {
            _login.ValidateIncorrectErrorMessage("Die Kombination aus Benutzername und Passwort ist ungültig.");
        }
    }
}
