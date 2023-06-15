using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObject.Pages
{
    public class SuccessPurchase
    {
        protected readonly IWebDriver _driver;
        public int numberOrder;
        private By _orderNumber = By.ClassName("order-number");
        private By _continueshoppingButton = By.ClassName("continue");
        public SuccessPurchase(IWebDriver driver) {

            _driver = driver;
        }

        public string continueShopping() {

         
            IWebElement orderNumber = _driver.FindElement(_orderNumber);
            var numberOrder = orderNumber.Text;
            IWebElement continueshoppingButton = _driver.FindElement(_continueshoppingButton);
            continueshoppingButton.Click();
            return numberOrder;
        }
    }
}
