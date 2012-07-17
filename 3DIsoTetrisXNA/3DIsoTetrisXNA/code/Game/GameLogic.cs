using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TetrisGame;

namespace TetrisGame
{
    public class GameLogic
    {
        protected int level = 1;
        protected int frameEllapsedSinceLastMove = 0;

        protected bool gameOver = false;
        protected float timeSincePreviousUpdate = 0;
        protected float timeSinceBeginning = 0;

        TetrisBlock currentBlock = null;
        TetrisGrid grid = new TetrisGrid();

        public GameLogic()
        {
            currentBlock = TetrisBlock.generateBlock();
        }


        public void updateGame(float time) 
        {
            if (!gameOver)
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
        }

        public void computeNextFrame()
        {
            frameEllapsedSinceLastMove++;
            if (this.HasToUpdate())
            {
                this.print();
                this.computeNextIteration();
                frameEllapsedSinceLastMove = 0;
            }
            if (timeSinceBeginning / level > 20)
            {
                level++;
            }
        }

        public void print()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            for (int y = 0; y < 20; y++)
            {
                string line = "|";
                for (int x = 0; x < 10; x++)
                {
                    TetrisCell currentCell = this.grid.getCell(x, y);
                    string temp;
                    if (currentCell.isEmpty())
                    {
                        temp = "$";
                    }
                    else
                    {
                        temp = "*";
                    }
                    line = line + temp + "|";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public void computeNextIteration()
        {
            try 
            {
                currentBlock.tryToMove(grid);
            }
            catch (UnableToMoveBlockException e)
            {
                this.grid.add(currentBlock);
                this.print();
                TetrisBlock blockToAdd = TetrisBlock.generateBlock();
                if (this.grid.canAdd(blockToAdd))
                {
                    currentBlock = blockToAdd;
                }
                else
                {
                    gameOver = true;
                    Console.WriteLine("!!!!!!!!!! Game Over !!!!!!!!!!");
                }
            }
        }

        public bool HasToUpdate()
        {
          //  if (frameEllapsedSinceLastMove > 0.0473f * level * level - 3.8782f * level + 63.654f)
            if (frameEllapsedSinceLastMove > 2)
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
