using PoP.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

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

    public enum SpellSlot
    {
        Spell1,
        Spell2,
        Spell3,
        Spell4
    }

    class Inventory
    {
        public bool IsOpened { get; set; } = false;
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
            { Slot.Ring, null},
            { Slot.MainSword, null},
            { Slot.MainCape, null}
        };

        private Dictionary<int, int> keys = new Dictionary<int, int>()
        {
            {65,0},
            {66,1},
            {67,2},
            {68,3},
            {69,4},
            {70,5},
            {71,6},
            {72,7},
            {73,8},
            {74,9},
            {75,10},
            {76,11},
            {77,12},
            {78,13},
            {79,14},
            {80,15},
            {81,16},
            {82,17},
            {83,18},
            {84,19},
            {85,20},
            {86,21},
            {87,22},
            {88,23},
            {89,24},
            {90,25},
            {49,26},
            {50,27},
            {51,28},
            {52,29},
            {53,30},
            {54,31},
            {55,32},
            {56,33},
            {57,34},
            {112,35},
            {113,36},
            {114,37},
            {115,38},
            {116,39}
        };

        public const int sorceryLimit = 40;
        public static List<Spell> spellList = new List<Spell>();
        public static Dictionary<SpellSlot, Spell> sorcery = new Dictionary<SpellSlot, Spell>()
        {
            { SpellSlot.Spell1, null },
            { SpellSlot.Spell2, null },
            { SpellSlot.Spell3, null },
            { SpellSlot.Spell4, null }
        };

        public static Weapon MainSword { get; set; } = new Weapon("Myrkrsverð", Slot.MainSword, 10, true);
        public static Armor MainCape { get; set; } = new Armor("Varnarmantill", Slot.MainCape, 10, true);

        public static bool ShowingItems;

        public Inventory()
        {
            // Create starting items and equip
            ShowingItems = true;
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

            // Create and add starting spells
            List<Spell> readSpells = SpellFactory.CreateSpellRange(FileInput.GetJsonDictList("res\\spells.json"));
            foreach (var item in readSpells)
            {
                item.Collect();
            }

            sorcery[SpellSlot.Spell1] = readSpells[0];
            sorcery[SpellSlot.Spell3] = readSpells[4];
            sorcery[SpellSlot.Spell2] = readSpells[7];

            KeyboardInput.KeyPressed += KeyPressed;

            Wire.Inventory.UpdateItemList(inventory);
        }

        public void KeyPressed(ConsoleKey key)
        {
            if (GameLoop.Phase == GamePhase.ADVENTURE)
            {
                if (!IsOpened)
                {
                    if (key == ConsoleKey.F12)
                    {
                        IsOpened = true;
                        GameLoop.playerMovement.DisableMovement();

                        if (ShowingItems)
                            Wire.Inventory.ToggleInUse();
                        else
                            Wire.Sorcery.ToggleInUse();
                    }
                    else
                    {
                        if (key == ConsoleKey.Q)
                        {
                            if (ShowingItems)
                                Wire.Inventory.PreviousPage();
                            else
                                Wire.Sorcery.PreviousPage();
                        }
                        else if (key == ConsoleKey.E)
                        {
                            if (ShowingItems)
                                Wire.Inventory.NextPage();
                            else
                                Wire.Sorcery.NextPage();
                        }
                    }
                }
                else
                {
                    if (key == ConsoleKey.Spacebar)
                    {
                        if (ShowingItems)
                        {
                            ShowingItems = false;

                            Wire.Disable(Wire.Inventory);
                            Wire.Enable(Wire.Sorcery);
                            Wire.Sorcery.SetInUse(false);
                        }
                        else
                        {
                            ShowingItems = true;

                            Wire.Disable(Wire.Sorcery);
                            Wire.Enable(Wire.Inventory);
                            Wire.Inventory.SetInUse(false);
                        }
                    }
                    else
                    {
                        if (ShowingItems)
                        {
                            try
                            {
                                int i = keys[(int)key];
                                inventory[i].Equip();
                                Wire.Inventory.UpdateItemList(inventory);
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            try
                            {
                                int i = keys[(int)key];
                                spellList[i].Equip();
                                Wire.Sorcery.UpdateSpellList(spellList);
                            }
                            catch (Exception) { }
                        }

                        if (ShowingItems)
                            Wire.Inventory.ToggleInUse();
                        else
                            Wire.Sorcery.ToggleInUse();
                    }

                    IsOpened = false;
                    GameLoop.playerMovement.EnableMovement();
                }
            }
            if (key == ConsoleKey.Enter)
            {
                Console.WriteLine(Wire.Sorcery.HasChanged);
            }
        }
    }
}
