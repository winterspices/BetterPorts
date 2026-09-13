using HarmonyLib;
using UnityEngine;

namespace BetterPorts
{
    internal class MethodPatches
    {
        [HarmonyPatch(typeof(Shipyard))]
        public class Patch_Shipyard
        {
            [HarmonyPrefix]
            [HarmonyPatch("DischargeShip")]
            public static bool DischargeShipPatch(Shipyard __instance)
            {
                GameObject drydock = Patches.drydock;
                drydock.transform.Find("water mask").gameObject.SetActive(false);
                drydock.transform.Find("drydock left").localRotation = Quaternion.Euler(0f, 0f, 135f);
                drydock.transform.Find("drydock right").localRotation = Quaternion.Euler(0f, 0f, -135f);

                return true;
            }

            [HarmonyPostfix]
            [HarmonyPatch("ActivateDocuments")]
            public static void ActivateDocumentsPatch(ShipyardDocuments __instance)
            {
                GameObject drydock = Patches.drydock;
                drydock.transform.Find("water mask").gameObject.SetActive(true);
                drydock.transform.Find("drydock left").localRotation = Quaternion.Euler(0f, 0f, 0f);
                drydock.transform.Find("drydock right").localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
}
