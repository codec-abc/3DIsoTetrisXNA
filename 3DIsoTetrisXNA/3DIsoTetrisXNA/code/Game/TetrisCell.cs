using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetrisGame
{
    public class TetrisCell
    {
        protected Color cellColor;
        protected bool empty = true;

        public bool isEmpty()
        {
            return empty;
        }
        public void setEmptyness(bool value)
        {
            this.empty = value;
        }

        public Color getColor()
        {
            return cellColor;
        }

        public void setColor(Color value)
        {
            this.cellColor = value;
        }
    }
}
