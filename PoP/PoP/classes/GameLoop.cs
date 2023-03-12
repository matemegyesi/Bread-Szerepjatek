using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PoP.classes
{
    class GameLoop
    {
        /// <summary>
        /// Képernyő frissítés állapota
        /// </summary>
        public bool Running { get; private set; }

        Display display;
        
        public void Start()
        {
            display = new Display();

            Running = true;
            Update();
        }

        private void Update()
        {
            DateTime lastTime = DateTime.Now;

            while (Running)
            {
                DateTime currentTime = DateTime.Now;
                double elapsedTime = (currentTime - lastTime).TotalMilliseconds;

                if(elapsedTime >= 33.33)
                {
                    lastTime = currentTime;

                    display.Render();

                    /*Item w1 = ItemFactory.CreateItem(ItemType.Weapon, "Kard", 10);
                    Item w2 = ItemFactory.CreateItem(ItemType.Weapon, "Lándzsa", 20);
                    Item a1 = ItemFactory.CreateItem(ItemType.Armor, "Sisak", 5);

                    w1.Collect();
                    w2.Collect();
                    a1.Collect();

                    display.drawString(w1.ToString().ToCharArray(), 10, 10);*/

                    int c = 0;

                    foreach (string item in FileInput.GetAllLinesAsList("D:\\My\\School\\Bread\\Bread-Szerepjatek\\PoP\\PoP\\res\\armorFile.txt"))
                    {

                        Item a1 = null;
                        string[] itemT = item.Split(';');
                        switch (itemT[3])
                        {
                            case "Head":
                                a1 = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Inventory.Slot.HEAD);
                                break;
                            case "Chest":
                                a1 = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Inventory.Slot.CHEST);
                                break;
                            case "Leg":
                                a1 = ItemFactory.CreateItem(ItemType.Armor, itemT[1], float.Parse(itemT[2]), Inventory.Slot.LEG);
                                break;
                            default:
                                break;
                        }

                        display.drawString(a1.ToString().ToCharArray(), 10, c++);
                    }
                }
            }
        }

        /*
        private string[] content;
        */

        /*/// <summary>
        /// Tartalom betöltése a GameLoop-ba
        /// </summary>
        public void Load(string[] content)
        {
            this.content = content;
        }*/

        /*/// <summary>
        /// GameLoop indítása
        /// </summary>
        public async void Start(){
            if (content == null){
                throw new ArgumentException("Content not loaded!");
            }
            // Tartalom betöltése
            // _content.Load();

            // Képernyő frissítés állapotának beállítása
            Running = true;

            // Játékidő beállítása
            DateTime _previousGameTime = DateTime.Now;

            while (Running)
            {
                // Kiszámolja, hogy mennyi idő telt el az utolsó ciklus óta
                TimeSpan GameTime = DateTime.Now - _previousGameTime;
                // Jelenlegi idő frissítése
                _previousGameTime = _previousGameTime + GameTime;
                // Tartalom frissítése
                content.Update(GameTime);

                // Frissítés 30fps-el
                await Task.Delay(4);
            }
        }*/

        /// <summary>
        /// GameLoop leállítása
        /// </summary>
        /*public void Stop()
        {
            Running = false;
            content.Unload();
        }

        public void Draw(string[] content)
        {
            content.Draw(content);
        }*/
    }
}
