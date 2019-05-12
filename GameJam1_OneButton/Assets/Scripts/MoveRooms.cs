using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRooms : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public GameObject currentRoom;

    public void MoveRoomsBack(){
        GameObject randomRoom = roomPrefabs[Random.RandomRange(0,roomPrefabs.Length)];
        randomRoom = Instantiate(randomRoom, new Vector3(0,0,20), Quaternion.identity);
        randomRoom.transform.SetParent(this.transform);

        randomRoom.GetComponent<Room>().MoveRoom(0.5f);
        currentRoom.GetComponent<Room>().MoveRoom(0.5f);
        currentRoom = randomRoom;
    }
}
