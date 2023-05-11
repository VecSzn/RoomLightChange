#region Assembly XPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// C:\Users\VecSzn\Desktop\Main Files\SCP服务器配置\插件\XPlugin.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using CommandSystem;
using CustomPlayerEffects;
using HarmonyLib;
using Interactables;
using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using MapGeneration;
using MapGeneration.Distributors;
using MEC;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Core.Items;
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
                Timing.KillCoroutines(colorhanle);
            }

            colorhanle = Timing.RunCoroutine(RandomEvent());
        }
        public Dictionary<string,Color> Colors { get; set; } = new Dictionary<string, Color>
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
        public IEnumerator<float> RandomEvent()
        {
            while (Round.IsRoundStarted)
            {
                yield return Timing.WaitForSeconds(2);
                FlickerableLightController[] array2 = UnityEngine.Object.FindObjectsOfType<FlickerableLightController>();
                foreach (FlickerableLightController flc in array2)
                {
                    if (Config.Rooms.Contains(flc.Room.Name.ToString()))
                    {
                        flc.WarheadLightColor = Colors[Config.Colors[new System.Random(Environment.TickCount).Next(0, 14)]];
                    };
                }
            }
        }
    }
}
