using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace sel_training_homework
{
    [TestFixture]
    public class homework14
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
        public void CheckLinksOpenInNewWindow()
        {
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sidebar")));
            //open Afganistan country
            driver.FindElement(By.CssSelector("a[href*='country_code=AF']")).Click();
            //save current window
            string mainWindow = driver.CurrentWindowHandle;
            ICollection<string> oldWindows;
            ICollection<string> newWindows;
            //get all external links
            IList<IWebElement> externalLinks = driver.FindElements(By.CssSelector(".fa.fa-external-link"));
            //click on each link
            foreach (IWebElement link in externalLinks)
            {
                //get all windows
                oldWindows = driver.WindowHandles;
                //click link
                link.Click();
                //get new windows
                newWindows = driver.WindowHandles;
                //get handle of the newly opened window
                string newWindowHandle = getNewWindowHandle(oldWindows, newWindows);
                //navigate to new window
                driver.SwitchTo().Window(newWindowHandle);
                //close new window
                driver.Close();
                // switch back to the original window
                driver.SwitchTo().Window(mainWindow);
            }


        }
        // method to get a handle of the newly opened window
        public string getNewWindowHandle (ICollection<string> newWindowHandles, ICollection<string> oldWindowHandles)
        {
            string newWindow = null;
            foreach (string i in oldWindowHandles)
            {
                if (newWindowHandles.Contains(i))
                {
                    continue;
                }
                else
                {
                    newWindow = i;
                    break;
                }
            }
            return newWindow;

        }


        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}