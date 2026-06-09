using Microsoft.Playwright;
using playwrightWithCFramework.Base;
namespace playwrightWithCFramework.Pages
{
    // The AdminPage class represents the admin page of the application and provides methods to interact with it.
    public class AdminPage : BasePage
    {
        public AdminPage(IPage page) : base(page)
        {
        }

        private ILocator adminHeader => Page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { NameString = "Admin" });
        private ILocator removeUserbutton => Page.Locator("//div[@class='orangehrm-container']//i[@class='oxd-icon bi-trash']");
        private ILocator tblUserNameList => Page.Locator("//div[@class='oxd-table-card']//div[@role='cell'][2]/div");
        private ILocator confirmationDialog => Page.Locator("//div[@role='document']"); 
        private ILocator confirmButton => Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { NameString = "Yes, Delete" });

        // Method to verify if the admin header is visible on the page, indicating that the user has successfully navigated to the admin page.
        public async Task<bool> IsAdminHeaderVisibleAsync()
        {
            return await IsVisibleAsync(adminHeader);
        }

        // Method to remove a user by clicking on the remove user button and confirming the action in the dialog.
        public async Task removeSpecificUserAsync(string username)
        {
            var userRows = await tblUserNameList.AllAsync();
            var removeButtons = await removeUserbutton.AllAsync();

            for (int rowIndex = 0; rowIndex < userRows.Count; rowIndex++)
            {
                var row = userRows[rowIndex];
                var text = await row.InnerTextAsync();
                if (text.Trim() == username)
                {
                    var removeButton = removeButtons[rowIndex];
                    await ClickAsync(removeButton);

                    if (await IsVisibleAsync(confirmationDialog))
                    {
                        await ClickAsync(confirmButton);
                        return;
                    }

                    throw new Exception("Confirmation dialog not displayed.");
                }
            }

            throw new Exception($"User '{username}' not found in the user list.");
        }

        // Method to retrieve a list of all usernames displayed in the user table on the admin page.
        public async Task<List<string>> GetUserNamesAsync()
        {
            var userRows = await tblUserNameList.AllAsync();
            var userNames = new List<string>();
            foreach (var row in userRows)
            {
                var text = await row.InnerTextAsync();
                userNames.Add(text.Trim());
            }
            return userNames;
        }

        // Method to retrieve a specific username by its index from the list of usernames on the admin page.
        public async Task<string> GetUserNameByIndexAsync(int index)
        {
            var userNames = await GetUserNamesAsync();
            if (index >= 0 && index < userNames.Count)
            {
                return userNames[index];
            }
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");
        }
    }
}