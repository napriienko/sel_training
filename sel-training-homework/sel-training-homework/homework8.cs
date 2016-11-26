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
    public class homework8
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
        public void CheckProductStickers()
        {
            driver.Url = "http://localhost/litecart";

            //get all products
            IList<IWebElement> products = driver.FindElements(By.CssSelector("li.product.column.shadow.hover-light"));
            foreach (IWebElement i in products)
            {
                //check each product has one sticker element
                Assert.IsTrue(i.FindElements(By.CssSelector(".sticker")).Count == 1);
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