using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMap : MonoBehaviour
{
    private int[,] coordinates;
    public CellBehaviour cell;

    // Start is called before the first frame update
    void Start()
    {
        /*
            0 -> Empty cell
            1 -> Cannot pass cell
            2 -> Can destroy cell
            3 -> Start
            4 -> End
        */

        coordinates = new int[10, 10] {
            {0, 0, 0, 2, 0, 0, 0, 1, 0, 4},
            {0, 1, 1, 1, 0, 1, 0, 1, 2, 1},
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 1, 0, 1, 1, 0, 0, 0},
            {1, 0, 1, 1, 0, 1, 1, 0, 0, 0},
            {1, 0, 1, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 1, 1, 1, 1, 1, 0, 2},
            {0, 0, 0, 0, 1, 0, 1, 1, 0, 0},
            {0, 3, 0, 0, 1, 0, 1, 1, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        for (int i = 0; i < coordinates.GetLength(0); i++)
        {
            for(int j = 0; j < coordinates.GetLength(1); j++)
            {
                Vector3 position = new Vector3(j - 4.5f, -0.5f, 4.5f - i);
                Quaternion rotation = new Quaternion();
                cell.CellCode = coordinates[i, j];
                Instantiate(cell, position, rotation);
                if(cell.CellCode == 3)
                {
                    GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    Instantiate(capsule);
                    capsule.transform.Translate(0, 5, 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int[,] getMap()
    {
        return coordinates;
    }
}
