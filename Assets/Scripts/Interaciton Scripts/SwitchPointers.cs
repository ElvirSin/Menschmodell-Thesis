using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;

// Author: Elvir Sinancevic
public class SwitchPointers : MonoBehaviour
{

    // References to the prefabs of both pointers
    public GameObject prefabPhysicsPointer = null;
    public GameObject prefabCanvasPointer = null;
    
    // Local variables for both pointers, to make manipulation easier
    private GameObject physicsPointer = null;
    private GameObject canvasPointer = null;

    // "Sync" variables
    private bool physicsPointerExists;
    private bool beingHandelt;
    
    // PlayerMenu
    private PlayerMenu playerMenu = null;
    
    // VRInput of EventSystem
    private VR_Input vrInput = null;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the PhysicsPointer at the begin
        SpawnPhysicsPointer();
        
        // Set the parameters
        physicsPointerExists = true;
        beingHandelt = false;
        
        // Find missing scripts etc.
        vrInput = GameObject.Find("EventSystem with VR Input").GetComponent<VR_Input>();
        playerMenu = GameObject.Find("Scripts").GetComponent<PlayerMenu>();

        // Set the Camera in the EventSystem to the Camera of the PhysicsPointer
        vrInput.eventCamera = physicsPointer.GetComponent<Camera>(); //todo diesen ansatz weiter verfolgen
    }

    // Update is called once per frame
    void Update()
    {
        // Always execute the function "handlePointerSwitch", but only if it is not already executing
        if (!beingHandelt)
        {
            HandlePointerSwitch();
        }
    }
    
    // Handles the pointer switching
    private void HandlePointerSwitch()
    {
        // Block the function from further execution
        beingHandelt = true;
        
        // If the PhysicsPointer and the menu are active at the same time, kill PhysicsPointer, spawn CanvasPointer
        if( physicsPointerExists && (GetComponent<PlayerMenu>().GetMenuStatus() || GetComponent<PlayerMenu>().GetOtherMenusStatus())  )
        {
            // Destroy PhysicsPointer and save the current state of the existence of the PhysicsPointer
            Destroy(physicsPointer);
            physicsPointerExists = false;
            // Spawn the CanvasPointer
            SpawnCanvasPointer();
            
            // Set the Camera in the Canvas of the open Menu to the Camera of the CanvasPointer
            // We need to add  "(Clone)" to the current Menus name, because it is automatically added when instantiating a prefab in the scene
            GameObject.Find(playerMenu.GetCurrentMenusName()+"(Clone)").GetComponent<Canvas>().worldCamera = canvasPointer.GetComponent<Camera>();
            
            // Set the Camera in the EventSystem to the Camera of the CanvasPointer
            vrInput.eventCamera = canvasPointer.GetComponent<Camera>();
            // Our CanvasPointer needs a reference to the EventSystem and the StandaloneInputModule
            canvasPointer.GetComponent<VR_CanvasPointer>().eventSystem = vrInput.GetComponent<EventSystem>();
            canvasPointer.GetComponent<VR_CanvasPointer>().inputModule = vrInput.GetComponent<StandaloneInputModule>();
        }
        // As soons as PhysicsPointer and Menu are both destroyed, destroy the CanvasPointer and spawn new PhysicsPointer
        else if (!physicsPointerExists && !GetComponent<PlayerMenu>().GetMenuStatus() && !GetComponent<PlayerMenu>().GetOtherMenusStatus())
        {
            // Destroy CanvasPointer and save the current state of the existence of the PhysicsPointer
            Destroy(canvasPointer);
            physicsPointerExists = true;
            // Spawn the PhysicsPointer
            SpawnPhysicsPointer();
            // Set the Camera in the EventSystem to the Camera of the PhysicsPointer
            vrInput.eventCamera = physicsPointer.GetComponent<Camera>();
        }

        // Unblock the function for future execution
        beingHandelt = false;
    }

    // Spawns the PhysicsPointer
    private void SpawnPhysicsPointer()
    {
        // Instantiate the pointer, set parent object and define the (local) position, rotation and scale
        physicsPointer = Instantiate(prefabPhysicsPointer, prefabPhysicsPointer.transform.position,
            Quaternion.identity);
        physicsPointer.transform.parent =  GameObject.Find("Controller (right)").transform;
        physicsPointer.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        physicsPointer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        physicsPointer.transform.localRotation = Quaternion.identity;
        physicsPointer.transform.localScale = new Vector3(1f, 1f, 1f );
    }

    // Spawns the CanvasPointer
    private void SpawnCanvasPointer()
    {
        // Instantiate the pointer, set parent object and define the (local) position, rotation and scale
        canvasPointer = Instantiate(prefabCanvasPointer, prefabCanvasPointer.transform.position,
            Quaternion.identity);
        canvasPointer.transform.parent =  GameObject.Find("Controller (right)").transform;
        canvasPointer.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        canvasPointer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        canvasPointer.transform.localRotation = Quaternion.identity;
        canvasPointer.transform.localScale = new Vector3(1f, 1f, 1f );
    }
}