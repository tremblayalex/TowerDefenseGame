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

    void Start()
    {
        towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
        grid = GameObject.FindWithTag("WorldGrid").GetComponent<GridLayout>();
        tilemapFloor = GameObject.FindWithTag("Floor").GetComponent<Tilemap>();
    }

    void Update()
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

        PressedEscapeButton();
    }

    void PressedEscapeButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            towerManager.DisableTowerPurchaseMode();
        }
    }
}


