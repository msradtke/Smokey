using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLogic : MonoBehaviour {
    bool isGridActive;
    Vector3 savedPosition;
    float savedZoom;

    public Camera Cam;
    public float Zoom = 2f;
    
    // Use this for initialization
    void Start () {
        savedPosition = Camera.main.transform.position;
        savedZoom = Cam.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("KeyDown");
            if (isGridActive)
                CloseGrid();
            else
                ShowGrid();
        }
    }

    void ShowGrid()
    {
        savedZoom = Cam.orthographicSize;
        Cam.orthographicSize = Zoom;
        GameUtility.IsGridActive = true;
        savedPosition = Cam.transform.position;
        isGridActive = true;
        Cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Cam.transform.position.z);
    }

    void CloseGrid()
    {
        Cam.orthographicSize = savedZoom;
        GameUtility.IsGridActive = false;
        Cam.transform.position = savedPosition;
        isGridActive = false;
    }
}
