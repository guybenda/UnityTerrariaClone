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

        public static readonly GameMaterial[] Materials;

        private static readonly Material[] InternalMaterials;

        static GameMaterial()
        {
            LoadMaterials();
            InitializeMaterials();
        }

        private GameMaterial(MaterialId id, Material material)
        {
            this.id = id;
            this.material = material;
        }

        private static GameMaterial[] InitializeMaterials()
        {
            return new GameMaterial[] {
                new GameMaterial(MaterialId.Dirt, MaterialByName("Brown")),
                new GameMaterial(MaterialId.Stone, MaterialByName("Gray"))
            };
        }

        private static Material[] LoadMaterials()
        {
            return Resources.LoadAll<Material>("TileMaterials");

        }

        private static Material MaterialByName(string name)
        {
            return InternalMaterials.Where(x => x.name == name).FirstOrDefault();
        }


        public static GameMaterial ById(MaterialId id)
        {
            return Materials.Where(x => x.id == id).FirstOrDefault();
        }
    }
}
