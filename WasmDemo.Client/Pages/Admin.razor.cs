using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WasmDemo.Shared;

namespace WasmDemo.Client.Pages
{
    public partial class Admin
    {
        [Inject] HttpClient? HttpClient { get; set; }
        
        private PersonModel _newPerson = new PersonModel();
        private List<PersonModel> _people = new List<PersonModel>();
        private List<KeyValuePair<int, string>> _shirtSizes;

        protected override async Task OnInitializedAsync()
        {
            this.GetPeople();
            this.GetShirtSizes();

            await base.OnInitializedAsync();
        }

        private void HandleSubmit()
        {
            _people.Add(_newPerson);
        }

        private void GetPeople()
        {
            _people.Add(new PersonModel { Name = "Jim Stevens", Birthdate = DateTime.Now.AddYears(-34), FavoriteFood = "Quiche Lorraine", ShirtSizeId = 3, SelfDescription = "I like to eat quiche and make croissants." });
            _people.Add(new PersonModel { Name = "Heather Smith", Birthdate = DateTime.Now.AddYears(-47), FavoriteFood = "Beef Rendang", ShirtSizeId = 2, SelfDescription = "I travel to the Far East for my Rendang." });
            _people.Add(new PersonModel { Name = "Beth Connel", Birthdate = DateTime.Now.AddYears(-22), FavoriteFood = "Chipotle", ShirtSizeId = 4, SelfDescription = "I play the clarinet and run marathons in my free time." });
        }

        private async void GetShirtSizes()
        {
            _shirtSizes = await HttpClient!.GetFromJsonAsync<List<KeyValuePair<int, string>>>("api/admin/shirt-sizes");
            StateHasChanged();
        }
    }
}
