using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;

namespace _2trde___Auction.PageFunctions
{
    public class AddNewCar
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private Regex pattern;

        private readonly By _fileUpload = By.ClassName("input-file");
        private readonly By _fieldName = By.XPath("//div[contains(@class,'form-label')]");
        private readonly By _startEnrichmentButton = By.XPath("//div[contains(@class,'pull-right')]/button[contains(@class,'primary')]");
        private readonly By _cancelButton = By.XPath("//button[contains(text(),'Start enrichment')]");
        private readonly string _fieldValue = "//div[contains(text(),'fieldName')]/following-sibling::div//input";

        //Checking storing and pre-filled data
        public string vin;
        public string registrationDate;
        public string mileage;
        public string enginePower;

        public AddNewCar(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        //Uploads PDF and pre-fills details
        public void NewCarFileUpload()
        {
            _driver.FindElement(_fileUpload).SendKeys("C:/Users/rohit/source/repos/2trde - Auction/2trde - Auction/TestData/fa2bd7231ab825c9dbe3cf214eb2ef830e0a4698.pdf");
            do
            {
                if (this.isElementPresent(By.XPath("//div[@role='document']")).Equals(true))
                    break;
                else
                {
                    try
                    {
                        //Unnecessary - added due to bug on uploading
                        _driver.FindElement(By.ClassName("dropbox")).Click();
                    }
                    catch { }
                } 
            } while (this.isElementPresent(By.XPath("//div[@role='document']")).Equals(false));
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@role='document']")));
            Thread.Sleep(2000);
        }

        //To be moved to common functions
        public bool isElementPresent(By locatorKey)
        {
            try
            {
                _driver.FindElement(locatorKey);
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        //Validate formats - can be refined
        public void ValidateNewCarEntryField(string fieldName)
        {
            switch (fieldName)
            {
                case "VIN":
                    pattern = new Regex(@"/^\w+$/");
                    vin = _driver.FindElement(By.XPath(_fieldValue.Replace("fieldName", fieldName))).GetAttribute("value");
                    pattern.Matches(vin); 
                    break;
                case "Mileage":
                    pattern = new Regex(@"^\d$");
                    mileage = _driver.FindElement(By.XPath(_fieldValue.Replace("fieldName", fieldName))).GetAttribute("value");
                    pattern.Matches(mileage);
                    break;
                case "Registration date":
                    //pattern = new Regex(@"^\d$");
                    registrationDate = _driver.FindElement(By.XPath(_fieldValue.Replace("fieldName", fieldName))).GetAttribute("value");
                    //pattern.Matches(registrationDate);
                    break;
            }
        }

        //Null or Empty check for the pre-filled fields from PDF
        public void ValidateFilledFields()
        {
            foreach(var element in _driver.FindElements(_fieldName))
            {
                string fieldEntry = _fieldValue.Replace("fieldName", element.Text.Replace("*", ""));
                Assert.NotNull(_driver.FindElement(By.XPath(fieldEntry)).Text);
                Assert.IsNotEmpty(_driver.FindElement(By.XPath(fieldEntry)).Text);
            }
            Assert.True(_driver.FindElement(_startEnrichmentButton).Enabled);
        }

        public void ClickStartEnrichment()
        {
            _driver.FindElement(_startEnrichmentButton).Click();
            Thread.Sleep(2000);
        }

        public void ClickCancel()
        {
            _driver.FindElement(_cancelButton).Click();
        }
    }
}
