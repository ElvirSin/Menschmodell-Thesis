using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Elvir Sinancevic
public class GlobalVariables : MonoBehaviour
{
    // Amount of unique robots
    public int uniqueRobots = 4;
    // Max amount of each robot
    public int maxR1 = 5;
    public int maxR2 = 4;
    public int maxR3 = 3;
    public int maxR4 = 2;
    // Current amount of each robot
    public int currentCountR1 = 1;
    public int currentCountR2 = 1;
    public int currentCountR3 = 1;
    public int currentCountR4 = 1;
    // Prefabs of each robot
    public GameObject prefabR1;
    public GameObject prefabR2;
    public GameObject prefabR3;
    public GameObject prefabR4;

    // Array for robots
    public GameObject[] robots;
    // Array for current amount
    public int[] currentAmount;
    // Array for max amount
    public int[] maxAmount;

    
    // Start is called before the first frame update
    void Start()
    {
        // Set up the arrays
        robots = new GameObject[]
        {
            prefabR1, prefabR2, prefabR3, prefabR4
        };
        currentAmount = new int[]
        {
            currentCountR1, currentCountR2, currentCountR3, currentCountR4
        };
        maxAmount = new int[]
        {
            maxR1, maxR2, maxR3, maxR4
        };

        for (int i = 0; i < robots.Length; i++)
        {
            SetUpName(robots[i].name, maxAmount[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Setup the correct name of each Robot at the beginning
    private void SetUpName(string name, int max)
    {
        // Find all instances with of each robot
        for (int i = 1; i <= max; i++)
        {
            GameObject obj = GameObject.Find(name);
            if(obj != null)
                obj.gameObject.name = name + " " + i + "/" + max;
        }
    }
    
    // Get the list of robots
    public GameObject[] GetRobotPrefabs()
    {
        return robots;
    }
    // Get the current amounts
    public int[] GetCurrentAmounts()
    {
        return currentAmount;
    }
    // Get the max amounts
    public int[] GetMaxAmounts()
    {
        return maxAmount;
    }
    // Set the current amount at pos
    public void SetCurrentCount(int pos, int value)
    {
        currentAmount[pos] = value;
    }
    // Set the max amount at pos
    public void SetMaxAmount(int pos, int value)
    {
        maxAmount[pos] = value;
    }
    
    // Optional: Here we could add functions to save and load the data in a file
    /*
     * 1. Erstelle Klasse Roboter mit notwendigen Variabeln: String PrefabName/Pfad oder sowas, int Aktuelle Anzahl, int Max Anzahl, Vector3[] Array an Positionen
     * 2. Speichere alle Roboter in einem Array in Global Variables ab : Roboter[]
     * 3. Rufe bei allen Robotern Spawn() Methode auf, die alle Roboter von dem Typen in der Szene platziert
     * 4. Gehe Roboter[] Array durch und speichere Prefab, Aktuelle Anzahl und Max Anzahl in den 3 Arrays die oben schon erzeugt sind
     * 
     * 5. Vor dem schließen beschreibe die Dateien neu
     *     --> Gehe die Szene für alle Roboter durch und prüfe Aktuelle Anzahl, Aktuelle Max Anzahl und vorallem Positionen und schreibe in eine neue Datei
     *
     * Alte Idee ............................................................................................................................................
     * Man könnte alle Prefabs aus einer Datei Laden. Zb. Format "Prefab - Aktuelle Anzahl(int) - Max Anzahl(int) - Positionen-der-Aktuell-Gespawnten[Array]"
     *
     * Gespeichert würde dann in: Prefabs[array], Aktuelle Anzahl[Array], Max Anzahl[Array], Positionen[][]
     *        --> Man müsste dann um das 2D Array einheitlich zu gestalten eine Max Anzahl an Possitionsdaten festlegen
     *
     * Am Ende müsste man alles speichern können
     *
     */
    
}
