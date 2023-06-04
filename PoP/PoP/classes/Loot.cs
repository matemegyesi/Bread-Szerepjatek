using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    internal class Loot
    {
        Random rand = new Random();

        public static Dictionary<int, int> RarityLevels = new Dictionary<int, int>(){
            {1, 30 },
            {2, 60 },
            {3, 90 },
            {4, 120 },
            {5, 150 }
        };

        public static List<Dictionary<string, object>> AllItems = new();

        public int Rarity { get; set; }
        
        public List<Item> ItemLoot { get; set; }

        public Loot(int rlevel)
        {
            if (AllItems.Count == 0)
                InitAllLoot();
            
            Rarity = rlevel;

            int max = RarityLevels[Rarity];

            List<Dictionary<string, object>> choices = new();

            foreach (Dictionary<string, object> item in AllItems)
            {
                if (item["type"].ToString() == "armor")
                    choices.AddRange(AllItems.Where(x => int.Parse(x["defense"].ToString()) >= max-30 && int.Parse(x["defense"].ToString()) <= max));
                else if (item["type"].ToString() == "weapon")
                    choices.AddRange(AllItems.Where(x => int.Parse(x["damage"].ToString()) >= max-30 && int.Parse(x["defense"].ToString()) <= max));
            }

            for (int i = 0; i < 4; i++)
            {
                int random = rand.Next(choices.Count);
                Dictionary<string, object> item = choices[random];
             
                if (item["type"].ToString() == "Armor")
                {
                    string name = item["name"].ToString();
                    double dod = double.Parse(item["defense"].ToString());
                    Slot slot = (Slot)Enum.Parse(typeof(Slot), item["slot"].ToString());

                    ItemLoot.Add(ItemFactory.CreateItem(ItemType.ARMOR, name, dod, slot));

                }
                else if (item["type"].ToString() == "Weapon")
                {
                    string name = item["name"].ToString();
                    double dod = double.Parse(item["damage"].ToString());
                    Slot slot = (Slot)Enum.Parse(typeof(Slot), item["slot"].ToString());

                    ItemLoot.Add(ItemFactory.CreateItem(ItemType.WEAPON, name, dod, slot));
                }
            }
        }

        public void InitAllLoot()
        {
            AllItems = FileInput.GetJsonDictList("//res//items//items_weapon.json");
        }
    }
}
