using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public enum eDirection
    {
        horizontal,
        vertical
    }
    public class Word
    {
        public string WordValue { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public eDirection Direction { get; set; }

    }
}
