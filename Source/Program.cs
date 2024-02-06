using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using WCSharp.Api;
using WCSharp.Events;
using WCSharp.Shared;
using WCSharp.Shared.Data;
using WCSharp.Shared.Extensions;
using WCSharp.Sync;
using static WCSharp.Api.Common;

namespace Source
{
    public class Program
    {
        private static timer RoundInterval;
        private static timerdialog Timerwindow;
        private static int waveint = 1;
        public static int TotalControlPoint = 0;
        public static int GoldPerPoint;
        public static List<CustomPlayer> Players { get; } = new List<CustomPlayer>();
        public static bool Debug { get; private set; } = false;
        public static group ControlPointGroup { get; } = CreateGroup();

        public static void Main()
        {
            // Delay a little since some stuff can break otherwise
            var timer = CreateTimer();
            TimerStart(timer, 0.01f, false, () =>
            {
                DestroyTimer(timer);
                Start();
            });
            for (var i = 0; i < 12; i++)
            {
                Players.Add(new CustomPlayer(Player(i)));
            }
        }

        private static void Start()
        {
            RoundInterval = CreateTimer();
            TimerStart(RoundInterval, 60, true, TimerFinished);

            Timerwindow = CreateTimerDialog(RoundInterval);
            TimerDialogDisplay(Timerwindow, true);
            TimerDialogSetTitle(Timerwindow, $"Turn {waveint}");
        }

        private static void TimerFinished()
        {
            waveint++;
            for (var i = 0; i < 12; i++)
            {
                int GoldPerPoint = TotalControlPoint * 100;
                SetPlayerState(Player(i), PLAYER_STATE_RESOURCE_GOLD, GetPlayerState(Player(i), PLAYER_STATE_RESOURCE_GOLD) + GoldPerPoint);
            }
            TimerDialogSetTitle(Timerwindow, $"Turn {waveint}");
        }

        public static void IntGroup()
        {
            GroupEnumUnitsInRange(Program.ControlPointGroup, 256, 256, 65536, null);
            foreach (var unit in Program.ControlPointGroup.ToList())
            {
                switch (unit.UnitType)
                {
                    case w;
                        // code block
                        break;
                    default:
                        // code block
                        break;
                }

            }
        }
    }
    public class CustomPlayer
    {
        public player Wc3Player { get; }
        public int GoldPerPoint { get; set; }
        public int TotalControlPoint { get; set; }

        public CustomPlayer(player wc3player)
        {
            Wc3Player = wc3player;
        }
    }
}