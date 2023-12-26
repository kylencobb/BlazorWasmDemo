using Microsoft.AspNetCore.Components;

namespace WasmDemo.Client.Pages
{
    public partial class Life
    {
        private bool[,] _cells;
        private bool _isPlaying;
        private int _iteration;
        private int _population;
        private int _iterateDelayMs;
        private List<string> _presetGrids = new List<string> { "Gosper Glider", "Simkin Glider" };
        private string _playIconClass
        {
            get
            {
                return _isPlaying ? "bi-play-fill text-success" : "bi-play text-white";
            }
        }
        private string _stopIconClass
        {
            get
            {
                return _isPlaying ? "bi-stop text-white" : "bi-stop-fill text-danger";
            }
        }

        protected override Task OnInitializedAsync()
        {
            _cells = new bool[100, 100];
            _isPlaying = false;
            _iteration = 0;
            _population = 0;
            _iterateDelayMs = 1;

            return base.OnInitializedAsync();
        }

        private void Flip(int rowIndex, int columnIndex, bool setTrue = false)
        {
            bool isPopulated = setTrue;
            
            if (!setTrue)
            {
                isPopulated = !_cells[rowIndex, columnIndex];
            }

            _cells[rowIndex, columnIndex] = isPopulated;

            if (isPopulated)
            {
                _population++;
                return;
            }

            _population--;
        }

        private async Task Play()
        {
            _isPlaying = true;

            while (_isPlaying)
            {
                Iterate();
                StateHasChanged();
                await Task.Delay(_iterateDelayMs);
            }
        }

        private void Iterate()
        {
            _iteration++;

            List<(int, int)> cellsForFlip = new List<(int, int)>();

            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0;  j < _cells.GetLength(1); j++)
                {
                    int activeNeighborCount = GetNeighborValues(i, j).Count(x => x == true);

                    if (_cells[i, j] && (activeNeighborCount == 2 || activeNeighborCount == 3))
                        continue;

                    if (_cells[i, j] && activeNeighborCount < 2)
                        cellsForFlip.Add((i, j));

                    if (_cells[i, j] && activeNeighborCount > 3)
                        cellsForFlip.Add((i, j));

                    if (!_cells[i, j] && activeNeighborCount == 3)
                        cellsForFlip.Add((i, j));
                }
            }

            foreach ((int, int) cell in cellsForFlip)
            {
                Flip(cell.Item1, cell.Item2);
            }
        }

        private List<bool> GetNeighborValues(int rowIndex, int columnIndex)
        {
            List<bool> neighbors = new List<bool>();

            if (columnIndex - 1 >= 0) neighbors.Add(_cells[rowIndex, columnIndex - 1]);
            if (rowIndex - 1 >= 0) neighbors.Add(_cells[rowIndex - 1, columnIndex]);

            if (columnIndex + 1 < 100) neighbors.Add(_cells[rowIndex, columnIndex + 1]);
            if (rowIndex + 1 < 100) neighbors.Add(_cells[rowIndex + 1, columnIndex]);

            if (columnIndex - 1 >= 0 && rowIndex + 1 < 100) neighbors.Add(_cells[rowIndex + 1, columnIndex - 1]);
            if (columnIndex + 1 < 100 && rowIndex - 1 >= 0) neighbors.Add(_cells[rowIndex - 1, columnIndex + 1]);
            
            if (columnIndex - 1 >= 0 && rowIndex - 1 >= 0) neighbors.Add(_cells[rowIndex - 1, columnIndex - 1]);
            if (columnIndex + 1 < 100 && rowIndex + 1 < 100) neighbors.Add(_cells[rowIndex + 1, columnIndex + 1]);

            return neighbors;
        }

        private async Task TemplateChanged(ChangeEventArgs args)
        {
            await OnInitializedAsync();

            switch (args.Value!.ToString())
            {
                case "Gosper Glider":
                    SetGosperGlider();
                    break;
                default:
                    break;
            }
        }

        private void SetGosperGlider()
        {
            Flip(15, 55);
            Flip(16, 53);
            Flip(16, 55);
            Flip(17, 43);
            Flip(17, 44);
            Flip(17, 51);
            Flip(17, 52);
            Flip(17, 65);
            Flip(17, 66);
            Flip(18, 42);
            Flip(18, 46);
            Flip(18, 51);
            Flip(18, 52);
            Flip(18, 65);
            Flip(18, 66);
            Flip(19, 31);
            Flip(19, 32);
            Flip(19, 41);
            Flip(19, 47);
            Flip(19, 51);
            Flip(19, 52);
            Flip(20, 31);
            Flip(20, 32);
            Flip(20, 41);
            Flip(20, 45);
            Flip(20, 47);
            Flip(20, 48);
            Flip(20, 53);
            Flip(20, 55);
            Flip(21, 41);
            Flip(21, 47);
            Flip(21, 55);
            Flip(22, 42);
            Flip(22, 46);
            Flip(23, 43);
            Flip(23, 44);
        }
    }
}
