using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    public class BlockL : TetrisBlock
    {
        public BlockL()
        {
            int[] point1 = { 5, 0 };
            int[] point2 = { 3, 1 };
            int[] point3 = { 4, 1 };
            int[] point4 = { 5, 1 };

            pos = new List<int[]>();
            pos.Add(point1);
            pos.Add(point2);
            pos.Add(point3);
            pos.Add(point4);
            blockColor = Color.Orange;
            this.name = "BlockL";
            Console.WriteLine("generating BlockL");
        }

        public override void rotate()
        {
        }


    }
}
