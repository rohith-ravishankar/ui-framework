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
    public class CarDetails
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly AddNewCar _addNewCar;

        private readonly string _filledFieldName = "//label[contains(text(),'filledFieldName')]/following-sibling::div";

        private readonly By _editDamages = By.XPath("//div[contains(@id,'car_details_damages')]/following-sibling::div//h5");
        private readonly By _engineDamage = By.Id("mui-component-select-car.engine_damage");
        private readonly By _transmissionDamage = By.Id("mui-component-select-car.transmission_damage");

        private readonly By _editHistoricalAttributes = By.XPath("//div[contains(@id,'car_details_historical_attributes')]/following-sibling::div//h5");
        private readonly By _roadworthy = By.Id("mui-component-select-car.roadworthy");
        private readonly By _options = By.XPath("//li[@role='option']");
        private readonly By _confirmDifference = By.XPath("//button/span[contains(text(),'Confirm difference')]");
        private readonly By _upholsteryWarning = By.XPath("//div[contains(@id,'upholstery_type')]/../../preceding-sibling::div");

        private readonly By _editEquipment = By.XPath("//div[contains(@id,'car_details_equipment')]/following-sibling::div//h5");
        private readonly By _upholsteryType = By.Id("mui-component-select-car.upholstery_type");

        private readonly By _editPreDamages = By.XPath("//div[contains(@id,'car_details_pre_damages')]/following-sibling::div//h5");
        private readonly By _editSpecialEquipment = By.XPath("//div[contains(@id,'car_details_special_equipment')]/following-sibling::div//h5");

        public CarDetails(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            _addNewCar = new AddNewCar(driver);
        }

        public void ValidateFieldEntries(string filledFieldName)
        {
            string fieldEntry = _filledFieldName.Replace("filledFieldName", filledFieldName);
            switch (filledFieldName)
            {
                case "VIN":
                    Assert.True(_driver.FindElement(By.XPath(fieldEntry)).Text.Equals(_addNewCar.vin));
                    break;
                case "Mileage":
                    Assert.True(_driver.FindElement(By.XPath(fieldEntry)).Text.Equals(_addNewCar.mileage));
                    break;
                case "Registration date":
                    Assert.True(_driver.FindElement(By.XPath(fieldEntry)).Text.Equals(_addNewCar.registrationDate));
                    break;
            }
            Assert.True(_driver.FindElement(By.XPath(fieldEntry)).Text.Equals(_addNewCar.vin));
        }

        //Entering data functions can be combined and made simpler
        public void EnterDamageDetails(string value)
        {
            this.ScrollToFindElement(_driver.FindElement(_editEquipment));
            _driver.FindElement(_editDamages).Click();
            Thread.Sleep(1000);
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_engineDamage));
            _driver.FindElement(_engineDamage).Click();
            this.SelectOption(_options, value);
            _driver.FindElement(_transmissionDamage).Click();
            this.SelectOption(_options, value);
        }

        public void EnterHistoricalDetails(string value)
        {
            this.ScrollToFindElement(_driver.FindElement(_editPreDamages));
            _driver.FindElement(_editHistoricalAttributes).Click();
            Thread.Sleep(1000);
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_roadworthy));
            _driver.FindElement(_roadworthy).Click();
            this.SelectOption(_options, value);
        }

        public void EnterEquipments(string value)
        {
            this.ScrollToFindElement(_driver.FindElement(_editSpecialEquipment));
            _driver.FindElement(_editEquipment).Click();
            Thread.Sleep(1000);
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_upholsteryType));
            _driver.FindElement(_upholsteryType).Click();
            this.SelectOption(_options, value);
            this.HoverOver(_upholsteryWarning);
            _driver.FindElement(_confirmDifference).Click();
        }

        //To be moved to common functions
        public void SaveDetail()
        {
            Actions action = new Actions(_driver);
            action.KeyDown(OpenQA.Selenium.Keys.Control).SendKeys("s").KeyUp(OpenQA.Selenium.Keys.Control);
            action.Build();
            action.Perform();
        }

        public void WaitForSavingData()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//body[@class='hide-scroll']")));
        }

        //To be moved to common functions
        public void ScrollToFindElement(IWebElement element)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public void SelectOption(By by, string option)
        {
            foreach (var element in _driver.FindElements(by))
            {
                if (element.Text.Contains(option))
                {
                    element.Click();
                    break;
                }
            }
        }

        //To be moved to common functions
        public void HoverOver(By by)
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(_driver.FindElement(by)).Click().Build().Perform();
        }
    }
}
