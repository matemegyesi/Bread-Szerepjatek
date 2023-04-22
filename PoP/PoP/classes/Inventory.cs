using PoP.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace PoP
{
    ///<summary>
    /// An enumeration representing the different item slots in the inventory.
    /// </summary>
    public enum Slot
    {
        //Armor
        Head,
        Chest,
        Leg,

        //Weapon
        Hand,
        Ring,

        //Constant
        MainSword,
        MainCape
    }
    class Inventory
    {
        ///<summary>
        /// The maximum number of items that the inventory can hold.
        /// </summary>
        public const int inventoryLimit = 40;

        ///<summary>
        /// The list of items in the inventory.
        /// </summary>
        public static List<Item> inventory = new List<Item>();

        ///<summary>
        /// A dictionary of gear slots and their corresponding equipped items.
        /// </summary>
        public static Dictionary<Slot, Item> gear = new Dictionary<Slot, Item>() {
            { Slot.Head, null},
            { Slot.Chest, null},
            { Slot.Leg, null},
            { Slot.Hand, null},
            { Slot.Ring, null}
        };

        public static Weapon MainSword { get; set; } = new Weapon("Myrkrsverð", Slot.MainSword, 10, true);
        public static Armor MainCape { get; set; } = new Armor("Varnarmantill", Slot.MainCape, 10, true);

        public Inventory()
        {
            // Create starting items and equip
            string json = File.ReadAllText("res\\items\\startinggear.json");
            List<Dictionary<string, object>> items = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);
            foreach (Dictionary<string, object> item in items)
            {
                if (item["type"].ToString() == "Armor")
                {
                    string name = item["name"].ToString();
                    double dod = double.Parse(item["defense"].ToString());
                    Slot slot = (Slot)Enum.Parse(typeof(Slot), item["slot"].ToString());

                    ItemFactory.CreateItem(ItemType.ARMOR, name, dod, slot).Equip();

                }else if (item["type"].ToString() == "Weapon")
                {
                    string name = item["name"].ToString();
                    double dod = double.Parse(item["damage"].ToString());
                    Slot slot = (Slot)Enum.Parse(typeof(Slot), item["slot"].ToString());

                    ItemFactory.CreateItem(ItemType.WEAPON, name, dod, slot).Equip();
                }
            }
        }
    }
}
