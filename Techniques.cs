using System.Linq;

namespace Sudoku
{
    public static class Techniques
    {
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

        public static void SingleCandidate(Sudoku sudoku)
        {
            foreach (var cell in sudoku.Cells)
                if (cell.Candidates.Count == 1)
                    cell.Set(cell.Candidates.Single());
        }
    }
}