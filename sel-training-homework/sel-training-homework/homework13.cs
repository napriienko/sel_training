using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace sel_training_homework
{
    [TestFixture]
    public class homework13
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
        public void AddRemoveProductsToCart()
        {
            driver.Url = "http://localhost/litecart";
            
            //get all products on the main page
            IList<IWebElement> products = driver.FindElements(By.CssSelector("li.product.column.shadow.hover-light"));
            
            IWebElement cartQuantity;
            int quantity = 0;
            //open product and add to the cart, repeat adding the product 3 times
            for (int i = 0; i < 3; i++)
            {
                products[i].Click();
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".content")));
                //if product has Size drop down then select a size before click Add to Cart
                if (driver.FindElements(By.CssSelector("select[name='options[Size]']")).Count > 0)
                {
                    SelectElement size = new SelectElement(driver.FindElement(By.CssSelector("select[name='options[Size]']")));
                    size.SelectByText("Small");
                }

                //click Add to Cart
                IWebElement addButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[name=add_cart_product]")));
                addButton.Click();
                cartQuantity = driver.FindElement(By.CssSelector("#cart .quantity"));
                quantity = quantity + 1;
                //wait until quantity is increased
                wait.Until(ExpectedConditions.TextToBePresentInElement(cartQuantity, quantity.ToString()));
                //navigate back to home page 
                driver.FindElement(By.CssSelector("a[href='/litecart/']")).Click();
                //get products again
                products = driver.FindElements(By.CssSelector("li.product.column.shadow.hover-light"));
            }
            //open cart list
            driver.FindElement(By.CssSelector("a[href*=checkout]")).Click();           
            IWebElement ordersTable;
            //remove all products
            while (driver.FindElements(By.CssSelector("form[name=cart_form]")).Count > 0)
            {
                //find orders table
                ordersTable = driver.FindElement(By.CssSelector(".dataTable"));
                //remove product
                driver.FindElement(By.CssSelector("button[name=remove_cart_item]")).Click();
                //wait the table is reloaded (eg table element is disappeared)
                wait.Until(ExpectedConditions.StalenessOf(ordersTable));

            } 

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}