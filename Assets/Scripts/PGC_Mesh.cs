using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//references:
//https://catlikecoding.com/unity/tutorials/procedural-grid/
//lecture

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
        SpawnPrimitiveCube();

        MeshWavePerls(5f, 5f);
        newverts = new Vector3((xSize + 1) * (ySize + 1));
        
    }

    private void Start()
    {
        Generate();
        MeshWavePerls(5f, 5f);
    }

    private void Update()
    {
        MeshWavePerls(5f, 5f);

        MeshWave();
    }

    private void MeshWave()
    {

    }
    private void MeshWavePerls()
    {

    }

    private void SpawnPrimitiveCube()
    {
        Rigidbody cubeRb;
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        cube.transform.position = new Vector3(20, 20, 20);
        cubeRb = cube.AddComponent<Rigidbody>();
        cubeRb.useGravity = true;
    }

    private void Generate()
    {


        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        rb.useGravity = false;
        rb.isKinematic = true;


        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;


        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }

        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshCollider.sharedMesh = mesh;
        meshCollider.convex = false;

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
