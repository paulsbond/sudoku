using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public static class Techniques
    {
        public static bool HiddenSingle(Sudoku sudoku)
        {
            foreach (var group in sudoku.Groups)
            {
                foreach (var number in group.Candidates.ToArray())
                {
                    var cells = group.Cells.Where(c =>
                        c.Candidates.Contains(number));
                    if (cells.Count() != 1) continue;
                    var cell = cells.Single();
                    Console.WriteLine("The only position a " + number + " can go in " + group.Name + " is " + cell.Name + " - Hidden Single");
                    cell.Set(number);
                    return true;
                }
            }
            return false;
        }

        public static bool NakedSingle(Sudoku sudoku)
        {
            foreach (var cell in sudoku.Cells)
            {
                if (cell.Candidates.Count == 1)
                {
                    var number = cell.Candidates.Single();
                    Console.WriteLine("The only number that can go in " + cell.Name + " is " + number + " - Naked Single");
                    cell.Set(number);
                    return true;
                }
            }
            return false;
        }

        public static bool LockedCandidates(Sudoku sudoku)
        {
            foreach (var group in sudoku.Groups)
            {
                foreach (var number in group.Candidates)
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
                        var otherCells = sharedGroup.Cells.Where(c =>
                            !cells.Contains(c) && c.Candidates.Contains(number));
                        if (otherCells.Any())
                        {
                            Console.WriteLine(
                                "The number " + number +
                                " in " + group.Name +
                                " can only be in " + sharedGroup.Name +
                                " so removing it as a candidate" +
                                " in other cells of " + sharedGroup.Name +
                                " - Locked Candidates");
                            foreach (var otherCell in otherCells)
                                otherCell.Candidates.Remove(number);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool NakedPairEtc(Sudoku sudoku)
        {
            foreach (var group in sudoku.Groups)
            {
                var subsets = new Dictionary<HashSet<int>, HashSet<Cell>>();
                foreach (var cell in group.Cells)
                {
                    if (cell.IsKnown) continue;
                    foreach (var subset in subsets.ToArray())
                    {
                        if (cell.Candidates.IsSubsetOf(subset.Key))
                        {
                            subset.Value.Add(cell);
                        }
                        var newKey = subset.Key.Union(cell.Candidates).ToHashSet();
                        var newValue = subset.Value.Append(cell).ToHashSet();
                        subsets.Add(newKey, newValue);
                    }
                    subsets.Add(cell.Candidates.ToHashSet(), new HashSet<Cell>() { cell });
                }
                foreach (var subset in subsets)
                {
                    if (subset.Key.Count != subset.Value.Count) continue;
                    var otherCells = group.Cells.Where(c =>
                        !subset.Value.Contains(c) &&
                        c.Candidates.Intersect(subset.Key).Any());
                    if (otherCells.Any())
                    {
                        Console.WriteLine(
                            "In " + group.Name + "," +
                            " the numbers " + string.Join(", ", subset.Key) +
                            " are shared between " + string.Join(" and ", subset.Value.Select(c => c.Name)) +
                            " so they cannot be in other cells" +
                            " - Naked Pair/Triplet/Quad/etc");
                        foreach (var otherCell in otherCells)
                            foreach (var number in subset.Key)
                                otherCell.Candidates.Remove(number);
                        return true;
                    }
                }
            }
            return false;
        }

        // TODO
        public static void HiddenPairEtc(Sudoku sudoku)
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
