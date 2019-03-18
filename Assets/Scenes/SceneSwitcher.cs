using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private Camera mainCamera;
    private Camera gridCamera;
    Scene mainScene;
    Scene gridScene;

    private bool mainSceneLoaded = true;
    // Use this for initialization
    void Start()
    {
        mainScene = SceneManager.GetSceneByName("MainScene");
        gridScene = SceneManager.GetSceneByName("GridScene");

        mainCamera = Camera.main;
        Camera[] cameras = new Camera[2];
        Camera.GetAllCameras(cameras);
        
        foreach(var c in cameras)
        {
            if (c.tag == "Grid")
                gridCamera = c;
        }
                
        Object.DontDestroyOnLoad(gameObject);

        GotoMainScene();
        SceneManager.LoadScene("Minimap",LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        gridScene = SceneManager.GetSceneByName("GridScene");
        //if (Input.GetKey(KeyCode.D) &&( Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) )
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("KeyDown");
            if (gridScene.isLoaded)
                UnloadGridScene();
            else
                GotoGridScene();
        }
    }

    public void GotoMainScene()
    {
        if(!mainScene.isLoaded)
            SceneManager.LoadScene("MainScene");
    }
    public void UnloadGridScene()
    {
        SceneManager.UnloadSceneAsync(gridScene);
    }
        public void GotoGridScene()
    {
        gridScene = SceneManager.GetSceneByName("GridScene");
        Debug.Log("GridScene");
        if (!gridScene.isLoaded)
            SceneManager.LoadScene("GridScene", LoadSceneMode.Additive);

    }

    void SplitScreen()
    {

    }
}
