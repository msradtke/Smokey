using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        target = GameUtility.CameraTarget;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
    
}
