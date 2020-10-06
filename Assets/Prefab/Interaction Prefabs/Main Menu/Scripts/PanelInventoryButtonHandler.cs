using UnityEngine;

// Author: Elvir Sinancevic
public class PanelInventoryButtonHandler : MonoBehaviour
{
    // The panels
    public GameObject thisPanel = null;
    public GameObject mainPanel = null;
    
    // Amounts
    private int[] maxCount;
    private int[] currentCount;
    
    // GlobalVariables
    private GlobalVariables vars = null;
    
    // SpawnHandler
    private SpawningHandler spawningHandler = null; //New Version
    
    // UI Elemeents
    private UnityEngine.UI.Text r1Text = null;
    private UnityEngine.UI.Text r2Text = null;
    private UnityEngine.UI.Text r3Text = null;
    private UnityEngine.UI.Text r4Text = null;

    // Start is called before the first frame update
    void Start()
    {
        // Find missing scripts
        vars = GameObject.Find("Scripts").GetComponent<GlobalVariables>();
        spawningHandler = GameObject.Find("Scripts").GetComponent<SpawningHandler>(); // New Version
        // Find missing UI Elements
        r1Text = GameObject.Find("R1Quantity").GetComponent<UnityEngine.UI.Text>();
        r2Text = GameObject.Find("R2Quantity").GetComponent<UnityEngine.UI.Text>();
        r3Text = GameObject.Find("R3Quantity").GetComponent<UnityEngine.UI.Text>();
        r4Text = GameObject.Find("R4Quantity").GetComponent<UnityEngine.UI.Text>();
        // Load the counts
        maxCount = vars.GetMaxAmounts();
        currentCount = vars.GetCurrentAmounts();
        // Update UI
        UpdateUI();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Load the counts
        maxCount = vars.GetMaxAmounts();
        currentCount = vars.GetCurrentAmounts();
        // Update UI
        UpdateUI();
    }

    // Update UI Elements
    public void UpdateUI()
    {
        r1Text.text = "Available: " + (maxCount[0]-currentCount[0]);
        r2Text.text = "Available: " + (maxCount[1]-currentCount[1]);
        r3Text.text = "Available: " + (maxCount[2]-currentCount[2]);
        r4Text.text = "Available: " + (maxCount[3]-currentCount[3]);
    }
    
    // Show Main-Panel
    public void GoBackToMainPanel()
    {
        thisPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    
    // Spawn R1 in SpawnHandler
    public void SpawnR1()
    {
        spawningHandler.SpawnRobot(1);
    }

    // Spawn R2 in SpawnHandler
    public void SpawnR2()
    {
        spawningHandler.SpawnRobot(2);
    }

    // Spawn R3 in SpawnHandler
    public void SpawnR3()
    {
        spawningHandler.SpawnRobot(3);
    }

    // Spawn R4 in SpawnHandler
    public void SpawnR4()
    {
        spawningHandler.SpawnRobot(4);
    }
}