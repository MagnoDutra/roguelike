using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenCleared;
    public List<GameObject> enemies = new List<GameObject>();

    public Room theRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && theRoom.roomActive && openWhenCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        }
    }
}