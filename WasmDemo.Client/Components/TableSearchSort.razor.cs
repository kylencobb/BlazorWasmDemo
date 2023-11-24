using Microsoft.AspNetCore.Components;

namespace WasmDemo.Client.Components
{
    public partial class TableSearchSort
    {
        [Parameter] public string Title { get; set; } = string.Empty;
        [Parameter] public IList<string[]> Items { get; set; } = new List<string[]>();
        [Parameter] public string[] ColumnNamesOrdered { get; set;} = new string[0];

        private List<Row> _rows = new List<Row>();
        private SortOrder _sortOrder = SortOrder.DESC;
        private string _sortRowActive = string.Empty;
        private string _inputText = string.Empty;
        private string InputText
        {
            get => _inputText;
            set
            {
                if (value != _inputText)
                {
                    _inputText = value;
                    InvokeAsync(Search);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            foreach (string[] row in Items)
            {
                var searchText = string.Empty;

                foreach (string cell in row) 
                {
                    searchText += $"{cell} ";
                }

                _rows.Add(new Row { SearchString = searchText, Show = true, RowValues = row });
            }
        }

        private void Search()
        {
            _rows.All(x => x.Show = true);

            if (string.IsNullOrWhiteSpace(_inputText))
                return;

            for (int i = 0; i < _rows.Count; i++) 
            {
                if (!_rows[i].SearchString.ToLower().Contains(_inputText.ToLower()))
                    _rows[i].Show = false;
            }
        }

        private void Sort(string fieldName)
        {
            int columnIndex = Array.FindIndex(ColumnNamesOrdered, x => x == fieldName);

            if (_sortRowActive != fieldName)
                _sortOrder = SortOrder.ASC;

            if (_sortRowActive == fieldName)
                _sortOrder = _sortOrder == SortOrder.ASC ? SortOrder.DESC : SortOrder.ASC;

            _sortRowActive = ColumnNamesOrdered[columnIndex];

            if (_sortOrder == SortOrder.ASC)
            {
                _rows = _rows.OrderBy(x => x.RowValues[columnIndex]).ToList();
                return;
            }

            _rows = _rows.OrderByDescending(x => x.RowValues[columnIndex]).ToList();

            StateHasChanged();
        }
    }
    public class Row
    {
        public string SearchString { get; set; } = string.Empty;
        public bool Show { get; set; } = true;
        public string[] RowValues { get; set; }
    }
    public enum SortOrder
    {
        ASC,
        DESC
    }
}
