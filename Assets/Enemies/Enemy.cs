using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hitPoints;
    private float movementSpeed;

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
        //transform.eulerAngles = new Vector3(0, 0, 0);

        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        float speed = 10f;

        float gas = Input.GetAxis("Vertical");
        float rotate = -Input.GetAxis("Horizontal");

        transform.Rotate(transform.forward, rotate * 200 * Time.deltaTime);

        transform.position += gameObject.transform.up * gas * speed * Time.deltaTime;
    }
}
