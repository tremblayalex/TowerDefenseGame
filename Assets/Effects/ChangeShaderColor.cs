using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShaderColor : MonoBehaviour
{
    Material material;
    private float interpolation = 0;
    private ParticleSystem particleSystem;
    private float explosionRadius = 10f;

    void Start()
    {
        material = GetComponent<ParticleSystemRenderer>().materials[0];
        particleSystem = GetComponent<ParticleSystem>();
       
        var main = particleSystem.main;
        //print(main.startSize);
        main.startSize = explosionRadius;
    }

    void Update()
    {
        interpolation = interpolation + Time.deltaTime * 0.8f;
        material.SetFloat("_Interpolation", interpolation);
    }
}
