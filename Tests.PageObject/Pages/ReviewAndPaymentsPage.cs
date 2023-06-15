using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObject.Pages
{
    public class ReviewAndPaymentsPage
    {
        private By _placeOrderButton = By.CssSelector(".checkout > span");
        //private By _placeOrderButton = By.XPath("//span[contains(.,'Place Order')]");



        protected readonly IWebDriver _driver;
        public ReviewAndPaymentsPage(IWebDriver driver)
        {
            _driver = driver;   
        }

        public void PlaceOrder() {

       

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_placeOrderButton));
            //wait.Until((driver) => driver.FindElement(_placeOrderButton))
            wait.Until(ExpectedConditions.ElementIsVisible(_placeOrderButton));
            var element = _driver.FindElement(_placeOrderButton);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element);
            actions.Click(element);
            actions.Perform();
       
        }


    }
}
