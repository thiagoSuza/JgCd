using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject hostPanel,clientPanel,rankPanel;

   public void OpenHostPanel()
   {
     hostPanel.SetActive(true);
   }

    public void CloseHostPanel()
    {
        hostPanel.SetActive(false);
    }

    public void OpenCLientPanel()
    {
        clientPanel.SetActive(true);
    }

    public void CloseClientPanel()
    {
        clientPanel.SetActive(false);
    }

    public void Ranking()
    {
        rankPanel.SetActive(true);
    }

    public void CloseRanking()
    {
        rankPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
