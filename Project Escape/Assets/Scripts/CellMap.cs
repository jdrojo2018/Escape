using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMap : MonoBehaviour
{
    Vector2 vSize;
    private int[,] coordinates;
    private Cell [,] cells;
    public float fCellOffsetX;
    public float fCellOffsetZ;
    public int iSizeX, iSizeY;
    public GameObject[] goModelList;
    private Vector2 vPlayerSpawn, vExit;



    // Start is called before the first frame update
    void Start()
    {
        cells = new Cell[iSizeX, iSizeY];

        /*
            0 -> Empty cell
            1 -> Cannot pass cell
            2 -> Can destroy cell
            3 -> Start
            4 -> End
        */

        fCellOffsetX = 4.5f;
        fCellOffsetZ = 4.5f;

        coordinates = new int[10, 10] {
            {0, 0, 0, 0, 0, 0, 0, 1, 0, 4},
            {0, 1, 1, 1, 0, 1, 0, 1, 0, 1},
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 1, 2, 1, 1, 0, 0, 0},
            {1, 0, 1, 1, 0, 1, 1, 0, 0, 0},
            {1, 0, 1, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 2, 1, 1, 1, 1, 1, 0, 2},
            {0, 0, 0, 0, 1, 2, 1, 1, 0, 0},
            {0, 3, 0, 0, 1, 0, 1, 1, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        for (int i = 0; i < coordinates.GetLength(0); i++)
        {
            for (int j = 0; j < coordinates.GetLength(1); j++)
            {
                cells[i, j] = new Cell();
                cells[i, j].SetCellCode(coordinates[i, j]);
                cells[i, j].SetPos(new Vector2(j - fCellOffsetZ, fCellOffsetX - i));
               
                if(coordinates[i, j] == 3)
                {
                    GameObject goPlayerCapsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    cells[i, j].SetModel(goModelList[0], goPlayerCapsule);
                    Destroy(goPlayerCapsule);
                    vPlayerSpawn.x = j;
                    vPlayerSpawn.y = i;
                }
                else
                {
                    cells[i, j].SetModel(goModelList[0], goModelList[coordinates[i, j]]);
                }
                cells[i, j].SetBehaviour();

            }
        }
        GameManager.manager.SpawnPlayer(vPlayerSpawn);
    }

    void Update()
    {
        
    }

    public int[,] getMap()
    {
        return coordinates;
    }
}
