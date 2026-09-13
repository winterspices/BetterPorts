using BepInEx;
using CustomIslandAPI;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterPorts
{
    public class Patches
    {
        public static Dictionary<string, TerrainData> data = new Dictionary<string, TerrainData>();
        public static bool terrainInstalled;
        public static GameObject drydockAsset;
        public static GameObject drydock;
        public static GameObject islandAsset;
        public static GameObject island;
        public static GameObject shelfAsset;
        public static GameObject shelf;
        public static GameObject ladderAsset;
        public static GameObject ladders;

        public static List<Port> ports;

        [HarmonyPatch(typeof(FloatingOriginManager))]
        public static class FloatingOriginManagerPatches
        {
            [HarmonyPrefix]
            [HarmonyPatch("Start")]
            public static void StartPatch(FloatingOriginManager __instance)
            {
                if (__instance.name == "_shifting world")
                {
                    TerrainSetup();

                    if (data.Count > 0)
                    {
                        Transform fa = GameObject.Find("island 15 M (Fort)").transform.Find("Terrain");
                        fa.GetComponent<Terrain>().terrainData = data["fa"];
                        fa.GetComponent<TerrainCollider>().terrainData = data["fa"];

                        Transform grc = GameObject.Find("island 1 A (gold rock)").transform.Find("Terrain");
                        grc.GetComponent<Terrain>().terrainData = data["grc"];
                        grc.GetComponent<TerrainCollider>().terrainData = data["grc"];

                        Transform dc = GameObject.Find("island 9 E (dragon cliffs)").transform.Find("Terrain");
                        dc.GetComponent<Terrain>().terrainData = data["dc"];
                        dc.GetComponent<TerrainCollider>().terrainData = data["dc"];
                        GameObject.Find("island 9 E (dragon cliffs)").transform.Find("Cube_003").transform.localPosition = new Vector3(525f, -14f, -218f);
                        GameObject.Find("island 9 E (dragon cliffs)").transform.Find("Cube_005").transform.localPosition = new Vector3(525f, 3f, -233f);

                        Debug.LogWarning("[Better Ports] Custom ports loaded");
                    }

                    // initialise the drydock, but don't add it to the FA scenery just yet
                    if (drydockAsset)
                    {
                        drydock = UnityEngine.Object.Instantiate<GameObject>(drydockAsset, __instance.transform);

                        Transform island = GameObject.Find("island 15 M (Fort)").transform;
                        drydock.transform.SetParent(island);
                        drydock.transform.localPosition = new Vector3(156.6f, 0f, 21.2f);

                        Debug.LogWarning("[Better Ports] Drydock loaded at Fort Aestrin");
                    }
                    else
                    {
                        Debug.LogError("[Better Ports] Could not load dry dock due to prefab not loading correctly. Did you assign the dry dock prefab to an asset bundle in Unity?");
                    }

                    // initialise the custom island
                    if (islandAsset)
                    {
                        island = UnityEngine.Object.Instantiate<GameObject>(islandAsset, __instance.transform);

                        IslandManager.InitialiseIsland(island, 67, PortRegion.emerald, Currency.emerald);

                        Debug.LogWarning("[Better Ports] Island loaded");
                    }
                    else
                    {
                        Debug.LogError("[Better Ports] Could not load island due to prefab not loading correctly. Did you assign the island prefab to an asset bundle in Unity?");
                    }

                    // initialise the custom island continental shelf
                    if (shelfAsset)
                    {
                        shelf = UnityEngine.Object.Instantiate<GameObject>(shelfAsset, __instance.transform);

                        Debug.LogWarning("[Better Ports] Island shelf loaded");
                    } else
                    {
                        Debug.LogError("[Better Ports] Could not load island shelf due to prefab not loading correctly. Did you assign the island prefab to an asset bundle in Unity?");
                    }

                    // initialise the ladders
                    if (ladderAsset)
                    {
                        GameObject fa = GameObject.Find("island 15 M (Fort)");
                        ladders = UnityEngine.Object.Instantiate<GameObject>(ladderAsset, fa.transform);
                        ladders.transform.parent = fa.transform;

                        foreach (Transform child in ladders.transform)
                        {
                            child.gameObject.AddComponent<PortLadder>();
                        }

                        Debug.LogWarning("[Better Ports] Port ladders installed");
                    } else
                    {
                        Debug.LogError("[Better Ports] Could not load port ladders due to prefab not loading correctly. Did you assign the island prefab to an asset bundle in Unity?");
                    }
                }
            }
        }

        private static void TerrainSetup()
        {
            try
            {
                string path = Paths.PluginPath + "\\Better Ports\\betterports";
                if (!File.Exists(path))
                {
                    Debug.LogError("Better Ports not installed correctly");
                    terrainInstalled = false;
                }
                else
                {
                    AssetBundle bundle = AssetBundle.LoadFromFile(path);

                    data["fa"] = bundle.LoadAsset<TerrainData>("fa");
                    data["grc"] = bundle.LoadAsset<TerrainData>("grc");
                    data["dc"] = bundle.LoadAsset<TerrainData>("dc");

                    // load the dry dock in
                    drydockAsset = (bundle.LoadAsset("Assets/Better Ports/drydock.prefab") as GameObject);

                    // custom island
                    islandAsset = (bundle.LoadAsset("Assets/Better Ports/island 67 Bottleneck.prefab") as GameObject);

                    // custom island continent shelf
                    shelfAsset = (bundle.LoadAsset("Assets/Better Ports/continent shelf bottleneck.prefab") as GameObject);

                    // custom island scenery
                    AssetBundle bundle2 = AssetBundle.LoadFromFile(Paths.PluginPath + "\\Better Ports\\betterportsscenery");
                    IslandManager.sceneIndexes.Add(67, "Assets/Better Ports/Scenes/island 67 Bottleneck.unity");

                    // add ladders to ports
                    ladderAsset = (bundle.LoadAsset("Assets/Better Ports/ladders.prefab") as GameObject);

                    terrainInstalled = true;
                    Debug.Log("[Better Ports] Bundles loaded");
                }
            }
            catch
            {
                Debug.LogError("Uh oh! Something went wrong with loading better port assets.");
            }
        }
    }
}
