using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviourPunCallbacks
{
    public GameManager gm;

    [SerializeField] private TextMeshProUGUI p1, p2, p3, p4;
    [SerializeField] private TextMeshProUGUI p1Score, p2Score, p3Score, p4Score;
    [SerializeField] private TextMeshProUGUI correctPointText;

    [SerializeField] private GameObject bx1, bx2, bx3;

    public bool pl1, pl2, pl3, pl4;

    public int scoreP1 =0, scoreP2 =0,scoreP3 = 0,scoreP4 = 0;
    public int moveP1 =0, moveP2 =0,moveP3 = 0,moveP4 = 0;
    public string nick1,nick2,nick3,nick4;

    public int playerIndex;
    PhotonView view;
    public int playerTurn =1;

    public int currentPlayers;

    [SerializeField] private AudioSource aud;
    [SerializeField] private AudioClip nextTurnAud,earnPointAud;
    public void AddIndex()
    {
        playerTurn++;
        aud.PlayOneShot(nextTurnAud);
        if (CheckIfPlayerInGame() == false)
        {
            gm.WrongAnswer();
        }
        else
        {

            if (playerTurn > currentPlayers)
            {
                playerTurn = 1;
            }
            SetPlayerInRow();
        }

       
       
        
        
    }

    public bool CheckIfPlayerInGame()
    {
        if(playerTurn == 1)
        {
            if(pl1 == false)
            {
                return false;
            }
        }

       else if (playerTurn == 2)
        {
            if (pl2 == false)
            {
                return false;
            }
        }

       else if (playerTurn == 3)
        {
            if (pl3 == false)
            {
                return false;
            }
        }

       else if (playerTurn == 4)
        {
            if (pl4 == false)
            {
               return false;
            }
        }

        return true;
    }

    public void AddScore()
    {
        aud.PlayOneShot(earnPointAud);
        switch (playerTurn)
        {
            case 1:
                scoreP1++;
                p1Score.text = scoreP1.ToString();
                correctPointText.text = "Acerto de " + p1.text + " Totalizando:" + p1Score.text + " pontos";
                break;
                case 2:
                scoreP2++;
                p2Score.text = scoreP2.ToString();
                correctPointText.text = "Acerto de " + p2.text + " Totalizando: " + p2Score.text + " pontos";
                break;
                case 3:
                scoreP3++;
                p3Score.text = scoreP3.ToString();
                correctPointText.text = "Acerto de " + p3.text + " Totalizando: " + p3Score.text + " pontos";
                break;
                case 4:
                scoreP4++;
                p4Score.text = scoreP4.ToString();
                correctPointText.text = "Acerto de " + p4.text + " Totalizando: " + p4Score.text + " pontos";
                break;
        }
    }

    public void AddMove()
    {
        switch (playerTurn)
        {
            case 1:
                moveP1++;
                
                break;
            case 2:
                moveP2++;
                
                break;
            case 3:
                moveP3++;
                
                break;
            case 4:
                moveP4++;
                
                break;
        }
    }
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // Obtém o número do jogador na sala
            playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;
          //  Debug.Log("Índice do jogador: " + playerIndex);
        }
        SetNames();
        UpdateCurrentPlayers();
        SetPlayerInRow();
    }

    public void SetBox(int aux)
    {
        if(aux == 2)
        {
            bx1.SetActive(true);
        }else if (aux == 3)
        {
            bx1.SetActive(true);
            bx2.SetActive(true);
        }else if(aux == 4)
        {
            bx1.SetActive(true);
            bx2.SetActive(true);    
            bx3.SetActive(true);    
        }
    }

    private void UpdateCurrentPlayers()
    {
        currentPlayers = PhotonNetwork.PlayerList.Length;
        SetBox(currentPlayers);


    }

    public void SetNames()
    {
        if (PhotonNetwork.InRoom)
        {
            // Obtém todos os jogadores na sala atual
            Player[] players = PhotonNetwork.PlayerList;


            if (players.Length == 1)
            {
                p1.text = players[0].NickName;
                nick1 = players[0].NickName;
                p1Score.text = "0";
                p2Score.text = "";
                p3Score.text = "";
                p4Score.text = "";

                p2.text = "";
                p3.text = "";
                p4.text = "";
                pl1 = true;
            }
            else if (players.Length == 2)
            {
                p1.text = players[0].NickName;
                p2.text = players[1].NickName;

                nick1 = players[0].NickName;
                nick2 = players[1].NickName;

                p1Score.text = "0";
                p2Score.text = "0";
                p3Score.text = "";
                p4Score.text = "";

                p3.text = "";
                p4.text = "";

                pl1 = true;
                pl2 = true;
            }
            else if (players.Length == 3)
            {
                p1.text = players[0].NickName;
                p2.text = players[1].NickName;
                p3.text = players[2].NickName;

                nick1 = players[0].NickName;
                nick2 = players[1].NickName;
                nick3 = players[2].NickName;

                p1Score.text = "0";
                p2Score.text = "0";
                p3Score.text = "0";
                p4Score.text = "";
                p4.text = "";

                pl1 = true;
                pl2 = true;
                pl3 = true;
            }
            else if (players.Length == 4)
            {
                p1.text = players[0].NickName;
                p2.text = players[1].NickName;
                p3.text = players[2].NickName;
                p4.text = players[3].NickName;

                nick1 = players[0].NickName;
                nick2 = players[1].NickName;
                nick3 = players[2].NickName;
                nick4 = players[3].NickName;


                p1Score.text = "0";
                p2Score.text = "0";
                p3Score.text = "0";
                p4Score.text = "0";

                pl1 = true;
                pl2 = true;
                pl3 = true;
                pl4 = true;
                
            }
        }
    }

    public void SetPlayerInRow()
    {
        switch (playerTurn)
        {
            case 1:
                p1.fontStyle = FontStyles.Bold;                
                p2.fontStyle = FontStyles.Normal;
                p3.fontStyle = FontStyles.Normal;
                p4.fontStyle = FontStyles.Normal;

                p1.color = new Color(0.6226415f, 0.2202741f, 0.2454221f);
                p2.color = Color.black;
                p3.color = Color.black;
                p4.color = Color.black;
                break;
                case 2:
                p1.color = Color.black;
                p2.color = new Color(0.6226415f, 0.2202741f, 0.2454221f);
                p3.color = Color.black;
                p4.color = Color.black;

                p1.fontStyle = FontStyles.Normal;
                p2.fontStyle = FontStyles.Bold;
                p3.fontStyle = FontStyles.Normal;
                p4.fontStyle = FontStyles.Normal;
                break;
                case 3:
                p1.color = Color.black;
                p2.color = Color.black;
                p3.color = new Color(0.6226415f, 0.2202741f, 0.2454221f);
                p4.color = Color.black;

                p1.fontStyle = FontStyles.Normal;
                p2.fontStyle = FontStyles.Normal;
                p3.fontStyle = FontStyles.Bold;
                p4.fontStyle = FontStyles.Normal;
                break;               
                case 4:
                p1.color = Color.black;
                p2.color = Color.black;
                p3.color = Color.black;
                p4.color = new Color(0.6226415f, 0.2202741f, 0.2454221f);

                p1.fontStyle = FontStyles.Normal;
                p2.fontStyle = FontStyles.Normal;
                p3.fontStyle = FontStyles.Normal;
                p4.fontStyle = FontStyles.Bold; 
                break;

        }
    }
}
