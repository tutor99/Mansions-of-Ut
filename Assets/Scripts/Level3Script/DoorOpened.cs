using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaOpenLevel3 : MonoBehaviour
{

    public Animator door;
    private bool inZone;
    private bool isDoorOpen;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inZone == true){
            
            isDoorOpen = !isDoorOpen;

            if(isDoorOpen == true){
                door.SetBool("DoorOpened", true);
                 
            };

            if(isDoorOpen == false){
                door.SetBool("DoorOpened", false);
            };
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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
