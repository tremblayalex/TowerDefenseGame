﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEffect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject, 2); // GetComponent<Rigidbody>();
       
      
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
