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
        public static int level = 1;
        protected int frameEllapsedSinceLastMove = 0;
        protected int keyboardUpdate = 0;

        protected GameTime timeOfBeginning;
        protected GameTime currentTime;

        public static bool gameOver = false;
        protected float timeSincePreviousUpdate = 0;
        public static float timeSinceBeginning = 0;
        protected float beginTime = 0;

        protected bool forceUpdate = false;

        public static double score = 0;

        TetrisBlock currentBlock = null;
        TetrisBlock nextBlock;
        TetrisGrid grid = new TetrisGrid();

        protected int maxLevel =100;
        protected int maxLevelTime = 180;

        protected TetrisGame myGame;

        public GameLogic(GameTime value, TetrisGame game)
        {
            currentBlock = TetrisBlock.generateBlock();
            nextBlock = TetrisBlock.generateBlock();
            this.timeOfBeginning = value;
            beginTime = value.TotalGameTime.Seconds + value.TotalGameTime.Milliseconds/1000.0f;
            timeSinceBeginning = 0;
            this.myGame = game;
            GameLogic.level = 1;
            GameLogic.score = 0;
            GameLogic.gameOver = false;
        }

        public void setBeginningTime(GameTime value)
        {
            this.timeOfBeginning=value;
            this.beginTime = value.TotalGameTime.Seconds + value.TotalGameTime.Milliseconds / 1000.0f;
        }

        public void updateGame(GameTime time) 
        {

            if (!gameOver)
            {
                this.currentTime = time;
                float newTime = timeSinceBeginning + time.ElapsedGameTime.Milliseconds / 1000.0f;
                if (newTime <= timeSinceBeginning)
                {
                    Console.WriteLine("problem");
                }
                timeSinceBeginning = newTime;
                this.computeKeyboardMove();
                if (timeSincePreviousUpdate > 1 / 70.0f)
                {
                    timeSincePreviousUpdate = 0;
                    this.computeNextFrame();
                }
                else
                {
                    timeSincePreviousUpdate = timeSincePreviousUpdate + time.ElapsedGameTime.Milliseconds / 1000.0f; ;
                }
            }
            else
            {
                if (TetrisGame.keyboardStates[0].IsKeyDown(Keys.N) && TetrisGame.keyboardStates[1].IsKeyDown(Keys.N))
                {
                    myGame.newGame();
                }
            }
        }

        public void computeNextFrame()
        {
            frameEllapsedSinceLastMove++;
            if (this.HasToUpdate() || forceUpdate)
            {
                this.forceUpdate = false;
                this.computeNextIteration();
                frameEllapsedSinceLastMove = 0;
            }
         //   float a = (float) (maxLevel) / (maxLevelTime * maxLevelTime);
         //   int newlevel = (int) (-a*(timeSinceBeginning - maxLevelTime) * (timeSinceBeginning - maxLevelTime) + maxLevel);
            float x = timeSinceBeginning;
            int newlevel = (int) (0.000013573f * x * x * x - 0.0079825f * x * x + 1.5527f * x);

            if (newlevel > level) 
            {
                level = newlevel;
            //    Console.WriteLine("level upgrade");
            }
            if (level > maxLevel)
            {
                    level = maxLevel;
            }
            this.checkGrid();
            this.print();
        }

        public void checkGrid()
        {
            bool keep = true;
            for (int j = 20-1; j >= 0 && keep; j--)
            {
                bool lineEmpty = false;
                for (int i = 0; i < 10 && !lineEmpty; i++)
                {
                    if (grid.getCell(i,j).isEmpty())
                    {
                        lineEmpty = true;
                    }
                }
                if(!lineEmpty)
                {
                    keep = false;
                    grid.clearLine(j);
                    GameLogic.score = GameLogic.score + level * 100;
                   // Console.WriteLine("points : " + GameLogic.score);
                }
            }
            if (!keep)
            {
                checkGrid();
            }
        }

        private void computeKeyboardMove()
        {

            bool spaceWasNotPressed = TetrisGame.keyboardStates[1].IsKeyUp(Keys.Space);
            bool spaceIsPressed = TetrisGame.keyboardStates[0].IsKeyDown(Keys.Space);

            if (spaceWasNotPressed && spaceIsPressed)
            {
                currentBlock.rotate(grid);
            }

            bool leftIsPressed = TetrisGame.keyboardStates[0].IsKeyDown(Keys.Left);
            bool rightIsPressed = TetrisGame.keyboardStates[0].IsKeyDown(Keys.Right);
            bool downIsPressed = TetrisGame.keyboardStates[0].IsKeyDown(Keys.Down);

            if (rightIsPressed && !leftIsPressed)
            {
                bool update = TetrisGame.keyboardStates[1].IsKeyUp(Keys.Right);
                bool keep = true;
                for (int i = 1; i < TetrisGame.keyboardStates.Count && keep; i++)
                {
                    keep = TetrisGame.keyboardStates[i].IsKeyDown(Keys.Right);
                }
                update =update || keep;
                if( update)
                currentBlock.moveRight(grid);
            }

            if (!rightIsPressed && leftIsPressed)
            {
                bool update = TetrisGame.keyboardStates[1].IsKeyUp(Keys.Left);
                bool keep = true;
                for (int i = 1; i < TetrisGame.keyboardStates.Count && keep; i++)
                {
                    keep = TetrisGame.keyboardStates[i].IsKeyDown(Keys.Left);
                }
                update = update || keep;
                if (update)
                currentBlock.moveLeft(grid);
            }

            if (downIsPressed)
            {
                this.forceUpdate = true;
            }

        }

        public void print()
        {
            TetrisGame.rootObject = new DummyObject();
            TetrisGame.rootObject.setVisible(false);
            Grid3D grid3D = new Grid3D();
            TetrisGame.rootObject.Add(grid3D);
            List<int[]> nextBlockPos = nextBlock.computeActualPos();
            Color cellColornextBlock = this.nextBlock.getColor();
            Vector3 center= new Vector3(0 * (1f + offset), 0, -2 * (1f + offset));
            for (int i = 0; i < nextBlockPos.Count; i++)
            {
                Vector3 pos = center + new Vector3(nextBlockPos[i][0] * (1f + offset), 0, nextBlockPos[i][1] * (1f + offset));
                Cube cube = new Cube(Vector3.One, pos);
                cube.setColor(cellColornextBlock);
                cube.Name = currentBlock.name;
                TetrisGame.rootObject.Add(cube);
            }
            
          // Console.WriteLine("");
          //  Console.WriteLine("");
            List<int[]> currentBlockPos = currentBlock.computeActualPos();
            for (int y = 0; y < 20; y++)
            {
              //  Console.Write("|");
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
                    //    Console.Write(temp);
                     //   Console.Write("|");
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
                    //    Console.Write("#");
                     //   Console.Write("|");
                    }
                }
              //  Console.WriteLine(line);
            //    Console.Write("\n");
            }
         //   Console.WriteLine("");
        //    Console.WriteLine("");
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

        public void computeNextIteration()
        {
            if (!currentBlock.canMoveTo(grid, currentBlock.computeNextPos(0,1)))
            {
                this.grid.add(currentBlock);
                TetrisBlock blockToAdd = this.nextBlock;
                if (this.grid.canAdd(blockToAdd))
                {
                    currentBlock = this.nextBlock;
                    this.nextBlock = TetrisBlock.generateBlock();
                }
                else
                {
                    gameOver = true;
                    Console.WriteLine("Game Over");
                }
            }
            else
            {
                currentBlock.translate(0, 1);
            }
        }

        public bool HasToUpdate()
        {
            int levelTime = (int)(-level * 6.0f / 10.0f + 60.0f);
            if (frameEllapsedSinceLastMove > levelTime)
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
