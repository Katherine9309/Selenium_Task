using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.PageObject.Pages
{
    public class ShippingPage
    {
        private By _addressInput = By.Name("street[0]");
        private By _cityInput = By.Name("city");
        private By _postCodeInput = By.Name("postcode");
        private By _countryInput = By.Name("country_id");
        private By _phoneNumberInput = By.Name("telephone");
        private By _nextButton = By.CssSelector("*[data-role='opc-continue']");
        private By _shippingMethodRadio = By.ClassName("radio");
        private By _newAddressButton = By.XPath("//*[contains(@data-bind,'New Address')]");
        private By _saveAddressRatio = By.Id("shipping-save-in-address-book");
        private By _shipHereButton = By.ClassName("action-save-address");
        private By _modalForm = By.Id("modal-content-10");
        protected readonly IWebDriver _driver;


        public ShippingPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void FillForm() {
     

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(_nextButton));
            IWebElement newAddressButton = _driver.FindElement(_newAddressButton);

            if (newAddressButton.Displayed)
            {
                newAddressButton.Click();
               // Thread.Sleep(3000);
                wait.Until((driver) => driver.FindElement(_modalForm));

                wait.Until(ExpectedConditions.ElementToBeClickable(_modalForm));
                IWebElement modalForm = _driver.FindElement(_modalForm);
                Actions action = new Actions(_driver);
                action.MoveToElement(modalForm);
                action.Perform();

                FillPersonalData();
                IWebElement saveAddressRatio = _driver.FindElement(_saveAddressRatio);
                saveAddressRatio.Click();
                IWebElement shipHereButton = _driver.FindElement(_shipHereButton);
                shipHereButton.Click();
            }
            else {
                FillPersonalData();
            }

           // Thread.Sleep(3000);
            wait.Until(driver =>
            {
               
                //wait.Until(ExpectedConditions.ElementToBeClickable(_shippingMethodRadio));

                wait.Until((driver) => driver.FindElement(_shippingMethodRadio).Displayed);
                IWebElement shippingMethodRadio = _driver.FindElement(_shippingMethodRadio);
                return shippingMethodRadio.Selected && shippingMethodRadio.GetAttribute("checked") == "true";
            });

            IWebElement shippingMethodRadio = _driver.FindElement(_shippingMethodRadio);
            shippingMethodRadio.Click();

            IWebElement nextButton = _driver.FindElement(_nextButton);
            nextButton.Click();

        }

        public void FillPersonalData() {
           
            IWebElement addresInput = _driver.FindElement(_addressInput);
            addresInput.SendKeys("Spur Road");

            IWebElement cityInput = _driver.FindElement(_cityInput);
            cityInput.SendKeys("London");

            IWebElement postCodeInput = _driver.FindElement(_postCodeInput);
            postCodeInput.SendKeys("SW1A 1AA");

            IWebElement countryInput = _driver.FindElement(_countryInput);
            countryInput.Click();
            var targetCountry = "United Kingdom";

            IWebElement countryOption = _driver.FindElement(By.XPath($"//option[text()='{targetCountry}']"));
            countryOption.Click();

            RandomPhoneNumber();


        }

        public void RandomPhoneNumber() {
            IWebElement phoneNumberInput = _driver.FindElement(_phoneNumberInput);
            Random ramdon = new Random();
            string phoneNumber = ramdon.Next(100000, 999999).ToString();
            phoneNumberInput.SendKeys(phoneNumber);
        }
    }
}
