using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR.WSA;

public class ClickManager : MonoBehaviour
{
    public enum shoppingPosibilities
    {
        Nothing,
        Turret1,
        Turret2,
        Turret3
    }

    public shoppingPosibilities ModePlaceTurret;

    //Necessaire pour faire spawn la tourelle
    public TowerScriptableObject[] towerScriptableObjects;
    public GameObject towerPrefab;

    //Nouvelle facon de faire
    private GridLayout grid;
    private Tilemap tilemapRoad;
    private Tilemap tilemapFloor;
    private Tilemap tilemapNature;

    bool Fire1InFonction = false;


    //Ancienne facon de faire
    Ray myRay;
    RaycastHit2D[] hits;
    RaycastHit2D hit;
    //public GameObject Tourrelle0; <----- À enlever je pense.


    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindWithTag("WorldGrid").GetComponent<GridLayout>();
        tilemapRoad = GameObject.FindWithTag("Road").GetComponent<Tilemap>();
        tilemapFloor= GameObject.FindWithTag("Floor").GetComponent<Tilemap>();
        tilemapNature = GameObject.FindWithTag("Nature").GetComponent<Tilemap>();

        ModePlaceTurret = shoppingPosibilities.Nothing;

    }

    // Update is called once per frame
    void Update()
    {
        OnClic();
        PressedEscapeButton();
    }

    void PressedEscapeButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ModePlaceTurret = shoppingPosibilities.Nothing;
        }
    }

    void OnClic()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            if (Fire1InFonction == false)
            {
                Fire1InFonction = true;
                VerifyClicMode();
            }

        }
        if (Input.GetAxis("Fire1") == 0)
        {
            if (ModePlaceTurret != shoppingPosibilities.Nothing)
            {
                ShowTheShadowOfTheTurret();
            }
            Fire1InFonction = false;
        }
    }
    void ShowTheShadowOfTheTurret()
    {
        if (GameObject.Find("ShadowTurret") != null)
        {
            Destroy(GameObject.Find("ShadowTurret"));
        }
        PlaceATurret(0.5f);

    }

    void VerifyClicMode()
    {
        if (ModePlaceTurret != shoppingPosibilities.Nothing)
        {
            BuyTheTurret();
        }
        //else
        {
            //Je ne sais pas ce que le joueur peut faire en ce moment à part acheter.
            //Il pourra upgrade? Sélectionner une tourrel?
        }
    }

    void BuyTheTurret()
    {
        Destroy(GameObject.Find("ShadowTurret"));
        if (SpendMoneyForTheTurret())
        {
            PlaceATurretOnTheField();
        }            
    }

    public bool SpendMoneyForTheTurret()
    {
        int prix = 100;
        bool canSpendMoney = GameObject.Find("Money").GetComponent<Money_Manager>().ChangerArgent(0 - prix);              
        
        Debug.Log(GameObject.Find("Money").GetComponent<Money_Manager>().money);
        return canSpendMoney;
    }

    void PlaceATurretOnTheField()
    {
        PlaceATurret(1f);
    }

    void PlaceATurret(float opacity)
    {
        Vector3Int mousePosition = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //Debug.Log("x : " + mousePosition.x + "   y : " + mousePosition.y);
        //SpawnNewTower(mousePosition.x, mousePosition.y);
        if (tilemapRoad.GetTile(mousePosition) == null && tilemapNature.GetTile(mousePosition) == null)
        {
            if (tilemapFloor.GetTile(mousePosition) != null)
            {
                if (IsATowerThere() == false)
                {
                    SpawnTowerPosition(mousePosition.x, mousePosition.y, opacity);
                }
            }
        }
    }



    public void SpawnTowerPosition(int x, int y, float opacity)
    {
        Debug.Log("We can place a turret here");
        SpawnNewTower(new Vector3(x + 0.5f, y + 0.5f, 0), opacity);
    }

    public void SpawnNewTower(Vector3 position, float opacity)
    {
        GameObject newTower = Instantiate(towerPrefab, position, Quaternion.identity);
        newTower.GetComponent<Tower>().setDamage(towerScriptableObjects[0].damage);
        newTower.GetComponent<Tower>().setRange(towerScriptableObjects[0].range);
        newTower.GetComponent<Tower>().setFireRate(towerScriptableObjects[0].fireRate);
        newTower.GetComponent<SpriteRenderer>().sprite = towerScriptableObjects[0].towerSprite;
        newTower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, opacity);
        if (opacity != 1f)
        {
            newTower.name = "ShadowTurret";
            newTower.GetComponent<Tower>().DisplayFireRange();
        }
    }


     
    bool IsATowerThere()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        LayerMask mask = LayerMask.GetMask("Tower");
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, mask);

        return DoesItHits();
    }

    bool DoesItHits()
    {
        bool towerIsHere = false;


         if (hit.collider != null)
         {
            Debug.Log("a tower is here");
            towerIsHere = true;
         }

        return towerIsHere;
    }

    bool WhatToDoOnClic()
    {
        bool towerIsHere = false;
        //List<string> gameObjectsNames = LookForNameObject(hits);

        if (hit.collider.gameObject.name.Contains("Tower"))
        {
            Debug.Log("a tower is here");
            towerIsHere = true;
        }
        return towerIsHere;
    }

    List<string> LookForNameObject(RaycastHit2D[] hits)
    {
        //Ca ne marche pas!!!!!!!!!!!!!!!!
        List<string> gameObjectsNames = new List<string>();
        foreach (var hit in hits)
        {
            gameObjectsNames.Add(hit.collider.gameObject.name);
        }
        return gameObjectsNames;
    }

    public void SelectTurret1()
    {
        ModePlaceTurret = shoppingPosibilities.Turret1;
    }

    //public void SelectTurretToBuy(shoppingPosibilities posibility)
    //{
    //    ModePlaceTurret = posibility;
    //}

    //public void SelectTurretToBuy(string posibility)
    //{
    //    if (posibility == "Nothing")
    //    {
    //        ModePlaceTurret = shoppingPosibilities.Nothing;
    //    }
    //    else if (posibility == "Turret1")
    //    {
    //        ModePlaceTurret = shoppingPosibilities.Turret1;
    //    }
    //    else if (posibility == "Turret2")
    //    {
    //        ModePlaceTurret = shoppingPosibilities.Turret2;
    //    }
    //    else if (posibility == "Turret3")
    //    {
    //        ModePlaceTurret = shoppingPosibilities.Turret3;
    //    }

    //}

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



//*/
