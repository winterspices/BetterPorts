using BepInEx;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterPorts
{
    [BepInDependency("com.winter.customislandapi")]
    [BepInPlugin("com.winter.betterports", "Better Ports", "1.0")]
    public class PortPatcher : BaseUnityPlugin
    {
        public const string pluginGuid = "com.winter.betterports";
        public const string pluginName = "Better Ports";
        public const string pluginVersion = "1.0";

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "com.winter.betterports");
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.P))
            {
                //Type type = typeof(PortLadder);

                //Debug.Log(type.FullName);

                //MethodInfo awake = type.GetMethod(
                //    "Start",
                //    BindingFlags.Instance |
                //    BindingFlags.Public |
                //    BindingFlags.NonPublic |
                //    BindingFlags.DeclaredOnly);

                //if (awake != null)
                //{
                //    Debug.Log($"Awake declared by {type.FullName}");
                //}
            }
        }

        private IEnumerator GetShaderFromScene()
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(27, LoadSceneMode.Additive);

            yield return load;

            Scene scene = SceneManager.GetSceneByBuildIndex(27);

            HashSet<Shader> shaders = new HashSet<Shader>();

            Debug.Log("Searching for shaders...");

            foreach (GameObject root in scene.GetRootGameObjects())
            {
                Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);

                foreach (Renderer renderer in renderers)
                {
                    foreach (Material material in renderer.sharedMaterials)
                    {
                        if (material != null && material.shader != null)
                        {
                            shaders.Add(material.shader);
                            //Debug.Log($"Found shader: {material.shader.name}");
                        }
                    }
                }
            }

            Debug.Log($"{shaders.Count} shaders found");

            foreach (Shader s in shaders)
            {
                Debug.Log($"Found shader: {s.name}");
            }

            yield return SceneManager.UnloadSceneAsync(scene);
        }
    }
}
