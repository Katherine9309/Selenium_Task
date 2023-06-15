using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace TestProject_UI_tests.Pages
{
    public class BasePage
    {
        private By _gearCategoryButtonLocator = By.Id("ui-id-6");
        private By _watchesCategoryButtonLocator = By.Id("ui-id-27");
        private By _shoppingCarLocator = By.ClassName("showcart");
        private By _proceedCheckoutButtonLocator = By.Id("top-cart-btn-checkout");
        private By _shoppingNumberCarLocator = By.CssSelector("span.counter-number");
        private By _alertMessageLocator = By.ClassName("messages");


        protected readonly IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public ProductPage OpenGearCategoryPage()
        {
            var element = _driver.FindElement(_gearCategoryButtonLocator);
            element.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.Title.StartsWith("Gear"));

            return new ProductPage(_driver);
        }
        public ProductPage OpenGearWatchesSection()
        {
            var element = _driver.FindElement(_gearCategoryButtonLocator);
            Actions action = new Actions(_driver);
            action.MoveToElement(element);
            action.Perform();
            TimeSpan.FromSeconds(4);
            var watches = _driver.FindElement(_watchesCategoryButtonLocator);
            watches.Click();
        
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            wait.Until((driver) => driver.Title.StartsWith("Watches"));

            return new ProductPage(_driver);
        }

        public ProductPage CheckOut ()
        {
          
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));           
            var element = _driver.FindElement(_shoppingCarLocator);
            Actions action = new Actions(_driver);
            action.MoveToElement(element);
            action.Click();
            action.Perform();
            TimeSpan.FromSeconds(5);
            var checkout = _driver.FindElement(_proceedCheckoutButtonLocator);
            checkout.Click();
            //wait.Until((driver) => driver.Title.StartsWith("Checkout"));
            
            return new ProductPage(_driver);
        }

        public int CkeckNumerItems() {
            GetAlertMessage();
            var element = _driver.FindElement(_shoppingNumberCarLocator);
            int value = int.Parse(element.Text);
            return value;
        }
        public string GetAlertMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until((driver) => driver.FindElement(_alertMessageLocator).Text.StartsWith("You added "));
            IWebElement alert = _driver.FindElement(_alertMessageLocator);
            return alert.Text;
        }
    }
}
