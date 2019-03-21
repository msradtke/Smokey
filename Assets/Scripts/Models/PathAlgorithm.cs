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
        public static int SemiGeneticGetNextLocation(GridArea gridArea, GridAreaLocation current, GridAreaLocation finishEntrance)
        {
            float entranceDirectionWeight = .9f; //tend to go towards entrance
            float straightLineWeight = .9f; //tendency to choose 
            float randomWeight = .1f;
            GridAreaLocation currentLocation = current;

            while (currentLocation != finishEntrance)
            {
                int yDirection = Random.Range(0, 2); //1 up
                int xDirection = Random.Range(0, 2); //1 right
                var nextLocation = new GridAreaLocation(currentLocation.X + xDirection, currentLocation.Y + yDirection);
                var direction = currentLocation.GetRelativeDirection(finishEntrance);
                Dictionary<int, float> nextWeightDict = new Dictionary<int, float>();
                for (int i = 0; i < 4; i++)
                {
                    float weight = .5f;
                    //0 top 1 right 2 down 4 left
                    if (i == 1 || i == 4)
                        nextWeightDict.Add(i, weight + entranceDirectionWeight);
                }
                return 0;
            }
        }
        static List<int> GetNextCell(Dictionary<int,float> nextWeightDict)
        {
            List<int> next = new List<int>();
            List<int> neighbors = nextWeightDict.Keys.ToList();

            int count = nextWeightDict.Count; // should be 4

            for (int i = 0; i < count; i++)
            {
                float totalWeight = neighbors.Sum(x=>nextWeightDict[x]);
                List<float> weights = new List<float>();
                neighbors.ForEach(x => weights.Add(nextWeightDict[x] / totalWeight));

                var r = Random.Range(0, 1);
                foreach(var n in neighbors)
                {
                    if(r < nextWeightDict[n])
                    {
                        next.Add(n);
                        neighbors.Remove(n);
                        break;
                    }
                }

            }
            return next;
        }

        
    }
}
