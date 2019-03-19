using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEntity : MonoBehaviour
{
    LineRenderer line;
    static GameObject previousSelected;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        Debug.Log("OnMousDown " + gameObject.name);
        if (previousSelected == gameObject)
            return;
        if (previousSelected != null)
        {
            var previousState = previousSelected.GetComponent<EntityState>();
            previousState.Unselect();
        }
        var state = gameObject.GetComponent<EntityState>();
        state.Select();
        previousSelected = gameObject;
    }


}


