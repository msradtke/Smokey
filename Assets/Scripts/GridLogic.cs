using Assets.Scripts.Models;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		public Dictionary<int, Sprite> SpriteDict;
		// Use this for initialization
		void Start ()
		{
			
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
			

			ClearGridParent ();


			for (int y = 0; y < height; ++y) 
				for (int x = 0; x < width; ++x)
			{
					var gt = Instantiate (gridPrefab, GridParent);
					gt.name = (x + "," + y);
					gt.transform.localPosition = new Vector3 (x, y, GridParent.transform.position.z);
					grid.Add (gt);
					gridModels.Add (new GridModel(gt));
				}
            gridArea.SetGrids(gridModels);

            var cpu = TestGrid.GetCpu();
			var cpuModel = GridUtility.GetEmptyGrid (gridArea);
            var cpuLocation = gridArea.GetCellLocation(cpuModel.Cells.First()); //need PORTS and ENTRANCE
            cpuModel.GridComponents.Add(cpu);
			var componentScript = cpuModel.GridTile.gameObject.GetComponent<ComponentScript>();
			componentScript.CreateComponent (cpu);

            //empty.GridTile.SetState(empty);

			var boostChip = TestGrid.GetBoostChip ();
			var boostModel = GridUtility.GetEmptyGrid (gridArea);
            var boostChipLocation = gridArea.GetCellLocation(boostModel.Cells.First());//need PORTS and ENTRANCE

            boostModel.AddComponent(boostChip);
			componentScript = boostModel.GridTile.gameObject.GetComponent<ComponentScript>();
			componentScript.CreateComponent (boostChip);
            boostModel.GridTile.SetState(boostModel);

            var componentCount = 4;
			var usedTiles = new List<GridTile> ();
			var max = grid.Count;
			int i = 0;

            List<GridAreaLocation> recvrLocations = new List<GridAreaLocation>();
			while (i < componentCount) {
                var receiverModel= GridUtility.GetEmptyGrid(gridArea);
                

                var gridComponent = new GridComponent ();
				gridComponent.Color = GameUtility.GetRandomColor ();
                var c=new CellLocation(Random.Range(0, 3), Random.Range(0, 3));
                var rCell = receiverModel.GetCell(c);// testing, picking first cell of the grid
                recvrLocations.Add(gridArea.GetCellLocation(rCell));

                gridComponent.CellLocations.Add (c);
                receiverModel.AddComponent(gridComponent);

				componentScript = receiverModel.GridTile.gameObject.GetComponent<ComponentScript>();
				componentScript.CreateComponent(gridComponent);
				++i;
                receiverModel.GridTile.SetState(receiverModel);
            }

            List<GridPath> paths = new List<GridPath>();
            foreach (var r in recvrLocations)
            {
                var gp = new GridPath();
                gp.StartEntrance = r;
                gp.FinishEntrance = cpuLocation;

                AStarPath aStar = new AStarPath();
                var path = aStar.FindPath(r, cpuLocation, gridArea);
                gp.Path = path;
                //GridUtility.GeneratePath(gp, gridArea);

                paths.Add(gp);
            }
            

            GridUtility.SetCellStates(paths, gridArea);
            gridModels.ForEach(x => x.GridTile.SetState(x));
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
