using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingAndCreditsPanel : MonoBehaviour
{
    [SerializeField] private GameObject rankinP, creditP;
   

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void ActiveCredit()
    {
        creditP.SetActive(true);
        rankinP.SetActive(false);
    }

    public void ActiveRanking()
    {
        creditP.SetActive(false);
        rankinP.SetActive(true);
    }

    public void OpenPP()
    {
        Application.OpenURL("https://evoluamaua.net/eureka/apoiadores.html");
    }
}
