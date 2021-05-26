using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IStorageService
    {
        bool Save(JigsawPuzzle jigsawPuzzle);
    }
}
