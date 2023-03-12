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
        /*
        private string[] content;
        */

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
            int count = 0;

            while (Running)
            {
                DateTime currentTime = DateTime.Now;
                double elapsedTime = (currentTime - lastTime).TotalMilliseconds;

                if(elapsedTime >= 33.33)
                {
                    lastTime = currentTime;

                    display.render();

                    Console.WriteLine($"{++count}");
                }
            }
        }

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
