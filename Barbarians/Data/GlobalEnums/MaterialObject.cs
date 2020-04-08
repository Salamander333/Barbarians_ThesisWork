using System.Collections.Generic;

namespace Barbarians.Data.GlobalEnums
{
    public class MaterialObject
    {
        public Materials Name { get; set; }

        public MaterialType Type { get; set; }

        public int Tier { get; set; }

        public MaterialObject(Materials material, MaterialType type, int tier)
        {
            this.Name = material;
            this.Type = type;
            this.Tier = tier;
        }
    }

    public static class MaterialList
    {
        private static MaterialObject Silk = new MaterialObject(Materials.Silk, MaterialType.Cloth, 1);
        private static MaterialObject Linen = new MaterialObject(Materials.Linen, MaterialType.Cloth, 2);
        private static MaterialObject Jute = new MaterialObject(Materials.Jute, MaterialType.Cloth, 3);

        private static MaterialObject Spruce = new MaterialObject(Materials.Spruce, MaterialType.Wood, 1);
        private static MaterialObject Oak = new MaterialObject(Materials.Oak, MaterialType.Wood, 2);
        private static MaterialObject Ash = new MaterialObject(Materials.Ash, MaterialType.Wood, 3);

        private static MaterialObject Copper = new MaterialObject(Materials.Copper, MaterialType.Metal, 1);
        private static MaterialObject Iron = new MaterialObject(Materials.Iron, MaterialType.Metal, 2);
        private static MaterialObject Mithril = new MaterialObject(Materials.Mithril, MaterialType.Metal, 3);

        private static MaterialObject Coins = new MaterialObject(Materials.Coins, MaterialType.Currency, 1);

        public static List<MaterialObject> MaterialObjects = new List<MaterialObject>()
        {
            Silk,
            Linen,
            Jute,
            Spruce,
            Oak,
            Ash,
            Copper,
            Iron,
            Mithril,
            Coins
        };
    }
}
