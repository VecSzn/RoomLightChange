#region Assembly XPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// C:\Users\VecSzn\Desktop\Main Files\SCP服务器配置\插件\XPlugin.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

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
        public List<RoomName> Rooms { get; set; } = new List<RoomName>
        {
            RoomName.Lcz914,
        };

        [Description("可变换颜色")]
        public List<Color> Colors { get; set; } = new List<Color>
        {
            Color.red,
            Color.yellow,
            Color.green,
            Color.blue,
            Color.cyan,
            Color.white,
            Color.magenta
        };
    }
}