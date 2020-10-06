using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Author: Elvir Sinancevic, based on the Youtube Tutorials of: https://www.youtube.com/channel/UCG8bDPqp3jykCGbx-CiL7VQ
public class PointerEvent_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    // Local variables for the colors
    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.grey;
    public Color32 m_DownColor = Color.red;
    // Local variable for the image component
    private Image m_Image = null;
    
    // Executed once the class PointerEvent_UI is called
    public void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    // Once the Pointer enters an Object
    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("Enter");
        // Change color
        m_Image.color = m_HoverColor;
    }

    // Once the Pointer exits an Object
    public void OnPointerExit(PointerEventData eventData)
    {
        //print("Exit");
        // Change color
        m_Image.color = m_NormalColor;
    }
    
    // Once you press down with the Pointer on an Object
    public void OnPointerDown(PointerEventData eventData)
    {
        //print("Down");
        // Change color
        m_Image.color = m_DownColor;
    }
    
    // Once you release with the Pointer on an Object
    public void OnPointerUp(PointerEventData eventData)
    {
        //print("Up");
        // We want the color only to change if we have a successfull click
    }
    
    // Once you perform an actual Click on a Object
    public void OnPointerClick(PointerEventData eventData)
    {
        //print("Click");
        // Change color
        m_Image.color = m_HoverColor;
    }
}