using UnityEngine;
using Valve.VR;

// Author: Elvir Sinancevic
public class ObjectMenu : MonoBehaviour
{
    // References to the prefabs of the Menu (this can Change in future and will still be working fine)
    public GameObject prefab;
    
    // Variable for the last clicked object
    public GameObject thisObject;

    // Our SteamVR Input Source (in our case button click)
    public SteamVR_Input_Sources clickButton = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean clickAction;
    
    // Local variable for the menu and the camera of the scene
    private GameObject menu = null;
    #pragma warning disable
    private GameObject camera = null;
    
    // "Sync" variables
    private bool menuShouldExist = false;
    private bool menuExists = false;
    private bool beingHandelt = false;
    
    // PlayerMenu
    private PlayerMenu playerMenu = null;
    
    // SpawnHandler
    private SpawningHandler spawningHandler = null; //New Version

    // Start is called before the first frame update
    void Start()
    {
        // Set the parameters and get camera for the placement of the menu
        menuShouldExist = false;
        menuExists = false;
        camera = GameObject.Find("Camera");
        // Find the missing scripts in the scene
        playerMenu = GameObject.Find("Scripts").GetComponent<PlayerMenu>();
        spawningHandler = GameObject.Find("Scripts").GetComponent<SpawningHandler>(); // New Version
    }

    // Update is called once per frame
    void Update()
    {
        // If the menu should exist, handle the button press (spawn one) and change the status of menuShouldExist to false, because it already exists
        if (!beingHandelt && menuShouldExist)
        {
            menuShouldExist = !menuShouldExist;
            HandleButtonPress();
        }
        
        // If the menu already exists and we get the right input from the player controller, close the menu (done by the same function)
        if (clickAction.GetStateDown(clickButton) && !beingHandelt && menuExists)
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

    // This function is called when we click on the object
    public void ClickHandler()
    {
        // Change menuShouldExist
        //menuShouldExist = !menuShouldExist;
        menuShouldExist = true;
    }
    
    // Handles the spawning and destroying of a menu in case of a button press
    public void HandleButtonPress()
    {
        // Block the function from further execution
        beingHandelt = true;

        // Set last clicked object
        spawningHandler.SetLastClicked(thisObject); // New Version
        
        // If no menu exists instantiate one
        if (!menuExists)
        //if(menuShouldExist)
        {
            // Instantiate menu and save the current state of its existence
            menu = Instantiate(prefab, camera.transform.position + camera.transform.forward * 2.5f, Quaternion.identity);
            menuExists = true;
            playerMenu.MarkOtherMenusOpen();
            playerMenu.SetOtherMenusName(prefab.name);
        }
        // If menu already exists we want to destroy it
        else
        {
            // Destroy the menu and save the current state of its existence
            Destroy(menu);
            menuExists = false;
            playerMenu.MarkOtherMenusClosed();
            playerMenu.SetOtherMenusName(null);
        }
     
        // Unblock the funciton for future execution
        beingHandelt = false;
    }
}