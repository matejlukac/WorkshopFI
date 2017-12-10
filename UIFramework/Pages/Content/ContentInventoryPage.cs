using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace UIFramework.Pages.Content
{
    // TODO 20: Implement ContentInventoryPage using data-ui attributes in the HTML output
    public class ContentInventoryPage : BasePage
    {
        public ContentInventoryPage(IWebDriver driver)
            : base(driver) { }

        private IWebElement NavigationButton;
        private IWebElement ClearFilterButton;
        private ReadOnlyCollection<IWebElement> Rows;

        public int RowsCount;

        // TODO 20a: Implement GoTo method
        // Open navigation menu and select content inventory item (data-ui-action)
        // Wait until the content inventory page is fully loaded (data-ui-app)
        public void GoTo()
        {
        }

        // TODO 20b: Set the filter and wait for the table to update (data-ui-action)
        public void Filter(string filterText)
        {
        }

        // TODO 20c: Set the filter and wait for the table to update (data-ui-collection; data-ui-object-id)
        public void FilterByWorkflowStep(string stepName)
        {
        }
    }
}
