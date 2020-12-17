using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShaderColor : MonoBehaviour
{
    private Material material;
    private float interpolation = 0;
    private ParticleSystem theParticleSystem;
    private float explosionRange = 1f;

    public void setExplosionRange(float inExplosionRange)
    {
        explosionRange = inExplosionRange * 2;
    }

    void Start()
    {
        material = GetComponent<ParticleSystemRenderer>().materials[0];
        theParticleSystem = GetComponent<ParticleSystem>();
       
        var main = theParticleSystem.main;
        main.startSize = explosionRange;
    }

    void Update()
    {
        interpolation = interpolation + Time.deltaTime * 2f;
        material.SetFloat("_Interpolation", interpolation);
    }
}
