using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    private GameObject selectedTower;
    private GameObject shadowTower;

    private UIManager uiManager;

    bool outSideMap;

    private void Start()
    {
        towerPurchaseSelectedIndex = -1;
        allPlacedTowers = new List<GameObject>();
        outSideMap = false;
        grid = GameObject.FindWithTag("WorldGrid").GetComponent<GridLayout>();
        tilemapRoad = GameObject.FindWithTag("Road").GetComponent<Tilemap>();
        tilemapNature = GameObject.FindWithTag("Nature").GetComponent<Tilemap>();
        spawnerTower = GameObject.Find("SpawnerTower").GetComponent<SpawnerTower>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void EnableTowerPurchaseMode(int towerIndex)
    {
        if (towerIndex >= 0 && towerIndex < towerScriptableObjects.Length)
        {
            selectedTilePosition.x = -1;
            towerPurchaseSelectedIndex = towerIndex;
        }
        NoTowerSelected();
    }

    public void DisableTowerPurchaseMode()
    {
        selectedTilePosition.x = -1;
        towerPurchaseSelectedIndex = -1;
        DestroyPreviousShadowTower();
    }

    public void NoTowerSelected()
    {
        if (selectedTower != null)
        {
            selectedTower.GetComponent<Tower>().HideFireRange();
            selectedTower = null;
        }       
        uiManager.HideShowTowerInformation(false);
    }

    public void MouseHoverMap(Vector3Int tilePosition)
    {
        outSideMap = true;
        UpdateTileSelectionToBuy(tilePosition);
    }

    public void MouseLeaveMap()
    {
        outSideMap = false;
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
                DisableTowerPurchaseMode();
            }
            selectedTilePosition.x = -1;
        }
        else if (towerPurchaseSelectedIndex == -1)
        {
            if (outSideMap)
            {
                SelectATower();
            }            
        }
    }

    private void SelectATower()
    {
        NoTowerSelected();
        selectedTower = FindTowerSelected(selectedTilePosition);
        if (selectedTower != null)
        {            
            ShowInformationAboutTheRightTower();
        }
        else
        {
            NoTowerSelected();
        }
    }
    private void ShowInformationAboutTheRightTower()
    {
        uiManager.HideShowTowerInformation(true);
        selectedTower.GetComponent<Tower>().DisplayFireRange();
        selectedTower.GetComponent<ActivatedTower>().ShowInformationOnSelection();
    }

    public void UpgradeTheRightTower()
    {
        if (selectedTower != null)
        {
            selectedTower.GetComponent<ActivatedTower>().Upgrade();
        }
    }

    public void SellTheRightTowerType()
    {
        if (selectedTower != null)
        {
            selectedTower.GetComponent<ActivatedTower>().Sell();
            allPlacedTowers.Remove(selectedTower);
            Destroy(selectedTower);
            NoTowerSelected();
        }
    }

    private void UpdateTileSelectionToBuy(Vector3Int tilePosition)
    {
        if (tilePosition != selectedTilePosition)
        {
            if (towerPurchaseSelectedIndex != -1)
            {
                DestroyPreviousShadowTower();
                if (CanPlaceTowerAtPosition(tilePosition))
                {
                    DisplayShadowTower(tilePosition);
                }
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

    private GameObject FindTowerSelected(Vector3Int position)
    {
        GameObject towerClicked = null;

        foreach (GameObject tower in allPlacedTowers)
        {
            Vector3Int towerCellPositon = grid.WorldToCell(tower.transform.position);

            if (towerCellPositon == position)
            {
                towerClicked = tower;
            }
        }

        return towerClicked;
    }
}
