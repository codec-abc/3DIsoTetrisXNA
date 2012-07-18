using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisGame
{
    public class BlockJ : TetrisBlock
    {
        public BlockJ()
        {
            int[] point1 = { 3, 0 };
            int[] point2 = { 3, 1 };
            int[] point3 = { 4, 1 };
            int[] point4 = { 5, 1 };

            pos = new List<int[]>();
            pos.Add(point1);
            pos.Add(point2);
            pos.Add(point3);
            pos.Add(point4);
            Console.WriteLine("generating BlockJ");
        }
    }
}
