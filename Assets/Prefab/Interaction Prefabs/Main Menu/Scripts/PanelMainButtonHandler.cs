using UnityEngine;

// Author: Elvir Sinancevic
public class PanelMainButtonHandler : MonoBehaviour
{
    // The panels
    public GameObject thisPanel = null;
    public GameObject optionsPanel = null;
    public GameObject inventoryPanel = null;

    // Start is called before the first frame update
    void Start()
    {
        // Find the panels
        //thisPanel = GameObject.Find("Panel_Main");
        //optionsPanel = GameObject.Find("Panel_Options");
        //inventoryPanel = GameObject.Find("Panel_Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Show Options-Panel
    public void ShowOptions()
    {
        thisPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    // Show Inventory-Panel
    public void ShowInventory()
    {
        thisPanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }
}
