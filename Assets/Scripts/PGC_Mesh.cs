using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//references:
//https://catlikecoding.com/unity/tutorials/procedural-grid/

public class PGC_Mesh : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Rigidbody rb;
    private MeshColliderCookingOptions meshColliderCookingOptions;

    private float yVal;
    public float offsetX;
    public float offsetY;

    private Vector3[] vertices;
    public int xSize;
    public int ySize;
    private Mesh mesh;

    private void Awake()
    {
        //add necessary components
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = Resources.Load<Material>("MeshMaterial");
        meshCollider = gameObject.AddComponent<MeshCollider>();
        rb = gameObject.AddComponent<Rigidbody>();

        Generate();

    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";


        vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        for(int i = 0, y = 0; y <= ySize; y++)
        {
            for(int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
            }
        }

        mesh.vertices = vertices;
    }

    //to show vertices
    private void OnDrawGizmos()
    {
        if(vertices == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

}
