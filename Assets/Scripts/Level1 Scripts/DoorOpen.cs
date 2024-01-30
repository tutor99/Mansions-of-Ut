using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaOpen : MonoBehaviour
{
    public Animator door;
    private bool inZone;
    private bool isDoorOpen;

    public void OpenDoor()
    {
        isDoorOpen = !isDoorOpen;

        if (isDoorOpen)
        {
            door.SetBool("DoorActive", true);
        }
        else
        {
            door.SetBool("DoorActive", false);
        }
    }

    public bool IsDoorOpen()
    {
        return isDoorOpen;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = false;
        }
    }
}
