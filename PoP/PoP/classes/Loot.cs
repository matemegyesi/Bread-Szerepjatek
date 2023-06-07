using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Loot
    {
        //Random rand = new Random();

        public static Dictionary<int, int> RarityLevels = new Dictionary<int, int>(){
            {1, 30 },
            {2, 60 },
            {3, 90 },
            {4, 120 },
            {5, 150 }
        };

        public static List<Item> AllWeapons = new List<Item>();
        public static List<Item> AllArmor = new List<Item>();

        public int Rarity { get; set; }
        
        public List<Item> ItemLoot = new List<Item>();

        public Loot(int rlevel)
        {
            if (AllWeapons.Count == 0 || AllArmor.Count == 0)
                InitAllLoot();
            
            Rarity = rlevel;

            int max = RarityLevels[Rarity];

            List<Item> _lootWeapon = AllWeapons.Where(x => x.DamageOrDefense <= max && x.DamageOrDefense >= max-30).ToList();
            List<Item> _lootDefence = AllArmor.Where(x => x.DamageOrDefense <= max && x.DamageOrDefense >= max-30).ToList();

            for (int i = 0; i < 2; i++)
            {
                ItemLoot.Add(_lootWeapon[new Random().Next(0, _lootWeapon.Count - 1)]);
                ItemLoot.Add(_lootDefence[new Random().Next(0, _lootDefence.Count - 1)]);
            }

        }

        public void InitAllLoot()
        {
            List<Dictionary<string, object>> _items = new List<Dictionary<string, object>>();
            _items.AddRange(FileInput.GetJsonDictList("res\\items\\items_weapon.json"));
            _items.AddRange(FileInput.GetJsonDictList("res\\items\\items_armor.json"));

            foreach(Dictionary<string, object> item in _items)
            {
                if (item["type"].ToString() == "Armor")
                {
                    string name = item["name"].ToString();
                    double dod = double.Parse(item["defense"].ToString());
                    Slot slot = (Slot)Enum.Parse(typeof(Slot), item["slot"].ToString());

                    AllArmor.Add(ItemFactory.CreateItem(ItemType.ARMOR, name, dod, slot));

                }
                else if (item["type"].ToString() == "Weapon")
                {
                    string name = item["name"].ToString();
                    double dod = double.Parse(item["damage"].ToString());
                    Slot slot = (Slot)Enum.Parse(typeof(Slot), item["slot"].ToString());

                    AllWeapons.Add(ItemFactory.CreateItem(ItemType.WEAPON, name, dod, slot));
                }
            }

        }
    }
}
