using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IStorageService
    {
        bool Save(JigsawPuzzle jigsawPuzzle);

        Task<JigsawPuzzleEntity> LoadAsync(string id);
    }
}
