using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisGame
{
    public class TetrisCell
    {
        protected bool empty = true;
        public bool isEmpty()
        {
            return empty;
        }
        public void setEmptyness(bool value)
        {
            this.empty = value;
        }
    }
}
