using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor;
    public int distanceToEnd;

    public Transform generatorPoint;

    public enum Direction { up, right, down, left };
    public Direction selectedDirection;

    public float xOffset = 18;
    public float yOffset = 10;

    public LayerMask whatIsRoom;

    private GameObject endRoom;
    private List<GameObject> roomList = new List<GameObject>();

    public RoomPrefabs rooms;
    private List<GameObject> generatedOutlines = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();

        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
            roomList.Add(newRoom);

            if(i+1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                roomList.RemoveAt(roomList.Count - 1);
                endRoom = newRoom;
            }

            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            while (Physics2D.OverlapCircle(generatorPoint.position, .2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }
        }

        CreateRoomOutline(Vector3.zero);
        foreach (GameObject room in roomList)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;
        }
    }

    public void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0), .2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0), .2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0), .2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0), .2f, whatIsRoom);

        int directionCount = 0;

        if (roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }

        switch (directionCount)
        {
            case 0:
                Debug.LogError("Found no room exists");
                break;
            case 1:
                if (roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, Quaternion.identity));
                }

                if (roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, Quaternion.identity));
                }

                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, Quaternion.identity));
                }

                if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, Quaternion.identity));
                }
                break; 
            case 2:
                if (roomAbove && roomBelow)                                    
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, Quaternion.identity));                    
                
                if (roomLeft && roomRight)
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, Quaternion.identity));

                if(roomAbove && roomRight)
                    generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, Quaternion.identity));

                if (roomBelow && roomRight)
                    generatedOutlines.Add(Instantiate(rooms.doubleRightDown, roomPosition, Quaternion.identity));

                if(roomBelow && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, Quaternion.identity));

                if (roomAbove && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, roomPosition, Quaternion.identity));
                break;
            case 3:
                if(roomAbove && roomRight && roomBelow)
                    generatedOutlines.Add(Instantiate(rooms.tripleUpRightDown, roomPosition, Quaternion.identity));

                if(roomRight && roomBelow && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.tripleRightDownLeft, roomPosition, Quaternion.identity));

                if( roomBelow && roomLeft && roomAbove)
                    generatedOutlines.Add(Instantiate(rooms.tripleDownLeftUp, roomPosition, Quaternion.identity));

                if(roomAbove && roomRight && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, Quaternion.identity));
                break;
            case 4:
                generatedOutlines.Add(Instantiate(rooms.fourway, roomPosition, Quaternion.identity));
                break;

        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singleLeft,
        doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp,
        tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
        fourway;
}
