using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverSound : MonoBehaviour, ISelectHandler
{
    
    public void OnSelect(BaseEventData eventData)
    {
        GameController.instance.uiHoverEvent.Post(gameObject);
    }
}
