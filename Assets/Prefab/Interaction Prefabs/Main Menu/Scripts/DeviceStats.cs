using UnityEngine;
using Valve.VR;
using System.Text;

// Author: Elvir Sinancevic
public class DeviceStats : MonoBehaviour
{
    // The IDs of the used devices. This needs to be changed when using other devices!
    // TODO: Maybe this could be automated?
    private const string leftFootID = "LHR-C4F9A61E";
    private const string rightFootID = "LHR-D461548C";
    private const string leftKneeID = "LHR-8E64C1A0";
    private const string rightKneeID = "LHR-E09061B8";
    private const string leftElbowID = "LHR-AC6CE134";
    private const string rightElbowID = "LHR-89B49C30";
    private const string pelvisID = "LHR-349D566C";
    private const string rightControllerID = "LHR-38ADCA9A";
    private const string leftControllerID = "LHR-F98BABEC";
    
    // Not needed, headset can be changed just the controllers and trackers should remain the same
    // private const string headSetID = "LHR-20A96F38";
    
    // Needed to get information from the trackers/controllers
    private ETrackedPropertyError error; // = new ETrackedPropertyError();
    private StringBuilder sb; // = new StringBuilder();
    private uint index;

    // Text colors
    private Color32 goodPercentage = Color.green;
    private Color32 mediumPercentage = Color.yellow;
    private Color32 badPercentage = Color.red;
    
    // UI Elements
    private UnityEngine.UI.Text leftFootText = null;
    private UnityEngine.UI.Text rightFootText = null;
    private UnityEngine.UI.Text leftKneeText = null;
    private UnityEngine.UI.Text rightKneeText = null;
    private UnityEngine.UI.Text leftElbowText = null;
    private UnityEngine.UI.Text rightElbowText = null;
    private UnityEngine.UI.Text pelvisText = null;
    private UnityEngine.UI.Text leftControllerText = null;
    private UnityEngine.UI.Text rightControllerText = null;
    
    // Start is called before the first frame update
    void Start()
    {
        // Error handler and string builder
        error = new ETrackedPropertyError();
        sb = new StringBuilder();
        
        // Find the UI elements
        leftFootText = GameObject.Find("LeftFootText").GetComponent<UnityEngine.UI.Text>();
        rightFootText = GameObject.Find("RightFootText").GetComponent<UnityEngine.UI.Text>();
        leftKneeText = GameObject.Find("LeftKneeText").GetComponent<UnityEngine.UI.Text>();
        rightKneeText = GameObject.Find("RightKneeText").GetComponent<UnityEngine.UI.Text>();
        leftElbowText = GameObject.Find("LeftElbowText").GetComponent<UnityEngine.UI.Text>();
        rightElbowText = GameObject.Find("RightElbowText").GetComponent<UnityEngine.UI.Text>();
        pelvisText = GameObject.Find("PelvisText").GetComponent<UnityEngine.UI.Text>();
        leftControllerText = GameObject.Find("LeftControllerText").GetComponent<UnityEngine.UI.Text>();
        rightControllerText = GameObject.Find("RightControllerText").GetComponent<UnityEngine.UI.Text>();

        NullAllEntries();
        AssignBatteryPercentages();
        //VRControllerState_t state = new VRControllerState_t();
        //Debug.Log(OpenVR.System.GetControllerState(0, ref state,
        //    (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)));
    }

    // Returns the battery percentage of a device with a given index
    public float GetBatteryPercentages(uint i)
    {
        index = (uint) i;
        float percentage = 0;
        percentage = OpenVR.System.GetFloatTrackedDeviceProperty(index, ETrackedDeviceProperty.Prop_DeviceBatteryPercentage_Float, ref error);
        //Debug.Log(sb.ToString());
        return percentage;
    }

    // Returns the color the text should have by using the function GetBatteryPercentages
    private Color32 GetColor(uint index)
    {
        if (GetBatteryPercentages(index) * 100 >= 66)
        {
            return goodPercentage;
        }
        if (GetBatteryPercentages(index) * 100 >= 33)
        {
            return mediumPercentage;
        }
        return badPercentage;
    }

    // Assigns the battery percentages for each device
    public void AssignBatteryPercentages()
    {
        // Local variable for the textcolor
        Color32 color = new Color32();
        VRControllerState_t state = new VRControllerState_t();
        
        // Go through all 15 possible devices
        for (int i = 0; i <= 15; i++)
        {
            index = (uint) i;
            OpenVR.System.GetStringTrackedDeviceProperty(index, ETrackedDeviceProperty.Prop_SerialNumber_String, sb, OpenVR.k_unMaxPropertyStringSize, ref error);
            // Switch all cases for all trackers/controllers, if a device is connected its text and color will be set
            switch (sb.ToString())
            {
                case leftFootID:
                    if (OpenVR.System.GetControllerState(index, ref state,
                        (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)))
                    {
                        leftFootText.text = "Left Foot: " + (int)(GetBatteryPercentages(index) * 100) + "%";
                        color = GetColor(index);
                        leftFootText.color = color;
                    }
                    break;
                case rightFootID:
                    if (OpenVR.System.GetControllerState(index, ref state,
                        (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)))
                    {
                        rightFootText.text = "Right Foot: " + (int)(GetBatteryPercentages(index) * 100) + "%";
                        color = GetColor(index);
                        rightFootText.color = color;
                    }
                    break;
                case leftKneeID:
                    if (OpenVR.System.GetControllerState(index, ref state,
                        (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)))
                    {
                        leftKneeText.text = "Left Knee: " + (int)(GetBatteryPercentages(index) * 100) + "%";
                        color = GetColor(index);
                        leftKneeText.color = color;
                    }
                    break;
                case rightKneeID:
                    if (OpenVR.System.GetControllerState(index, ref state,
                        (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)))
                    {
                        rightKneeText.text = "Right Knee: " + (int)(GetBatteryPercentages(index) * 100) + "%";
                        color = GetColor(index);
                       rightKneeText.color = color;
                    }
                    break;
                case leftElbowID:
                    if (OpenVR.System.GetControllerState(index, ref state,
                        (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)))
                    {
                        leftElbowText.text = "Left Elbow: " + (int)(GetBatteryPercentages(index) * 100) + "%";
                        color = GetColor(index);
                        leftElbowText.color = color;
                    }
                    break;
                case rightElbowID:
                    if (OpenVR.System.GetControllerState(index, ref state,
                        (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)))
                    {
                        rightElbowText.text = "Right Elbow: " + (int)(GetBatteryPercentages(index) * 100) + "%";
                        color = GetColor(index);
                        rightElbowText.color = color;
                    }
                    break;
                case pelvisID:
                    if (OpenVR.System.GetControllerState(index, ref state,
                        (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)))
                    {
                        pelvisText.text = "Pelvis: " + (int)(GetBatteryPercentages(index) * 100) + "%";
                        color = GetColor(index);
                        pelvisText.color = color;
                    }
                    //VRControllerState_t state = new VRControllerState_t();
                    //Debug.Log(OpenVR.System.GetControllerState(index, ref state, (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)));
                    break;
                case leftControllerID:
                    if (OpenVR.System.GetControllerState(index, ref state,
                        (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)))
                    {
                        leftControllerText.text = "LeftController: " + (int)(GetBatteryPercentages(index) * 100) + "%";
                        color = GetColor(index);
                        leftControllerText.color = color;
                    }
                    //GameObject.Find("LeftController").GetComponent<UnityEngine.UI.Text>().text = "LeftController: " + (GetBatteryPercentages(index) * 100) + "%";
                    //color = GetColor(index);
                    //GameObject.Find("LeftController").GetComponent<UnityEngine.UI.Text>().color = color;
                    //Debug.Log("hi");
                    
                    //VRControllerState_t state2 = new VRControllerState_t();
                    //Debug.Log("Hi:" + OpenVR.System.GetControllerState(index, ref state2, (uint) System.Runtime.InteropServices.Marshal.SizeOf(state2)));
                    break;
                case rightControllerID:
                    if (OpenVR.System.GetControllerState(index, ref state,
                        (uint) System.Runtime.InteropServices.Marshal.SizeOf(state)))
                    {
                        rightControllerText.text = "RightController: " + (int)(GetBatteryPercentages(index) * 100) + "%";
                        color = GetColor(index);
                        rightControllerText.color = color;
                    }
                    break;
            }
        }
    }

    // Revert all the entries
    private void NullAllEntries()
    {
        leftFootText.text = "Left Foot: OFF ";
        leftFootText.color = Color.gray;
        
        rightFootText.text = "Right Foot: OFF"; 
        rightFootText.color = Color.gray;
        
        leftKneeText.text = "Left Knee: OFF";
        leftKneeText.color = Color.gray;
        
        rightKneeText.text = "Right Knee: OFF";
        rightKneeText.color = Color.gray;
        
        leftElbowText.text = "Left Elbow: OFF";
        leftElbowText.color = Color.gray;
        
        rightElbowText.text = "Right Elbow: OFF";
        rightElbowText.color = Color.gray;
        
        pelvisText.text = "Pelvis: OFF";
        pelvisText.color = Color.gray;
        
        leftControllerText.text = "LeftController: OFF";
        leftControllerText.color = Color.gray;
        
        rightControllerText.text = "RightController: OFF";
        rightControllerText.color = Color.gray;
    }
}