using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;
using Valve.VR;

// Author: Elvir Sinancevic & the Developers of FinalIK
public class CalibrationController : MonoBehaviour
{
    // The following code is written by the developers of the FinalIK plugin
    // I only added the code in Line 23-51, so that i can calibrate without pressing a specific character on the keyboard
    [Tooltip("Reference to the VRIK component on the avatar.")] public VRIK ik;
    [Tooltip("The settings for VRIK calibration.")] public VRIKCalibrator.Settings settings;
    [Tooltip("The HMD.")] public Transform headTracker;
    [Tooltip("(Optional) A tracker placed anywhere on the body of the player, preferrably close to the pelvis, on the belt area.")] public Transform bodyTracker;
    [Tooltip("(Optional) A tracker or hand controller device placed anywhere on or in the player's left hand.")] public Transform leftHandTracker;
    [Tooltip("(Optional) A tracker or hand controller device placed anywhere on or in the player's right hand.")] public Transform rightHandTracker;
    [Tooltip("(Optional) A tracker placed anywhere on the ankle or toes of the player's left leg.")] public Transform leftFootTracker;
    [Tooltip("(Optional) A tracker placed anywhere on the ankle or toes of the player's right leg.")] public Transform rightFootTracker;
    [Header("Data stored by Calibration")] 
    public VRIKCalibrator.CalibrationData data = new VRIKCalibrator.CalibrationData();
    
    //------------------------------------------------------------------------------------------------------------------------------------------------
    // Local variable for the trackerController
    public TrackerAssignment trackerController;
    // "Sync" variable
    private bool beingHandelt = false;
    
    // Update is called once per frame
    void Update()
    {
        // Calibrate it the calibration button on the left gamepad is clicked
        if(SteamVR_Actions.default_Calibrate.GetStateDown(SteamVR_Input_Sources.LeftHand) && !beingHandelt)
        {
            Calibrate();
        }
    }
    
    // Called to calibrate the human
    public void Calibrate() 
    { 
        // Block the function from further execution
        beingHandelt = true;
        // Assign trackers
        trackerController.AssignTrackers();
        // Save the calibration data after calibration
        data = VRIKCalibrator.Calibrate(ik, settings, headTracker, bodyTracker, leftHandTracker, rightHandTracker, leftFootTracker, rightFootTracker);
        // Unblock the funciton for future execution
        beingHandelt = false;
    } 
    //------------------------------------------------------------------------------------------------------------------------------------------------
    
    void LateUpdate() 
    { 
        if (Input.GetKeyDown(KeyCode.C)) 
        { 
            // Calibrate the character, store data of the calibration
            data = VRIKCalibrator.Calibrate(ik, settings, headTracker, bodyTracker, leftHandTracker, rightHandTracker, leftFootTracker, rightFootTracker);
        }

        /*
        * calling Calibrate with settings will return a VRIKCalibrator.CalibrationData, which can be used to calibrate that same character again exactly the same in another scene (just pass data instead of settings),
        * without being dependent on the pose of the player at calibration time.
        * Calibration data still depends on bone orientations though, so the data is valid only for the character that it was calibrated to or characters with identical bone structures.
        * If you wish to use more than one character, it would be best to calibrate them all at once and store the CalibrationData for each one.
        */ 
        if (Input.GetKeyDown(KeyCode.D)) 
        { 
            if (data.scale == 0f) 
            {
                Debug.LogError("No Calibration Data to calibrate to, please calibrate with settings first."); 
            }
            else 
            { 
                // Use data from a previous calibration to calibrate that same character again.
                VRIKCalibrator.Calibrate(ik, data, headTracker, bodyTracker, leftHandTracker, rightHandTracker, leftFootTracker, rightFootTracker);
            }
        }

        // Recalibrates avatar scale only. Can be called only if the avatar has been calibrated already.
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (data.scale == 0f)
            { 
                Debug.LogError("Avatar needs to be calibrated before RecalibrateScale is called.");
            }
            VRIKCalibrator.RecalibrateScale(ik, settings);
        }
    }
}
