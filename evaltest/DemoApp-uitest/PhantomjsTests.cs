using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.PhantomJS;
using System.IO;
using System.Threading;

namespace UITest
{
    [TestFixture]
    public class PhantomjsTests
    {
        public static PhantomJSDriver _driver;

        [OneTimeSetUp]
        public void SetUp()
        {
            string dir = Path.GetFullPath(".");
            _driver = new PhantomJSDriver(dir);
        }

        [Test,Property("Topic","Creating Controller with View")]
        public void CustomerForm()
        {
            try
            {
                _driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(30));
                _driver.Navigate().GoToUrl("http://localhost:5000/Customer");

                IWebElement btncustomer = _driver.FindElementById("btnnewcustomer");
                _driver.ExecuteScript("arguments[0].click();", btncustomer);

                _driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(30));

                IWebElement txtname = _driver.FindElementById("CustName");
                IWebElement txtaddress = _driver.FindElementById("Address");
                IWebElement txtphone = _driver.FindElementById("contact");
                Console.Out.WriteLine("New Customer form existence checked");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

        [Test,Property("Topic", "Validating User Input")]
        public void CustomerRequiredCheck()
        {
            try
            {
                _driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(30));
                _driver.Navigate().GoToUrl("http://localhost:5000/Customer");

                IWebElement btncustomer = _driver.FindElementById("btnnewcustomer");
                _driver.ExecuteScript("arguments[0].click();", btncustomer);

                _driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(30));

                var alltexts = _driver.FindElementsByCssSelector("[data-val='true']");
                System.Collections.Generic.List<string> controls = new System.Collections.Generic.List<string>();
                foreach (var item in alltexts)
                {
                    controls.Add(item.GetAttribute("Id"));
                }

                Assert.IsTrue(controls.Contains("CustName"));
                Assert.IsTrue(controls.Contains("Address"));
                Assert.IsTrue(controls.Contains("contact"));
                Console.Out.WriteLine("Required Field check is succeed");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

        [Test, Property("Topic", "Validating User Input")]
        public void Customer_Phone_type()
        {
            try
            {
                _driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
                _driver.Navigate().GoToUrl("http://localhost:5000/Customer/NewCustomer");
                IWebElement txtphone = _driver.FindElement(By.Id("contact"));

                Assert.AreEqual("tel", txtphone.GetAttribute("type"));
                Console.Out.WriteLine("Phone number field is checked");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

        [Test, Property("Topic", "Validating User Input")]
        public void Customer_Invalid_inputs()
        {
            try
            {
                _driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
                _driver.Navigate().GoToUrl("http://localhost:5000/Customer/NewCustomer");

                IWebElement btnsubmit = _driver.FindElementById("btncreate");
                btnsubmit.Submit();

                IWebElement nameerror = _driver.FindElementById("CustName-error");
                IWebElement addresserror = _driver.FindElementById("Address-error");
                IWebElement contacterror = _driver.FindElementById("contact-error");

                Assert.AreEqual("Customer Name is required", nameerror.GetAttribute("textContent"));
                Assert.AreEqual("Address is required", addresserror.GetAttribute("textContent"));
                Assert.AreEqual("Phone Number is required", contacterror.GetAttribute("textContent"));
                Console.Out.WriteLine("Required field validation succeed");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Thread.Sleep(10000);
            _driver.Quit();
        }
    }
}
