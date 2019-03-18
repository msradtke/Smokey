using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class EntityTrigger : MonoBehaviour
    {
        void Start()
        {
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject == null)
                    return;
                var script = collision.gameObject.GetComponent<MoveEntity>();
                script.IsCollided = true;
                Debug.Log("Collided");
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            //if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject == null)
                    return;
                var script = collision.gameObject.GetComponent<MoveEntity>();
                script.IsCollided = false;
                Debug.Log("Exit Collided");
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log("collider stay");
        }
        
    }
}
