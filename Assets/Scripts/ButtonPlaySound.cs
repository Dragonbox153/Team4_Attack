using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaySound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void OnClick()
    {
        audioSource.Play();
    }
}
