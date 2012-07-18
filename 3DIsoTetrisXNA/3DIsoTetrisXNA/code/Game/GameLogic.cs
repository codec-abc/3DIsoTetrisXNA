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
        public static float offset = 0.5f;
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
            TetrisGame.rootObject.setVisible(false);
            Grid3D grid3D = new Grid3D();
            TetrisGame.rootObject.Add(grid3D);
         //   TetrisGame.rootObject.Add(new Cube(Vector3.One, Vector3.Zero));
         //   TetrisGame.rootObject.Add(new Cube(Vector3.One, new Vector3(3f * 9, 0, 0)));
         //   TetrisGame.rootObject.Add(new Cube(Vector3.One, new Vector3(0, 0, 3f * 19)));
          //  TetrisGame.rootObject.Add(new Cube(Vector3.One, new Vector3(3f * 9, 0, 3f * 19)));

            Console.WriteLine("");
            Console.WriteLine("");
            List<int[]> currentBlockPos = currentBlock.computeActualPos();
            for (int y = 0; y < 20; y++)
            {
                Console.Write("|");
                string line = "|";
                for (int x = 0; x < 10; x++)
                {
                    TetrisCell currentCell = this.grid.getCell(x, y);
                    string temp="$";
                    bool activeOnCell = false;
                    bool addBlock = false;
                    Color cellColor=Color.Black;
                    
                    for (int i = 0; i < currentBlockPos.Count && activeOnCell==false; i++)
                    {
                        
                        int xCube = currentBlockPos[i][0] ;
                        int yCube = currentBlockPos[i][1];
                        if (xCube == x && yCube == y)
                        {
                            addBlock = true;
                            activeOnCell = true;
                            cellColor = this.currentBlock.getColor();
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
                            addBlock = true;
                            cellColor = currentCell.getColor();
                        }
                    }
                    
                    temp = GameLogic.getConsoleColor(cellColor);
                    line = line + temp + "|";
                    if (addBlock)
                    {
                        Console.Write(temp);
                        Console.Write("|");
                        Vector3 pos = new Vector3(x * (1f+offset), 0, y * (1f+offset));
                        Cube cube = new Cube(Vector3.One, pos);
                        cube.setColor(cellColor);
                        if (activeOnCell)
                        {
                            cube.Name = currentBlock.name;
                        }
                        TetrisGame.rootObject.Add(cube);
                    }
                    else
                    {
                        Console.Write("#");
                        Console.Write("|");
                    }
                }
              //  Console.WriteLine(line);
                Console.Write("\n");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public static String getConsoleColor(Color cellColor)
        {
            if (cellColor.Equals(Color.Black))
            {
                return "x";
            }
            else if (cellColor.Equals(Color.Cyan))
            {
                return "I";
            }
            else if (cellColor.Equals(Color.Blue))
            {
                return "J";
            }
            else if (cellColor.Equals(Color.Orange))
            {
                return "L";
            }
            else if (cellColor.Equals(Color.Yellow))
            {
                return "O";
            }
            else if (cellColor.Equals(Color.Green))
            {
                return "S";
            }
            else if (cellColor.Equals(Color.Purple))
            {
                return "T";
            }
            else if (cellColor.Equals(Color.Red))
            {
                return "Z";
            }
            else 
            {
                return "X";
            }
        }

        public void update3DScene()
        {

        }

        public void computeNextIteration()
        {

            if(!currentBlock.wasAbleToMove(grid))
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
