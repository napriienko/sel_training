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

            for (int i=0; i < countriesNumber-1; i++)
            {
                //compare contry name with the next country name
                string countryName = countries[i].FindElement(By.CssSelector("a")).Text;
                string nextCountryName = countries[i+1].FindElement(By.CssSelector("a")).Text;
                int compare = String.Compare(countryName, nextCountryName);
                //if result <0 then two strings are sorted in acsending order
                Assert.IsTrue(compare < 0);
                //get zones
                IWebElement zoneElement = countries[i].FindElement(By.CssSelector("td:nth-child(6)"));
                string zonesNumber = zoneElement.Text;
                //if it has more than o zone the navigate to zones page
                if (Convert.ToInt32(zonesNumber) > 0)
                {
                    countries[i].FindElement(By.CssSelector("a")).Click();
                    //get all zones rows
                    IList<IWebElement> zonesElements = driver.FindElements(By.CssSelector("#table-zones tr"));
                    //check they are sorted
                    for (int j=1; j<zonesElements.Count-2; j++)
                    {
                        string zone = zonesElements[j].FindElement(By.CssSelector("td:nth-child(3)")).GetAttribute("textContent");
                        string zoneNext = zonesElements[j+1].FindElement(By.CssSelector("td:nth-child(3)")).GetAttribute("textContent");
                        Assert.IsTrue(String.Compare(zone, zoneNext) < 0);
                    }
                    //navigate back to the Countries list
                    driver.FindElement(By.CssSelector("#box-apps-menu li:nth-child(3)")).Click();
                }
                //get again all coutries rows elements as the page is reloaded
                countries = driver.FindElements(By.CssSelector(".dataTable .row"));
            }
 
            
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
            for (int i=0; i<zonesElement.Count; i++)
            {
                zonesElement[i].FindElement(By.CssSelector("a")).Click();
                IList<IWebElement> zones = driver.FindElements(By.CssSelector("select[name*='zone_code'] option[selected='selected']"));
                //check zones are sorted
                for (int j = 1; j < zones.Count - 1; j++)
                {
                    string zone = zones[j].Text;
                    string zoneNext = zones[j + 1].Text;
                    Assert.IsTrue(String.Compare(zone, zoneNext) < 0);
                }
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