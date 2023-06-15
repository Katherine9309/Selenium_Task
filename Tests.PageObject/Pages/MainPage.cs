using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using Tests.PageObject.Pages;

namespace TestProject_UI_tests.Pages
{
    public class MainPage : BasePage
    {
        private By _signInButtonLocator = By.ClassName("authorization-link");
        private By _createAccountButtonLocator = By.CssSelector("a[href='https://magento.softwaretestingboard.com/customer/account/create/']");

        private By _welcomeMessageLocator = By.ClassName("logged-in");
        private By _customerButtonLocator = By.ClassName("customer-welcome");
        private By _myaccountLocator = By.CssSelector(".customer-menu li:nth-child(1)");
        

        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        public CustomerLoginPage ClickSignInButton()
        {
            var element = _driver.FindElement(_signInButtonLocator);
            element.Click();

            return new CustomerLoginPage(_driver);
        }



        public string GetSignInButtonText()
        {
            var element = _driver.FindElement(_signInButtonLocator);

            return element.Text;
        }

        public string GetWelcomeMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            wait.Until((driver) => driver.FindElement(_welcomeMessageLocator).Text.StartsWith("Welcome, "));

            return _driver.FindElement(_welcomeMessageLocator).Text;
        }


        public CustomerNewAccount ClickCreateAnAccountButton()
        {
            var element = _driver.FindElement(_createAccountButtonLocator);
            element.Click();

            return new CustomerNewAccount(_driver);
        }

        public ProductPage ClickCustomerButton()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.FindElement(_welcomeMessageLocator).Text.StartsWith("Welcome, "));
            var element = _driver.FindElement(_customerButtonLocator);
            Actions action = new Actions(_driver);
            action.MoveToElement(element);
            action.Click();
            action.Perform();

            return new ProductPage(_driver);
        }

        public AccountPage ClickMyAccountOption() {

            var element = _driver.FindElement(_myaccountLocator);
            Actions action = new Actions(_driver);
            action.MoveToElement(element);
            action.Click();
            action.Perform();
            return new AccountPage(_driver);
        }
    }
}
