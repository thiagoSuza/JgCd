using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSelecCardAmount : MonoBehaviour
{
    [SerializeField] private Image img10, img30;

    private void Start()
    {
        img10.color = new Color(1, 1, 1, 0.5f);
        img30.color = new Color(1, 1, 1, 1);
        PlayerPrefs.SetInt("Card", 30);
    }

    public void Set10()
    {
        img10.color = new Color(1,1, 1,1);
        img30.color = new Color(1,1, 1,0.5f);
        PlayerPrefs.SetInt("Card", 10);
    }

    public void Set30()
    {
        img10.color = new Color(1, 1, 1, 0.5f);
        img30.color = new Color(1, 1, 1, 1);
        PlayerPrefs.SetInt("Card", 30);
    }
}
