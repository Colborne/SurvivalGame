using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    InputManager inputManager;

    private void Awake() {
        inputManager = FindObjectOfType<InputManager>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = inputManager.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }
}
