using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;
using TestProject_UI_tests.Pages;
using Tests.PageObject.Pages;

namespace TestProject_UI_tests
{
    public class Tests
    {
        [ThreadStatic]
        private static IWebDriver _driver;

        [ThreadStatic]
        private static MainPage _mainPage;

    

        [SetUp]
        public void SetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");

            _driver = new ChromeDriver(chromeOptions);

            _driver.Manage().Window.Maximize();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/men.html");

            _mainPage = new MainPage(_driver);
            
        }

        [Test]
        public void LogoutUser_CheckSignInButtonText_IsSignIn()
        {
            string actual = _mainPage.GetSignInButtonText();

            Assert.AreEqual("Sign In", actual);
        }

        [Test]
        public void MainPage_LoginByValidUser_WelcomeMessageIsCorrect()
        {
            var customerLoginPage = _mainPage.ClickSignInButton();

            customerLoginPage.Login("moderya7@gmail.com", "test_password1");

            string actual = _mainPage.GetWelcomeMessage();

            Assert.AreEqual("Welcome, Serhii Mykhailov!", actual);
        }

        [Test]
        public void ValideUser_OpenGear_ProductListIsCorrect()
        {
            var customerLoginPage = _mainPage.ClickSignInButton();

            customerLoginPage.Login("moderya7@gmail.com", "test_password1");

            var productPage = _mainPage.OpenGearCategoryPage();

            productPage.ScrollToProducts();

            IEnumerable<string> actual = productPage.GetProductInfoNames();
            IEnumerable<string> expected = new[]
            {
                "Fusion Backpack",
                "Push It Messenger Bag",
                "Affirm Water Bottle",
                "Sprite Yoga Companion Kit"
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void LogoutUser_AddProductToCart_AlertIsCorect()
        {
            ProductPage productPage = _mainPage.OpenGearCategoryPage();

            productPage.AddFirstProductToCart();

            var actual = productPage.GetAlertMessage();

            Assert.AreEqual("You added Fusion Backpack to your shopping cart.", actual);
        }

        [Test]
        public void ValidUser_OpenWatchesAddDashDigitalWatchFinishedOrder_CheckInOrders() { 
        
            //Precondition 
         
            ReviewAndPaymentsPage _reviewAndPaymentsPage = new ReviewAndPaymentsPage(_driver);
            ShippingPage _shippingPage = new ShippingPage(_driver);
            SuccessPurchase _successPurchase = new SuccessPurchase(_driver);
            

            var customerLoginPage = _mainPage.ClickSignInButton();
            customerLoginPage.Login("moderya7@gmail.com", "test_password1");

            //Action
            var productPage = _mainPage.OpenGearWatchesSection();
            productPage.ScrollToProducts2();
            productPage.AddSelectedProductToCart();
            productPage.GetAlertMessage();
            productPage.CheckOut();
           
            Thread.Sleep(3000);
            _shippingPage.FillForm();
            Thread.Sleep(3000);
           
            _reviewAndPaymentsPage.PlaceOrder();
            _mainPage.GetWelcomeMessage();
            var orderNumber = _successPurchase.continueShopping();
            var myAccountPage = new AccountPage(_driver, orderNumber);
            Thread.Sleep(3000);

            _mainPage.ClickCustomerButton();
            _mainPage.ClickMyAccountOption();

            myAccountPage.CheckMyOrders();
            myAccountPage.viewOrder();

            IEnumerable<string> actual = myAccountPage.viewDetails();

            //Assertion
            IEnumerable<string> expected = new[]
            {
                "Dash Digital Watch",
                "$92.00",
                "$5.00",
                "$97.00"
            };
            CollectionAssert.AreEqual(expected, actual);
        }


        [Test]
        public void CreateUser_FillAllFieldsExceptEmail_CheckErrorMessage() {
            //precondition 
            //action
            var customerNewAccount = _mainPage.ClickCreateAnAccountButton();
            customerNewAccount.LoginWithOutEmail("Carlos", "perez", "test_password1");
            var actual= customerNewAccount.GetAlertMessageEmail();
            //assert
            Assert.AreEqual("This is a required field.", actual);
        }

        [Test]
        public void ValidUser_OpenGearPageAndAddTree_CheckNumberCart()
        {
            //precondition
            var customerLoginPage = _mainPage.ClickSignInButton();
            customerLoginPage.Login("moderya7@gmail.com", "test_password1");

            //action 
            var productPage = _mainPage.OpenGearCategoryPage();
            productPage.OpenBagsCategoryFromLeftMenu();
            productPage.AddFirstProductToCart();
            productPage.AddSecondProductToCart(2);
            productPage.OpenProductAndAddToCart(3);
            ItemPage itemPage = new ItemPage(_driver);
            itemPage.AddItemToCart();
            var itemsActual = itemPage.CkeckNumerItems();

            //assert
            Assert.AreEqual(3,itemsActual);
        }


        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}