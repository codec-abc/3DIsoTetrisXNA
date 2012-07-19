using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisGame
{
    public class TetrisGrid
    {
        protected TetrisCell[,] grid = new TetrisCell[10, 20];

        public TetrisGrid()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 19; y >= 0; y--)
                {
                    TetrisCell currentCell = new TetrisCell();
                    this.grid[x, y] = currentCell;
                }
            }
        }

        public bool canAdd(TetrisBlock blockToAdd)
        {
            List<int[]> pos = blockToAdd.computeActualPos();
            for (int i = 0; i < pos.Count; i++)
            {
                int[] currentPos = pos[i];
                if (!grid[currentPos[0], currentPos[1]].isEmpty())
                {
                    return false;
                }
            }
            return true;
        }

        public TetrisCell getCell(int x, int y) 
        {
            return this.grid[x,y];
        }

        public void add(TetrisBlock currentBlock)
        {
            List<int[]> pos = currentBlock.computeActualPos();
            for (int i = 0; i < pos.Count; i++)
            {
                int[] currentPos = pos[i];
                grid[currentPos[0], currentPos[1]].setEmptyness(false);
                grid[currentPos[0], currentPos[1]].setColor(currentBlock.getColor());
                
            }
        }

        public void clearLine(int k)
        {
            for (int j = k; j > 0; j--)
            {
                for (int i = 0; i < 10 ; i++)
                {
                    this.grid[i,j]=this.getCell(i, j - 1);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                this.grid[i, 0] = new TetrisCell();
            }
        }


    }
}
