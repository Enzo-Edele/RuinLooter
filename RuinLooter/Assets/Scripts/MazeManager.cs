using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    private static MazeManager _instance;
    public static MazeManager Instance
    {
        get
        {
            if (_instance == null)
            {

            }
            return _instance;
        }
    }
    public bool[,] datas = new bool[20, 20];
    public RoomDatas startRoom;
    private int maxRoom;
    void Awake()
    {
        _instance = this;
        for (int i = 0; i < 20; i++)
        {
            for(int j = 0; j < 20; j++)
            {
                datas[i, j] = false;
            }
        }
        Spawn(startRoom, 9, 9, transform.position);
    }

    void Spawn(RoomDatas room, int xPos, int yPos, Vector2 position)
    {
        Debug.Log(xPos + ", " + yPos);
        if (xPos > -1 && xPos < 20 && yPos > -1 && yPos < 20)
        {
            Instantiate(room, position, Quaternion.identity);
            datas[xPos, yPos] = true;
            RoomDatas newRoom;
            foreach (Spawner spawn in room.spawners)
            {
                if (spawn.direction == 1)
                {
                    xPos += 1;
                    newRoom = room;
                    Spawn(newRoom, xPos, yPos, spawn.transform.position);
                }
                else if (spawn.direction == 2)
                { 
                    yPos -= 1;
                    newRoom = room;
                    Spawn(newRoom, xPos, yPos, spawn.transform.position);
                }
            }
        }
    }
}
