using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PoP.classes
{
    class Action
    {
        public static Dictionary<string, Func<object, object>> actions = new Dictionary<string, Func<object, object>>();

        public Action()
        {
            actions.Add("getitem", (param) => {
                string[] parameters = JsonSerializer.Deserialize<string[]>(param.ToString());
                switch (parameters[0])
                {
                    case "weapon":
                        ItemFactory.CreateWeapon(parameters[1], double.Parse(parameters[2]), (Slot)Enum.Parse(typeof(Slot), parameters[3])).Collect();
                        break;
                    case "armor":
                        ItemFactory.CreateArmor(parameters[1], double.Parse(parameters[2]), (Slot)Enum.Parse(typeof(Slot), parameters[3])).Collect();
                        break;
                    default:
                        break;
                }
                return true;
            });
        }
    }
}
