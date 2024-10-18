using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    public GameObject Door;
    public GameObject DoorPos;
    public void OnUnlocked()
    {
            Debug.Log("Door unlocked");
            Door.transform.position = DoorPos.transform.position;
    }

}
