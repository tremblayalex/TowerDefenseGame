using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    string[] ModeDeClicPossible = { "Normal", "Achat" };
    string ModeDeClic;
    public GameObject Tourrelle0;
    Ray myRay;
    RaycastHit2D[] hits;


    // Start is called before the first frame update
    void Start()
    {
        ModeDeClic = "Normal";
    }

    // Update is called once per frame
    void Update()
    {
        FindWhereWeClicked();
        PressedEscapeButton();
    }

    void PressedEscapeButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ModeDeClic = "Normal";
        }
    }

    void FindWhereWeClicked()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
            DoesItHits();
        }
    }

    void DoesItHits()
    {
        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                //Debug.Log("Something was clicked!");
                //
                Debug.Log(hit.collider.gameObject.name);
                WhatToDoOnClic();
                //hit.collider.gameObject.GetComponent<OnClic>(OnClic).;
            }
            else
            {
                //Debug.Log("Mouse Clicked");
            }
        }
        
    }

    void WhatToDoOnClic()
    {
        List<string> gameObjectsNames = LookForNameObject(hits);
        if (gameObjectsNames.Contains("Nature")|| gameObjectsNames.Contains("Road"))
        {
            Debug.Log("Clic on Road or Nature");
            //NothingToDo();
        }
        else if (gameObjectsNames.Contains("TurretsOnFloor"))
        {
            Debug.Log("Clic on a Turret");
            //SelectTurretOnMap();
        }
        else if (gameObjectsNames.Contains("TurretsInMenu"))
        {
            Debug.Log("Clic on a turret in the menu");
            ModeDeClic = ModeDeClicPossible[1];
        }
        else
        {
            Debug.Log("Clic on Floor");
            PlaceTurret(hits);
        }


    }

    void PlaceTurret(RaycastHit2D[] hits)
    {
        if (ModeDeClic == ModeDeClicPossible[1])
        {
            Instantiate(Tourrelle0, hits[0].point, Quaternion.identity);
        }
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