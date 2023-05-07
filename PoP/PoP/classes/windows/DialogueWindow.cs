using System;
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
                AddLine($" {Style.Color("Next [SPACE]", history.Count > 0 ? ColorAnsi.WHITE : ColorAnsi.DARK_GREY)}  {Style.Color(Border.SINGLE_VERTICAL, ColorAnsi.DARK_GREY)}  {Style.Color("Skip [ENTER]", ColorAnsi.DARK_GREY)}");

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
                    AddLine($" {Style.Color("Next [SPACE]", history.Count > 0 ? ColorAnsi.WHITE : ColorAnsi.DARK_GREY)}  {Style.Color(Border.SINGLE_VERTICAL, ColorAnsi.DARK_GREY)}  {Style.Color("Skip [ENTER]", ColorAnsi.DARK_GREY)}");

                    historyChanged = false;
                }

                return LineList;
            }
        }

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

        public void ProgressCombat(string name, List<string> lineList, ColorAnsi color = ColorAnsi.WHITE)
        {
            List<string> actorLines = new List<string>();
            for (int i = 0; i < lineList.Count; i++)
            {
                if (i == 0)
                {
                    actorLines.Add(Style.GetRemainingSpace(name, speakerMaxWidth - 3) + Style.Color(name, color) + "   ");
                }
                else
                {
                    actorLines.Add(Style.GetBlankLine(speakerMaxWidth));
                }
            }

            List<string> combatLineList = new List<string>();
            for (int i = 0; i < actorLines.Count; i++)
            {
                combatLineList.Add(actorLines[i] + lineList[i]);
            }

            history.AddRange(combatLineList);

            HasChanged = true;
        }

        public void ClearDialogue()
        {
            history.Clear();

            historyChanged = true;
            HasChanged = true;
        }
    }
}
