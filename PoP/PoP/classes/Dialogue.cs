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
        /// <summary>
        /// A dictionary that represents the conversation in the game, mapping each dialogue ID to its corresponding text.
        /// </summary>
        public Dictionary<string, string> conversation;

        /// <summary>
        /// The index of the current line of dialogue being displayed.
        /// </summary>
        public int dialogueIndex = 0;

        private int personID;

        /// <summary>
        /// Initializes a new instance of the Dialogue class with the specified ID, coordinates, file path, and name.
        /// </summary>
        /// <param name="id">The ID of the dialogue.</param>
        /// <param name="x">The X coordinate of the dialogue.</param>
        /// <param name="y">The Y coordinate of the dialogue.</param>
        /// <param name="path">The file path of the JSON file containing the dialogue data.</param>
        /// <param name="name">The name of the dialogue.</param>
        public Dialogue(int id, int x, int y, string path, string name) : base(id, x, y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            Path = path;
            Name = name;
            personID = 0;

            string json = File.ReadAllText(path);
            conversation = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }

        /// <summary>
        /// Sets the <see cref="Map"/>.CurrentLocation to this instance and starts the Dialogue
        /// </summary>
        public override void LoadLocation()
        {
            GameLoop.display.WipeTextBox();
            Map.CurrentLocation = this;
            GameLoop.Phase = GamePhase.DIALOGUE;

            Start();
        }

        /// <summary>
        /// Starts the dialogue.
        /// </summary>
        public override void Start()
        {
            KeyboardInput.KeyPressed += KeyPressed;
            GameLoop.display.DrawConversation(conversation[dialogueIndex.ToString()], 4, 50, Name, personID++);
        }

        public void KeyPressed(ConsoleKey key)
        {
            // Check if the game is currently in the dialogue phase and if the spacebar key was pressed
            if (GameLoop.Phase == GamePhase.DIALOGUE && key == ConsoleKey.Spacebar)
            {

                // Increment the dialogue index to display the next line
                dialogueIndex += 1;

                //Check whether if it is the end of the conversation
                if(dialogueIndex < conversation.Count)
                {
                    // Clear the text box and display the next line of dialogue
                    GameLoop.display.WipeTextBox();
                    GameLoop.display.DrawConversation(conversation[dialogueIndex.ToString()], 4, 50, Name, personID++);
                }
                else
                {
                    // Clear the text box and end the dialogue phase
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
