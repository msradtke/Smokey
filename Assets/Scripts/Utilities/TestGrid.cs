using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class TestGrid
    {
        static readonly int _areaWidthMin = 5;
        static readonly int _areaWidthMax = 15;
        static readonly int _areaHeightMin = 5;
        static readonly int _areaHeightMax = 15;
        static System.Random ran;

        static TestGrid()
        {
            ran = new System.Random();
        }

        public static GridArea GetGridArea()
        {
            int width = ran.Next(_areaWidthMin, _areaWidthMax);
            int height = ran.Next(_areaHeightMin, _areaHeightMax);
            var gridArea = GridUtility.NewGridArea(width, height);
            //var location = GridUtility.GridLocation;

            return gridArea;
        }

        public static Chip GetCpu()
        {
            var numPorts = 4;
            var portPos = GetRandomPorts(numPorts);
            var ports = new List<Port>();
            foreach (var pos in portPos)
                ports.Add(new Port(pos));
            var cpu = new Chip(ChipSize.Small, ports);
            return cpu;

        }

        public static Chip GetBoostChip()
        {
            var ports = new List<Port>();
            var p = new Port(ran.Next(1, 12));
            var boostChip = new Chip(ChipSize.Small,new List<Port> { p });
            return boostChip;
        }

        static List<int> GetRandomPorts(int count)
        {
            var list = new List<int>();
            int num = 0;
            int i = 0;
            while (i < count)
            {
                num = ran.Next(1, 12);
                if (list.Contains(num))
                    continue;
                list.Add(num);
                ++i;
            }
            return list;
        }
    }
}
