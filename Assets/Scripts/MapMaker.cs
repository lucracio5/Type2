using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;


public class MapMaker : MonoBehaviour
{
    List<Vector3> verts = new List<Vector3>();
    List<int> triangles = new List<int>();
    public int height;
    public int width;
    


    // Start is called before the first frame update
    void Start()
    {
        BuildMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void BuildMesh()
    {

        for (int y = 0;y <= height*4;y++)
        {
            for(int x = 0;x <= width*4;x++)
            {
                verts.Add(new Vector3((float)x,0f, (float)y));
            }
        
        }
        for(int x = 0; x < width;x++)
        {

            for(int y = 0;y <= height;y++)
            {
                int total = 0;
                total = y * (width * 4) + y + (height*4);//x*4
                Down_right(total);
                Down_right(total+4);
                Up_right(total+8);
                Up_right(total+12);

            }
        }
            
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    void Down_right(int _total)
    {
        int height_offset = (width * 4) + 1;
        int width_offset = 4;
        triangles.Add(_total);
        triangles.Add(_total + width_offset);
        triangles.Add(_total + height_offset + width_offset);
        triangles.Add(_total);
        triangles.Add(_total + height_offset + width_offset);
        triangles.Add(_total + height_offset);
    }
    void Up_right(int _total)
    {
        int height_offset = (width * 4) + 1;
        int width_offset = 4;
        triangles.Add(_total);
        triangles.Add(_total + width_offset);
        triangles.Add(_total + height_offset);
        triangles.Add(_total +  width_offset);
        triangles.Add(_total + height_offset + width_offset);
        triangles.Add(_total + height_offset);
    }

}
