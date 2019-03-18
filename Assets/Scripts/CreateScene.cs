using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScene : MonoBehaviour {
    private GameObject go;
	// Use this for initialization
	void Start () {
        go = new GameObject();
        CreateBackgroundChunk(0, 0);
        Test();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void Test()
    {
        var size = new Vector2(BackgroundSprite.bounds.size.x / transform.localScale.x, BackgroundSprite.bounds.size.y / transform.localScale.y);
        GameObject child;
        for (int ix = 1; ix < 100; ++ix)
        {
            for (int iy = 1; iy < 100; ++iy)
            {

                child = Instantiate(go);
                child.transform.position = new Vector3(size.x * ix, size.y * iy, Z);
                child.transform.parent = transform;
                if (iy == 1)
                {
                    var z = MainCamera.transform.position.z;
                    //MainCamera.transform.position = new Vector3(size.x * ix, size.y * iy,z);
                }
            }
        }
    }
    public void CreateBackgroundChunk(int x, int y)
    {
        
        var chunkSize = GameUtility.ChunkSize;

        var render = go.AddComponent<SpriteRenderer>();
        render.sprite = BackgroundSprite;
        var size = new Vector2(BackgroundSprite.bounds.size.x / transform.localScale.x, BackgroundSprite.bounds.size.y / transform.localScale.y);

        go.transform.position = transform.position;

    }

    public Sprite BackgroundSprite;
    public Camera MainCamera;
    public int Z;
}
