using System.Collections.Generic;
using Assets.Scripts.Generators;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private readonly Room room = new Room();
    public GameObject square;

    private List<Vector2> newList;
    private Transform boardHolder;

    // Use this for initialization
    void Start()
    {
        GenerateRoom();
    }

    private void GenerateRoom()
    {
        var board = GameObject.Find("Board");
        if (board != null)
        {
            Destroy(board);
            boardHolder = new GameObject("Board").transform;
        }
        else
        {
            boardHolder = new GameObject("Board").transform;
        }

        newList = new List<Vector2>();
        newList = room.Generate(1000);
        Debug.Log(newList.Count);

        foreach (var vector2 in newList)
        {
            //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
            GameObject instance =
                Instantiate(square, vector2, Quaternion.identity) as GameObject;

            //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
            instance.transform.SetParent(boardHolder);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            GenerateRoom();
        }
    }



    // TODO: implement LoadingScreen

    // TODO: implement Menu

}
