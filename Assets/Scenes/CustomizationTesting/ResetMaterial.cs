using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMaterial : MonoBehaviour
{
    private Renderer renderer;
    //private Material m_Material;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        //m_Material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetMaterial() {
        renderer.material.color = Color.white;
        //m_Material.color = Color.white;
    }
}
