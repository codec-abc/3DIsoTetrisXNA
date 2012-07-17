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

        public abstract void tryToMove(List<TetrisBlock> BlockInGame);

        public static TetrisBlock generateBlock()
        {
            random.Next(1, 7);
            throw new NotImplementedException();
        }
    }
}

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