using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeShaderColor : MonoBehaviour
{
    // Start is called before the first frame update
    Material material;
    private float interpolation = 0;
    private ParticleSystem particleSystem;
    void Start()
    {
      
        material = GetComponent<ParticleSystemRenderer>().materials[0];
        particleSystem = GetComponent<ParticleSystem>();
       
        var main = particleSystem.main;
        print(main.startSize);
        main.startSize = 10f;
     

    }

    // Update is called once per frame
    void Update()
    {
        //print("allo");
       
        interpolation = interpolation + Time.deltaTime * 0.8f;
        material.SetFloat("_Interpolation", interpolation);
    }
}
