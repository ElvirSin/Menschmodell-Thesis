using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

// Author: Elvir Sinancevic
public class PanelMovementR4ButtonHandler : MonoBehaviour
{
    // Local variables
    // The panels
    public GameObject thisPanel = null;
    public GameObject mainPanel = null;
    // The roboter and some UI elements
    private GameObject roboter;
    private GameObject xtext;
    private GameObject ytext;
    private GameObject ztext;
    private GameObject stepSizeSlider = null;
    private float stepSize;
    private GameObject sliderText;

    // Start is called before the first frame update
    void Start()
    {
        // Find the roboter that should be moved based on the last clicked object
        roboter = GameObject.Find(GameObject.Find("Scripts").GetComponent<SpawningHandler>().GetLastClicked());
        
        // Find the different UI elements
        stepSizeSlider = GameObject.Find("StepSizeSlider");
        xtext = GameObject.Find("XPos");
        ytext = GameObject.Find("YPos");
        ztext = GameObject.Find("ZPos");
        sliderText = GameObject.Find("SizeOfSteps");
        
        // Set the step size value
        stepSize = stepSizeSlider.GetComponent <Slider> ().value;;
        
        // Set the texts
        xtext.GetComponent<UnityEngine.UI.Text>().text = "X: " + System.Math.Round(roboter.transform.position.x, 1);
        ytext.GetComponent<UnityEngine.UI.Text>().text = "Y: " + System.Math.Round(roboter.transform.position.y, 1);
        ztext.GetComponent<UnityEngine.UI.Text>().text = "Z: " + System.Math.Round(roboter.transform.position.z, 1);
        sliderText.GetComponent<UnityEngine.UI.Text>().text = "Size of steps: " + stepSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the texts
        xtext.GetComponent<UnityEngine.UI.Text>().text = "X: " + System.Math.Round(roboter.transform.position.x, 1);
        ytext.GetComponent<UnityEngine.UI.Text>().text = "Y: " + System.Math.Round(roboter.transform.position.y, 1);
        ztext.GetComponent<UnityEngine.UI.Text>().text = "Z: " + System.Math.Round(roboter.transform.position.z, 1);
        
        //Update the slider
        stepSize = stepSizeSlider.GetComponent <Slider> ().value;;
        sliderText.GetComponent<UnityEngine.UI.Text>().text = "Size of steps: " + stepSize;
    }
    
    // Show Main-Panel
    public void GoBackToMainPanel()
    {
        thisPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    
    // Move the robot up
    public void moveUp()
    {
        // Dont let the roboter be higher than a certain height (for example the maximum allowed height in the building) [3 is just a placeholder value!]
        if (System.Math.Round(roboter.transform.position.y, 1) < 3.0f)
        {
            roboter.transform.position = roboter.transform.position + Vector3.up * stepSize;
        }
        else
        {
            Debug.Log("Not so high!");
        }
    }
    
    // Move the robot down
    public void moveDown()
    {
        // Dont let the roboter go into the ground
        if (System.Math.Round(roboter.transform.position.y, 1) > 0.0f)
        {
            roboter.transform.position = roboter.transform.position + Vector3.down * stepSize;
        }
        else
        {
            Debug.Log("Not so low!");
        }
    }
    
    // Move the robot right
    public void moveRight()
    {
        roboter.transform.position = roboter.transform.position + Vector3.right * stepSize;
    }

    // Move the robot left
    public void moveLeft()
    {
        roboter.transform.position = roboter.transform.position + Vector3.left * stepSize;
    }

    // Move the robot forward
    public void moveForwards()
    {
        roboter.transform.position = roboter.transform.position + Vector3.forward * stepSize;
    }

    // Move the robot backward
    public void moveBackwards()
    {
        roboter.transform.position = roboter.transform.position + Vector3.back * stepSize;
    }
    
    // Pick up the robot into the inventory
    public void PickUp()
    {
        //GameObject.Find("Scripts").GetComponent<SpawnHandler>().RemoveR4();
        GameObject.Find("Scripts").GetComponent<SpawningHandler>().RemoveRobot(4);
    }
}
