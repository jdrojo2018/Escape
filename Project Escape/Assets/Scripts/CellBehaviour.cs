using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public int CellCode;

    private Renderer thisRend;

    void Start()
    {
        thisRend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (CellCode)
        {
            case 0:
                thisRend.material.SetColor("_Color", Color.white);
                break;
            case 1:
                thisRend.material.SetColor("_Color", Color.red);
                break;
            case 2:
                thisRend.material.SetColor("_Color", Color.yellow);
                break;
            case 3:
                thisRend.material.SetColor("_Color", Color.blue);
                break;
            case 4:
                thisRend.material.SetColor("_Color", Color.magenta);
                break;
            case 5: //Just in order to debug
                thisRend.material.SetColor("_Color", Color.black);
                break;
        }

        
    }
}
