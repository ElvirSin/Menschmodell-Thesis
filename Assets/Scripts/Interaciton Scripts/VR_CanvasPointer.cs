using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Author: Elvir Sinancevic, based on the Youtube Tutorials of: https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ
public class VR_CanvasPointer : MonoBehaviour
{
    // Default length for the pointer ray, can be changed to whatever we need
    public float defaultLength = 3.0f;

    // Public variables (references) to the EventSystem and the InputModule in our scene, has to be set
    // by hand or by other scripts like SwitchPointer.cs
    public EventSystem eventSystem = null;
    public StandaloneInputModule inputModule = null;

    // Local Variable for LineRenderer
    private LineRenderer lineRenderer = null;

    // Awake Function, executed as the name says
    private void Awake()
    {
        // Get the LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Update the length of the line each frame
        UpdateLength();
    }

    // Updates the length of the ray
    private void UpdateLength()
    {
        // Set start and end position of the LineRenderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, GetEnd());
    }

    // Similar to CalculateEnd in the PhysicsPointer, but the calculation is moved to a new CalculateEnd function
    private Vector3 GetEnd()
    {
        // Get the distance of the canvas and the default end position by passing defaultLength to CalculateEnd
        float distance = GetCanvasDistance();
        Vector3 endPosition = CalculateEnd(defaultLength);

        // If the distance is 0.0f it means we have a collision --> calculate the new endPosiion
        if (distance != 0.0f)
        {
            endPosition = CalculateEnd(distance);
        }

        // Return the endPosition as a Vector3
        return endPosition;
    }
    
    // Calculates the distance of the canvas
    private float GetCanvasDistance()
    {
        // Get data, set the position to our mousePosition
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = inputModule.inputOverride.mousePosition;
        
        // Raycast using data, save the results
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);
        
        // Get closest result (first in the list) and get the distance of it
        RaycastResult closestResult = FindFirstRaycast(results);
        float distance = closestResult.distance;
        
        // Clamp
        distance = Mathf.Clamp(distance, 0.0f, defaultLength);

        // Return distance as a float
        return distance;
    }

    // Return the first result in the list (the list is already sorted)
    private RaycastResult FindFirstRaycast(List<RaycastResult> results)
    {
        // Iterate through the list
        foreach (RaycastResult result in results)
        {
            // Skip if the current result is not a GameObject
            if (!result.gameObject)
            {
                continue;
            }
            
            // As soon as we find a RaycastResult with a GameObject involved -> return
            return result;
        }
        
        // Return new empty RaycastResult if we dont have any results
        return new RaycastResult();
    }

    // Calculation of the (default) end of the ray
    private Vector3 CalculateEnd(float length)
    {
        // Calculate the (default) end of our line renderer, starting at the position of our hand
        // Adding on to the vector the forward vector of it times the length
        return  transform.position + (transform.forward) * length;;
    }
}