using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static PoP.classes.KeyboardInput;
using System.IO;

namespace PoP.classes
{
    class Dialogue : Location
    {
        public Dictionary<string, string> conversation;

        public int dialogueIndex = 0;

        public Dialogue(int id, int x, int y, string path) : base(id, x, y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            Path = path;

            string json = File.ReadAllText(path);
            conversation = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }

        public override void LoadLocation()
        {
            Map.CurrentLocation = this;
            GameLoop.Phase = GamePhase.DIALOGUE;

            Start();
        }

        public override void Start()
        {
            KeyboardInput.KeyPressed += KeyPressed;
            GameLoop.display.DrawString(conversation[dialogueIndex.ToString()], 4, 50);
        }

        public void KeyPressed(ConsoleKey key)
        {
            if (GameLoop.Phase == GamePhase.DIALOGUE && key == ConsoleKey.Spacebar)
            {
                dialogueIndex += 1;

                if(dialogueIndex < conversation.Count)
                {
                    GameLoop.display.WipeTextBox();
                    GameLoop.display.DrawString(conversation[dialogueIndex.ToString()], 4, 50);
                }
                else
                {
                    GameLoop.display.WipeTextBox();
                    End();
                }
            }
        }
        public override void End()
        {
            KeyboardInput.KeyPressed -= KeyPressed;
            isCompleted = true;
            GameLoop.Phase = GamePhase.ADVENTURE;
        }
    }
}
