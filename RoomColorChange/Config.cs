using System.Collections.Generic;
using System.ComponentModel;
using MapGeneration;
using PlayerRoles;
using PluginAPI.Core.Zones;
using UnityEngine;

namespace RoomLightChange
{
    public class Config
    {
        [Description("Debug")]
        public bool Debug { get; set; } = true;

        [Description("是否开启变色插件")]
        public bool Enable { get; set; } = true;


        [Description("可变色房间")]
        public List<string> Rooms { get; set; } = new List<string>
        {
            "Lcz914",
        };

    
        [Description("可变换颜色")]
        public List<string> Colors { get; set; } = new List<string>
        {
            "red",
            "yellow",
            "green",
            "blue",
            "cyan",
            "white",
            "magenta",
        };
    }
}