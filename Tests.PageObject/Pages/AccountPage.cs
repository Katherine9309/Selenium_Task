using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_UI_tests.Pages;

namespace Tests.PageObject.Pages
{
    public class AccountPage : BasePage
    {
        private By _myOrderMenuOption = By.LinkText("My Orders");

        private By _orderNumber;
        private By _detailsOrder;

        private By _productName = By.CssSelector(".col>.product");
        private By _subTotal = By.CssSelector("*[data-th='Subtotal']>.price");
        private By _shipping = By.CssSelector("*[data-th='Shipping & Handling']>.price");
        private By _total = By.XPath("//*[@data-th='Grand Total']/descendant::*[@class='price']");
        public AccountPage(IWebDriver driver, string value) : base(driver) {
            _orderNumber = By.XPath($"//td[@class='col id' and text()='{value}']");
            _detailsOrder = By.XPath($"//td[@class='col id' and text()='{value}']/following-sibling::td[@class='col actions']//a[@class='action view']");
        }

        public AccountPage(IWebDriver driver):base(driver) { 
        }
        public void CheckMyOrders() {
            IWebElement myOrderMenuOption = _driver.FindElement(_myOrderMenuOption);
            myOrderMenuOption.Click();
        }

        public void viewOrder() {
            IWebElement myOrderNumber = _driver.FindElement(_orderNumber);
            IWebElement detailsOrder = _driver.FindElement(_detailsOrder); 
            detailsOrder.Click();
        }

        public IEnumerable<string> viewDetails()
        {
            List<string> details = new List<string>();
           
            // get the product name
            IWebElement productName = _driver.FindElement(_productName);
            details.Add(productName.Text);
            //get the subtotal
            IWebElement subTotal = _driver.FindElement(_subTotal);
            details.Add(subTotal.Text);
            //get shipping&handing
            IWebElement shipping = _driver.FindElement(_shipping);
            details.Add(shipping.Text);
            //grand total
            IWebElement Total = _driver.FindElement(_total);
            details.Add(Total.Text);

            return details;
        }

    }
}
