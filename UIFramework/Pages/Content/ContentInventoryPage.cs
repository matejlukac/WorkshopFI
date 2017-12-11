using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UIFramework.Pages.Content
{
    // TODO 20: Implement ContentInventoryPage using data-ui attributes in the HTML output
    public class ContentInventoryPage : BasePage
    {
        public ContentInventoryPage(IWebDriver driver)
            : base(driver) { }

        private IWebElement NavigationButton => Driver.FindElement(By.XPath("//*[@data-ui-action='open-navigation']"));
        private IWebElement ClearFilterButton => Driver.FindElement(By.XPath("//*[@data-ui-action='clear-filter']"));
        private IWebElement FilterElement => Driver.FindElement(By.XPath("//*[@data-ui-action='filter']"));
        private IWebElement Table => Driver.FindElement(By.XPath("//*[@data-ui-collection='inventory-items']"));
        public ReadOnlyCollection<IWebElement> Rows => Table.FindElements(By.XPath(".//*[@data-ui-object-id][.//*[data-ui-action='select']]"));

        public int RowsCount;

        // TODO 20a: Implement GoTo method
        // Open navigation menu and select content inventory item (data-ui-action)
        // Wait until the content inventory page is fully loaded (data-ui-app)
        public void GoTo()
        {
            NavigationButton.Click();
            Driver.FindElement(By.XPath("//*[@data-ui-nav='content-item-listing']")).Click();

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.IgnoreExceptionTypes(new Type[] { typeof(StaleElementReferenceException) });

            wait.Until(d =>
            {
                return d.FindElements(By.XPath("//*[@data-ui-app='content-item-listing']")).Count > 0;
            });

        }

        // TODO 20b: Set the filter and wait for the table to update (data-ui-action)
        public void Filter(string filterText)
        {
            FilterElement.SendKeys(filterText);
            System.Threading.Thread.Sleep(2000);
        }

        // TODO 20c: Set the filter and wait for the table to update (data-ui-collection; data-ui-object-id)
        public void FilterByWorkflowStep(string stepName)
        {
            var list = Driver.FindElement(By.XPath("//*[@data-ui-collection='workflow-steps']"));
            list.FindElement(By.XPath($".//*[@data-ui-object-id='{stepName}']")).Click();
            System.Threading.Thread.Sleep(2000);
        }
    }
}
