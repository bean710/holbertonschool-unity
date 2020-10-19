using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public AudioSource hoverSound;
    public AudioSource clickSound;

    public void OnPointerDown(PointerEventData eventData)
    {
        clickSound.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverSound.Play();
    }
}
