using HarmonyLib;
using UnityEngine;
using System.Linq;

namespace BetterPorts
{
    [HarmonyPatch(typeof(Port))]
    public class IslandPortPatcher
    {
        [HarmonyPrefix]
        [HarmonyPatch("Start")]
        public static bool StartPatch(Port __instance, ref Port[] ___destinationPorts)
        {
            if (Patches.ports == null)
            {
                Patches.ports = GameObject.FindObjectsOfType<Port>().ToList();
            }

            if (__instance.name == "port 67 Bottleneck")
            {
                ___destinationPorts[0] = Patches.ports.FirstOrDefault(p => p.name == "port E 9 (Dragon cliffs)");
                ___destinationPorts[1] = Patches.ports.FirstOrDefault(p => p.name == "port E 13 Sage Hills");
                ___destinationPorts[2] = Patches.ports.FirstOrDefault(p => p.name == "port E 14 Serpent Isle");
                ___destinationPorts[3] = Patches.ports.FirstOrDefault(p => p.name == "port E 12 New Port");
                ___destinationPorts[4] = Patches.ports.FirstOrDefault(p => p.name == "port E 10 sanctuary");
                ___destinationPorts[5] = Patches.ports.FirstOrDefault(p => p.name == "port E 11 crab beach");
                ___destinationPorts[6] = Patches.ports.FirstOrDefault(p => p.name == "port E 29 (jungle)");
                ___destinationPorts[7] = Patches.ports.FirstOrDefault(p => p.name == "port L 22 Lagoon Bay");
                ___destinationPorts[8] = Patches.ports.FirstOrDefault(p => p.name == "port A0 (Gold Rock)");

            }

            return true;
        }
    }
}
