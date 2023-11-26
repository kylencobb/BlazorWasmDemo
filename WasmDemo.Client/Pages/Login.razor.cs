using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using WasmDemo.Shared;
using WasmDemo.Client.Services;

namespace WasmDemo.Client.Pages
{
    public partial class Login
    {
        [Inject] NavigationManager? NavigationManager { get; set; }
        [Inject] AuthenticationService? AuthenticationService { get; set; }

        private LoginModel _loginModel = new LoginModel();

        private async void HandleLogin()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnPage", out var _returnPage))
            {
                _loginModel.ReturnPage = _returnPage;
            }

            try
            {
                var result = await AuthenticationService.Login(_loginModel);

                NavigationManager.NavigateTo($"{_loginModel.ReturnPage}");
            }
            catch (Exception ex)
            {
                //handle
            }
        }
    }
}
