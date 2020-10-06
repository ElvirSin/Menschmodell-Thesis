using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Elvir Sinancevic
public class PanelMainR3ButtonHandler : MonoBehaviour
{
    // Local variables
    public GameObject thisPanel = null;
    public GameObject movementPanel = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Show Movement-Panel
    public void ShowMovement()
    {
        thisPanel.SetActive(false);
        movementPanel.SetActive(true);
    }
}
