﻿using System;
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

            int[] point5 = { 4, 0 };
            int[] point6 = { 4, 1 };
            int[] point7 = { 4, 2 };
            int[] point8 = { 4, 3 };

            int[] point9 = { 3, 0 };
            int[] point10 = { 4, 0 };
            int[] point11 = { 5, 0 };
            int[] point12 = { 6, 0 };

            int[] point13 = { 4, 0 };
            int[] point14 = { 4, 1 };
            int[] point15 = { 4, 2 };
            int[] point16 = { 4, 3 };
            
            pos0.Add(point1);
            pos0.Add(point2);
            pos0.Add(point3);
            pos0.Add(point4);

            pos1.Add(point5);
            pos1.Add(point6);
            pos1.Add(point7);
            pos1.Add(point8);

            pos2.Add(point9);
            pos2.Add(point10);
            pos2.Add(point11);
            pos2.Add(point12);

            pos3.Add(point13);
            pos3.Add(point14);
            pos3.Add(point15);
            pos3.Add(point16);

            this.pos = pos0;

            blockColor = Color.Cyan;
            this.name = "BlockI";
        //    Console.WriteLine("generating BlockI");
        }
    }
}
