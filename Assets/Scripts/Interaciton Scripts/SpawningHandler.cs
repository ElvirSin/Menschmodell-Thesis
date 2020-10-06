using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

// Author: Elvir Sinancevic
public class SpawningHandler : MonoBehaviour
{ 
    // Last clicked GameObject
    public GameObject lastClicked = null;
    
    // The main camera
    #pragma warning disable
    private GameObject camera = null;
    
    // GlobalVariables
    private GlobalVariables vars = null;
    
    // Array for each robot
    private GameObject[] robots;
    // Array for current count of each robot
    private int[] currentCount;
    // Array for max count of each robot
    private int[] maxCount;
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the global variables scrips
        vars = GameObject.Find("Scripts").GetComponent<GlobalVariables>();
        // Find the camera and load the current roboter-count and the max roboter-count from the GlobalVariables
        camera = GameObject.Find("Camera");
        // Load the robots
        robots = vars.GetRobotPrefabs();
        // Load the counts
        currentCount = vars.GetCurrentAmounts();
        maxCount = vars.GetMaxAmounts();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the arrays
        robots = vars.GetRobotPrefabs();
        currentCount = vars.GetCurrentAmounts();
        maxCount = vars.GetMaxAmounts();
    }

    // Update the last clicked object, called by ObjectMenu
    public void SetLastClicked(GameObject obj)
    {
        lastClicked = obj;
    }
    
    // Return the name of the last clicked object, called by the MovementScripts of each roboter
    public String GetLastClicked()
    {
        return lastClicked.gameObject.name;
    }

    // Spawn any robot you have
    public void SpawnRobot(int robot)
    {
        // The pos of the prefab in the array (array starts at zero, so substract one)
        int pos = robot - 1;
        // Current count and max
        int count = currentCount[pos];
        int max = maxCount[pos];
        GameObject r;
        // Only spawn the robot if currentAmount < maxAmount, also update the GlobalVariable and name the robot
        if (count < max)
        {
            // Update the count
            vars.SetCurrentCount(pos, count+1);
            // Instantiate the robot
            r = Instantiate(robots[pos], camera.transform.position + camera.transform.forward * 1.0f, Quaternion.identity);
            r.transform.position = new Vector3(r.transform.position.x, 0.0f, r.transform.position.z);
            // Name the prefab
            for (int i = 1; i <= max; i++)
            {
                if (!GameObject.Find("Robot Nr." + robot +" " + i + "/" + max))
                {
                    r.gameObject.name = "Robot Nr." + robot +" " + i + "/" + max;
                    break;
                }
            }
            // Set the reference in the ObjectMenu Script on the prefab
            r.GetComponent<ObjectMenu>().thisObject = r;
        }
    }

    // Remove any robot
    public void RemoveRobot(int robot)
    {
        // The pos of the prefab in the array (array starts at zero, so substract one)
        int pos = robot - 1;
        // Current count
        int count = currentCount[pos];
        //Update the count
        vars.SetCurrentCount(pos, count - 1);
        // Destroy the open menu and the gameobject
        lastClicked.GetComponent<ObjectMenu>().HandleButtonPress();
        GameObject.Destroy(lastClicked);
    }
}
