using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public GameObject goPlayer;
    public GameObject goExit;
    public GameObject goMap;

    private void Awake()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(this.gameObject);


    }
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
            //if (UnityEditor.EditorApplication.isPlaying)
            //{
            //    UnityEditor.EditorApplication.isPlaying = false;
            //}
        }
    }

    public void SpawnPlayer(Vector2 vPos)
    {
        GameObject go = Instantiate(goPlayer, new Vector3(vPos.x, 0.0f, vPos.y), Quaternion.identity);
        go.GetComponent<PlayerController>().SetBoardIndexPosition(vPos);
    }

}
