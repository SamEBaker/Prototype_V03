using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//https://www.youtube.com/watch?v=cLzG1HDcM4s

public class Interact : MonoBehaviour
{
    public string popUpMessage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            // Find the player GameObject and get the PlayerController component
            Player playerController = other.GetComponent<Player>();
            playerController.InteractPopUp(popUpMessage);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerController = other.GetComponent<Player>();
            playerController.InteractPopUp(" ");
        }

    }
}
