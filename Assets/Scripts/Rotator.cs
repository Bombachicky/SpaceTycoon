using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public Vector3Int size { get; private set; }
    private Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void get_collider_vertex_positions_local()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        vertices = new Vector3[4];
        vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
    }

    private void calculate_size_in_cells()
    {
            Vector3Int[] Vertices = new Vector3Int[vertices.Length];

        for(int i = 0; i < vertices.Length; i++)
        {
                 Vector3 worldPos = transform.TransformPoint(vertices[i]);
                 vertices[i] = Building_System.current.grid_layout.WorldToCell(worldPos);
        }
        size = new Vector3Int(Mathf.Abs((Vertices[0] - Vertices[1]).x), Mathf.Abs((Vertices[0] - Vertices[3]).y), 1);
    }

    public void rotate()
    {
        transform.Rotate(new Vector3(0, 90, 0));
        size = new Vector3Int(size.y, size.x, 1);

        Vector3[] Vertices = new Vector3[vertices.Length];
        for(int i = 0; i < Vertices.Length; i++)
        {
            vertices[i] = Vertices[(i + 1) % vertices.Length];
        }
        vertices = Vertices;
    }
    
}
