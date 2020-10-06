using UnityEngine;

// Author: Elvir Sinancevic
public class PanelOptionsButtonHandler : MonoBehaviour
{
    // The panels
    public GameObject thisPanel = null;
    public GameObject mainPanel = null;

    // GlobalVariables
    private GlobalVariables vars = null;
    
    // UI Textelements
    private UnityEngine.UI.Text textAmountR1;
    private UnityEngine.UI.Text textAmountR2;
    private UnityEngine.UI.Text textAmountR3;
    private UnityEngine.UI.Text textAmountR4;
    
    // Amounts
    private int[] maxCount;
    private int[] currentCount;
    
    // Array for each robot
    private GameObject[] robots;

    // Start is called before the first frame update
    void Start()
    {
        // Find missing scripts
        vars = GameObject.Find("Scripts").GetComponent<GlobalVariables>();
        // Find missing UI Elements
        textAmountR1 = GameObject.Find("AmountR1").GetComponent<UnityEngine.UI.Text>();
        textAmountR2 = GameObject.Find("AmountR2").GetComponent<UnityEngine.UI.Text>();
        textAmountR3 = GameObject.Find("AmountR3").GetComponent<UnityEngine.UI.Text>();
        textAmountR4 = GameObject.Find("AmountR4").GetComponent<UnityEngine.UI.Text>();
        // Load the robots
        robots = vars.GetRobotPrefabs();
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
        textAmountR1.text = maxCount[0].ToString();
        textAmountR2.text = maxCount[1].ToString();
        textAmountR3.text = maxCount[2].ToString();
        textAmountR4.text = maxCount[3].ToString();
    }
    
    // Show Main-Panel
    public void GoBackToMainPanel()
    {
        thisPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    
    // Increase max amount of a robot
    public void IncAmount(int robot)
    {
        // The pos of the prefab in the array (array starts at zero, so substract one)
        int pos = robot - 1;
        // Old and new max amount
        int old_max = maxCount[pos];
        int new_max = maxCount[pos] + 1;
        // Update amount in array
        vars.SetMaxAmount(pos, new_max);
        // Update naming
        UpdateNaming(robots[pos].name, old_max, new_max);
    }

    // Decrease max amount of a robot
    // Only decrease if the new Max-Amount is NOT smaller then the amount of currently active Robots
    public void DecAmount(int robot)
    { 
        // The pos of the prefab in the array (array starts at zero, so substract one)
        int pos = robot - 1; 
        // Only decrease if max > current
        if(maxCount[pos] > currentCount[pos]) 
        { 
            // Old and new max amount
            int old_max = maxCount[pos]; 
            int new_max = maxCount[pos] - 1; 
            // Update amount in array
            vars.SetMaxAmount(pos, new_max); 
            // Update naming
            UpdateNaming(robots[pos].name, old_max, new_max); 
        }
    }
    
    // Update the naming of each Robot after each change of the Max-Amount
    private void UpdateNaming(string name, int old_val, int new_val)
    {
        // Find all objects with the old naming (has the old_val at the end) and update with new_val
        for (int i = 1; i <= old_val; i++)
        {
            GameObject obj = GameObject.Find(name + " " + i + "/" + old_val);
            if(obj != null)
                obj.gameObject.name = name + " " + i + "/" + new_val;
        }
    }
}