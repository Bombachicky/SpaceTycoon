using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Building_System : MonoBehaviour
{
    private Vector3 offset;

    private bool IsMouseDown = false;

    public static Building_System current;

    public GridLayout grid_layout;
    private Grid grid;
    [SerializeField] private Tilemap main_tilemap;
    [SerializeField] private TileBase white_tile;
    public Vector3Int size { get; private set; }
    private Vector3[] vertices;

    public GameObject prefab1;

    bool direction;
    
    #region Unity Regions

    private void Awake()
    {
        current = this;
        grid = grid_layout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gameManager.instance.pauseMenu)
        {
            InitializeWithObject(prefab1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    rotate(hit.collider.transform, true);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    rotate(hit.collider.transform, false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (IsMouseDown)
            {
                OnMouseDrag();
            }
            else
            {
                OnMouseDown();
                IsMouseDown = true;
            }

        }
        else
        {
            IsMouseDown = false;
        }
    }

    #endregion

    #region Utils
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 snap_coordinate_to_grid(Vector3 position)
    {
        Vector3Int cell_pos = grid_layout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cell_pos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }
    #endregion

    #region Building Placement

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = snap_coordinate_to_grid(GetMouseWorldPosition());
        GameObject obj = Instantiate(prefab, position, Quaternion.identity, main_tilemap.transform);
        obj.AddComponent<Object_Drag>();
        obj.GetComponent<Rotator>();
    }

    #endregion

    private void OnMouseDown()
    {   
        offset = gameObject.transform.position - Building_System.GetMouseWorldPosition();
    }


    private void OnMouseDrag()
    {
        Vector3 pos = Building_System.GetMouseWorldPosition() + offset;
        transform.position = Building_System.current.snap_coordinate_to_grid(pos);
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

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            vertices[i] = Building_System.current.grid_layout.WorldToCell(worldPos);
        }
        size = new Vector3Int(Mathf.Abs((Vertices[0] - Vertices[1]).x), Mathf.Abs((Vertices[0] - Vertices[3]).y), 1);
    }

    public void rotate(Transform object_to_rotate, bool direction)
    {
        if(direction)
        {
            object_to_rotate.Rotate(new Vector3(0, 90, 0));
            size = new Vector3Int(size.y, size.x, 1);

            Vector3[] Vertices = new Vector3[vertices.Length];
            for (int i = 0; i < Vertices.Length; i++)
            {
                vertices[i] = Vertices[(i + 1) % vertices.Length];
            }
            vertices = Vertices;
        }
        if(!direction)
        {
            object_to_rotate.Rotate(new Vector3(0, -90, 0));
            size = new Vector3Int(size.y, size.x, 1);

            Vector3[] Vertices = new Vector3[vertices.Length];
            for (int i = 0; i < Vertices.Length; i++)
            {
                vertices[i] = Vertices[(i + 1) % vertices.Length];
            }
            vertices = Vertices;
        }
    }
}
