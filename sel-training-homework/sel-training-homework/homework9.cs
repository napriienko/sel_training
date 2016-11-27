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
    public class homework9
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
        public void CheckCountriesAndZonesAreSorted()
        {
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sidebar")));

            //get all coutries rows elements
            IList<IWebElement> countries = driver.FindElements(By.CssSelector(".dataTable .row"));
            //get total namber of countries rows
            int countriesNumber = driver.FindElements(By.CssSelector(".dataTable .row")).Count;
            List<string> countryNames = new List<string>();            
            for (int i = 0; i < countriesNumber; i++)
            {
                countryNames.Add(countries[i].FindElement(By.CssSelector("a")).Text);
                string zoneText = countries[i].FindElement(By.CssSelector("td:nth-child(6)")).Text;
                int zoneNumber = Convert.ToInt32(zoneText);
                if (zoneNumber > 0)
                {
                    countries[i].FindElement(By.CssSelector(".fa-pencil")).Click();
                    IList<IWebElement> zonesElements = driver.FindElements(By.CssSelector("#table-zones tr"));
                    List<string> zonesNames = new List<string>();
                    for (int j = 1; j < zonesElements.Count-1; j++ )
                    {
                        zonesNames.Add(zonesElements[j].FindElement(By.CssSelector("td:nth-child(3)")).GetAttribute("textContent"));
                    }
                    CollectionAssert.IsOrdered(zonesNames);
                    //navigate back to countries list
                    driver.FindElement(By.CssSelector("#box-apps-menu li:nth-child(3)")).Click();
                    //get countries elements again as the page is reloaded
                    countries = driver.FindElements(By.CssSelector(".dataTable .row"));
                }
            }
            CollectionAssert.IsOrdered(countryNames);
        }

        [Test]
        public void CheckGeoZonesAreSorted()
        {
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sidebar")));

            //getting all geo zones
            IList<IWebElement> zonesElement = driver.FindElements(By.CssSelector(".dataTable .row"));
            //for each geo zone navigate to the zones page           
            for (int i = 0; i<zonesElement.Count; i++)
            {
                zonesElement[i].FindElement(By.CssSelector("a")).Click();
                IList<IWebElement> zones = driver.FindElements(By.CssSelector("select[name*='zone_code'] option[selected='selected']"));
                //check zones are sorted
                List<string> zonesList = new List<string>();
                foreach (IWebElement j in zones)
                {
                    zonesList.Add(j.Text);
                }
                CollectionAssert.IsOrdered(zonesList);
                //navigate back to geo zones page, is done by Cancel button                
                driver.FindElement(By.Name("cancel")).Click();
                //getting list of geo zones again
                zonesElement = driver.FindElements(By.CssSelector(".dataTable .row"));
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