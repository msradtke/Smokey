using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class MoveEntity : MonoBehaviour
    {

        Rigidbody2D rb2d;
        SpriteRenderer sprRender;
        
        public float Speed = 10;
        public float RotationSpeed = 1;
        public Camera Camera;
        public bool IsCollided { get; set; }

        private void Start()
        {

            rb2d = GetComponent<Rigidbody2D>();
            sprRender = GetComponent<SpriteRenderer>();
            GameUtility.CameraTarget = gameObject.transform;
            //rb2d.centerOfMass = sprRender.sprite.pivot;
        }

        private void FixedUpdate()
        {
            if (GameUtility.IsGridActive)
                return;
            HandleMovement();
            Camera.transform.position = new Vector3(rb2d.transform.position.x, rb2d.transform.position.y, Camera.transform.position.z);
            
        }
        void TestMovement()
        {
            var pos = transform.position;
            var buffer = pos;
            float rot = 1;
            if (Input.GetKey(KeyCode.D)) //rotate right
            {
                rb2d.MoveRotation(rb2d.rotation + 1*RotationSpeed*Time.deltaTime*-1);
                
                //buffer += Vector3.right;
                //if (Input.GetKey(KeyCode.S))
                    
                //    transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * RotationSpeed, Space.World);
                //if (Input.GetKey(KeyCode.W))
                //    transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * RotationSpeed, Space.World);

            }
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.S))
                    transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * RotationSpeed, Space.World);
                if (Input.GetKey(KeyCode.W))
                    transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * RotationSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.W))
            {
                //transform.position += transform.up * Time.deltaTime* Speed;
                transform.position += transform.up * Speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {

                transform.position += transform.up * Time.deltaTime * Speed * -1;

            }
        }
        void HandleMovement()
        {
            var pos = transform.position;
            var buffer = pos;
            float rot = 1;
            if (Input.GetKey(KeyCode.D)) //rotate right
            {
                if (Input.GetKey(KeyCode.S))
                    transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * RotationSpeed, Space.World);
                else if (Input.GetKey(KeyCode.W))
                    transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * RotationSpeed, Space.World);
                else
                    transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * RotationSpeed, Space.World);

            }
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.S))
                    transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * RotationSpeed, Space.World);
                else if (Input.GetKey(KeyCode.W))
                    transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * RotationSpeed, Space.World);
                else
                    transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * RotationSpeed, Space.World);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.up * Speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                //rb2d.MovePosition(new Vector2(rb2d.position.x, rb2d.position.y + Time.deltaTime * Speed * -1));
                transform.position += transform.up * Time.deltaTime * Speed * -1;
            }
            pos = buffer;
            //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * Speed);
        }

    }
}
