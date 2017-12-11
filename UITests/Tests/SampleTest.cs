using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using UIFramework;
using UIFramework.Pages.Content;
using UIFramework.Pages.Workflow;

namespace UITests.Tests
{
    public class SampleTest : SeleniumTest
    {
        [Test]
        public void A_DriverExample()
        {
            // TODO 2: Create a simple test
            // Decorate method with [Test] attribute
            // Create an instance of WebDriver
            // Go to the url of your choice using IWebDriver.Navigate()
            // Dispose the driver

            var driver = InitWebDriver();
            driver.Navigate().GoToUrl("http://localhost");
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void B_LoginTestExample()
        {
            // TODO 5: Create login test
            // Navigate to login page https://app.kenticocloud.com/sign-in
            // Find username textbox by id and set the value
            // Find password textbox by id and set the value
            // Find login button and submit
            Driver.Navigate().GoToUrl(LoginPage.LOGIN_URL);
            Driver.FindElement(By.Id("login_username")).SendKeys("TestCrunch2017@gmail.com");
            Driver.FindElement(By.Id("login_password")).SendKeys("TestCrunch2017");
            Driver.FindElement(By.Id("btn-login")).Click();
        }


        [Test]
        public void C_LoginTestExampleWithPODP()
        {
            // TODO 7: Refactor login test - use page object design pattern
            // Create a new instance of LoginPage
            // Navigate to login page
            // Sign in
            var page = new LoginPage(Driver);
            page.GoTo();
            page.SignIn("TestCrunch2017@gmail.com", "TestCrunch2017");
        }


        [Test]
        public void D_FindingElementsExample()
        {
            // TODO 9: Looking for elements  
            // Fill in missing identifiers
            var driver = InitWebDriver();

            driver.Navigate().GoToUrl(LoginPage.LOGIN_URL);
            driver.FindElement(By.Name("login[username]")).SendKeys("TestCrunch2017@gmail.com");
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("TestCrunch2017");
            driver.FindElement(By.TagName("button")).Click();

            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void E_SupportPageObjectsExample()
        {
            // TODO 11: Initialize page elements with PageFactory
            // Create a new instance of LoginPageWithAttributes
            // Navigate to Login page
            // Initialize elements in the page with PageFactory.InitElements
            // Sign in
            var page = new LoginPageWithAttributes();
            Driver.Navigate().GoToUrl(LoginPage.LOGIN_URL);
            PageFactory.InitElements(Driver, page);
            page.SignIn("TestCrunch2017@gmail.com", "TestCrunch2017");
        }

        [Test]
        public void F_DriverWaitExample()
        {
            var page = new LoginPage(Driver);
            page.GoTo();
            page.SignIn("TestCrunch2017@gmail.com", "TestCrunch2017");

            // TODO 13: Waiting
            // Sign in
            // Wait for the Content inventory page to load
            // The page is fully loaded when the table with data is displayed

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.IgnoreExceptionTypes(new Type[] { typeof(StaleElementReferenceException) });

            wait.Until(d =>
            {
                return d.FindElements(By.XPath("//div[@class='scroll-table']")).Count > 0;
            });

            // TODO 14: Refactor waiting using ExpectedConditions
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='scroll-table']")));
        }

        [Test]
        public void G_CreateNewWorkflow()
        {
            // TODO 18a: Workflow test
            // Create a new workflow
            // Edit the created workflow      

            var page = new LoginPage(Driver);
            page.GoTo();
            page.SignIn("TestCrunch2017@gmail.com", "TestCrunch2017");

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.IgnoreExceptionTypes(new Type[] { typeof(StaleElementReferenceException) });

            wait.Until(d =>
            {
                return d.FindElements(By.XPath("//div[@class='scroll-table']")).Count > 0;
            });

            var listing = new WorkflowListingPage(Driver);
            listing.GoTo();
            listing.CreateWorkflow("test");
            var editWorkflowElement = listing.EditWorkflow("test");

            // TODO 18b: Use assert in test
            // Assert the name of newly created workflow
            Assert.That(editWorkflowElement.GetName, Is.EqualTo("test"));

            // TODO 19b: Create a screenshot in test
            TakeScreenShot($"C:\\Logs\\{Guid.NewGuid().ToString()}mytest1.png");
        }

        [Test]
        public void H_FilterItemsInInventory()
        {
            // TODO 21: Create a test of content inventory filter
            var page = new LoginPage(Driver);
            page.GoTo();
            page.SignIn("TestCrunch2017@gmail.com", "TestCrunch2017");

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.IgnoreExceptionTypes(new Type[] { typeof(StaleElementReferenceException) });

            wait.Until(d =>
            {
                return d.FindElements(By.XPath("//div[@class='scroll-table']")).Count > 0;
            });

            var contentPage = new ContentInventoryPage(Driver);
            contentPage.Filter("My beautiful filter");

            Assert.That(contentPage.Rows.Count, Is.EqualTo(0));
        }
    }
}
