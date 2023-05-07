﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static PoP.classes.KeyboardInput;
using System.IO;
using System.Windows.Markup;

namespace PoP.classes
{
    class Dialogue : Location
    {
        /// <summary>
        /// A dictionary that represents the conversation in the game, mapping each dialogue ID to its corresponding text.
        /// </summary>
        public List<Dictionary<string, object>> conversation;

        /// <summary>
        /// The index of the current line of dialogue being displayed.
        /// </summary>
        public int dialogueIndex = 0;

        /// <summary>
        /// Initializes a new instance of the Dialogue class with the specified ID, coordinates, file path, and name.
        /// </summary>
        /// <param name="id">The ID of the dialogue.</param>
        /// <param name="x">The X coordinate of the dialogue.</param>
        /// <param name="y">The Y coordinate of the dialogue.</param>
        /// <param name="path">The file path of the JSON file containing the dialogue data.</param>
        public Dialogue(int id, int x, int y, bool isHidden, string path) : base(id, x, y)
        {
            this.id = id;
            positionX = x;
            positionY = y;
            Path = path;

            string json = File.ReadAllText(path);
            conversation = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

            this.isHidden = isHidden;
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
            base.Start();
            KeyboardInput.KeyPressed += KeyPressed;
            Wire.Dialogue.ProgressDialogue(conversation[dialogueIndex]["actor"].ToString(), conversation[dialogueIndex]["text"].ToString());
        }

        public void KeyPressed(ConsoleKey key)
        {
            if (conversation[dialogueIndex].ContainsKey("action")) {
                Action.actions[conversation[dialogueIndex]["action"].ToString()](conversation[dialogueIndex]["param"]);
            }
            // Check if the game is currently in the dialogue phase and if the spacebar key was pressed
            if (GameLoop.Phase == GamePhase.DIALOGUE && key == ConsoleKey.Spacebar)
            {

                // Increment the dialogue index to display the next line
                dialogueIndex += 1;

                //Check whether if it is the end of the conversation
                if(dialogueIndex < conversation.Count)
                {
                    // Display the next line of dialogue
                    Wire.Dialogue.ProgressDialogue(conversation[dialogueIndex]["actor"].ToString(), conversation[dialogueIndex]["text"].ToString());
                }
                else
                {
                    // Clear the text box and end the dialogue phase
                    Wire.Dialogue.ClearDialogue();
                    End();
                }
            }
        }
        public override void End()
        {
            base.End();
            KeyboardInput.KeyPressed -= KeyPressed;

            isCompleted = true;
            Wire.Map.UpdateLocation(this);
            GameLoop.Phase = GamePhase.ADVENTURE;
        }
    }
}
