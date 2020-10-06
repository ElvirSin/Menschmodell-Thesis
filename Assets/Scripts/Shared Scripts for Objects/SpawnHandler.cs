using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

// Author: Elvir Sinancevic
public class SpawnHandler : MonoBehaviour
{
    // The robot prefabs
    public GameObject prefabR1;
    public GameObject prefabR2;
    public GameObject prefabR3;
    public GameObject prefabR4;
    
    // Currently spawned robot
    private GameObject r1;
    private GameObject r2;
    private GameObject r3;
    private GameObject r4;
    
    // Count variables for the amount of each robot
    private int countR1;
    private int countR2;
    private int countR3;
    private int countR4;
    
    // Max amount of each robot
    private int maxR1;
    private int maxR2;
    private int maxR3;
    private int maxR4;
    
    // Last clicked GameObject
    public GameObject lastClicked = null;
    
    // The main camera
    #pragma warning disable
    private GameObject camera = null;
    
    // GlobalVariables
    private GlobalVariables vars = null;
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the global variables scrips
        vars = GameObject.Find("Scripts").GetComponent<GlobalVariables>();
        // Find the camera and load the current roboter-count and the max roboter-count from the GlobalVariables
        camera = GameObject.Find("Camera");
        countR1 = vars.currentCountR1; 
        countR2 = vars.currentCountR2; 
        countR3 = vars.currentCountR3; 
        countR4 = vars.currentCountR4;
        maxR1 = vars.maxR1;
        maxR2 = vars.maxR2;
        maxR3 = vars.maxR3;
        maxR4 = vars.maxR4;
    }

    // Update is called once per frame
    void Update()
    {
        // The max needs to be updated, if the max amount gets changed you can place more robots
        maxR1 = vars.maxR1;
        maxR2 = vars.maxR2;
        maxR3 = vars.maxR3;
        maxR4 = vars.maxR4;
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
    
    // Spawn robot 1
    public void SpawnR1()
    {
        // Only spawn the robot if currentAmount < maxAmount, also update the GlobalVariable and name the robot
        if (countR1 < maxR1)
        {
            // Update the count
            countR1 += 1;
            vars.currentCountR1 = countR1;
            // Instantiate the robot
            r1 = Instantiate(prefabR1, camera.transform.position + camera.transform.forward * 1.0f, Quaternion.identity);
            r1.transform.position = new Vector3(r1.transform.position.x, 0.0f, r1.transform.position.z);
            // Name the prefab
            for (int i = 1; i <= maxR1; i++)
            {
                if (!GameObject.Find("Rmk3 Robot Nr.1 " + i + "/" + maxR1))
                {
                    r1.gameObject.name = "Rmk3 Robot Nr.1 " + i + "/" + maxR1;
                    break;
                }
            }
            // Set the reference in the ObjectMenu Script on the prefab
            r1.GetComponent<ObjectMenu>().thisObject = r1;
        }
    }

    // Spawn robot 2
    public void SpawnR2()
    {
        // Only spawn the robot if currentAmount < maxAmount, also update the GlobalVariable and name the robot
        if (countR2 < maxR2)
        {
            // Update the count
            countR2 += 1;
            vars.currentCountR2 = countR2;
            //Instantiate the prefab
            r2 = Instantiate(prefabR2, camera.transform.position + camera.transform.forward * 1.0f, Quaternion.identity);
            r2.transform.position = new Vector3(r2.transform.position.x, 0.0f, r2.transform.position.z);
            // Name the prefab
            for (int i = 1; i <= maxR2; i++)
            {
                if (!GameObject.Find("Rmk3 Robot Nr.2 " + i + "/" + maxR2))
                {
                    r2.gameObject.name = "Rmk3 Robot Nr.2 " + i + "/" + maxR2;
                    break;
                }
            }
            // Set the reference in the ObjectMenu Script on the prefab
            r2.GetComponent<ObjectMenu>().thisObject = r2;
        }
    }

    // Spawn robot 3
    public void SpawnR3()
    {
        // Only spawn the robot if currentAmount < maxAmount, also update the GlobalVariable and name the robot
        if (countR3 < maxR3)
        {
            // Update the count
            countR3 += 1;
            vars.currentCountR3 = countR3;
            //Instantiate the prefab
            r3 = Instantiate(prefabR3, camera.transform.position + camera.transform.forward * 1.0f, Quaternion.identity);
            r3.transform.position = new Vector3(r3.transform.position.x, 0.0f, r3.transform.position.z);
            // Name the prefab
            for (int i = 1; i <= maxR3; i++)
            {
                if (!GameObject.Find("Rmk3 Robot Nr.3 " + i + "/" + maxR3))
                {
                    r3.gameObject.name = "Rmk3 Robot Nr.3 " + i + "/" + maxR3;
                    break;
                }
            }
            // Set the reference in the ObjectMenu Script on the prefab
            r3.GetComponent<ObjectMenu>().thisObject = r3;
        }
    }

    // Spawn robot 4
    public void SpawnR4()
    {
        // Only spawn the robot if currentAmount < maxAmount, also update the GlobalVariable and name the robot
        if (countR4 < maxR4)
        {
            // Update the count
            countR4 += 1;
            vars.currentCountR4 = countR4;
            //Instantiate the prefab
            r4 = Instantiate(prefabR4, camera.transform.position + camera.transform.forward * 1.0f, Quaternion.identity);
            r4.transform.position = new Vector3(r4.transform.position.x, 0.0f, r4.transform.position.z);
            // Name the prefab
            for (int i = 1; i <= maxR4; i++)
            {
                if (!GameObject.Find("Rmk3 Robot Nr.4 " + i + "/" + maxR4))
                {
                    r4.gameObject.name = "Rmk3 Robot Nr.4 " + i + "/" + maxR4;
                    break;
                }
            }
            // Set the reference in the ObjectMenu Script on the prefab
            r4.GetComponent<ObjectMenu>().thisObject = r4;
        }
    }

    // Remove Robot1
    public void RemoveR1()
    {
        // Update the count
        countR1 -= 1;
        vars.currentCountR1 = countR1;
        // Destroy the open menu and the gameobject
        lastClicked.GetComponent<ObjectMenu>().HandleButtonPress();
        GameObject.Destroy(lastClicked);
    }
    
    // Remove Robot1
    public void RemoveR2()
    {
        // Update the count
        countR2 -= 1;
        vars.currentCountR2 = countR2;
        // Destroy the open menu and the gameobject
        lastClicked.GetComponent<ObjectMenu>().HandleButtonPress();
        GameObject.Destroy(lastClicked);
    }
    
    // Remove Robot1
    public void RemoveR3()
    {
        // Update the count
        countR3 -= 1;
        vars.currentCountR3 = countR3;
        // Destroy the open menu and the gameobject
        lastClicked.GetComponent<ObjectMenu>().HandleButtonPress();
        GameObject.Destroy(lastClicked);
    }
    
    // Remove Robot1
    public void RemoveR4()
    {
        // Update the count
        countR4 -= 1;
        vars.currentCountR4 = countR4;
        // Destroy the open menu and the gameobject
        lastClicked.GetComponent<ObjectMenu>().HandleButtonPress();
        GameObject.Destroy(lastClicked);
    }
}
