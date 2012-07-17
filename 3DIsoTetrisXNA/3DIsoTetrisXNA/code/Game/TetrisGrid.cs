using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisGame.code.Game
{
    class TetrisGrid
    {
        protected TetrisCell[,] grid = new TetrisCell[10, 20];
        public bool canAdd(TetrisBlock blockToAdd)
        {
            List<int[]> pos = blockToAdd.getPos();
            for (int i = 0; i < pos.Count; i++)
            {
                int[] currentPos = pos[i];
                if (grid[currentPos[0], currentPos[1]].isEmpty())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
