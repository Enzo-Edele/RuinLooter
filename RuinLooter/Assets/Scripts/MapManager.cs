using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                
            }
            return _instance;
        }
    }
    public List<Room> rooms = null;
    public List<Room> first = null;
    List<Room> memory = null;
    public static int count;
    void Awake()
    {
        _instance = this;
        int random = Random.Range(0, first.Count);
        Vector2 spawnPos = transform.position;
        spawnPos.x = 0.0f;
        spawnPos.y = 0.0f;
        Room firstRoom = Instantiate(first[random], spawnPos, Quaternion.identity);
        //memory.Add(firstRoom);
        count = 0;
        SpawnRoom(first[random]);
    }

    private void SpawnRoom(Room room)
    {
        if (count > 50)
        {
            Room endRoom = Instantiate(room.end, room.transform.position - room.exits[0].position, Quaternion.identity);
            return;
        }

        int randomRoom = Random.Range(0, room.rooms.Count - 1);
        Room newRoom = room.rooms[randomRoom];
        for(int i=0; i < newRoom.entries.Count; i++)
        {
            for(int j=0; j < room.exits.Count; j++)
            {
                //for (int k = 0; k < memory.Count; k++)
                {
                    if (newRoom.entries[i].IsDoorValid(room.exits[j])/* || memory[k].wall[i].IsDoorValid(room.exits[j])*/)
                    {
                        Room newRoomObject = Instantiate(newRoom, newRoom.entries[i].position + room.transform.position, Quaternion.identity);
                        count += 1 + newRoom.count;
                        //memory.Add(newRoomObject);
                        SpawnRoom(newRoomObject);
                        break;
                    }
                }
            }
        }  
    }
}
