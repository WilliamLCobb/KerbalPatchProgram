using System;
using BepInEx;
using I2.Loc;
using KSP.Game;
using UnityEngine;
using KSP.OAB;
using KSP.Sim;
using KSP.Sim.impl;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace KSPTestMod
{
    // Attribute that contains information about the plugin. Required for the plugin to be loaded
    [BepInPlugin("KerbalPatchProgram", "A collection of bug fixes for KSP2 bugs.", "0.1.0")]
    public class KSPUtilsMod : BaseUnityPlugin
    {
        public void Start()
        {
            var g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Component.Destroy(g.GetComponent<SphereCollider>());
            Component.Destroy(g.GetComponent<Rigidbody>());
            g.transform.localScale = new Vector3(0, 0, 0);
            g.AddComponent<KSPUtils>();
        }
    }

    public class KSPUtils : KerbalMonoBehaviour
    {

        private GameInstance gameInstance = null;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        public void Update()
        {
            if (gameInstance == null)
            {
                gameInstance = FindObjectOfType<GameInstance>();
            }

            if (gameInstance != null)
            {
                FixVesselSituation();
            }
        }

        private void FixVesselSituation()
        {
            var vessel = Game.ViewController.GetActiveVehicle(true)?.GetSimVessel();
            if(vessel==null)
            {
                return;
            }

            if (vessel.Landed && vessel.AltitudeFromTerrain > 50)
            {
                vessel.SimulationObject.objVesselBehavior.CheckSetLanded();
            }
        }
    }
}