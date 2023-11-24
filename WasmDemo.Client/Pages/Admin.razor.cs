using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using WasmDemo.Shared;

namespace WasmDemo.Client.Pages
{
    public partial class Admin
    {
        [Inject] HttpClient? HttpClient { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        
        private PersonModel _newPerson = new PersonModel();
        private List<PersonModel> _people = new List<PersonModel>();
        private List<KeyValuePair<int, string>> _shirtSizes;

        protected override async Task OnInitializedAsync()
        {
            _shirtSizes = await HttpClient!.GetFromJsonAsync<List<KeyValuePair<int, string>>>("api/admin/get-shirt-sizes");
            _people = await HttpClient!.GetFromJsonAsync<List<PersonModel>>("api/admin/get-people");
        }

        private async void HandleSubmit()
        {
            _people.Add(_newPerson);
            _newPerson = new PersonModel();

            await JS.InvokeVoidAsync("collapseCard", "person-add-button");
        }
    }
}
