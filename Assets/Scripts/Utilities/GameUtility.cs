using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
namespace Assets.Scripts.Utilities

{
    public static class GameUtility
    {
        public readonly static int ChunkSize = 12;
        public static Transform CameraTarget { get; set; }
        public static bool IsGridActive = false;
        public static void LoadEntity()
        {
            
        }

		public static Color GetColor(float r, float g, float b, float a)
		{
			return new Color (r, g, b, a);
		}
		public static Color GetRandomColor(bool randomAlpha = false)
		{
			if(randomAlpha)
				return new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
			return new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), 1f);

		}

    }
}
