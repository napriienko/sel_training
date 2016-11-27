using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.Generic;

namespace sel_training_homework
{
    [TestFixture]
    public class homework10
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        [Test]
        public void CheckCorrectProductPageOpened()
        {
            driver.Url = "http://localhost/litecart";            
            //get all campaign products elements main page
            IList<IWebElement> products = driver.FindElements(By.CssSelector("#box-campaigns ul li"));
            //get name of the first campaign product
            string name = products[0].FindElement(By.CssSelector(".name")).Text;
            //get element of the regular price and campaing price
            IWebElement regularPrice = products[0].FindElement(By.CssSelector(".regular-price"));
            IWebElement campaignPrice = products[0].FindElement(By.CssSelector(".campaign-price"));
            //get text of regular and campaing price
            string regularPriceValue = regularPrice.Text;
            string campaingPriceValue = campaignPrice.Text;
            //get classes used for prices
            string regularPriceValueClass = regularPrice.GetAttribute("className");
            string campaingPriceValueClass = campaignPrice.GetAttribute("className");
            //get font, color, text decoration of the regular price
            string regularPriceColor = regularPrice.GetCssValue("color");
            string regularPriceFontSize = regularPrice.GetCssValue("font-size");
            string regularPriceText = regularPrice.GetCssValue("text-decoration");
            //get font, color, text decoration of the campaign price
            string campaingPriceColor = campaignPrice.GetCssValue("color");
            string campaingPriceFontSize = campaignPrice.GetCssValue("font-size");
            string campaingPriceText = campaignPrice.GetCssValue("text-decoration");
            //navigate to the product page from the main page
            products[0].Click();
            //get product element
            IWebElement productPage = driver.FindElement(By.CssSelector("#box-product"));
            //get product name
            string nameProductPage = productPage.FindElement(By.CssSelector(".title")).Text;
            //get product prices elements
            IWebElement regularPriceProductPage = productPage.FindElement(By.CssSelector(".regular-price"));
            IWebElement campaignPriceProductPage = productPage.FindElement(By.CssSelector(".campaign-price"));
            //get texts for regular and campaing price 
            string regularPriceValueProductPage = regularPriceProductPage.Text;
            string campaingPriceValueProductPage = campaignPriceProductPage.Text;
            //get classes attributes
            string regularPriceValuepPoductPageClass = regularPriceProductPage.GetAttribute("className");
            string campaingPriceValueProductPageClass = campaignPriceProductPage.GetAttribute("className");
            //get regular price styles
            string regularPriceColorProductPage = regularPriceProductPage.GetCssValue("color");
            string regularPriceFontSizeProductPage = regularPriceProductPage.GetCssValue("font-size");
            string regularPriceTextProductPage = regularPriceProductPage.GetCssValue("text-decoration");
            //get campaing price styles
            string campaingPriceColorProductPage = campaignPriceProductPage.GetCssValue("color");
            string campaingPriceFontSizeProductPage = campaignPriceProductPage.GetCssValue("font-size");
            string campaingPriceTextProductPage = campaignPriceProductPage.GetCssValue("text-decoration");

            //check nproduct name on the main page and on the product page are the same
            Assert.AreEqual(name, nameProductPage);
            //check regular prices are the same
            Assert.AreEqual(regularPriceValue, regularPriceValueProductPage);
            //check campaign prices are the same
            Assert.AreEqual(campaingPriceValue, campaingPriceValueProductPage);
            //check classes used for styling are the same
            Assert.AreEqual(regularPriceValueClass, regularPriceValuepPoductPageClass);
            Assert.AreEqual(campaingPriceValueClass, campaingPriceValueProductPageClass);
            //check text decoration are the same
            Assert.AreEqual(regularPriceText, regularPriceTextProductPage);
            Assert.AreEqual(campaingPriceText, campaingPriceTextProductPage);
            //Note: can not compare color and font sizes of the prices as they are different so used to compare classes

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}