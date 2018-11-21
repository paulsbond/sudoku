using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    [Serializable]
    public class Cell
    {
        public int Row;
        public int Col;
        public int? Number;
        public HashSet<int> Candidates = new HashSet<int> {1,2,3,4,5,6,7,8,9};
        public IList<Group> Groups = new List<Group>();

        public Cell(int row, int col, char value)
        {
            Row = row;
            Col = col;
            if (!Char.IsDigit(value)) return;
            Number = int.Parse(value.ToString());
            Candidates.Clear();
        }

        public string Name => $"Row {Row + 1}, column {Col + 1}";

        public bool IsKnown => Number != null;

        public void Set(int number)
        {
            if (!Candidates.Contains(number)) return;
            Number = number;
            Candidates.Clear();
            foreach (var group in Groups)
                group.RemoveCandidate(number);
        }

        public override string ToString() =>
            (Number == null) ? " " : Number.ToString();
    }
}
