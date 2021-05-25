using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class JigsawPuzzle
    {
        public string BackgroundImage { get; set; }

        public string MissingPieceImage { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
