using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public enum MaterialId
    {
        Dirt,
        Stone,
    }

    public class GameMaterial
    {
        public readonly MaterialId id;
        public readonly Material material;

        public static readonly GameMaterial[] Materials = InitializeMaterials();
        private static readonly Material[] InternalMaterials = LoadMaterials();

        private static GameMaterial[] InitializeMaterials()
        {
            return new GameMaterial[] { };
        }

        private static Material[] LoadMaterials()
        {
            return Resources.LoadAll<Material>("TileMaterials");
        }

        public static GameMaterial ById(MaterialId id)
        {
            return Materials.Where(x => x.id == id).FirstOrDefault();
        }
    }
}
