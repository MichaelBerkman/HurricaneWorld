using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCode : MonoBehaviour {

   public float size = 0.1f;
    public float speed = 2.0f;
    public float noiseStrength = 1f;
    public float noise = 1f;

    private Vector3[] baseHeight;

    

	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        if (baseHeight == null) {
            baseHeight = mesh.vertices;
        }

        Vector3[] vertices = new Vector3[baseHeight.Length];

        for(int i=0; i<vertices.Length;i++)
        {
            Vector3 vertex = baseHeight[i];
            vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * size;
            vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noise, baseHeight[i].y + Mathf.Sin(Time.time * .1f)) * noiseStrength;
            vertices[i] = vertex;

        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

	}
}
