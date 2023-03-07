using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace _2trde___Auction.PageFunctions
{
    public class Dashboard
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _dashboardTitle = By.Id("page-title-wrapper");
        private readonly By _myCreatedAuction = By.XPath("//div[contains(@class,'container')][1]/div/div/div[1]//a[contains(@href,'auctions/')]");
        private string _auctionDetailsTab = "//button//span[contains(text(),'tabName')]";

        public Dashboard(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(180));
        }

        public void ValidateDashboard()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_dashboardTitle));
            Assert.True(_driver.FindElement(_dashboardTitle).Text.Contains("Dashboard"));
        }

        public void ClickOnLink(string name)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText(name)));
            _driver.FindElement(By.LinkText(name)).Click();
        }

        public void WaitForEnrichmentCompletion()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(),'Enrichment in progress')]")));
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[contains(text(),'Enrichment in progress')]")));
        }

        public void ClickMyAuction()
        {
            _driver.FindElement(_myCreatedAuction).Click();
        }

        public void ClickMyAuctionTab(string tabName)
        {
            _driver.FindElement(By.XPath(_auctionDetailsTab.Replace("tabName", tabName))).Click();
        }
    }
}
