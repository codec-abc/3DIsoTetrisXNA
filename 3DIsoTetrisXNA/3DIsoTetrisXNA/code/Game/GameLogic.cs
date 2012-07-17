using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisGame
{
    public class GameLogic
    {
        protected int level = 1;
        protected int frameEllapsedSinceLastMove = 0;
        
        protected float timeSincePreviousUpdate = 0;
        protected float timeSinceBeginning = 0;

        TetrisBlock currentBlock = null;
        List<TetrisBlock> BlockInGame = new List<TetrisBlock>();


        public void updateGame(float time) 
        {
            timeSinceBeginning = timeSinceBeginning + time;
            if (timeSincePreviousUpdate > 1 / 60.0f)
            {
                timeSincePreviousUpdate = 0;
                this.computeNextFrame();
            }
            else
            {
                timeSincePreviousUpdate = timeSincePreviousUpdate + time;
            }
        }

        public void computeNextFrame()
        {
            frameEllapsedSinceLastMove++;
            if (this.HasToUpdate())
            {
                this.computeNextIteration();
                frameEllapsedSinceLastMove = 0;
            }
            if (timeSinceBeginning / level > 20)
            {
                level++;
            }
        }

        public void computeNextIteration()
        {
            try 
            {
                currentBlock.tryToMove(BlockInGame);
            }
            catch (UnableToMoveBlockException e)
            {
                TetrisBlock blockToAdd = TetrisBlock.generateBlock();
                BlockInGame.Add(blockToAdd);
                currentBlock = blockToAdd;
            }
        }

        public bool HasToUpdate()
        {
            if (frameEllapsedSinceLastMove > 0.0473f * level * level - 3.8782f * level + 63.654f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
