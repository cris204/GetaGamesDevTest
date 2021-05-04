using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PopUpWidget : MonoBehaviour
{
    public GameObject container;

    public UnityEvent onOpen;
    public UnityEvent onClose;

    private void OnEnable()
    {
        Open();
    }

    public void Open()
    {
        onOpen?.Invoke();
        gameObject.SetActive(true);
        container.SetActive(true);
        container.transform.localScale = Vector3.zero;

        iTween.ScaleTo(container, iTween.Hash("scale", Vector3.one,
            "time", 0.2f));
    }
    public void Close()
    {
        iTween.ScaleTo(container, iTween.Hash("scale", Vector3.zero,
            "time", 0.2f,
            "oncomplete", "oncompleteclose"));

    }

    private void OnCompleteClose()
    {
        onClose?.Invoke();
        container.SetActive(false);
        gameObject.SetActive(false);
    }

}

