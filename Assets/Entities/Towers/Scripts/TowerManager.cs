using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerManager : MonoBehaviour
{
    public TowerScriptableObject[] towerScriptableObjects;

    private GridLayout grid;
    private Tilemap tilemapRoad;
    private Tilemap tilemapNature;
    private SpawnerTower spawnerTower;
    private MoneyManager moneyManager;

    private int towerPurchaseSelectedIndex;
    private Vector3Int selectedTilePosition;

    private List<GameObject> allPlacedTowers;
    private GameObject shadowTower;

    private void Start()
    {
        towerPurchaseSelectedIndex = -1;
        allPlacedTowers = new List<GameObject>();

        grid = GameObject.FindWithTag("WorldGrid").GetComponent<GridLayout>();
        tilemapRoad = GameObject.FindWithTag("Road").GetComponent<Tilemap>();
        tilemapNature = GameObject.FindWithTag("Nature").GetComponent<Tilemap>();
        spawnerTower = GameObject.Find("SpawnerTower").GetComponent<SpawnerTower>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }

    public void EnableTowerPurchaseMode(int towerIndex)
    {
        if (towerIndex >= 0 && towerIndex < towerScriptableObjects.Length)
        {
            towerPurchaseSelectedIndex = towerIndex;
        }
    }

    public void DisableTowerPurchaseMode()
    {
        towerPurchaseSelectedIndex = -1;
        DestroyPreviousShadowTower();
    }

    public void MouseHoverMap(Vector3Int tilePosition)
    {
        if (towerPurchaseSelectedIndex != -1)
        {
            UpdateTileSelection(tilePosition);
        }     
    }

    public void MouseLeaveMap()
    {
        DestroyPreviousShadowTower();
    }

    public void MouseClick()
    {
        if (shadowTower != null)
        {
            DestroyPreviousShadowTower();

            if (moneyManager.SpendMoney(towerScriptableObjects[towerPurchaseSelectedIndex].price))
            {
                Vector3 actualTowerPosition = ConvertTilePositionToTowerPosition(selectedTilePosition);
                allPlacedTowers.Add(spawnerTower.SpawnTower(actualTowerPosition, towerPurchaseSelectedIndex));
            }       
        }
    }

    private void UpdateTileSelection(Vector3Int tilePosition)
    {
        if (tilePosition != selectedTilePosition)
        {
            DestroyPreviousShadowTower();

            if (CanPlaceTowerAtPosition(tilePosition))
            {
                DisplayShadowTower(tilePosition);
            }

            selectedTilePosition = tilePosition;
        }
    }

    private void DestroyPreviousShadowTower()
    {
        if (shadowTower != null)
        {
            Destroy(shadowTower);
        }
    }

    private void DisplayShadowTower(Vector3Int tilePosition)
    {
        Vector3 actualTowerPosition = ConvertTilePositionToTowerPosition(tilePosition);

        shadowTower = spawnerTower.SpawnShadowTower(actualTowerPosition, towerPurchaseSelectedIndex);
    }

    private Vector3 ConvertTilePositionToTowerPosition(Vector3Int tilePosition)
    {
        return new Vector3(tilePosition.x + 0.5f, tilePosition.y + 0.5f);
    }

    private bool CanPlaceTowerAtPosition(Vector3Int position)
    {
        return !TileContainsRoadOrNature(position) && !IsTowerAtPosition(position);
    }

    private bool TileContainsRoadOrNature(Vector3Int position)
    {
        return tilemapRoad.GetTile(position) != null || tilemapNature.GetTile(position) != null;
    }

    private bool IsTowerAtPosition(Vector3Int position)
    {
        bool isTowerAtPosition = false;

        foreach (GameObject tower in allPlacedTowers)
        {
            Vector3Int towerCellPositon = grid.WorldToCell(tower.transform.position);

            if (towerCellPositon == position)
            {
                isTowerAtPosition = true;
            }
        }

        return isTowerAtPosition;
    }
}
