using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SendData : MonoBehaviour
{
    private const string DATABASE_URL = "https://jogo-da-velha-f5199-default-rtdb.firebaseio.com/";
    private const string SECRET_KEY = "mGWqO233vkvQAQyrKLb6Y6mHkgS6EdO8HepoH7us";

    void Start()
    {
        // O código de inicialização pode ser mantido aqui, se necessário
    }

  /* private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            SendDataToDatabase("Sem Pontuação", 0, 0, "1");
            SendDataToDatabase("Sem Pontuação", 0, 0, "2");
            SendDataToDatabase("Sem Pontuação", 0, 0, "3");
            SendDataToDatabase("Sem Pontuação", 0, 0, "4");
        }
    }*/

    public void SendDataToDatabase(string name, int score, int row, string node)
    {
        string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
        Data newData = new Data(name, score, row, currentDate);
        string jsonData = JsonUtility.ToJson(newData);

        StartCoroutine(PostRequest(node, jsonData));
    }

    IEnumerator PostRequest(string node, string json)
    {
        string url = DATABASE_URL + node + ".json?auth=" + SECRET_KEY;
        byte[] postData = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao enviar dados para o banco de dados: " + request.error);
        }
        else
        {
            Debug.Log("Dados enviados para o banco de dados com sucesso!");
        }
    }

    [Serializable]
    public class Data
    {
        public string name;
        public int score;
        public int row;
        public string date;

        public Data(string name, int score, int row, string date)
        {
            this.name = name;
            this.score = score;
            this.row = row;
            this.date = date;
        }
    }
}