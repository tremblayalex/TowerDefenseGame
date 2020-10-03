using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
            DoesItHits(hit);
            

        }
       
    }

    void DoesItHits(RaycastHit2D [] hits)
    {
        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                //Debug.Log("Something was clicked!");
                //Debug.Log(hit.collider.gameObject.name);

                WhatToDoOnClic(hits);
                //hit.collider.gameObject.GetComponent<OnClic>(OnClic).;
            }
            else
            {
                //Debug.Log("Mouse Clicked");
            }
        }
        /*if (hit.collider != null)
        {
            Debug.Log("Something was clicked!");
            Debug.Log(hit..gameObject.name);

            //hit.collider.attachedRigidbody.AddForce(Vector2.up);
            //hit.collider.gameObject.GetComponent<OnClic>(OnClic).;
        }
        else
        {
            Debug.Log("Mouse Clicked");
        }*/
    }

    void WhatToDoOnClic(RaycastHit2D[] hits)
    {
        List<string> gameObjectsNames = LookForNameObject(hits);
        if (gameObjectsNames.Contains("Nature")|| gameObjectsNames.Contains("Road"))
        {
            Debug.Log("Clic on Road or Nature");
        }
        else if (gameObjectsNames.Contains("Turrets"))
        {
            Debug.Log("Clic on a Turret");
        }
        else
        {
            Debug.Log("Clic on Floor");
        }
        
        
        /*
            PlaceTurret();
            SelectTurretOnMap();
            SelectTurretInShoop();
       */

    }
    List<string> LookForNameObject(RaycastHit2D[] hits)
    {
        List<string> gameObjectsNames = new List<string>();
        foreach (var hit in hits)
        {
            gameObjectsNames.Add(hit.collider.gameObject.name);
        }
        return gameObjectsNames;
    }

}





//Solution à Alex pour le clic. Il 
/*
 
     using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OnClic : MonoBehaviour
{
    public Camera camera;
    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Fire1") != 0)
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);
            if (tilemap.HasTile(gridPos))
            {
                Debug.Log(tilemap.GetTile(gridPos).name);
            }
            
            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //if (hit != null)
            //{
            //    Transform objectHit = hit.transform;

            //    DoesItHits(hit);
            //    // Do something with the object that was hit by the raycast.
            //}
            
        }
    }

    void DoesItHits(RaycastHit2D hit)
{
    if (hit.collider != null)
    {
        Debug.Log("Grass was clicked!");
        //Debug.Log(hit.collider.gameObject.name);
        //hit.collider.attachedRigidbody.AddForce(Vector2.up);
        //hit.collider.gameObject.GetComponent<OnClic>(OnClic).;
    }
    else
    {
        Debug.Log("Mouse Clicked");
    }
}

public void ClicFunction()
{

}
public void GoToFirstMap()
{
    //OnClic.LoadScene("SampleScene");
}
}



*/