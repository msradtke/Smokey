using Assets.Scripts.Models;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class GridLogic : MonoBehaviour
	{
		bool isGridActive;
		Vector3 savedPosition;
		float savedZoom;

		public GridTile gridPrefab;

		public Camera Cam;
		public int CameraMoveSpeed = 5;
		public float Zoom = 2f;
		public Transform GridParent;
		public GridModel GridModel;
		Sprite GridSprite;
		public Sprite[] Sprites = new Sprite[10];
		public List<Sprite> TestSprite;
		public Dictionary<int, Sprite> SpriteDict;
		// Use this for initialization
		void Start ()
		{
			TestSprite = new List<Sprite> {
				GridSprite
			};
			SpriteDict = new Dictionary<int, Sprite> {
				{ 0,GridSprite }
			};
			Sprites [0] = GridSprite;
			savedPosition = Camera.main.transform.position;
			savedZoom = Cam.orthographicSize;
		}

		// Update is called once per frame
		void Update ()
		{
            
			if (Input.GetKeyDown (KeyCode.D) && Input.GetKey (KeyCode.LeftControl)) {
				Debug.Log ("KeyDown");
				if (isGridActive)
					CloseGrid ();
				else
					ShowGrid ();
			}


			if (isGridActive)
				MoveCamera ();
		}

		void MoveCamera ()
		{
			if (Input.GetKey (KeyCode.D)) { //rotate right
				Cam.transform.position += transform.right * Time.deltaTime * CameraMoveSpeed;

			}
			if (Input.GetKey (KeyCode.A)) {
				Cam.transform.position -= transform.right * Time.deltaTime * CameraMoveSpeed;
			}
			if (Input.GetKey (KeyCode.W)) {
				Cam.transform.position += transform.up * Time.deltaTime * CameraMoveSpeed;
			}
			if (Input.GetKey (KeyCode.S)) {
				Cam.transform.position -= transform.up * Time.deltaTime * CameraMoveSpeed;

			}
		}

		void ShowGrid ()
		{
			savedZoom = Cam.orthographicSize;
			Cam.orthographicSize = Zoom;
			GameUtility.IsGridActive = true;
			savedPosition = Cam.transform.position;
			isGridActive = true;
			GenerateRandomGrid ();
			Cam.transform.position = new Vector3 (GridParent.transform.position.x, GridParent.transform.position.y, Cam.transform.position.z);
		}

		void CloseGrid ()
		{
			Cam.orthographicSize = savedZoom;
			GameUtility.IsGridActive = false;
			Cam.transform.position = savedPosition;
			isGridActive = false;
		}

		void GenerateRandomGrid ()
		{
			var gridArea = TestGrid.GetGridArea ();
			var width = gridArea.Width;
			var height = gridArea.Height;
			var grid = new List<GridTile> ();
			var gridModels = new List<GridModel> ();
			gridArea.Grids = gridModels;

			ClearGridParent ();

			for (int x = 0; x < width; ++x)
				for (int y = 0; y < height; ++y) {
					var gt = Instantiate (gridPrefab, GridParent);
					gt.transform.localPosition = new Vector3 (x, y, GridParent.transform.position.z);
					grid.Add (gt);
					gridModels.Add (new GridModel {GridTile = gt});
				}

			var cpu = TestGrid.GetCpu();
			var empty = GridUtility.GetEmptyGrid (gridArea);
			var componentScript = empty.GridTile.gameObject.GetComponent<ComponentScript>();
			componentScript.CreateComponent (cpu);


			var boostChip = TestGrid.GetBoostChip ();
			empty = GridUtility.GetEmptyGrid (gridArea);
			componentScript = empty.GridTile.gameObject.GetComponent<ComponentScript>();
			componentScript.CreateComponent (boostChip);


			var componentCount = 4;
			var usedTiles = new List<GridTile> ();
			var max = grid.Count;
			int i = 0;
			while (i < componentCount) {
				var gt = grid [Random.Range (0, max)];
				if (usedTiles.Contains (gt))
					continue;
				usedTiles.Add (gt);
				var gridComponent = new GridComponent ();
				gridComponent.Color = GameUtility.GetRandomColor ();
				var cell = new Cell ();
				gridComponent.Cells.Add (cell);
				cell.Location = new CellLocation{X=Random.Range(0,3), Y=Random.Range(0,3)};
				componentScript = gt.gameObject.GetComponent<ComponentScript>();
				componentScript.CreateComponent(gridComponent);
				++i;
			}
			
			//gridComponent.Cell.Location = new CellLocation { X = 0, Y = 1 };
			//componentScript.CreateComponent (gridComponent);


			//g.gameObject.AddComponent<ComponentScript> ();


		}

		void ClearGridParent ()
		{
			var count = GridParent.childCount;
			for (int i = 0; i < count; i++) {
				GameObject.Destroy (GridParent.GetChild (i).gameObject);
			}
		}


	}
}
