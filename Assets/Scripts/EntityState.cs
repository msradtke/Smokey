using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class EntityState : MonoBehaviour
    {
        public Collider Collider;
        private void Start()
        {
            var circle = gameObject.GetComponentInChildren<LineRenderer>();
            var collider = gameObject.GetComponent<Collider2D>();
            EntityUtility.GetSelectionCircle(collider, circle);
            circle.enabled = false;
        }
        private void FixedUpdate()
        {
            
        }

        public void Unselect()
        {
            Debug.Log("Unselect");
            var circle = gameObject.GetComponentInChildren<LineRenderer>();
            if (circle != null)
                circle.enabled = false;
        }

        public void Select()
        {
            var circle = gameObject.GetComponentInChildren<LineRenderer>();
            if (circle != null)
            {
                circle.enabled = true;
            }
        }
    }
}
