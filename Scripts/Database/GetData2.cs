using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GetData2 : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI rowText;
    public TextMeshProUGUI dateText;

    const string DATABASE_URL = "https://jogo-da-velha-f5199-default-rtdb.firebaseio.com/";
    private const string SECRET_KEY = "mGWqO233vkvQAQyrKLb6Y6mHkgS6EdO8HepoH7us"; // Lembre-se de manter isso seguro!
    public string id;

    public List<string> st = new List<string>();
    private void Awake()
    {
        StartFetchingData2();
    }
    void Start()
    {
        Invoke("StartFetchingData", 2f);
    }

    public void GetCede(string id)
    {
        string texto = id;

        int startIndex = 0;

        while (true)
        {
            startIndex = texto.IndexOf("-", startIndex);

            if (startIndex == -1)
                break;

            int endIndex = texto.IndexOf("\"", startIndex + 1);

            if (endIndex == -1)
            {
                Debug.Log("Ponto final não encontrado.");
                break;
            }

            string resultado = texto.Substring(startIndex, endIndex - startIndex);
            st.Add(resultado);


            startIndex = endIndex; // move o startIndex para a próxima posição depois do fim
        }
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            StartFetchingData();
        }

    }
    public void StartFetchingData()
    {
        StartCoroutine(GetDataR());
    }

    public void StartFetchingData2()
    {
        StartCoroutine(Startt());
    }


    IEnumerator Startt()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(DATABASE_URL + "2.json"))
        {
            yield return www.SendWebRequest();

            // Verifica o status da solicitação
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao fazer a solicitação: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                GetCede(jsonResponse);


            }
        }
    }

    IEnumerator GetDataR()
    {
        int scoreM = 0, index = 0;
        for (int i = 0; i < st.Count; i++)
        {
            RestClient.Get<Data>(DATABASE_URL + "2/" + st[i] + ".json").Then(response =>
            {
                if (response.score >= scoreM)
                {
                    scoreM = response.score;
                    index = i;
                }


            }).Catch(error =>
            {
                Debug.LogError("Erro ao obter dados do banco de dados: " + error.Message);
            });
        }


        StartCoroutine(HighScore(index));
        yield return null;
    }

    IEnumerator HighScore(int x)
    {

        RestClient.Get<Data>(DATABASE_URL + "2/" + st[x] + ".json").Then(response =>
        {
            nameText.text = response.name;
            scoreText.text = response.score.ToString();
            rowText.text = response.row.ToString();
            dateText.text = response.date.ToString();


        }).Catch(error =>
        {
            Debug.LogError("Erro ao obter dados do banco de dados: " + error.Message);
        });


        yield return null;
    }


    [Serializable]
    public class Data
    {
        public string name;
        public int score;
        public int row;
        public string date;
    }
}
