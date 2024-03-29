﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes.windows
{
    internal class DialogueWindow : Window
    {
        public List<string> history { get; private set; } = new List<string>();

        private int speakerMaxWidth;

        // Change detection
        private bool historyChanged;

        public DialogueWindow()
        {
            Height = 14;
            Width = 235;

            speakerMaxWidth = 35; // Should be at least 30
        }

        protected override List<string> GenerateLines()
        {
            if (CachedLineList.Count != Height)
            {
                LineList.Clear();

                // Removes surplus dialogue
                int _dialogueLineCount = history.Count;
                if (_dialogueLineCount > Height - 2)
                {
                    history.RemoveRange(0, _dialogueLineCount - (Height - 2));
                }

                // Fills the remaining lines
                int _remainingLineCount = Height - LineList.Count;
                for (int i = 0; i < _remainingLineCount - history.Count - 2; i++)
                {
                    AddBlankLine();
                }

                // Dialogue
                foreach (string line in history)
                {
                    AddLine(line);
                }

                // Options menu
                AddLine(Style.Color(Style.GetBlankLine(Width, Border.SINGLE_HORIZONTAL), ColorAnsi.DARK_GREY));
                AddLine($" {Style.Color("Next [SPACE]", history.Count > 0 ? ColorAnsi.WHITE : ColorAnsi.DARK_GREY)}  {Style.Color(Border.SINGLE_VERTICAL, ColorAnsi.DARK_GREY)}  {Style.Color("Skip [SHIFT]", ColorAnsi.DARK_GREY)}");

                return LineList;
            }
            else
            {
                LineList.RemoveRange(0, Height - 2);

                int _dialogueLineCount = history.Count;
                if (_dialogueLineCount > Height - 2)
                {
                    history.RemoveRange(0, _dialogueLineCount - (Height - 2));
                }

                InsertLineRange(0, history);

                int _remainingLineCount = Height - LineList.Count;
                for (int i = 0; i < _remainingLineCount; i++)
                {
                    InsertBlankLine(0);
                }

                if (historyChanged)
                {
                    LineList.RemoveRange(LineList.Count - 2, 2);

                    AddLine(Style.Color(Style.GetBlankLine(Width, Border.SINGLE_HORIZONTAL), ColorAnsi.DARK_GREY));
                    AddLine($" {Style.Color("Next [SPACE]", history.Count > 0 ? ColorAnsi.WHITE : ColorAnsi.DARK_GREY)}  {Style.Color(Border.SINGLE_VERTICAL, ColorAnsi.DARK_GREY)}  {Style.Color("Skip [SHIFT]", ColorAnsi.DARK_GREY)}");

                    historyChanged = false;
                }

                return LineList;
            }
        }

        /// <summary>
        /// Adds a new dialogue line to the bottom.
        /// </summary>
        /// <param name="actor">Name of the actor. (Should be "..." if it's narration.)</param>
        /// <param name="line">The line of dialogue/narration.</param>
        /// <param name="color">The color of the actor's name.</param>
        public void ProgressDialogue(string actor, string line, ColorAnsi color = ColorAnsi.WHITE)
        {

            if (history.Count == 0)
            {
                historyChanged = true;
            }

            if (actor != "...")
            {
                List<string> spokenLines = Style.BreakLine(line, Width - speakerMaxWidth);

                List<string> speakerLines = new List<string>();
                for (int i = 0; i < spokenLines.Count; i++)
                {
                    if (i == 0)
                    {
                        if (actor.Length <= speakerMaxWidth)
                        {
                            speakerLines.Add(Style.GetRemainingSpace(actor, speakerMaxWidth - 3) + Style.Color(actor, color) + "   ");
                        }
                        else
                        {
                            speakerLines.Add("! NAME IS TOO LONG !" + Style.GetBlankLine(speakerMaxWidth));
                        }
                    }
                    else
                    {
                        speakerLines.Add(Style.GetBlankLine(speakerMaxWidth));
                    }
                }

                List<string> dialogueLineList = new List<string>();
                for (int i = 0; i < speakerLines.Count; i++)
                {
                    dialogueLineList.Add(speakerLines[i] + spokenLines[i]);
                }

                history.Add(string.Empty);
                history.AddRange(dialogueLineList);
            }
            else
            {
                List<string> spokenLines = Style.BreakLine(line, Width - 5);

                List<string> dialogueLineList = new List<string>();
                for (int i = 0; i < spokenLines.Count; i++)
                {
                    dialogueLineList.Add(Style.Color("     " + spokenLines[i], color));
                }

                history.Add(Style.GetBlankLine(Width));
                history.AddRange(dialogueLineList);
            }

            HasChanged = true;
        }

        /// <summary>
        /// Adds a new combat line to the bottom.
        /// </summary>
        /// <param name="name">Left side of the combat line.</param>
        /// <param name="actionDescription">Right side of the combat line.</param>
        /// <param name="color">The color of the feft side of the combat line.</param>
        public void ProgressCombat(string name, string actionDescription, ColorAnsi color = ColorAnsi.WHITE)
        {
            string _actorLine = Style.GetRemainingSpace(name, speakerMaxWidth - 1) + Style.Color(name, color) + " ";
            
            string _combatLine = actionDescription;

            history.Add(_actorLine + _combatLine);

            HasChanged = true;
        }

        /// <summary>
        /// Adds a new blank line to the bottom.
        /// </summary>
        public void ProgressBlank()
        {
            history.Add(string.Empty);
        }

        /// <summary>
        /// Clears the dialogue box.
        /// </summary>
        public void ClearDialogue()
        {
            history.Clear();

            historyChanged = true;
            HasChanged = true;
        }
    }
}
