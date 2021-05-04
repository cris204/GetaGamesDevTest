using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    public RectTransform targetTransform;
    public Vector3 initScale = Vector3.one;
    public bool isClicked;
    public bool isScaleSetup;
    public bool disableScale;

    private void Awake()
    {
        if (targetTransform != null) {
            targetTransform = GetComponent<RectTransform>();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        if (!isScaleSetup) {
            initScale = (targetTransform != null ? targetTransform : transform).localScale;
            isScaleSetup = true;
        }
        if (!this.disableScale) {

            if(targetTransform != null) {

                iTween.ScaleTo(targetTransform.gameObject, initScale * 1.05f, 0.2f);

            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        isClicked = false;
        if (!disableScale) {
            if (targetTransform != null) {
                iTween.ScaleTo(targetTransform.gameObject, initScale, 0.2f);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClicked = true;
        if (!disableScale) {
            if (targetTransform != null) {
                iTween.ScaleTo(targetTransform.gameObject, initScale * 0.9f, 0.2f);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked) {
            if (!this.disableScale) {
                if (targetTransform != null) {
                    iTween.ScaleTo(targetTransform.gameObject, initScale, 0.2f);
                }
            }
        }
    }

}
