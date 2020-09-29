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
            /*
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (hit != null)
            {
                Transform objectHit = hit.transform;

                DoesItHits(hit);
                // Do something with the object that was hit by the raycast.
            }
            */
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
