using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject_UI_tests.Pages;

namespace Tests.PageObject.Pages
{
    public class CustomerNewAccount : BasePage
    {
        private By _firstNameInputLocator = By.Id("firstname");
        private By _lastNameInputLocator = By.Name("lastname");
        private By _passwordInputLocator = By.Name("password");
        private By _confirmPasswordInputLocator = By.Id("password-confirmation");
        private By _createAnAccountButtonLocator = By.ClassName("submit");
        private By _alertMessageEmailLocator = By.Id("email_address-error");

        public CustomerNewAccount(IWebDriver driver) : base(driver)
        {   
        
        }


        public void LoginWithOutEmail(string firstName,string lastName, string password)
        {
            EnterFirstName(firstName);
            EnterLastName(lastName);
            EnterPassword(password);
            ConfirmPassword(password);
            ClickCreateAnAccountButton();
        }

        public void EnterFirstName(string value)
        {
            var element = _driver.FindElement(_firstNameInputLocator);

            element.SendKeys(value);
        }

        public void EnterLastName(string value)
        {
            var element = _driver.FindElement(_lastNameInputLocator);

            element.SendKeys(value);
        }
        public void EnterPassword(string value)
        {
            var element = _driver.FindElement(_passwordInputLocator);

            element.SendKeys(value);
        }

        public void ConfirmPassword(string value)
        {
            var element = _driver.FindElement(_confirmPasswordInputLocator);

            element.SendKeys(value);
        }

        public void ClickCreateAnAccountButton()
        {
            var element = _driver.FindElement(_createAnAccountButtonLocator);

            element.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            //wait.Until((driver) => !driver.Title.StartsWith("Customer Login "));
        }

        public string GetAlertMessageEmail()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.FindElement(_alertMessageEmailLocator).Text.StartsWith("This is a required "));

            IWebElement alert = _driver.FindElement(_alertMessageEmailLocator);

            return alert.Text;
        }

    }
}
