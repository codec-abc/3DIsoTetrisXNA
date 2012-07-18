using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    public class BlockI : TetrisBlock
    {
        public BlockI()
        {
            int[] point1 = { 3, 0 };
            int[] point2 = { 4, 0 };
            int[] point3 = { 5, 0 };
            int[] point4 = { 6, 0 };

            pos = new List<int[]>();
            pos.Add(point1);
            pos.Add(point2);
            pos.Add(point3);
            pos.Add(point4);
            blockColor = Color.Cyan;
            this.name = "BlockI";
            Console.WriteLine("generating BlockI");
        }
    }
}
