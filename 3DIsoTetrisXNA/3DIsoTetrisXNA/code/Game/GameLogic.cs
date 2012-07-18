using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;
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
            TetrisGame.rootObject = new DummyObject();
            Console.WriteLine("");
            Console.WriteLine("");
            List<int[]> currentBlockPos = currentBlock.computeActualPos();
            for (int y = 0; y < 20; y++)
            {
                string line = "|";
                for (int x = 0; x < 10; x++)
                {
                    TetrisCell currentCell = this.grid.getCell(x, y);
                    string temp="$";
                    bool activeOnCell = false;
                    for (int i = 0; i < currentBlockPos.Count && activeOnCell==false; i++)
                    {
                        int xCube = currentBlockPos[i][0] ;
                        int yCube = currentBlockPos[i][1];
                        if (xCube == x && yCube == y)
                        {
                            temp = "*";
                            activeOnCell = true;
                        }
                    }
                    if (!activeOnCell)
                    {
                        if (currentCell.isEmpty())
                        {
                            temp = "$";
                        }
                        else
                        {
                            temp = "*";
                        }
                    }
                    line = line + temp + "|";
                    if (temp.Equals("*"))
                    {
                        Vector3 pos = new Vector3(x * 3f, 0, y * 3f);
                        Cube cube = new Cube(Vector3.One, pos);
                        TetrisGame.rootObject.Add(cube);
                    }
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public void update3DScene()
        {

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
