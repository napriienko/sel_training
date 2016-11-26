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
    public class homework7
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
        public void CheckLeftMenuNavigation()
        {
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sidebar")));

            //get the total number of menu items
            int nItems = driver.FindElements(By.CssSelector("li#app-")).Count;

            for (int i=1; i<=nItems; i++)
            {
                driver.FindElement(By.CssSelector("li#app-:nth-child(" + i + ")")).Click();
                //search for element again as the page is reloaded
                IWebElement menuItem = driver.FindElement(By.CssSelector("li#app-:nth-child("+i+")"));
                //check the header of the menu item is displayed
                Assert.IsTrue(driver.FindElement(By.CssSelector("#content h1")).Displayed);
                //get the total number of subitems
                int nSubItems = menuItem.FindElements(By.CssSelector("li")).Count;
                for (int j=1; j<=nSubItems; j++)
                {
                    IWebElement subitem = menuItem.FindElement(By.CssSelector("li:nth-child(" + j + ")"));
                    subitem.Click();
                    //check the header of the menu subitem is displayed
                    Assert.IsTrue(driver.FindElement(By.CssSelector("#content h1")).Displayed);
                    //search for menu item element again as the page is reloaded
                    menuItem = driver.FindElement(By.CssSelector("li#app-:nth-child(" + i + ")"));

                }

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