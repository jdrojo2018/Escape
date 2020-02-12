using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canPass;
    private Renderer thisRend;

    void Start()
    {
        thisRend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPass)
        {
            thisRend.material.SetColor("_Color", Color.white);
        }
        else
        {
            thisRend.material.SetColor("_Color", Color.red);
        }
    }
}
