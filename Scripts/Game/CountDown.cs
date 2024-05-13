using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public GameManager manager;

    [SerializeField]
    private float totalTime = 15f;

    [SerializeField]
    private TextMeshProUGUI timerText;

    public float currentTime;

    public bool isLock;

    public GameObject panelA, panelB,imgGroupA,imgGroupB;

    private void Awake()
    {
        isLock = true;
    }
    private void Start()
    {
        manager.isLocked = true;
        currentTime = totalTime;
        Invoke("StartTimer",2f);
       // panelB.SetActive(false);
       // imgGroupB.SetActive(false);
        //Invoke("SetGroupB", 14f);
        //Invoke("SetGroupA", 29.8f);
    }

    public void SetGroupB()
    {
        panelA.SetActive(false);
        panelB.SetActive(true);
        imgGroupA.SetActive(false);
        imgGroupB.SetActive(true);
    }
    public void SetGroupA()
    {
        panelA.SetActive(false);
        panelB.SetActive(false);
        imgGroupA.SetActive(true);
        imgGroupB.SetActive(false);
    }

    private void Update()
    {
        if (currentTime > 0 && !isLock)
        {
            currentTime -= Time.deltaTime;


            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);


            timerText.text = formattedTime;
        }
        if(currentTime < 0 ) 
        {

            
            manager.WrongAnswer();

        }


        float floatValue = 3.14159f;
        string formattedFloat = floatValue.ToString("0.00");

    }

    public void StartTimer()
    {
        isLock = false;
        manager.isLocked = false;
    }
}
