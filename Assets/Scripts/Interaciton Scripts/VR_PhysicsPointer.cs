using UnityEngine;

// Author: Elvir Sinancevic, based on the Youtube Tutorials of: https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ
public class VR_PhysicsPointer : MonoBehaviour
{
    // Default length for the pointer ray, can be changed to whatever we need
    public float defaultLength = 3.0f;
    
    // Local variable for the LineRenderer
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
        lineRenderer.SetPosition(1, CalculateEnd());
    }

    // Calculates the end of the ray
    private Vector3 CalculateEnd()
    {
        // Create a Raycast and call the default end and pass in the default length
        RaycastHit hit = CreateForwardRaycast();            // Create the forward raycast
        Vector3 endPosition = DefaultEnd(defaultLength);    // Just in case we dont hit anything calculate the default end

        // If we hit a collider, update the endPosition to the hit point
        if (hit.collider)
        {
            endPosition = hit.point;
        }

        // Return the endPosition as a Vector3
        return endPosition;
    }

    // Performs a forward raycast
    private RaycastHit CreateForwardRaycast()
    {
        // The variable we will return
        RaycastHit hit;
        
        // Create a new ray that starts at our controller and points forward, out from it
        Ray ray = new Ray(transform.position, transform.forward);
        
        // Give the raycast the ray, our hit variable and the information how far we want it to go
        Physics.Raycast(ray, out hit, defaultLength);

        // Return any hit information
        return hit;
    }

    // Calculates the default end of the ray, when no object is hit
    private Vector3 DefaultEnd(float length)
    {
        // Calculate the default end of our line renderer, starting at the position of our hand
        // Adding on to the vector the forward vector of it times the length
        return  transform.position + (transform.forward) * length;;
    }
}