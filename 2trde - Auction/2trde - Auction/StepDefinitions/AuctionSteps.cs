using _2trde___Auction.PageFunctions;
using _2trde___Auction.Utils;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace _2trde___Auction.StepDefinitions
{
    [Binding]
    public class AuctionSteps
    {
        private readonly IWebDriver _driver;
        private readonly AddNewCar _addNewCar;
        private readonly CarDetails _carDetails;
        private readonly AuctionDetails _auctionDetails;

        public AuctionSteps(IConfiguration configuration, IWebDriver driver)
        {
            _driver = driver;
            _addNewCar = new AddNewCar(driver);
            _carDetails = new CarDetails(driver);
            _auctionDetails = new AuctionDetails(driver);
        }

        [When(@"I upload car details pdf")]
        public void WhenIUploadCarDetailsPdf()
        {
            _addNewCar.NewCarFileUpload();
        }

        [When(@"I validate formats of fields")]
        [Then(@"I validate formats of fields")]
        public void WhenIValidateFormatsOfFields()
        {
            _addNewCar.ValidateNewCarEntryField("VIN");
            _addNewCar.ValidateNewCarEntryField("Mileage");
            _addNewCar.ValidateNewCarEntryField("Registration date");
        }

        [When(@"I validate all fields are filled")]
        [Then(@"I validate all fields are filled")]
        public void WhenIValidateFieldsAreFilled()
        {
            _addNewCar.ValidateFilledFields();
        }

        [When(@"I validate the filled values")]
        [Then(@"I validate the filled values")]
        public void WhenIValidateFilledValues()
        {
            _carDetails.ValidateFieldEntries("VIN");
            _carDetails.ValidateFieldEntries("Mileage");
            _carDetails.ValidateFieldEntries("Registration date");
        }

        [When(@"I have '(.*)' '(.*)'")]
        public void WhenIHave(string value, string field)
        {
            switch (field)
            {
                case "Engine damage":
                    _carDetails.EnterDamageDetails(value);
                    break;
                case "Upholster type":
                    _carDetails.EnterEquipments(value);
                    break;
                case "Roadworthy":
                    _carDetails.EnterHistoricalDetails(value);
                    break;
            }
            _carDetails.SaveDetail();
            _carDetails.WaitForSavingData();
        }

        [When(@"I enter '(.*)'")]
        public void WhenIEnter(string field)
        {
            _auctionDetails.ClickEditAll();
            switch (field)
            {
                case "Auction end time":
                    _auctionDetails.EnterAuctionDate();
                    break;
                case "Vehicle location":
                    //_carDetails.EnterEquipments(value);
                    break;
            }
            _auctionDetails.SaveDetail();
            //_carDetails.WaitForSavingData();
        }
    }
}
