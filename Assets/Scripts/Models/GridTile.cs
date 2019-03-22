using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class GridTile : MonoBehaviour
    {
        public Sprite Grid;
        public Sprite Left;
        public Sprite Top;
        public Sprite Bottom;
        public Sprite Right;

        //change to sprites?
        public SpriteRenderer portL1;
        public SpriteRenderer portL2;
        public SpriteRenderer portL3;
        public SpriteRenderer portR1;
        public SpriteRenderer portR2;
        public SpriteRenderer portR3;
        public SpriteRenderer portT1;
        public SpriteRenderer portT2;
        public SpriteRenderer portT3;
        public SpriteRenderer portB1;
        public SpriteRenderer portB2;
        public SpriteRenderer portB3;
        public SpriteRenderer portConnectL1;
        public SpriteRenderer portConnectL2;
        public SpriteRenderer portConnectL3;
        public SpriteRenderer portConnectR1;
        public SpriteRenderer portConnectR2;
        public SpriteRenderer portConnectR3;
        public SpriteRenderer portConnectT1;
        public SpriteRenderer portConnectT2;
        public SpriteRenderer portConnectT3;
        public SpriteRenderer portConnectB1;
        public SpriteRenderer portConnectB2;
        public SpriteRenderer portConnectB3;
        public SpriteRenderer forground;
        public SpriteRenderer background;
        public SpriteRenderer chip;





        //public ComponentScript ComponentScript;

        public GridTile()
        {

        }
        public void Start()
        {

        }

        public void SetState(GridModel gridModel)
        {
            SetPaths(gridModel);
            SetPorts(gridModel);

        }
        int pathSortingOrder=11;
        int componentSortingOrder = 12;
        void SetPaths(GridModel gridModel)
        {
            foreach (var cell in gridModel.Cells)
            {
                if (cell.TopNeighbor != null)
                {
                    AddSpriteRenderer(Top, GridUtility.GetSpritePositionForPath(cell.Location), pathSortingOrder);
                    if (!gridModel.Cells.Contains(cell.TopNeighbor))
                        SetCrossingLine(cell, cell.TopNeighbor, Top);
                }
                if (cell.RightNeighbor != null)
                {
                    AddSpriteRenderer(Right, GridUtility.GetSpritePositionForPath(cell.Location), pathSortingOrder);
                    if (!gridModel.Cells.Contains(cell.RightNeighbor))
                        SetCrossingLine(cell, cell.RightNeighbor, Right);
                }
                if (cell.BottomNeighbor != null)
                {
                    AddSpriteRenderer(Bottom, GridUtility.GetSpritePositionForPath(cell.Location), pathSortingOrder);
                    if (!gridModel.Cells.Contains(cell.BottomNeighbor))
                        SetCrossingLine(cell, cell.BottomNeighbor, Bottom);
                }
                if (cell.LeftNeighbor != null)
                {
                    AddSpriteRenderer(Left, GridUtility.GetSpritePositionForPath(cell.Location), pathSortingOrder);
                    if (!gridModel.Cells.Contains(cell.LeftNeighbor))
                        SetCrossingLine(cell, cell.LeftNeighbor, Left);
                }
            }
        }

        void SetCrossingLine(Cell cell, Cell neighbor, Sprite sprite)
        {
            var pos = GridUtility.GetSpritePositionForPathCrossing(cell, neighbor);
            AddSpriteRenderer(sprite,pos,pathSortingOrder);
        }    

        void AddSpriteRenderer(Sprite sprite,Vector3 location, int sorting,string name = "")
        {
            var go = new GameObject(name);
            
            go.transform.parent = gameObject.transform;
			go.transform.localPosition = location;
            go.AddComponent<SpriteRenderer>();
            var sr = go.GetComponent<SpriteRenderer>();
            sr.sprite = sprite;
            sr.sortingOrder = sorting;
        }

        void SetPorts(GridModel gridModel)
        {
            if (gridModel.GridComponents.Count(x => x.GetType() == typeof(Chip)) < 1)
                return;
            foreach(var component in gridModel.GridComponents)
            {
                if (component.GetType() != typeof(Chip))
                    continue;
                var chip = (Chip)component;
                foreach(var port in chip.Ports)
                {
                    var go = new GameObject("Port");
                    go.transform.parent = gameObject.transform;
                    go.transform.localPosition = Vector3.zero;
                    go.AddComponent<SpriteRenderer>();
                    var sr = go.GetComponent<SpriteRenderer>();
                    

                    switch(port.Location)
                    {
                        case 0:
                            sr.sprite = portT1.sprite;
                            break;
                        case 1:
                            sr.sprite = portT2.sprite;
                            break;
                        case 2:
                            sr.sprite = portT3.sprite;
                            break;
                        case 3:
                            sr.sprite = portR1.sprite;
                            break;
                        case 4:
                            sr.sprite = portR2.sprite;
                            break;
                        case 5:
                            sr.sprite = portR3.sprite;
                            break;
                        case 6:
                            sr.sprite = portB1.sprite;
                            break;
                        case 7:
                            sr.sprite = portB2.sprite;
                            break;
                        case 8:
                            sr.sprite = portB3.sprite;
                            break;
                        case 9:
                            sr.sprite = portL1.sprite;
                            break;
                        case 10:
                            sr.sprite = portL2.sprite;
                            break;
                        case 11:
                            sr.sprite = portL3.sprite;
                            break;

                    }
                }
            }
        }

    }
}
