using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace _2trde___Auction.PageFunctions
{
    public class Login
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _emailId = By.XPath("//input[@name='email']");
        private readonly By _password = By.XPath("//input[@name='password']");
        private readonly By _loginButton = By.XPath("//button[@type='submit']");
        private readonly By _forgotPassword = By.XPath("//a[contains(@href,'/request')]");
        private readonly By _incorrectErrorMessage = By.XPath("//div[@class='MuiAlert-message']");
        private readonly By _resetPassword = By.XPath("//div[@id='root']//h1");

        public Login(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        public void LaunchUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void EnterLoginDetails(string emailId, string password)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_emailId));
            _driver.FindElement(_emailId).SendKeys(emailId);
            _driver.FindElement(_password).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            _driver.FindElement(_loginButton).Click();
        }

        public void ClickForgotPasswordLink()
        {
            _driver.FindElement(_forgotPassword).Click();
        }

        public void ValidateIncorrectErrorMessage(string errorMessage)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_incorrectErrorMessage));
            _driver.FindElement(_incorrectErrorMessage).Text.Contains(errorMessage);
        }

        public void ValidateResetPassword()
        {
            //_wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_resetPassword));
            Assert.True(_driver.Url.Contains("/request_password"));
            Assert.True(_driver.FindElement(_resetPassword).Text.Contains("Passwort vergessen"));
        }
    }
}
