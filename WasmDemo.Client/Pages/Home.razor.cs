using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace WasmDemo.Client.Pages
{
    public partial class Home
    {
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        private readonly string[] _tableHeaders = new string[] { "Name", "Favorite Color", "Favorite Ice Cream" }; 
        private List<string[]> _tableRows = new List<string[]>();
        
        protected override async Task OnInitializedAsync()
        {
            this.GenerateTableRows(); ;
        }

        private void GenerateTableRows()
        {
            var random = new Random();

            for (var i = 0; i < 50; i++)
            {
                var number = random.Next(0, 6);
                var firstName = number == 5 ? "Jennifer" : number == 4 ? "Randy" : number == 3 ? "Heather" : number == 2 ? "Steve" : number == 1 ? "Sarah" : "Bart";

                number = random.Next(0, 6);
                var lastName = number == 5 ? "Connor" : number == 4 ? "Johnson" : number == 3 ? "Cooper" : number == 2 ? "Smith" : number == 1 ? "Benson" : "Travis";

                number = random.Next(0, 6);
                var color = number == 5 ? "Orange" : number == 4 ? "Yellow" : number == 3 ? "Brown" : number == 2 ? "Red" : number == 1 ? "Purple" : "Green";

                number = random.Next(0, 6);
                var iceCream = number == 5 ? "Pistachio" : number == 4 ? "Rocky Road" : number == 3 ? "Mint Chip" : number == 2 ? "Vanilla" : number == 1 ? "Chocolate" : "Coffee";

                _tableRows.Add(new string[] { $"{lastName}, {firstName}", color, iceCream });
            }
        }
    }
}
