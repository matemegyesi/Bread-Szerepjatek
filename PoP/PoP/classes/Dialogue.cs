using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PoP.classes.KeyboardInput;

namespace PoP.classes
{
    class Dialogue : Location
    {
        public string[] content;
        public int dialogueIndex = 0;

        public Dialogue(int id, int x, int y) : base(id, x, y)
        {

        }

        public override void LoadLocation()
        {
            Map.CurrentLocation = this;
            GameLoop.Phase = GamePhase.DIALOGUE;
            GameLoop.display.DrawString("loc", 10, 10);
        }

        public override void Start()
        {
            KeyboardInput.KeyPressed += KeyPressed;
            while (dialogueIndex < content.Length)
            {
                GameLoop.display.DrawString(content[dialogueIndex], 100, 100);
            }
        }
        public void KeyPressed(ConsoleKey key)
        {
            if (GameLoop.Phase == GamePhase.DIALOGUE && key == ConsoleKey.Spacebar)
                Map.CurrentLocation.IncreaseDialogueIndex();
        }
        public override void End()
        {

        }

        public override void IncreaseDialogueIndex()
        {
            dialogueIndex += 1;
        }
    }
}
