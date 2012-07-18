using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    public abstract class TetrisBlock
    {
        protected static Random random = new Random();
        public string name = "";
        protected Color blockColor = new Color(new Vector3(0.5f, 0.5f, 0.5f));
        protected int x = 0;
        protected int y = 0;
        protected int rotation = 0;
        protected static int numberOfBlocks = 7;
        protected List<int[]> pos;

        public Color getColor()
        {
            return this.blockColor;
        }

        public void setColor(Color value)
        {
            this.blockColor = value;
        }

        public void tryToMove(TetrisGrid grid)
        {

            List<int[]> newPos = this.computeNextPos();
            List<int[]> Pos = this.computeActualPos();
            // compute next position
            bool canMove = true;
            for (int i = 0; i < newPos.Count; i++)
            {
                try
                {
                    TetrisCell cell = grid.getCell(newPos[i][0], newPos[i][1]);
                    if (!cell.isEmpty())
                    {
                        canMove = false;
                    }
                }
                catch (Exception e)
                {
                    canMove = false;
                }
            }
            // test if new position intersect at least one full cell
            if (!canMove) // if intersect -> place block in grid at its location, throw execption
            {
                for (int i = 0; i < pos.Count; i++)
                {
                    TetrisCell cell = grid.getCell(Pos[i][0], Pos[i][1]);
                    cell.setEmptyness(false);
                }
                throw new UnableToMoveBlockException();
            }
            else //  else -> update block position
            {
                this.y = this.y + 1;
            }
        }

        private List<int[]> computeNextPos()
        {
            List<int[]> returnValue = new List<int[]>();
            for (int i = 0; i < this.pos.Count; i++)
            {
                int xCube = pos[i][0]+this.x;
                int yCube = pos[i][1]+this.y;
                yCube++;
                int[] tab = new int[2];
                tab[0] = xCube;
                tab[1] = yCube;
                returnValue.Add(tab);
            }
            return returnValue;
        }

        public List<int[]> computeActualPos()
        {
            List<int[]> returnValue = new List<int[]>();
            for (int i = 0; i < this.pos.Count; i++)
            {
                int xCube = pos[i][0] + this.x;
                int yCube = pos[i][1] + this.y;
                int[] tab = new int[2];
                tab[0] = xCube;
                tab[1] = yCube;
                returnValue.Add(tab);
            }
            return returnValue;
        }

        public static TetrisBlock generateBlock()
        {
            int i  = random.Next(1, 7);
            switch (i)
            {
                case 1: return new BlockI();
                case 2: return new BlockJ();
                case 3: return new BlockL();
                case 4: return new BlockO();
                case 5: return new BlockS();
                case 6: return new BlockT();
                case 7: return new BlockZ();
                default: return new BlockI();
            }
        }
    }
}
// board : 10 columns * 20 row
/*
 
 
**** : I
 
 
*
***   : J

 
  *
*** : L

 
**
**  : O

 
 **
**   : S

 
 *
*** : T

 
**
 ** : Z
 
 
*/