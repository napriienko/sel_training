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
    public class homework11
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
        public void RegisterNewCustomer()
        {
            driver.Url = "http://localhost/litecart";
            driver.FindElement(By.CssSelector("a[href*=create_account]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("form[name=customer_form]")));
            
            //generate random email
            Random rnd = new Random();
            string email = "mail" + rnd.Next(1000) + "@test.mail";
            //fill in registry form
            driver.FindElement(By.CssSelector("input[name=firstname]")).SendKeys("MyFirstName");
            driver.FindElement(By.CssSelector("input[name=lastname]")).SendKeys("MyLastName");
            driver.FindElement(By.CssSelector("input[name=address1]")).SendKeys("MyAddress");
            driver.FindElement(By.CssSelector("input[name=postcode]")).SendKeys("A1A 1A1");
            driver.FindElement(By.CssSelector("input[name=city]")).SendKeys("MyCity");
            //select country
            SelectElement country = new SelectElement(driver.FindElement(By.CssSelector("select[name=country_code]")));
            country.SelectByText("Canada");
            //fill in contry, email and passwords
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[name=phone]")).SendKeys("+12345678");
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys("password");
            driver.FindElement(By.CssSelector("input[name=confirmed_password]")).SendKeys("password");
            driver.FindElement(By.CssSelector("button[name=create_account]")).Click();
            //wait for zone drop down appears
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("select[name=zone_code]")));
            //select a zone
            SelectElement zone = new SelectElement(driver.FindElement(By.CssSelector("select[name=zone_code]")));
            zone.SelectByText("Manitoba");
            //fill in passwords again as it was cleared
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys("password");
            driver.FindElement(By.CssSelector("input[name=confirmed_password]")).SendKeys("password");
            //click register again
            driver.FindElement(By.CssSelector("button[name=create_account]")).Click();

            //wait for logout button to be appeared
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href*=logout]")));
            //logout
            driver.FindElement(By.CssSelector("a[href*=logout]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[name=login]")));
            //login again with created account
            driver.FindElement(By.CssSelector("input[name=email]")).Clear();
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[name=password]")).Clear();
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys("password");
            driver.FindElement(By.CssSelector("button[name=login]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href*=logout]")));
            //logout
            driver.FindElement(By.CssSelector("a[href*=logout]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[name=login]")));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}