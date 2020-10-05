using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    private int hitPoints;
    private float movementSpeed;

    private GridLayout grid;
    private Tilemap tilemap;
    private Vector3 pathEndPosition;

    private Vector3 targetPosition;  

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

    void Start()
    {
        grid = GameObject.FindWithTag("Road").GetComponentInParent<GridLayout>();
        tilemap = GameObject.FindWithTag("Road").GetComponent<Tilemap>();
        pathEndPosition = GameObject.FindWithTag("PathEnd").transform.position;

        targetPosition = transform.position;
    }

    void Update()
    {
        if (transform.position == pathEndPosition)
        {
            Destroy(gameObject);
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

    private void CalculateNewTarget()
    {
        Vector3 forwardPosition = transform.position + transform.up;
        Vector3 leftPosition = transform.position - transform.right;
        Vector3 rightPosition = transform.position + transform.right;

        Vector3Int forwardCellPosition = grid.WorldToCell(forwardPosition);
        Vector3Int leftCellPosition = grid.WorldToCell(leftPosition);
        Vector3Int rightCellPosition = grid.WorldToCell(rightPosition);

        if (tilemap.GetTile(forwardCellPosition) != null)
        {
            targetPosition = forwardPosition;
        }
        else if (tilemap.GetTile(leftCellPosition) != null)
        {
            targetPosition = leftPosition;
            RotateToLeft();
        }
        else if (tilemap.GetTile(rightCellPosition) != null)
        {
            targetPosition = rightPosition;
            RotateToRight();
        }
        else
        {
            print("[Error] No longer able to find next path!");
        }
    }

    private void RotateToLeft()
    {
        float currentRotation = transform.eulerAngles.z;

        SetRotation(currentRotation + 90);
    }

    private void RotateToRight()
    {
        float currentRotation = transform.eulerAngles.z;

        SetRotation(currentRotation - 90);
    }

    private void SetRotation(float angle)
    {
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void MoveTowardsTarget()
    {
        float maxMovementDistance = movementSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, maxMovementDistance);
    }
}
