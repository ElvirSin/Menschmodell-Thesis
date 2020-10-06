using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Elvir Sinancevic, based on the Youtube Tutorials of: https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ
public class VR_Pointer : MonoBehaviour
{
    // Default length of our pointer
    public float defaultLength = 3.0f;
    // Line renderer to draw the line
    private LineRenderer lineRenderer = null;

    // Executed once the class VR_Pointer is called
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

    // Helper Function to update the length
    private void UpdateLength()
    {
        // Set start and end position of the LineRenderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, CalculateEnd());
    }

    // Calculate the end of the raycast
    private Vector3 CalculateEnd()
    {
        // Create a Raycast and call the default end and pass in the default length
        RaycastHit hit = CreateForwardRaycast();            // Create the forward raycast
        Vector3 endPosition = DefaultEnd(defaultLength);    // Just in case we dont hit anything calculate the default end

        // If we hit a collider update the endPosition to the hit pointts
        if (hit.collider)
        {
            endPosition = hit.point;
        }

        // Return the endPosition
        return endPosition;
    }

    // Forward Raycast
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

    // The default end of the Raycast
    private Vector3 DefaultEnd(float length)
    {
        // Calculate the default end of our line renderer, starting at the position of our hand
        // Adding on to the vector the forward vector of it times the length
        return  transform.position + (transform.forward) * length;;
    }
}
