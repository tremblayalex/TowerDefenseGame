using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShaderColor : MonoBehaviour
{
    private Material material;
    private float interpolation = 0;
    private ParticleSystem particleSystem;
    private float explosionRange = 1f;

    public void setExplosionRange(float inExplosionRange)
    {
        explosionRange = inExplosionRange * 2;
    }

    void Start()
    {
        material = GetComponent<ParticleSystemRenderer>().materials[0];
        particleSystem = GetComponent<ParticleSystem>();
       
        var main = particleSystem.main;
        //print(main.startSize);
        main.startSize = explosionRange;
    }

    void Update()
    {
        interpolation = interpolation + Time.deltaTime * 2f;
        material.SetFloat("_Interpolation", interpolation);
    }
}
