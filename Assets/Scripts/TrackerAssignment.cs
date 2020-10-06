using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;
using System.Text;
using RootMotion.Demos;
using RootMotion.FinalIK;

// Based on our "Bachelorpraktikum" at the DiK
public class TrackerAssignment : MonoBehaviour
{
    // Reference to the inverse kinematics script on our dummy, provided by the FinalIK plugin
    [Header("Dummy")]
    public VRIK ik;

    // Reference to the calibrator
    [Header("Calibtration Controller")]
    public CalibrationController calibrator;

    // References to our optional calibration targets
    [Header("Targets")]
    public SteamVR_TrackedObject leftFoot;
    public SteamVR_TrackedObject rightFoot;
    public SteamVR_TrackedObject leftKnee;
    public SteamVR_TrackedObject rightKnee;
    public SteamVR_TrackedObject leftElbow;
    public SteamVR_TrackedObject rightElbow;
    public SteamVR_TrackedObject pelvis;

    // References to the necsessary body parts
    [Header("Transforms")]
    public Transform leftFootTransform;
    public Transform rightFootTransform;
    public Transform pelvisTransform;
    public Transform leftKneeTransform;
    public Transform rightKneeTransform;
    public Transform leftElbowTransform;
    public Transform rightElbowTransform;

    // IDs of the used trackers
    private const string leftFootID = "LHR-C4F9A61E";
    private const string rightFootID = "LHR-D461548C";
    private const string leftKneeID = "LHR-8E64C1A0";
    private const string rightKneeID = "LHR-E09061B8";
    private const string leftElbowID = "LHR-AC6CE134";
    private const string rightElbowID = "LHR-89B49C30";
    private const string pelvisID = "LHR-349D566C";

    // Locally used ErrorHandler, StringBuilder and uint index
    private ETrackedPropertyError error;// = new ETrackedPropertyError();
    private StringBuilder sb; // = new StringBuilder();
    private uint index;

    // Start is called before the first frame update
    void Start()
    {
        // Set ErrorHandler and StringBuilder
        error = new ETrackedPropertyError();
        sb = new StringBuilder();
    }

    // Assign all active trackers to the corresponding targets
    public void AssignTrackers() 
    {
        // All targets get the corresponding TrackerID but only if the Tracker is connected
        for(int i = 0; i <= 15; i++)
        {
            index = (uint) i;
            OpenVR.System.GetStringTrackedDeviceProperty(index, ETrackedDeviceProperty.Prop_SerialNumber_String, sb, OpenVR.k_unMaxPropertyStringSize, ref error);
            switch(sb.ToString())
            {
                case leftFootID:
                    leftFoot.SetDeviceIndex(i);
                    Debug.Log("lf");
                    break;
                case rightFootID:
                    rightFoot.SetDeviceIndex(i);
                    Debug.Log("rf");
                    break;
                case leftKneeID:
                    leftKnee.SetDeviceIndex(i);
                    Debug.Log("lk");
                    break;
                case rightKneeID:
                    rightKnee.SetDeviceIndex(i);
                    Debug.Log("rk");
                    break;
                case leftElbowID:
                    leftElbow.SetDeviceIndex(i);
                    Debug.Log("le");
                    break;
                case rightElbowID:
                    rightElbow.SetDeviceIndex(i);
                    Debug.Log("re");
                    break;
                case pelvisID:
                    pelvis.SetDeviceIndex(i);
                    Debug.Log("p");
                    break;      
            }
        }
        // Assigning the Targets for the CalibrationController --> Only happens if the necessary Trackers are connected!
        // Left Foot
        if (leftFoot.index != Valve.VR.SteamVR_TrackedObject.EIndex.None)
        {
            calibrator.leftFootTracker = leftFootTransform;
        }
        else
        {
            calibrator.leftFootTracker = null;
        }
        // Right Foot
        if (rightFoot.index != Valve.VR.SteamVR_TrackedObject.EIndex.None)
        {
            calibrator.rightFootTracker = rightFootTransform;

        }
        else
        {
            calibrator.rightFootTracker = null;
        }
        // Pelvis
        if (pelvis.index != Valve.VR.SteamVR_TrackedObject.EIndex.None)
        {
            calibrator.bodyTracker = pelvisTransform;
        }
        else
        {
            calibrator.bodyTracker = null;

        }
        
        // Assign the Targets for the VR IK Script + adjust bendGoalWeight --> Only happens if the necessary Trackers are connected!
        // Left Knee
        if (leftKnee.index != Valve.VR.SteamVR_TrackedObject.EIndex.None)
        {
            ik.solver.leftLeg.bendGoal = leftKneeTransform;
            ik.solver.leftLeg.bendGoalWeight = 1f;
        }
        else
        {
            ik.solver.leftLeg.bendGoal = null;
            ik.solver.leftLeg.bendGoalWeight = 0f;
        }
        // Right Knee
        if (rightKnee.index != Valve.VR.SteamVR_TrackedObject.EIndex.None)
        {
            ik.solver.rightLeg.bendGoal = rightKneeTransform;
            ik.solver.rightLeg.bendGoalWeight = 1f;
        }
        else
        {
            ik.solver.rightLeg.bendGoal = null;
            ik.solver.rightLeg.bendGoalWeight = 0f;
        }
        // Left Elbow
        if (leftElbow.index != Valve.VR.SteamVR_TrackedObject.EIndex.None)
        {
            ik.solver.leftArm.bendGoal = leftElbowTransform;
            ik.solver.leftArm.bendGoalWeight = 1f;
        }
        else
        {
            ik.solver.leftArm.bendGoal = null;
            ik.solver.leftArm.bendGoalWeight = 0;
        }
        // Right Elbow
        if (rightElbow.index != Valve.VR.SteamVR_TrackedObject.EIndex.None)
        {
            ik.solver.rightArm.bendGoal = rightElbowTransform;
            ik.solver.rightArm.bendGoalWeight = 1f;
        }
        else
        {
            ik.solver.rightArm.bendGoal = null;
            ik.solver.rightArm.bendGoalWeight = 0;
        }
    }
}
