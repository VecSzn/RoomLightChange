using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using CommandSystem;
using CustomPlayerEffects;
using HarmonyLib;
using Interactables;
using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using JetBrains.Annotations;
using MapGeneration;
using MapGeneration.Distributors;
using MEC;
using PlayerRoles;
using PlayerRoles.Voice;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Core.Items;
using PluginAPI.Core.Zones;
using PluginAPI.Enums;
using PluginAPI.Events;
using UnityEngine;

namespace RoomLightChange
{
    public class Plugin
    {
        public static PluginHandler _pluginHandler;

        public CoroutineHandle AutoNukeHandle;


        [PluginConfig]
        public Config Config;

        private CoroutineHandle colorhanle;

        public List<Scp079Generator> _generators = new List<Scp079Generator>();

        public static Plugin Instance { get; private set; }

        public List<FlickerableLightController> Flicks = new List<FlickerableLightController>();

        [PluginEntryPoint("RoomLightChange", "RoomLightChange", "1.0.0", "Vec")]
        private void LoadPlugin()
        {
            Instance = this;
            EventManager.RegisterEvents(this);
        }

        [PluginEvent(ServerEventType.RoundStart)]
        private void OnRoundStart()
        {
            if (colorhanle.IsRunning)
            {
               Flicks.Clear();
                Timing.KillCoroutines(colorhanle);
            }
            int found = 0;
            int total = ChangeToName().Count();
            Log.Info($"Found:{found} Total:{total}");
            FlickerableLightController[] array2 = UnityEngine.Object.FindObjectsOfType<FlickerableLightController>();
            foreach (FlickerableLightController flc in array2)
            {
                if (found >= total)
                {
                    break;
                }
                if (ChangeToName().Contains(flc.Room.Name) == true)
                {
                   Flicks.Add(flc);
                };
            }
            colorhanle = Timing.RunCoroutine(RandomEvent());
        }
        public Dictionary<string, Color> Colors { get; set; } = new Dictionary<string, Color>
        {
            {
                "red",
                Color.red
            },
            {
                "yellow",
                Color.yellow
            },
            {
                "green",
                Color.green
            },
            {
                "blue",
                Color.blue
            },
            {
                "cyan",
                Color.cyan
            },
            {
                "white",
                Color.white
            },
            {
                "magenta",
                Color.magenta
            },
        };
        public List<string> RoomTypes { get; set; } = new List<string>
        {
        "Unnamed",
        "LczClassDSpawn",
        "LczComputerRoom",
        "LczCheckpointA",
        "LczCheckpointB",
        "LczToilets",
        "LczArmory",
        "Lcz173",
        "LczGlassroom",
        "Lcz330",
        "Lcz914",
        "LczGreenhouse",
        "LczAirlock",
        "HczCheckpointToEntranceZone",
        "HczCheckpointA",
        "HczCheckpointB",
        "HczWarhead",
        "Hcz049",
        "Hcz079",
        "Hcz096",
        "Hcz106",
        "Hcz939",
        "HczMicroHID",
        "HczArmory",
        "HczServers",
        "HczTesla",
        "EzCollapsedTunnel",
        "EzGateA",
        "EzGateB",
        "EzRedroom",
        "EzEvacShelter",
        "EzIntercom",
        "EzOfficeStoried",
        "EzOfficeLarge",
        "EzOfficeSmall",
        "Outside",
        "Pocket",
        "HczTestroom"
        };
        public int GetNum(string wanted)
        {
            int roomNames = 0;
            foreach (string name in RoomTypes)
            {
                roomNames++;
                if(name == wanted)
                {

                    break;    
                }
            }
            return roomNames;
        }
        public List<RoomName> ChangeToName()
        {
           List<RoomName> roomNames = new List<RoomName>();
            foreach (string name in Config.Rooms)
            {
                roomNames.Add((RoomName)(GetNum(name)-1));
            }
            return roomNames;
        }

        public IEnumerator<float> ChangeColor()
        {
            foreach (FlickerableLightController flc in Flicks)
            {
                    flc.Network_warheadLightOverride = true;
                    flc.NetworkLightsEnabled = true;
                    flc.Network_warheadLightColor = Colors[Config.Colors[new System.Random(Environment.TickCount).Next(1, 7)]];
            }
            yield return Timing.WaitForSeconds(0.001f);
        }
    public IEnumerator<float> RandomEvent()
        {
            while (true)
            {
                Timing.RunCoroutine(ChangeColor());
                yield return Timing.WaitForSeconds(2f);
            }
        }
    }
}
