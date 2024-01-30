using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpen : MonoBehaviour
{

    public Animator drawer;
    private bool inZone;
    private bool isDrawerOpen;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inZone == true){
            
            isDrawerOpen = !isDrawerOpen;

            if(isDrawerOpen == true){
                drawer.SetBool("OpenDrawer", true);
                 
            };

            if(isDrawerOpen == false){
                drawer.SetBool("OpenDrawer", false);
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
