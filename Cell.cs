using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Cell
    {
        public int Row;
        public int Col;
        public int Box;
        public char Value;
        public HashSet<char> PossibleValues;
        public IList<Group> Groups;

        public Cell(int row, int col, IEnumerable<char> possibleValues)
        {
            Row = row;
            Col = col;
            Box = GetBox();
            Value = ' ';
            PossibleValues = new HashSet<char>(possibleValues);
            Groups = new List<Group>();
        }

        public string Name => $"Row {Row + 1}, column {Col + 1}";

        private int GetBox()
        {
            if (Row < 3)
            {
                if (Col < 3) return 0;
                if (Col < 6) return 1;
                return 2;
            }
            if (Row < 6)
            {
                if (Col < 3) return 3;
                if (Col < 6) return 4;
                return 5;
            }
            if (Col < 3) return 6;
            if (Col < 6) return 7;
            return 8;
        }

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
                group.RemovePossibileValue(value);
        }

        public void RemovePossibileValue(char value)
        {
            if (!PossibleValues.Contains(value)) return;
            PossibleValues.Remove(value);
            if (PossibleValues.Count == 1) SetValue(PossibleValues.Single());
        }

        public bool IsKnown => Value != ' ';

        public override string ToString() => Value.ToString();
    }
}
