using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class JigsawPuzzleInfo
    {
        public string Id { get; set; }

        public int X { get; set; }

        public DateTimeOffset SubmittedAt { get; set; }

        public DateTimeOffset ExpiredAt { get; set; }
    }
}
