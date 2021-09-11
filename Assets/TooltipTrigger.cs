using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Multiline()]
    public string content;
    public string header;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //LeanTween.delayedCall(0.5f, () =>
        //{
        TooltipSystem.Show(content, header);
        //});

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //LeanTween.cancel();
        TooltipSystem.Hide();
    }
}
