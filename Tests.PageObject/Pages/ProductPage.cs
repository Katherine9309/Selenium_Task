using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TestProject_UI_tests.Pages
{
    public class ProductPage : BasePage
    {
        private By _productInfoElementLocator = By.ClassName("product-item-info");
        
        private By _productInfoNames = By.ClassName("product-item-link");
        private By _toCartButton = By.ClassName("tocart");
        private By _toCartButton5 = By.CssSelector(".item:nth-child(5) .actions-primary span");
        private By _DashDigitalWatch = By.XPath("//a[@class='product-item-link'][contains(., 'Dash Digital Watch')]/ancestor::*[@class='product-item-info']");
        private By _bagsOptionLeftMenu = By.LinkText("Bags");



        public ProductPage(IWebDriver driver) : base(driver)
        {
        }

        public void ScrollToProducts()
        {
            var elements = _driver.FindElements(_productInfoElementLocator);

            Actions actions = new Actions(_driver);
            actions.ScrollToElement(elements.Last());
            actions.Perform();
        }

        public IEnumerable<string> GetProductInfoNames()
        {
            var elements = _driver.FindElements(_productInfoElementLocator);

            IEnumerable<IWebElement> productInfoNames = elements
                .Select(i => i.FindElement(_productInfoNames));

            IEnumerable<string> actual = productInfoNames.Select(i => i.Text);
            return actual;
        }

        public void AddFirstProductToCart()
        {
            var elements = _driver.FindElements(_productInfoElementLocator);
            IWebElement targetProduct = elements.First();

            Actions actions = new Actions(_driver);
            actions.MoveToElement(targetProduct);
            actions.Perform();

            IWebElement productAddToCartButton = targetProduct.FindElement(_toCartButton);

            productAddToCartButton.Click();
        }

        public void AddSecondProductToCart(int elementNumber)
        {
            var elements = _driver.FindElements(_productInfoElementLocator);
            IWebElement targetProduct = elements.Skip(elementNumber-1).FirstOrDefault();

            Actions actions = new Actions(_driver);
            actions.MoveToElement(targetProduct);
            actions.Perform();

            IWebElement productAddToCartButton = targetProduct.FindElement(_toCartButton);
            productAddToCartButton.Click();
        }

        public void OpenProductAndAddToCart(int elementNumber)
        {
            var elements = _driver.FindElements(_productInfoElementLocator);
            IWebElement targetProduct = elements.Skip(elementNumber - 1).FirstOrDefault();
            targetProduct.Click();
        }

       

        public void AddSelectedProductToCart()
        {
            IWebElement element = _driver.FindElement(_DashDigitalWatch);

            Actions actions = new Actions(_driver);
            actions.MoveToElement(element);
            actions.Perform();

            TimeSpan.FromSeconds(4);
            
            IWebElement productAddToCartButton = _driver.FindElement(_toCartButton5);
            productAddToCartButton.Click();

        }

        public void ScrollToProducts2() {

            var element = _driver.FindElement(_DashDigitalWatch);
            Actions actions = new Actions(_driver);
            actions.ScrollToElement(element);
            actions.Perform();
        }

        public void OpenBagsCategoryFromLeftMenu() {
            var element = _driver.FindElement(_bagsOptionLeftMenu);
            element.Click();

        }

    }
}
