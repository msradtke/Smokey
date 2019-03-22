using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Models
{
	public static class PathAlgorithm
	{
		public static List<GridAreaLocation> SemiGeneticGetNextLocation (GridArea gridArea, GridAreaLocation current, GridAreaLocation finishEntrance, GridAreaLocation previousLocation)
		{
			float entranceDirectionWeight = 700f; //tend to go towards entrance
			float straightLineWeight = .7f; //tendency to choose 
			float randomWeight = .1f;
			GridAreaLocation currentLocation = current;

			GridAreaLocation previousVector = previousLocation;
			if (previousLocation == null) {
				previousVector = new GridAreaLocation (0,0);
				straightLineWeight = 0;
			}
			//int yDirection = Random.Range (0, 2); //1 up
			//int xDirection = Random.Range (0, 2); //1 right
			//var nextLocation = new GridAreaLocation (currentLocation.X + xDirection, currentLocation.Y + yDirection);
			var direction = currentLocation.GetRelativeDirection (finishEntrance);
			Dictionary<GridAreaLocation, float> nextWeightDict = new Dictionary<GridAreaLocation, float> ();
								 
			for (int i = 0; i < 4; i++) {
				float weight = randomWeight;
				var v = NeighborToLocation (i);
				if (v.X == direction.x) {
					weight += entranceDirectionWeight;
					if (v.X == previousVector.X)
						weight += straightLineWeight;
				}
				if (v.Y == direction.y) {
					weight += entranceDirectionWeight;
					if (v.Y == previousVector.Y)
						weight += straightLineWeight;
				}
				nextWeightDict.Add (v, weight);

			}
			var wints = GetNextCell (nextWeightDict);
			return wints;
            
		}

		static List<GridAreaLocation> GetNextCell (Dictionary<GridAreaLocation,float> nextWeightDict)
		{
			List<GridAreaLocation> next = new List<GridAreaLocation> ();
			List<GridAreaLocation> neighbors = nextWeightDict.Keys.ToList ();

			int count = nextWeightDict.Count; // should be 4

			for (int i = 0; i < count; i++) {
				float totalWeight = neighbors.Sum (x => nextWeightDict [x]);
				List<float> weights = new List<float> ();

                float sum = 0;
                foreach (var n in neighbors)
                {
                    var nxt = nextWeightDict[n];
                    weights.Add((nxt + sum) / totalWeight);
                    sum += nxt;
                }
                int cnt= 0;
				float r = Random.Range (0f, 1f);
				foreach (var n in neighbors) {
					if (r < weights[cnt]) {
						next.Add (n);
						neighbors.Remove (n);
						break;
					}
                    cnt++;
				}
			}
			return next;
		}

		static GridAreaLocation NeighborToLocation (int n)
		{
			switch (n) {
			case 0:
				return new GridAreaLocation (0, 1);
			case 1:
				return new GridAreaLocation (1, 0);
			case 2:
				return new GridAreaLocation (0, -1);
			case 3:
				return new GridAreaLocation (-1, 0);
			default:
				return null;
			}
		}

	}
}
