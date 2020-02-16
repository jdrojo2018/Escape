using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int iColumnPosition;
    public int iRowPosition;
    public float fMovementSpeed;
    public int iLookDirection;
    public int iMoveDirection;
    private CellMap controlador;
    private float fTraveledDistance; 

    // Start is called before the first frame update
    void Start()
    {
        iColumnPosition = 0;
        iRowPosition = 0;
        iMoveDirection = -1;
        fMovementSpeed = 5.0f;
        iLookDirection = -1;
        fTraveledDistance = 0;

        controlador = GameObject.Find("CellContainer").GetComponent(typeof(CellMap)) as CellMap;
        if (!controlador) Debug.Log("No encontrado");
    }
    // Update is called once per frame
    void Update()
    {

        if (iMoveDirection==-1) {
            KeyboardCheck();
            if (iMoveDirection >= 0 && !CanMoveTowardsDirection(iMoveDirection)) iMoveDirection = -1;
        }
        else
        {
            UpdateMovement();
        }
    }

    private void KeyboardCheck()
    {
        if (Input.GetKey(KeyCode.W)) iMoveDirection = 0;
        if (Input.GetKey(KeyCode.A)) iMoveDirection = 1;
        if (Input.GetKey(KeyCode.S)) iMoveDirection = 2;
        if (Input.GetKey(KeyCode.D)) iMoveDirection = 3;
    }

    private void UpdateMovement()
    {
        RotateFacingDirection(iMoveDirection);

        transform.Translate(Vector3.forward * fMovementSpeed * Time.deltaTime);
        fTraveledDistance += fMovementSpeed * Time.deltaTime;

        if (fTraveledDistance >= 1)
        {
            //Centra el movimiendo a la casilla y finaliza el movimiento.
            transform.position = new Vector3(iColumnPosition - controlador.posz, 0.5f, controlador.posx - iRowPosition);
            iMoveDirection = -1;
            fTraveledDistance = 0;
            Debug.Log("Estoy en: " + iColumnPosition + " " + iRowPosition);
        }
    }


    private bool CanMoveTowardsDirection(int c)
    {
        int[,] mapa = controlador.getMap();
        switch (c)
        {
            case 0://Arriba
                if (iRowPosition == 0) return false;

                if (mapa[iRowPosition - 1, iColumnPosition] == 0)
                {
                    iRowPosition--;
                    return true;
                }
                break;
            case 1://Izquierda
                if (iColumnPosition == 0) return false;

                if (mapa[iRowPosition, iColumnPosition - 1] == 0)
                {
                    iColumnPosition--;
                    return true;
                }
                break;
            case 2://Abajo
                if (iRowPosition == 9) return false;

                if (mapa[iRowPosition + 1, iColumnPosition] == 0)
                {
                    iRowPosition++;
                    return true;
                }
                break;

            case 3://Derecha
                if (iColumnPosition == 9) return false;

                if (mapa[iRowPosition, iColumnPosition + 1] == 0)
                {
                    iColumnPosition++;
                    return true;
                }
                break;
        }
        return false;
    }
    private bool RotateFacingDirection(int newDir)
    {
        if (newDir != iLookDirection)
        {
            switch (newDir)
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
            iLookDirection = newDir;
        }
        return false;
    }
}
