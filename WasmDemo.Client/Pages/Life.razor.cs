namespace WasmDemo.Client.Pages
{
    public partial class Life
    {
        private bool[,] _cells;
        private bool _isPlaying;
        private int _iteration;
        private int _population;
        private int _iterateDelayMs;

        protected override Task OnInitializedAsync()
        {
            _cells = new bool[100, 100];
            _isPlaying = false;
            _iteration = 0;
            _population = 0;
            _iterateDelayMs = 1;

            return base.OnInitializedAsync();
        }

        private void Flip(int rowIndex, int columnIndex, bool isIncrementing = true)
        {  
            bool flippedValue = !_cells[rowIndex, columnIndex];

            _cells[rowIndex, columnIndex] = flippedValue;

            if (flippedValue)
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
    }
}
