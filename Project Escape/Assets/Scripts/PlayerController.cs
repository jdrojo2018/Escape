using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int iBoardColumnIndex = 0;
    public int iBoardRowIndex = 0;
    public float fMovementSpeed;
    public int iLookDirection;
    public int iDirectionToMove;

    private CellMap cmController;

    int[,] iiCellBoard;
    
    private float fTraveledDistance; 
    
    // Start is called before the first frame update
    void Start()
    {
        iDirectionToMove = -1;
        fMovementSpeed = 2.0f;
        iLookDirection = -1;
        fTraveledDistance = 0;
        cmController = GameObject.Find("CellContainer").GetComponent(typeof(CellMap)) as CellMap;
        iiCellBoard = cmController.getMap();


        if (!cmController) Debug.Log("No encontrado");
        CenterPosicionOnCell();
    }
    // Update is called once per frame
    void Update()
    {
        if (iDirectionToMove == -1) {

            CheckKeyboardInput();

            if (iDirectionToMove >= 0 && !IsMovementAvailable(iDirectionToMove)) iDirectionToMove = -1;
        }
        else
        {
            rotateFacingDirection();

            transform.Translate(Vector3.forward * fMovementSpeed * Time.deltaTime);
            fTraveledDistance += fMovementSpeed * Time.deltaTime;

            if (fTraveledDistance >= 1)
            {
                CenterPosicionOnCell();

                if (LevelExitReached())
                {
                    Application.Quit();
                    //if (UnityEditor.EditorApplication.isPlaying)
                    //{
                    //    UnityEditor.EditorApplication.isPlaying = false;
                    //}
                }
            }
        }
    }

    private void CheckKeyboardInput()
    {
        if (Input.GetKey(KeyCode.W)) iDirectionToMove = 0;
        if (Input.GetKey(KeyCode.A)) iDirectionToMove = 1;
        if (Input.GetKey(KeyCode.S)) iDirectionToMove = 2;
        if (Input.GetKey(KeyCode.D)) iDirectionToMove = 3;
    }

    private bool LevelExitReached()
    {
        return iiCellBoard[iBoardRowIndex, iBoardColumnIndex] == 4;
    }

    private bool IsMovementAvailable(int c)
    {

        switch (c)
        {
            case 0: //Arriba
                if (iBoardRowIndex == 0) return false;

                if (IsCellWalkable(iBoardRowIndex - 1, iBoardColumnIndex))
                {
                    iBoardRowIndex--;
                    return true;
                }
                break;
            case 1: //Izquierda
                if (iBoardColumnIndex == 0) return false;

                if (IsCellWalkable(iBoardRowIndex, iBoardColumnIndex - 1))
                {
                    iBoardColumnIndex--;
                    return true;
                }
                break;
            case 2: //Abajo
                if (iBoardRowIndex == 9) return false;

                if (IsCellWalkable(iBoardRowIndex + 1, iBoardColumnIndex))
                {
                    iBoardRowIndex++;
                    return true;
                }
                break;

            case 3: //Derecha
                if (iBoardColumnIndex == 9) return false;

                if (IsCellWalkable(iBoardRowIndex, iBoardColumnIndex + 1))
                {
                    iBoardColumnIndex++;
                    return true;
                }
                break;
        }

        return false;
    }

    private bool IsCellWalkable(int indexRow, int indexColumn)
    {
        return !(IsCellAWall(indexRow, indexColumn) || IsCellAnObstacle(indexRow, indexColumn));
    }

    private bool IsCellAWall(int indexRow, int indexColumn)
    {
        return (iiCellBoard[indexRow, indexColumn] == 1);
    }

    private bool IsCellAnObstacle(int indexRow, int indexColumn)
    {
        return (iiCellBoard[indexRow, indexColumn] == 2);
    }

    private bool rotateFacingDirection()
    {
        if (iDirectionToMove != iLookDirection)
        {
            switch (iDirectionToMove)
            {
                case 0:
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case 1:
                    this.transform.rotation = Quaternion.Euler(0, -90, 0);
                    break;
                case 2:
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case 3:
                    this.transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
            }
            iLookDirection = iDirectionToMove;
        }

        return false;
    }

    public void SetBoardIndexPosition(Vector2 vPos)
    {
        iBoardColumnIndex = (int) vPos.x;
        iBoardRowIndex = (int) vPos.y;
    }

    void CenterPosicionOnCell()
    {
        //Centra el movimiendo a la casilla.
        transform.position = new Vector3(iBoardColumnIndex - cmController.fCellOffsetZ, 0.5f, cmController.fCellOffsetX - iBoardRowIndex);
        iDirectionToMove = -1;
        fTraveledDistance = 0;
        Debug.Log("Estoy en: " + iBoardColumnIndex + " " + iBoardRowIndex);
    }
}
