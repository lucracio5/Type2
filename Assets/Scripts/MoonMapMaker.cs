using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Timeline;

public class MoonMapMaker : MonoBehaviour
{
    [SerializeField]
    int cell_width = 0;
    [SerializeField]
    int cell_height = 0;
    public List<Vector3> verts = new List<Vector3>();
    public List<int> tris = new List<int>();
    int vert_width;
    public List<MapCell> mapCells = new List<MapCell>();
    public Material material;
    public GameObject Gamemanager;


    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        vert_width = (cell_width * 4) + 1;

        // loop twice and create the vertices
        for (int y = 0; y < (cell_height * 4) + 1; y++)
        {
            for (int x = 0; x < (cell_width * 4) + 1; x++)
            {
                verts.Add(new Vector3(x, 0, -y));
            }
        }
        // loop for each cell
        for (int x = 0; x < cell_width; x++)
        {
            for (int y = 0; y < cell_height; y++)
            {
                int origin = (x * 4) + (y * 4 * vert_width);
                DownTris(origin);
                UpTris(origin + 2);
                DownTris(origin + vert_width);
                UpTris(origin + vert_width + 2);

                UpTris(origin + (vert_width * 2));
                DownTris(origin + (vert_width * 2) + 2);
                UpTris(origin + (vert_width * 3));
                DownTris(origin + (vert_width * 3) + 2);
            }
        }
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        // make the MapCells
        for (int y = 0; y < cell_height; y++)
        {
            for (int x = 0; x < cell_width; x++)
            {
                MapCell newcell = new MapCell(x, y, vert_width);
                newcell.cell_number = mapCells.Count;
                newcell.map_width = cell_width;
                newcell.map_height = cell_height;

                mapCells.Add(newcell);
            }
        }

        // after all cells have been created, get the neighbor cells
        for (int c = 0; c < mapCells.Count; c++)
        {
            for (int d = 0; d < 8; d++)
            {
                if (mapCells[c].GetNeighbor(d) >= 0)
                {
                    mapCells[c].neighbors[d] = mapCells[c].GetNeighbor(d);
                }
            }
        }

        mapCells[0].ChangeHeight(0);
        mapCells[1170].Add_saved_building(3);
        mapCells[1172].Add_saved_building(0);
        mapCells[1174].Add_saved_building(4);
        mapCells[1176].Add_saved_building(2);
        mapCells[1178].Add_saved_building(2);
        mapCells[994].Add_saved_building(6);
        mapCells[1000].Add_saved_building(7);

        StartCoroutine(DelayedBegin());
    }

    ///*
    IEnumerator DelayedBegin()
    {
        yield return null; // Wait one frame
        Gamemanager.GetComponent<Variable_Tracker>().Begin();
    }
    //*/

    public void DownTris(int _origin)
    {
        tris.Add(_origin); 
        tris.Add(_origin + vert_width + 1);
        tris.Add(_origin + vert_width);

        tris.Add(_origin);
        tris.Add(_origin + 1);
        tris.Add(_origin + vert_width + 1);

        tris.Add(_origin + 1);
        tris.Add(_origin + vert_width + 2);
        tris.Add(_origin + vert_width + 1);

        tris.Add(_origin + 1);
        tris.Add(_origin + 2);
        tris.Add(_origin + vert_width + 2);
    }


    public void UpTris(int _origin)
    {
        tris.Add(_origin);
        tris.Add(_origin + 1);
        tris.Add(_origin + vert_width);

        tris.Add(_origin + 1);
        tris.Add(_origin + vert_width + 1);
        tris.Add(_origin + vert_width);

        tris.Add(_origin + 1);
        tris.Add(_origin + 2);
        tris.Add(_origin + vert_width + 1);

        tris.Add(_origin + 2);
        tris.Add(_origin + vert_width + 2);
        tris.Add(_origin + vert_width + 1);
    }

    public MapCell NearestCell(Vector3 _pos)
    {
        MapCell result = null;

        float max = Mathf.Infinity;
        for (int i = 0; i < mapCells.Count; i++)
        {
            if (Vector3.Distance(mapCells[i].centerpoint, _pos) < max)
            {
                max = Vector3.Distance(mapCells[i].centerpoint, _pos);
                result = mapCells[i];
            }
        }

        return result;
    }
    public void Destroy_building(MapCell cell)
    {
        if (cell.building != null)
        {
            Destroy(cell.building);
        }
    }
   

}


public class MapCell
{
    MoonMapMaker map;
    public int cell_number;
    public int map_height;
    public int map_width;
    public int center = -1;
    public Vector3 centerpoint;
    int[] mid_verts = new int[8];
    int[] edge_verts = new int[16];
    public int height = 0;
    public int[] neighbors = {-1, -1, -1, -1, -1, -1, -1, -1 };
    public GameObject building;
    public int contents = -1;


    public MapCell(int x, int y, int width)
    {
        map = GameObject.Find("Map").GetComponent<MoonMapMaker>();
        int origin = (x * 4) + (y * 4 * width);
        center = origin + 2 + (width * 2);
        centerpoint = map.verts[center];
        mid_verts[0] = center - width + 1;
        mid_verts[1] = center + 1;
        mid_verts[2] = center + width + 1;
        mid_verts[3] = center + width;
        mid_verts[4] = center + width - 1;
        mid_verts[5] = center - 1;
        mid_verts[6] = center - width - 1;
        mid_verts[7] = center - width;

        edge_verts[0] = mid_verts[0] - width + 1;
        edge_verts[1] = mid_verts[0] + 1;
        edge_verts[2] = mid_verts[1] + 1;
        edge_verts[3] = mid_verts[2] + 1;
        edge_verts[4] = mid_verts[2] + width + 1;
        edge_verts[5] = mid_verts[2] + width;
        edge_verts[6] = mid_verts[3] + width;
        edge_verts[7] = mid_verts[4] + width;
        edge_verts[8] = mid_verts[4] + width - 1;
        edge_verts[9] = mid_verts[4] - 1;
        edge_verts[10] = mid_verts[5] - 1;
        edge_verts[11] = mid_verts[6] - 1;
        edge_verts[12] = mid_verts[6] - width - 1;
        edge_verts[13] = mid_verts[6] - width;
        edge_verts[14] = mid_verts[7] - width;
        edge_verts[15] = mid_verts[0] - width;
    }
    public void Add_building(int build_index) //Add building with fade in
    {
            MoonMapMaker maker = GameObject.Find("Map").GetComponent<MoonMapMaker>();
            if (building != null)
            {
                maker.Destroy_building(this);
            }
            contents = build_index;
            centerpoint = map.verts[center];
            GameObject instance = GameObject.Find("Map").GetComponent<MapPointer>().buildings[build_index].prefab;
            building = Object.Instantiate(instance, centerpoint, Quaternion.identity);
            building.gameObject.SetActive(true);
            GameObject.Find("Map").GetComponent<MapPointer>().ChangeActiveCursor(0);
            GameObject.Find("Game Manager").GetComponent<Variable_Tracker>().Shop_button_1();
            if (build_index == 7)
            {
                maker.mapCells[cell_number - 1].building = building;//left 1
                maker.mapCells[cell_number - 2].building = building;//left 2
                maker.mapCells[cell_number - map_width].building = building; //up 1
                maker.mapCells[cell_number + map_width].building = building; // back 1
                maker.mapCells[(cell_number + map_width) - 1].building = building; //no results
            }
        if (build_index == 5)
        {

            maker.mapCells[cell_number - (map_width * 2) - 1].make_clickable(build_index, building); //up 2 right 1
            maker.mapCells[cell_number - 1].make_clickable(build_index, building); //left 1
            maker.mapCells[cell_number - 2].make_clickable(build_index, building);//left 2
            maker.mapCells[cell_number + 1].make_clickable(build_index, building); //right 1 
            maker.mapCells[cell_number + 2].make_clickable(build_index, building); //right 2
            maker.mapCells[cell_number - map_width].make_clickable(build_index, building); //up 1
            maker.mapCells[cell_number - 2 * map_width].make_clickable(build_index, building); //up 2
            maker.mapCells[cell_number + map_width].make_clickable(build_index, building); //back 1
            maker.mapCells[cell_number + 2 * map_width].make_clickable(build_index, building); //back 2
            maker.mapCells[cell_number + 1 + (2 * map_width)].make_clickable(build_index, building); //back 2 left 1,2,3
            maker.mapCells[cell_number + 2 + (2 * map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number + 3 + (2 * map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 1 + (2 * map_width)].make_clickable(build_index, building); //back 2 right 1,2,3
            maker.mapCells[cell_number - 2 + (2 * map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 3 + (2 * map_width)].make_clickable(build_index, building);

            maker.mapCells[cell_number + 1 + (map_width)].make_clickable(build_index, building); //back 1 right 1,2,3, left 1,2,3
            maker.mapCells[cell_number + 2 + (map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number + 3 + (map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 1 + (map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 2 + (map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 3 + (map_width)].make_clickable(build_index, building);

            maker.mapCells[cell_number - map_width -1].make_clickable(build_index, building); //up 1 Right 1
            maker.mapCells[cell_number - map_width - map_width + 1].make_clickable(build_index, building); //up 2 Right 1

            maker.mapCells[cell_number - map_width + 1].make_clickable(build_index, building); //up 1 Left 1
            maker.mapCells[cell_number - map_width + 2].make_clickable(build_index, building); //up 1 Left 2
            maker.mapCells[cell_number - map_width * 2 + 1].make_clickable(build_index, building); //up 2 Left 1
            maker.mapCells[cell_number - map_width*2 + 2].make_clickable(build_index, building); //up 2 Left 1

        }


    }
    public void make_clickable(int index, GameObject building)
    {
        GameObject.Find("Map").GetComponent<MoonMapMaker>().mapCells[cell_number].building = building;//left 1
    }
    
    public void add_marker()
    {
        MoonMapMaker maker = GameObject.Find("Map").GetComponent<MoonMapMaker>();
        if (building != null)
        {
            maker.Destroy_building(this);
        }
        contents = 8;
        centerpoint = map.verts[center];
        GameObject instance = GameObject.Find("Map").GetComponent<MapPointer>().buildings[8].prefab;
        building = Object.Instantiate(instance, centerpoint, Quaternion.identity);
        building.gameObject.SetActive(true);
        GameObject.Find("Map").GetComponent<MapPointer>().ChangeActiveCursor(0);
        GameObject.Find("Game Manager").GetComponent<Variable_Tracker>().Shop_button_1();

    }
    public void Add_saved_building(int build_index) //Add building without fade in called in the load part
    {

        MoonMapMaker maker = GameObject.Find("Map").GetComponent<MoonMapMaker>();
        if (building != null)
        {
            GameObject.Find("Map").GetComponent<MoonMapMaker>().Destroy_building(this);
        }
        contents = build_index;
        centerpoint = map.verts[center];
        GameObject instance = GameObject.Find("Map").GetComponent<MapPointer>().buildings[build_index].prefab;
        building = Object.Instantiate(instance, centerpoint, Quaternion.identity);
        building.gameObject.SetActive(true);
        Transparency t = building.GetComponentInChildren<Transparency>();
        if (t != null)
        {

            t.ForceOpaque();
        }
        else
            Debug.LogWarning("transperencey is null");
        
        
        
        if (build_index == 7)
        {
            GameObject.Find("Map").GetComponent<MoonMapMaker>().mapCells[cell_number - 1].building = building;//left 1
            GameObject.Find("Map").GetComponent<MoonMapMaker>().mapCells[cell_number - 2].building = building;//left 2
            GameObject.Find("Map").GetComponent<MoonMapMaker>().mapCells[cell_number - map_width].building = building; //up 1
            GameObject.Find("Map").GetComponent<MoonMapMaker>().mapCells[cell_number + map_width].building = building; // back 1
            GameObject.Find("Map").GetComponent<MoonMapMaker>().mapCells[(cell_number + map_width)-1].building = building; //no results
        }
        if (build_index == 5)
        {
            GameObject.Find("Game Manager").GetComponent<Variable_Tracker>().max_energy = 500;
            maker.mapCells[cell_number - (map_width * 2) - 1].make_clickable(build_index, building); //up 2 right 1
            maker.mapCells[cell_number - 1].make_clickable(build_index, building); //left 1
            maker.mapCells[cell_number - 2].make_clickable(build_index, building);//left 2
            maker.mapCells[cell_number + 1].make_clickable(build_index, building); //right 1 
            maker.mapCells[cell_number + 2].make_clickable(build_index, building); //right 2
            maker.mapCells[cell_number - map_width].make_clickable(build_index, building); //up 1
            maker.mapCells[cell_number - 2 * map_width].make_clickable(build_index, building); //up 2
            maker.mapCells[cell_number + map_width].make_clickable(build_index, building); //back 1
            maker.mapCells[cell_number + 2 * map_width].make_clickable(build_index, building); //back 2
            maker.mapCells[cell_number + 1 + (2 * map_width)].make_clickable(build_index, building); //back 2 left 1,2,3
            maker.mapCells[cell_number + 2 + (2 * map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number + 3 + (2 * map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 1 + (2 * map_width)].make_clickable(build_index, building); //back 2 right 1,2,3
            maker.mapCells[cell_number - 2 + (2 * map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 3 + (2 * map_width)].make_clickable(build_index, building);

            maker.mapCells[cell_number + 1 + (map_width)].make_clickable(build_index, building); //back 1 right 1,2,3, left 1,2,3
            maker.mapCells[cell_number + 2 + (map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number + 3 + (map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 1 + (map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 2 + (map_width)].make_clickable(build_index, building);
            maker.mapCells[cell_number - 3 + (map_width)].make_clickable(build_index, building);

            maker.mapCells[cell_number - map_width - 1].make_clickable(build_index, building); //up 1 Right 1
            maker.mapCells[cell_number - map_width - map_width + 1].make_clickable(build_index, building); //up 2 Right 1

            maker.mapCells[cell_number - map_width + 1].make_clickable(build_index, building); //up 1 Left 1
            maker.mapCells[cell_number - map_width + 2].make_clickable(build_index, building); //up 1 Left 2
            maker.mapCells[cell_number - map_width * 2 + 1].make_clickable(build_index, building); //up 2 Left 1
            maker.mapCells[cell_number - map_width * 2 + 2].make_clickable(build_index, building); //up 2 Left 1

        }
    }
    public string encode_cell()
    {
        string result = "";
        result += contents.ToString();
        return result;
    }
    public void ChangeHeight(int height_change)
    {
        height = height + height_change;
        map.verts[center] = new Vector3(map.verts[center].x, (float)height *.5f, map.verts[center].z);
        centerpoint = map.verts[center];
        foreach (var mid in mid_verts)
        {
            map.verts[mid] = new Vector3(map.verts[mid].x, (float)height * .5f, map.verts[mid].z);
        }
        float[] heights = new float[8];
        for(int i = 0; i < 8; i++)
        {
            if (neighbors[i] != -1)
            {
                heights[i] = map.mapCells[neighbors[i]].height;
            }
        }

        map.verts[edge_verts[0]] = new Vector3(map.verts[edge_verts[0]].x, (height + heights[7] + heights[0] + heights[1]) / 8f, map.verts[edge_verts[0]].z);
        map.verts[edge_verts[1]] = new Vector3(map.verts[edge_verts[1]].x, (height + heights[1]) / 4f, map.verts[edge_verts[1]].z);
        map.verts[edge_verts[2]] = new Vector3(map.verts[edge_verts[2]].x, (height + heights[1]) / 4f , map.verts[edge_verts[2]].z);
        map.verts[edge_verts[3]] = new Vector3(map.verts[edge_verts[3]].x, (height + heights[1]) / 4f, map.verts[edge_verts[3]].z);
        map.verts[edge_verts[4]] = new Vector3(map.verts[edge_verts[4]].x, (height + heights[1] + heights[2] + heights[3]) / 8f, map.verts[edge_verts[4]].z);
        map.verts[edge_verts[5]] = new Vector3(map.verts[edge_verts[5]].x, (height + heights[3]) / 4f, map.verts[edge_verts[5]].z);
        map.verts[edge_verts[6]] = new Vector3(map.verts[edge_verts[6]].x, (height + heights[3]) / 4f, map.verts[edge_verts[6]].z);
        map.verts[edge_verts[7]] = new Vector3(map.verts[edge_verts[7]].x, (height + heights[3]) / 4f, map.verts[edge_verts[7]].z);
        map.verts[edge_verts[8]] = new Vector3(map.verts[edge_verts[8]].x, (height + heights[3] + heights[4] + heights[5]) / 8f, map.verts[edge_verts[8]].z);

        map.verts[edge_verts[9]] = new Vector3(map.verts[edge_verts[9]].x, (height + heights[5]) / 4f, map.verts[edge_verts[9]].z);
        map.verts[edge_verts[10]] = new Vector3(map.verts[edge_verts[10]].x, (height + heights[5]) / 4f, map.verts[edge_verts[10]].z);
        map.verts[edge_verts[11]] = new Vector3(map.verts[edge_verts[11]].x, (height + heights[5]) / 4f, map.verts[edge_verts[11]].z);
        map.verts[edge_verts[12]] = new Vector3(map.verts[edge_verts[12]].x, (height + heights[5] + heights[6] + heights[7]) / 8f, map.verts[edge_verts[12]].z);
        map.verts[edge_verts[13]] = new Vector3(map.verts[edge_verts[13]].x, (height + heights[7]) / 4f, map.verts[edge_verts[13]].z);
        map.verts[edge_verts[14]] = new Vector3(map.verts[edge_verts[14]].x, (height + heights[7]) / 4f, map.verts[edge_verts[14]].z);
        map.verts[edge_verts[15]] = new Vector3(map.verts[edge_verts[15]].x, (height + heights[7]) / 4f, map.verts[edge_verts[15]].z);


        Mesh mesh = map.GetComponent<MeshFilter>().mesh;
        mesh.vertices = map.verts.ToArray();
        mesh.RecalculateNormals();
        map.GetComponent<MeshFilter>().mesh = mesh;
        map.GetComponent<MeshCollider>().sharedMesh = mesh;
    }


    public int GetNeighbor(int dir)
    {
        int result = -1;
        if (dir == 0)
        {
            // if we're not on the top row or right column
            if (cell_number - map_width >= 0 && (cell_number % map_width) != (map_width - 1))
            {
                result = cell_number - map_width + 1;
            }
        }
        if (dir == 1)
        {
            // if we're not on right column
            if ((cell_number % map_width) != (map_width - 1))
            {
                result = cell_number + 1;
            }
        }
        if (dir == 2)
        {
            // if we're not on bottom row or right column
            if (cell_number !< (map_height * map_width) - map_width && (cell_number % map_width) != (map_width - 1))
            {
                result = cell_number + map_width + 1;
            }
        }
        if (dir == 3)
        {
            // if we're not on the bottom row
            if (cell_number !< (map_height * map_width) - map_width)
            {
                result = cell_number + map_width;
            }
        }
        if (dir == 4)
        {
            // if we're not on the bottom row or left column
            if (cell_number !< (map_height * map_width) - map_width && (cell_number % map_width) != 0)
            {
                result = cell_number + map_width - 1;
            }
        }
        if (dir == 5)
        {
            // if we're not on the left column
            if ((cell_number % map_width) != 0)
            {
                result = cell_number - 1;
            }
        }
        if (dir == 6)
        {
            // if we're not on the top row or left column
            if (cell_number - map_width >= 0 && (cell_number % map_width) != 0)
            {
                result = cell_number - map_width - 1;
            }
        }
        if (dir == 7)
        {
            // if we're not on the top row
            if (cell_number - map_width >= 0)
            {
                result = cell_number - map_width;
            }
        }
        return result;
    }



}

