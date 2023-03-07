using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;

namespace _2trde___Auction.PageFunctions
{
    public class AuctionDetails
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _auctionDate = By.XPath("//input[@name='end_ts']");
        private readonly By _editAllButton = By.XPath("//div[contains(@class,'pull-right')]/button[contains(@class,'Secondary')]");
        private readonly By _saveButton = By.XPath("//div[contains(@class,'pull-right')]/button[contains(@class,'Primary')]");

        public AuctionDetails(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        public void EnterAuctionDate()
        {
            string auctionDateBuilder = SelectWeekday().ToString("dd/mm/yyyy") + " " + SelectWeekday().AddMinutes(60).ToString("HH:mm");
            _driver.FindElement(_auctionDate).SendKeys(auctionDateBuilder);
        }

        //To be moved to common functions
        public DateTime SelectWeekday()
        {
            Random random = new Random();
            DateTime date = DateTime.Today.AddDays(random.Next(10,20));
            string day = date.ToString("dddd");
            if (day.Equals("Saturday") || day.Equals("Sunday"))
            {
                SelectWeekday();
            }
            return date;
        }

        public void SaveDetail()
        {
            _driver.FindElement(_saveButton).Click();
            Thread.Sleep(2000);
        }

        public void ClickEditAll()
        {
            _driver.FindElement(_editAllButton).Click();
            Thread.Sleep(2000);
        }
    }
}
