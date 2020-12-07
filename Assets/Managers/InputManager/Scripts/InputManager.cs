using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;
using UnityEngine.XR.WSA;

public class InputManager : MonoBehaviour
{
    private GridLayout grid;
    private Tilemap tilemapFloor;

    private TowerManager towerManager;

    bool mousePreviouslyInMap = false;
    bool mouseDown = false;

    bool keyDownEscape = false;
    bool keyDown1 = false;
    bool keyDown2 = false;
    bool keyDown3 = false;

    void Start()
    {
        towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
        grid = GameObject.FindWithTag("WorldGrid").GetComponent<GridLayout>();
        tilemapFloor = GameObject.FindWithTag("Floor").GetComponent<Tilemap>();
    }

    void Update()
    {
        CheckMouseHover();
        CheckMouseClick();
        CheckKeyDown();
    }

    private void CheckMouseHover()
    {
        Vector3Int cellPosition = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (tilemapFloor.GetTile(cellPosition) != null)
        {
            towerManager.MouseHoverMap(cellPosition);
            mousePreviouslyInMap = true;
        }
        else if (mousePreviouslyInMap)
        {
            towerManager.MouseLeaveMap();
            mousePreviouslyInMap = false;
        }
    }

    private void CheckMouseClick()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            if (!mouseDown)
            {
                towerManager.MouseClick();

                mouseDown = true;
            }
        }
        else
        {
            mouseDown = false;
        }
    }

    private void CheckKeyDown()
    {
        CheckKeyDownEscape();
        CheckKeyDown1();
        CheckKeyDown2();
        CheckKeyDown3();
    }

    private void CheckKeyDownEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!keyDownEscape)
            {
                towerManager.DisableTowerPurchaseMode();

                keyDownEscape = true;
            }       
        }
        else
        {
            keyDownEscape = false;
        }
    }

    private void CheckKeyDown1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!keyDown1)
            {
                towerManager.EnableTowerPurchaseMode(0);

                keyDown1 = true;
            }
        }
        else
        {
            keyDown1 = false;
        }
    }

    private void CheckKeyDown2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!keyDown2)
            {
                towerManager.EnableTowerPurchaseMode(1);

                keyDown2 = true;
            }
        }
        else
        {
            keyDown2 = false;
        }
    }

    private void CheckKeyDown3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!keyDown3)
            {
                towerManager.EnableTowerPurchaseMode(2);

                keyDown3 = true;
            }
        }
        else
        {
            keyDown3 = false;
        }
    }
}


