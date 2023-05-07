using PoP.classes.windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.classes
{
    internal class WindowRenderer
    {
        public static int Height { get; private set; } = 63;
        public static int Width { get; private set; } = 237;

        private static List<string> windowLines = new List<string>(Height);

        public static MapWindow Map = new MapWindow();
        public static StatsWindow Stats = new StatsWindow();
        public static GearWindow Gear = new GearWindow();
        public static InventoryWindow Inventory = new InventoryWindow();
        public static DialogueWindow Dialogue = new DialogueWindow();

        public static void Initialize()
        {
            Map.IsEnabled = true;
            Stats.IsEnabled = true;
            Gear.IsEnabled = true;
            Inventory.IsEnabled = true;
            Dialogue.IsEnabled = true;
        }

        public static List<string> Get()
        {
            windowLines.Clear();

            for (int _ = 0; _ < Height - 1; _++)
            {
                windowLines.Add(string.Empty);
            }

            // MMM § I      CCC S I
            // MMM G I      CCC S I
            // DDDDDDD      DDDDDDD

            Border border = new Border(true, false, false, true);
            Border borderFull = new Border(false, true, true, false);

            if (Map.IsEnabled)
            {
                AddWindow(border.Surround(Map.GetLines, Map.Width));
            }

            if (Stats.IsEnabled)
            {
                border.TopLeft = Border.DOUBLE_T_TOP;
                AddWindow(border.Surround(Stats.GetLines, Stats.Width));
            }

            if (Gear.IsEnabled)
            {
                border.TopLeft = Border.DOUBLE_T_LEFT;
                AddWindow(border.Surround(Gear.GetLines, Gear.Width), Stats.Height + 2);
            }

            if (Inventory.IsEnabled)
            {
                border.IntersectionList.Add(new Intersection(BorderSide.Left, Stats.Height + 1, Border.DOUBLE_T_RIGHT));

                border.TopLeft = Border.DOUBLE_T_TOP;
                AddWindow(border.Surround(Inventory.GetLines, Inventory.Width));
                border.IntersectionList.Clear();
            }

            if (Dialogue.IsEnabled)
            {
                border.IntersectionList.Add(new Intersection(BorderSide.Top, Map.Width + 1, Border.DOUBLE_T_BOTTOM));
                border.IntersectionList.Add(new Intersection(BorderSide.Top, Map.Width + Stats.Width + 2, Border.DOUBLE_T_BOTTOM));

                border.TopLeft = Border.DOUBLE_T_LEFT;
                AddWindow(border.Surround(Dialogue.GetLines, Dialogue.Width), Map.Height + 2);
                border.IntersectionList.Clear();
            }


            borderFull.IntersectionList.Add(new Intersection(BorderSide.Right, 0, Border.DOUBLE_TOPRIGHT));
            borderFull.IntersectionList.Add(new Intersection(BorderSide.Right, Inventory.Height + 1, Border.DOUBLE_T_RIGHT));
            borderFull.IntersectionList.Add(new Intersection(BorderSide.Bottom, 0, Border.DOUBLE_BOTTOMLEFT));
            return borderFull.Surround(windowLines, Width - 1);
        }

        private static void AddWindow(List<string> lineList, int startIndex = 1)
        {
            for (int i = 0; i < lineList.Count; i++)
            {
                windowLines[i + (startIndex - 1)] += lineList[i];
            }
        }

        public static void Enable(Window window)
        {
            window.IsEnabled = true;
            window.HasChanged = true;
        }

        public static void Disable(Window window)
        {
            window.IsEnabled = false;
        }
    }
}
