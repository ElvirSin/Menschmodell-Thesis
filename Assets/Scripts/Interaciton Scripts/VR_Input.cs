using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

// Author: Elvir Sinancevic, based on the Youtube Tutorials of: https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ
public class VR_Input : BaseInput
{
    // EventCamera of the currently used pointer (can and needs to be assigned by other scripts like SwitchPointers.cs etc.)
    public Camera eventCamera = null;

    // Our SteamVR Input Source (in our case button click)
    //public SteamVR_ActionSet actionSet;
    public SteamVR_Input_Sources clickButton = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean clickAction;

    protected override void Start()
    {
        //actionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }
    // Awake Function, executed as the name says
    protected override void Awake()
    {
        // Get the BaseInputModul -> inputOverride
        GetComponent<BaseInputModule>().inputOverride = this;
    }

    // Overriding basic input functions
    public override bool GetMouseButton(int button)
    {
        return clickAction.GetState(clickButton);
    }
    
    public override bool GetMouseButtonDown(int button)
    {
        return clickAction.GetStateDown(clickButton);
    }

    public override bool GetMouseButtonUp(int button)
    {
        return clickAction.GetStateUp(clickButton);
    }

    // "MousePosition" is the middle of the screen (in other word the camera)
    public override Vector2 mousePosition
    {
        get { return new Vector2(eventCamera.pixelWidth / 2, eventCamera.pixelHeight / 2); }
    }
}