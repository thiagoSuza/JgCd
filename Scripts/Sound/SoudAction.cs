using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoudAction : MonoBehaviour
{
    [SerializeField] private AudioSource aud;


    public void PlayS()
    {
        aud.Play();
    }
}
