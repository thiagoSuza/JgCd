using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class JoinAndCreatGame : MonoBehaviourPunCallbacks
{
    public TMP_InputField  enterIp, HostnameIp,clientNameIP;

    private const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int codeLength = 5;

    private string GenerateRoomCode()
    {
        char[] chars = new char[codeLength];
        System.Random random = new System.Random();

        for (int i = 0; i < codeLength; i++)
        {
            chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }


    public void CreatRoom()
    {
        ChangeName();
        string roomCode = GenerateRoomCode();
        PlayerPrefs.SetString("Code", roomCode);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom(roomCode);
    }

    public void JoinRoom()
    {
        ChangeNameClient();
        PlayerPrefs.SetString("Code", enterIp.text);
        PhotonNetwork.JoinRoom(enterIp.text);
    }

    public void Exit()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel(1);
    }

    public void ChangeName()
    {
        PhotonNetwork.NickName = HostnameIp.text;
    }

    public void ChangeNameClient()
    {
        PhotonNetwork.NickName = clientNameIP.text;
    }

}
