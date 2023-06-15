using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_UI_tests.Pages;

namespace Tests.PageObject.Pages
{
    
    public class ItemPage : BasePage
    {
        private By _cartButton = By.Id("product-addtocart-button");
        public ItemPage(IWebDriver driver) : base (driver) { 
        
        }

        public void AddItemToCart() {
            IWebElement cardButton = _driver.FindElement(_cartButton);
            cardButton.Click();
        }
    }
}
