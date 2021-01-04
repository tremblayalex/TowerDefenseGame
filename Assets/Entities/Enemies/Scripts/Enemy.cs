using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    private int hitPoints;
    private float movementSpeed;
    private int dropMoney;
    private float distanceTravelled;
    private int dammageEndOfPath;

    private GridLayout grid;
    private Tilemap tilemap;
    private HPManager hpManager;
    private MoneyManager moneyManager;
    private KillCounterManager killCounterManager;
    private Vector3 pathEndPosition;

    private Vector3 targetPosition;

    private float freezeTime;
    private float slownessMultiplier;

    public void setHitPoints(int inHitPoints)
    {
        hitPoints = inHitPoints;
    }

    public int getHitPoints()
    {
        return hitPoints;
    }

    public float getMovementSpeed()
    {
        return movementSpeed;
    }

    public void setMovementSpeed(float inMovementSpeed)
    {
        movementSpeed = inMovementSpeed;
    }

    public int getDropMoney()
    {
        return dropMoney;
    }

    public void setDropMoney(int inDropMoney)
    {
        dropMoney = inDropMoney;
    }

    public int getDammageEndOfPath()
    {
        return dammageEndOfPath;
    }

    public void setDammageEndOfPath(int inDammageEndOfPath)
    {
        dammageEndOfPath = inDammageEndOfPath;
    }

    public float getDistanceTravelled()
    {
        return distanceTravelled;
    }

    void Start()
    {
        distanceTravelled = 0f;
        freezeTime = 0f;
        slownessMultiplier = 1f;

        grid = GameObject.FindWithTag("Road").GetComponentInParent<GridLayout>();
        tilemap = GameObject.FindWithTag("Road").GetComponent<Tilemap>();
        pathEndPosition = GameObject.FindWithTag("PathEnd").transform.position;
        hpManager = GameObject.Find("HPManager").GetComponent<HPManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        killCounterManager = GameObject.Find("KillCounterManager").GetComponent<KillCounterManager>();

        targetPosition = transform.position;
    }

    void Update()
    {
        if (transform.position == pathEndPosition)
        {
            EnnemyEndThePath();
        }
        else if (transform.position == targetPosition)
        {
            CalculateNewTarget();
        }
        else
        {
            MoveTowardsTarget();
        }    
    }

    void EnnemyEndThePath()
    {
        Destroy(gameObject);
        hpManager.GetComponent<HPManager>().LoseHP(dammageEndOfPath);
    }

    private void CalculateNewTarget()
    {
        Vector3 forwardPosition = transform.position + transform.up;
        Vector3 leftPosition = transform.position - transform.right;
        Vector3 rightPosition = transform.position + transform.right;

        Vector3Int forwardCellPosition = grid.WorldToCell(forwardPosition);
        Vector3Int leftCellPosition = grid.WorldToCell(leftPosition);
        Vector3Int rightCellPosition = grid.WorldToCell(rightPosition);

        Vector3 cellToCenterOffset = new Vector3(0.5f, 0.5f, 0);
        Vector3 forwardCellCenterPosition = forwardCellPosition + cellToCenterOffset;
        Vector3 leftCellCenterPosition = leftCellPosition + cellToCenterOffset;
        Vector3 rightCellCenterPosition = rightCellPosition + cellToCenterOffset;

        if (tilemap.GetTile(forwardCellPosition) != null)
        {
            targetPosition = forwardCellCenterPosition;
        }
        else if (tilemap.GetTile(leftCellPosition) != null)
        {
            targetPosition = leftCellCenterPosition;
        }
        else if (tilemap.GetTile(rightCellPosition) != null)
        {
            targetPosition = rightCellCenterPosition;
        }
        else
        {
            print("[Error] No longer able to find next path!");
        }

        RotateTowardsTarget();
    }

    public void Damage(int damage)
    {    
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    public void Freeze(float inFreezeTime, float inSlownessMultiplier)
    {
        freezeTime = inFreezeTime;
        slownessMultiplier = inSlownessMultiplier;
    }

    public void KillEnemy()
    {
        killCounterManager.AddOneKill();
        moneyManager.AddMoney(dropMoney);
        Destroy(gameObject);
    }

    private void SetRotation(float angle)
    {
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void RotateTowardsTarget()
    {
        float angleSpriteCorrection = -90;

        Vector2 current = transform.position;
        Vector2 targetPosition2D = targetPosition;
        var direction = targetPosition2D - current;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + angleSpriteCorrection, Vector3.forward);
    }

    private void MoveTowardsTarget()
    {
        if (freezeTime > 0)
        {
            freezeTime -= Time.deltaTime;
        }
        else
        {
            slownessMultiplier = 1f;
        }

        float maxMovementDistance = movementSpeed * slownessMultiplier * Time.deltaTime;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, maxMovementDistance);

        distanceTravelled += Vector3.Distance(transform.position, newPosition);
        transform.position = newPosition;
    }
}
