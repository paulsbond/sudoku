using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public static class Techniques
    {
        // Look through all groups
        // and find out if one of the candidates
        // only has a single cell that it can go in.
        public static void SinglePosition(Sudoku sudoku)
        {
            foreach (var group in sudoku.Groups)
            {
                var candidates = group.Candidates.ToArray();
                foreach (var number in candidates)
                {
                    var cells = group.Cells.Where(c =>
                        c.Candidates.Contains(number));
                    if (cells.Count() != 1) continue;
                    cells.Single().Set(number);
                }
            }
        }

        // Look through all the cells
        // and find out if it only has a single candidate
        public static void SingleCandidate(Sudoku sudoku)
        {
            foreach (var cell in sudoku.Cells)
                if (cell.Candidates.Count == 1)
                    cell.Set(cell.Candidates.Single());
        }

        // Look through possible positions for a candidate in the groups
        // if all of the candidates belong to another group
        // then remove the candidate from other cells in the other group
        public static void SharedGroups(Sudoku sudoku)
        {
            foreach (var group in sudoku.Groups)
            {
                var candidates = group.Candidates.ToArray();
                foreach (var number in candidates)
                {
                    var cells = group.Cells.Where(c =>
                        c.Candidates.Contains(number)).ToArray();
                    if (cells.Length < 2 || cells.Length > 3) continue;
                    var sharedGroups = cells[0].Groups.AsEnumerable();
                    for (var i = 1; i < cells.Length; i++)
                        sharedGroups = sharedGroups.Intersect(cells[i].Groups);
                    foreach (var sharedGroup in sharedGroups)
                    {
                        if (sharedGroup == group) continue;
                        foreach (var otherCell in sharedGroup.Cells)
                        {
                            if (cells.Contains(otherCell)) continue;
                            otherCell.Candidates.Remove(number);
                        }
                    }
                }
            }
        }

        // If a candidate has only two positions in a group
        // and another candidate has the same two positions
        // then remove other candidates in those positions
        // TODO: Expand to any number not just pairs
        public static void DisjointSubsets(Sudoku sudoku)
        {
            foreach (var group in sudoku.Groups)
            {
                var possibleSubsets = new Dictionary<int, Cell[]>();
                var candidates = group.Candidates.ToArray();
                foreach (var number in candidates)
                {
                    var cells = group.Cells.Where(c =>
                        c.Candidates.Contains(number)).ToArray();
                    if (cells.Length != 2) continue;
                    foreach (var possibleSubset in possibleSubsets)
                    {
                        if (possibleSubset.Value[0] == cells[0] &&
                            possibleSubset.Value[1] == cells[1])
                        {
                            cells[0].Candidates = new HashSet<int>() { possibleSubset.Key, number };
                            cells[1].Candidates = new HashSet<int>() { possibleSubset.Key, number };
                        }
                    }
                    possibleSubsets.Add(number, cells);
                }
            }
        }
    }
}
