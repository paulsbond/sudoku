using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Cell
    {
        public int Row;
        public int Col;
        public char Value = ' ';
        public HashSet<char> PossibleValues;
        public IList<Group> Groups = new List<Group>();

        public Cell(int row, int col, IEnumerable<char> possibleValues)
        {
            Row = row;
            Col = col;
            PossibleValues = new HashSet<char>(possibleValues);
        }

        public string Name => $"Row {Row + 1}, column {Col + 1}";

        public void SetValue(char value)
        {
            if (value == ' ' || Value == value) return;
            if (!PossibleValues.Contains(value))
            {
                Console.WriteLine($"{Name} cannot be {value}.");
                Environment.Exit(0);
            }
            Value = value;
            PossibleValues.Clear();
            foreach (var group in Groups)
                group.RemovePossibleValue(value);
        }

        public void SingleCandidate()
        {
            if (PossibleValues.Count == 1)
                SetValue(PossibleValues.Single());
        }

        public void RemovePossibleValue(char value)
        {
            if (!PossibleValues.Contains(value)) return;
            PossibleValues.Remove(value);
        }

        public override string ToString() => Value.ToString();
    }
}
