using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace PoP.classes
{
    class FileInput
    {
        public static string[] GetAllLines(string filePath)
        {
            return File.ReadAllLines(filePath, Encoding.UTF8);
        }
        public static List<string> GetAllLinesAsList(string filePath)
        {
            return File.ReadAllLines(filePath, Encoding.UTF8).ToList();
        }

        public static Dictionary<string, object> GetJsonDict(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }

        public static List<Dictionary<string, object>> GetJsonDictList(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);
        }

        public static List<Effect> GetEffectList(string[] data)
        {
            List<Effect> effectList = new List<Effect>();
            
            foreach (string effect in data)
            {
                switch (effect)
                {
                    case "Burn":
                        effectList.Add(Effect.Burn);
                        break;

                    case "Freeze":
                        effectList.Add(Effect.Freeze);
                        break;

                    case "Stun":
                        effectList.Add(Effect.Stun);
                        break;

                    case "Poison":
                        effectList.Add(Effect.Poison);
                        break;

                    case "Bleed":
                        effectList.Add(Effect.Bleed);
                        break;

                    case "Buff":
                        effectList.Add(Effect.Buff);
                        break;

                    case "Debuff":
                        effectList.Add(Effect.Debuff);
                        break;
                }
            }

            return effectList;
        }
    }
}
