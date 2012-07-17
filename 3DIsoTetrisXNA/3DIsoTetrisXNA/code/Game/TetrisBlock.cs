using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisGame
{
    public abstract class TetrisBlock
    {
        protected static Random random = new Random();
        protected static int numberOfBlocks = 7;
        protected List<int[]> pos;
        public void tryToMove(List<TetrisBlock> BlockInGame)
        {
            throw new NotImplementedException();
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

        internal List<int[]> getPos()
        {
            throw new NotImplementedException();
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