using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SphereReveal : MonoBehaviour
{
    //static radius of the sphere
    public float radius = 6f;
    //dynamic radius (changes with time)
    public float rad;
    //positive=sphere reveals, negative=sphere hides
    public float softness = 0.05f;
    
    


    

    private void Start()
    {
        rad = radius;
    }
    
    void Update()
    {
        
        radius = radius- (rad/10)*Time.deltaTime;
        Shader.SetGlobalVector("_GLOBALMaskPosition", transform.position);
        Shader.SetGlobalFloat("_GLOBALMaskRadius", radius);
        Shader.SetGlobalFloat("_GLOBALMaskSoftness", softness);
       
    }
}
