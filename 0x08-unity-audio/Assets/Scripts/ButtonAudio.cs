using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource hover;

    public void MouseEnter() {
        Debug.Log("Foo");
        hover.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover.Play();
    }
}
