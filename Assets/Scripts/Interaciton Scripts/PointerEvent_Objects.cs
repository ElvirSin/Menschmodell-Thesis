using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Author: Elvir Sinancevic, based on the Youtube Tutorials of: https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ
public class PointerEvent_Objects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    // Local variables for the colors
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color enterColor = Color.gray;
    [SerializeField] private Color downColor = Color.red;
    [SerializeField] private UnityEvent OnClick = new UnityEvent();

    // Local variables for the meshRenderer(s)
    private MeshRenderer meshRenderer = null;
    private MeshRenderer[] meshRenderers = null;

    // Executed once the class PointerEvent_Objects is called
    private void Awake()
    {
        // Try to get the meshRenderer, but sometimes the parent object does not have a meshRenderer
        try
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        // If the parent object has no meshRenderer get all the meshRenderers from the child objects
        meshRenderers = new MeshRenderer[GetComponentsInChildren<MeshRenderer>().Length];
        meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
    }

    // Once the Pointer enters an Object
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set color to the/all meshRenderer/s
        if (meshRenderer != null)
        {
            meshRenderer.material.color = enterColor;
        }

        if (meshRenderers.Length > 0)
        {
            foreach(MeshRenderer mesh in meshRenderers)
            {
                mesh.material.color = enterColor;
            }
        }
        //meshRenderer.material.color = enterColor;
        //print("Enter");
    }

    // Once the Pointer exits an Object
    public void OnPointerExit(PointerEventData eventData)
    {
        // Set color to the/all meshRenderer/s
        if (meshRenderer != null)
        {
            meshRenderer.material.color = normalColor;
        }
        if (meshRenderers.Length > 0)
        {
            foreach(MeshRenderer mesh in meshRenderers)
            {
                mesh.material.color = normalColor;
            }
        }
        //meshRenderer.material.color = normalColor;
        //print("Exit");
    }

    // Once you press down with the Pointer on an Object
    public void OnPointerDown(PointerEventData eventData)
    {
        // Set color to the/all meshRenderer/s
        if (meshRenderer != null)
        {
            meshRenderer.material.color = downColor;
        }
        if (meshRenderers.Length > 0)
        {
            foreach(MeshRenderer mesh in meshRenderers)
            {
                mesh.material.color = downColor;
            }
        }
        //meshRenderer.material.color = downColor;
        //print("Down");
    }

    // Once you release with the Pointer on an Object
    public void OnPointerUp(PointerEventData eventData)
    {
        // Set the color to the/all meshRenderer/s
        if (meshRenderer != null)
        {
            meshRenderer.material.color = enterColor;
        }
        if (meshRenderers.Length > 0)
        {
            foreach(MeshRenderer mesh in meshRenderers)
            {
                mesh.material.color = enterColor;
            }
        }
        //meshRenderer.material.color = enterColor;
        //print("Up");
    }

    // Once you perform an actual Click on a Object
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
       //print("Click");
    }
}