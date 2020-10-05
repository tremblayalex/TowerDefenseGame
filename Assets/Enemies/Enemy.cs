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

    public int HitPoints
    {
        get { return hitPoints; }
        set { hitPoints = HitPoints; }
    }

    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = MovementSpeed; }
    }

    void Start()
    {
        grid = GameObject.FindWithTag("Road").GetComponentInParent<GridLayout>();
        tilemap = GameObject.FindWithTag("Road").GetComponent<Tilemap>();

        //transform.eulerAngles = new Vector3(0, 0, 180);
        //transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        float speed = 10f;

        float gas = Input.GetAxis("Vertical");
        float rotate = -Input.GetAxis("Horizontal");

        transform.Rotate(transform.forward, rotate * 200 * Time.deltaTime);
        transform.position += gameObject.transform.up * gas * speed * Time.deltaTime;

        //CheckRoadTile();
        IsPathForward();
    }

    private void IsPathForward()
    {
        Vector3Int cellPosition = grid.WorldToCell(transform.position + transform.up);

        print(transform.eulerAngles.z);
        print("Cell forward: " + (tilemap.GetTile(cellPosition) != null));
        print(cellPosition.x + " " + cellPosition.y + " " + cellPosition.z);
    }

    private void CheckRoadTile()
    {
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        print("Road: " + (tilemap.GetTile(cellPosition) != null));
        print(cellPosition.x + " " + cellPosition.y + " " + cellPosition.z);
    }
}
