﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            DoesItHits(hit);
            //Debug.Log("Mouse Clicked");
        }
    }

    void DoesItHits(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            Debug.Log("Something was clicked!");
            Debug.Log(hit.collider.gameObject.name);
            //hit.collider.attachedRigidbody.AddForce(Vector2.up);
            //hit.collider.gameObject.GetComponent<OnClic>(OnClic).;
        }
        else
        {
            Debug.Log("Mouse Clicked");
        }
    }
}
