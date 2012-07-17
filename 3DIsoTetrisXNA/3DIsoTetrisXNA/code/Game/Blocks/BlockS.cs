using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisGame
{
    public class BlockS : TetrisBlock
    {
        public BlockS()
        {
            int[] point1 = { 4, 0 };
            int[] point2 = { 5, 0 };
            int[] point3 = { 3, 1 };
            int[] point4 = { 4, 1 };

            pos = new List<int[]>();
            pos.Add(point1);
            pos.Add(point2);
            pos.Add(point3);
            pos.Add(point4);
        }
    }
}
