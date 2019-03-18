using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class EntityUtility
    {
        public static LineRenderer GetSelectionCircle(Collider2D collider, LineRenderer line)
        {
            Debug.Log("get selection circle");
            if (collider == null)
            {
                Debug.Log("collider is null");
                return null;
            }
            
            var width = collider.bounds.size.x;
            var height = collider.bounds.size.y;

            var size = width > height ? width : height;
            int segments = 50;
            float xradius = 5;
            float yradius = 5;
            line.positionCount = segments + 1;
            line.useWorldSpace = false;

            float x;
            float y;
            float z;
            line.startWidth = .05f;
            line.endWidth = .05f;

            float angle = 20f;

            for (int i = 0; i < (segments + 1); i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * size * 2;
                y = Mathf.Cos(Mathf.Deg2Rad * angle) * size * 2;

                line.SetPosition(i, new Vector3(x, y, 0));

                angle += (360f / segments);


            }

            line.sortingLayerName = "Entities";

            return line;
        }
    }
}
