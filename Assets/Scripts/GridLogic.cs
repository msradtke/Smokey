using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLogic : MonoBehaviour
{
    bool isGridActive;
    Vector3 savedPosition;
    float savedZoom;

    public Camera Cam;
    public float Zoom = 2f;
    public Transform Grid;
    public Transform GridParent;

    // Use this for initialization
    void Start()
    {
        savedPosition = Camera.main.transform.position;
        savedZoom = Cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
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
        GenerateRandomGrid();
        Cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Cam.transform.position.z);
    }

    void CloseGrid()
    {
        Cam.orthographicSize = savedZoom;
        GameUtility.IsGridActive = false;
        Cam.transform.position = savedPosition;
        isGridActive = false;
    }

    void GenerateRandomGrid()
    {
        var gridArea = TestGrid.GetGridArea();
        var width = gridArea.Width;
        var height = gridArea.Height;
        var grid = new Transform[width, height];

        for (int x = 0; x < width; ++x)
            for (int y = 0; y < height; ++y)
            {
                grid[x, y] = Instantiate(Grid,GridParent);
                grid[x, y].transform.localPosition = new Vector3(x, y, GridParent.transform.position.z);
            }


    }


}
