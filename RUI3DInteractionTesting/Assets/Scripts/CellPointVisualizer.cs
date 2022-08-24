using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CellPointVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject pre_cell;
    [SerializeField] private GameObject parent;
    [SerializeField] private string filename;
    [SerializeField] List<Coordinate> coordindates = new List<Coordinate>();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
        CreateCells();
    }

    void CreateCells()
    {
        for (int i = 0; i < coordindates.Count; i++)
        {
            GameObject cell = Instantiate(pre_cell, Vector3.zero, Quaternion.identity, parent.transform);
            //cell.transform.parent = parent.transform;
            cell.transform.position = new Vector3(-coordindates[i].x, coordindates[i].y, coordindates[i].z);
            Debug.Log("old: " + cell.transform.GetMatrix());
            Matrix4x4 matrixReflected = cell.transform.GetMatrix() * ReflectX();
            Debug.Log("matrixReflected: " + matrixReflected);
            cell.transform.position = matrixReflected.GetPosition();
            cell.transform.rotation = matrixReflected.rotation;
            cell.transform.localScale = matrixReflected.lossyScale;
            Debug.Log("new: " + cell.transform.GetMatrix());
        }
    }

    void ReadCSV()
    {
        using (var reader = new StreamReader("Assets/Data/" + filename + ".csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                coordindates.Add(new Coordinate(float.Parse(line.Split(',')[0]), float.Parse(line.Split(',')[1]), float.Parse(line.Split(',')[2])));
            }
        }
    }

    Matrix4x4 ReflectX()
    {
        var result = new Matrix4x4(
            new Vector4(-1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, -1, 0),
            new Vector4(0, 0, 0, 1)
        );
        return result;
    }

    [Serializable]
    struct Coordinate
    {
        public float x;
        public float y;
        public float z;

        public Coordinate(float x, float y, float z)
        {
            this.x = x; this.y = y; this.z = z;
        }
    }
}
