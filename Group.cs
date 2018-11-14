using System.Collections.Generic;

namespace Sudoku
{
    public class Group
    {
        public IList<Cell> Cells = new List<Cell>();

        public void RemovePossibileValue(char value)
        {
            foreach (var cell in Cells)
                cell.RemovePossibileValue(value);
        }
    }
}
