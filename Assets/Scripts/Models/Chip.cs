using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.Models
{
	public class Chip : GridComponent
    {
        public ChipSize Size { get; private set; }
        public int PortCount { get; private set; }


        public Chip(ChipSize size, List<Port> ports)
        {
            Size = size;
            PortCount = ports.Count;
            Ports = ports;
        }
        public int GetMaxPortCount()
        {
            return (int)Size * 3 * 4; //3 per side, 4 side 
        }

        public List<Port> Ports { get; private set; }



    }
    public enum ChipSize
    {
        Small,
        Medium,
        Large
    }
}
