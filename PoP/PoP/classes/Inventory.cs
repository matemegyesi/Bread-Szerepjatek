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

        public static Dictionary<int, string> keys = new Dictionary<int, string>() {
            {0,"A"},
            {1,"B"},
            {2,"C"},
            {3,"D"},
            {4,"E"},
            {5,"F"},
            {6,"G"},
            {7,"H"},
            {8,"/"},
            {9,"J"},
            {10,"K"},
            {11,"L"},
            {12,"M"},
            {13,"N"},
            {14,"O"},
            {15,"P"},
            {16,"Q"},
            {17,"R"},
            {18,"S"},
            {19,"T"},
            {20,"U"},
            {21,"V"},
            {22,"W"},
            {23,"X"},
            {24,"Y"},
            {25,"Z"},
            {26,"1"},
            {27,"2"},
            {28,"3"},
            {29,"4"},
            {30,"5"},
            {31,"6"},
            {32,"7"},
            {33,"8"},
            {34,"9"},
            {35,"F1"},
            {36,"F2"},
            {37,"F3"},
            {38,"F4"},
            {39,"F5"}
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
                if (item["name"].ToString() == "Lance")
                {
                    string name = item["name"].ToString();
                    double dod = double.Parse(item["damage"].ToString());
                    Slot slot = (Slot)Enum.Parse(typeof(Slot), item["slot"].ToString());

                    ItemFactory.CreateItem(ItemType.WEAPON, name, dod, slot).Collect();
                }
                if (item["type"].ToString() == "Armor")
                {
                    string name = item["name"].ToString();
                    double dod = double.Parse(item["defense"].ToString());
                    Slot slot = (Slot)Enum.Parse(typeof(Slot), item["slot"].ToString());

                    ItemFactory.CreateItem(ItemType.ARMOR, name, dod, slot).Equip();

                }
                else if (item["type"].ToString() == "Weapon")
                {
                    string name = item["name"].ToString();
                    double dod = double.Parse(item["damage"].ToString());
                    Slot slot = (Slot)Enum.Parse(typeof(Slot), item["slot"].ToString());

                    ItemFactory.CreateItem(ItemType.WEAPON, name, dod, slot).Equip();
                }
            }

            KeyboardInput.KeyPressed += KeyPressed;
        }

        public void KeyPressed(ConsoleKey key)
        {
            
        }
    }
}
