using Assets.Scripts.Models;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentScript : MonoBehaviour {
	public Sprite[] Sprites;

	List<Sprite> _sprites;
	// Use this for initialization
	void Start () {
		_sprites = Sprites.ToList ();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	void Awake()
	{
	}
    public void CreateComponent(GridComponent gridComponent)
    {
		_sprites = new List<Sprite>(Sprites);
		var child = new GameObject();
		var spriteRenderer = child.AddComponent<SpriteRenderer> ();
        Sprite sprite = null;
		if (gridComponent.GetType()== typeof(GridComponent))
				sprite = _sprites.FirstOrDefault(x => x.name == "basicComponent");
		else if (gridComponent.GetType()== typeof(Chip))
				sprite = _sprites.FirstOrDefault(x => x.name == "chipSmall");
		
        spriteRenderer.sprite = sprite;
        var location = GridUtility.GetCellPosition(gridComponent);
		spriteRenderer.color = gridComponent.Color;
		spriteRenderer.sortingOrder = 15;
		child.transform.parent = gameObject.transform;
		child.transform.localPosition = location;
		
    }

}
