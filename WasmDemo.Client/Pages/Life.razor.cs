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
        private string _playIconClass { get { return _isPlaying ? "bi-play-fill text-success" : "bi-play text-white"; } }
        private string _stopIconClass { get { return _isPlaying ? "bi-stop text-white" : "bi-stop-fill text-danger"; } }

        protected override Task OnInitializedAsync()
        {
            _cells = new bool[100, 100];
            _isPlaying = false;
            _iteration = 0;
            _population = 0;
            _iterateDelayMs = 1;

            return base.OnInitializedAsync();
        }

        private void Flip(int rowIndex, int columnIndex)
        {
            _cells[rowIndex, columnIndex] = !_cells[rowIndex, columnIndex];

            if (_cells[rowIndex, columnIndex] == false)
            {
                _population--;
                return;
            }

            _population++;
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
                case "Simkin Glider":
                    SetSimkinGlider();
                    break;
                default:
                    break;
            }
        }

        private void SetGosperGlider()
        {
            Flip(10, 55);
            Flip(11, 53);
            Flip(11, 55);
            Flip(12, 43);
            Flip(12, 44);
            Flip(12, 51);
            Flip(12, 52);
            Flip(12, 65);
            Flip(12, 66);
            Flip(13, 42);
            Flip(13, 46);
            Flip(13, 51);
            Flip(13, 52);
            Flip(13, 65);
            Flip(13, 66);
            Flip(14, 31);
            Flip(14, 32);
            Flip(14, 41);
            Flip(14, 47);
            Flip(14, 51);
            Flip(14, 52);
            Flip(15, 31);
            Flip(15, 32);
            Flip(15, 41);
            Flip(15, 45);
            Flip(15, 47);
            Flip(15, 48);
            Flip(15, 53);
            Flip(15, 55);
            Flip(16, 41);
            Flip(16, 47);
            Flip(16, 55);
            Flip(17, 42);
            Flip(17, 46);
            Flip(18, 43);
            Flip(18, 44);
        }

        private void SetSimkinGlider()
        {
            Flip(12, 35);
            Flip(12, 36);
            Flip(12, 42);
            Flip(12, 43);
            Flip(13, 35);
            Flip(13, 36);
            Flip(13, 42);
            Flip(13, 43);
            Flip(15, 39);
            Flip(15, 40);
            Flip(16, 39);
            Flip(16, 40);
            Flip(21, 57);
            Flip(21, 58);
            Flip(21, 60);
            Flip(21, 61);
            Flip(22, 56);
            Flip(22, 62);
            Flip(23, 56);
            Flip(23, 63);
            Flip(23, 66);
            Flip(23, 67);
            Flip(24, 56);
            Flip(24, 57);
            Flip(24, 58);
            Flip(24, 62);
            Flip(24, 66);
            Flip(24, 67);
            Flip(25, 61);
            Flip(29, 55);
            Flip(29, 56);
            Flip(30, 55);
            Flip(31, 56);
            Flip(31, 57);
            Flip(31, 58);
            Flip(32, 58);
        }
    }
}
