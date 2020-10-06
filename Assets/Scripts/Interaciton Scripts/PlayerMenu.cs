using System;
using UnityEngine;
using Valve.VR;

// Author: Elvir Sinancevic
public class PlayerMenu : MonoBehaviour
{
    // References to the prefabs of the Menu (this can Change in future and will still be working fine)
    public GameObject prefab;
    
    // Our SteamVR Input Source (in our case button click)
    public SteamVR_Input_Sources clickButton = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean clickAction;
    
    // Local variables for the menu and the camera of the scene
    private GameObject menu = null;
    #pragma warning disable
    private GameObject camera = null;

    // "Sync" variables
    private bool menuExists = false;
    private bool beingHandelt = false;
    private bool otherMenusOpen = false;
    private String otherMenusName = null;

    // Start is called before the first frame update
    void Start()
    { 
        // Set the parameters and get camera for the placement of the menu
        menuExists = false; 
        beingHandelt = false;
        otherMenusOpen = false;
        otherMenusName = null;
        camera = GameObject.Find("Camera");
    }

    // Update is called once per frame
    public void Update()
    {
        // Execute the function only if the button is pressed and if it is not already running
        // For dev.purposes you can also press X to execute the funtion
        if ((clickAction.GetStateDown(clickButton) || Input.GetKeyDown(KeyCode.X)) && !beingHandelt)
        {
            HandleButtonPress();
        }

        // Update the position and rotation of the menu
        if (menuExists)
        {
            menu.transform.position = camera.transform.position + camera.transform.forward * 2.5f;
            menu.transform.rotation = new Quaternion(0.0f, camera.transform.rotation.y, 0.0f, camera.transform.rotation.w);
        }
    }

    // Returns the state of the menu
    public bool GetMenuStatus()
    {
        // Return true if the menu is currently active, else false, mainly used in SwitchPointer.cs
        return menuExists;
    }

    // Handles the spawning and destroying of a menu in case of a button press
    private void HandleButtonPress()
    {
        // Block the function from further execution
        beingHandelt = true;

        // If no menu exists instantiate one
        if (!menuExists && !otherMenusOpen)
        {
            // Instantiate menu and save the current state of its existence
            //menu = Instantiate(prefab, prefab.transform.position, Quaternion.identity)
            menu = Instantiate(prefab, camera.transform.position + camera.transform.forward * 2.5f, Quaternion.identity);
            menuExists = true;
        }
        // If menu already exists we want to destroy it
        else
        {
            // Destroy the menu and save the current state of its existence
            Destroy(menu);
            menuExists = false;
        }
     
        // Unblock the funciton for future execution
        beingHandelt = false;
    }
    
    // When we open another menu we should mark it
    public void MarkOtherMenusOpen()
    {
        otherMenusOpen = true;
    }

    // When we close another menu we should mark it
    public void MarkOtherMenusClosed()
    {
        otherMenusOpen = false;
    }

    // Return true if other menus are open, else false
    public bool GetOtherMenusStatus()
    {
        return otherMenusOpen;
    }

    // Set the name of the "other" open menu
    public void SetOtherMenusName(String name)
    {
        otherMenusName = name;
    }

    // Get the name of the "other" (open) menu
    public String GetOtherMenusName()
    {
        return otherMenusName;
    }

    // Get the name of the currently open menu
    public String GetCurrentMenusName()
    {
        if (otherMenusOpen)
        {
            return otherMenusName;
        }
        else
        {
            //return prefab.ToString();
            return prefab.name;
        }
    }
}