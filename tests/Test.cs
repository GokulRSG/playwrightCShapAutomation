using NUnit.Framework;
using playwrightWithCFramework.Base;

namespace playwrightWithCFramework.Tests
{
    [TestFixture]
    public class Test : Hooks
    {
        // This test verifies that a user can log in with valid credentials.
        [Test]
        public async Task ValidLoginTest()
        {
            var loginPage = new Pages.LoginPage(Page);
            var dashboardPage = new Pages.DashboardPage(Page);
            var userName = (await loginPage.getUserNameAsync()).Split(':')[1].Trim();
            var password = (await loginPage.getPasswordAsync()).Split(':')[1].Trim();
            await loginPage.LoginAsync(userName, password);
            Assert.That(await dashboardPage.GetWelcomeMessageAsync(), Is.EqualTo("Dashboard"), "Login was not successful.");
        }

        [Test]
        public async Task AdminPageNavigationTest()
        {
            var loginPage = new Pages.LoginPage(Page);
            var navigateComponents = new Pages.NavigateComponents(Page);
            var AdminPage = new Pages.AdminPage(Page);
            var userName = (await loginPage.getUserNameAsync()).Split(':')[1].Trim();
            var password = (await loginPage.getPasswordAsync()).Split(':')[1].Trim();
            await loginPage.LoginAsync(userName, password);
            await navigateComponents.NavigateToAdminPageAsync();
            // Add assertions to verify that the admin page is displayed correctly.
            Assert.That(await AdminPage.IsAdminHeaderVisibleAsync(), Is.True, "Admin page is not displayed correctly.");
            var removedUserName = await AdminPage.GetUserNameByIndexAsync(1);
            await AdminPage.removeSpecificUserAsync(removedUserName);
            Assert.That(await AdminPage.GetUserNamesAsync(), Does.Not.Contain(removedUserName), $"User '{removedUserName}' was not removed successfully.");

        }
    }
}