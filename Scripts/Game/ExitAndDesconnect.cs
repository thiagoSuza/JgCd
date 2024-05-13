using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAndDesconnect : MonoBehaviourPunCallbacks
{
    public TurnManager turnManager;
    public void Exit()
    {
        Cancel();
        Invoke("SetOff",1f);
       
    }

    public void SetOff()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

    public void RankingPanel()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(3);
    }

    public void Cancel()
    {
        int playerIndex = turnManager.playerIndex;

        if (PhotonNetwork.IsConnected)
        {
            photonView.RPC("CancelR", RpcTarget.All, playerIndex);
        }
        else
        {
            // Se não estiver conectado, chama o método localmente
            CancelR(playerIndex);
        }
    }

    [PunRPC]
    public void CancelR(int playerIndex)
    {
        // Verifica se o índice do jogador está dentro do intervalo válido
        if (playerIndex >= 1 && playerIndex <= 4)
        {
            // Define o indicador pl correspondente como false
            switch (playerIndex)
            {
                case 1:
                    turnManager.pl1 = false;
                    break;
                case 2:
                    turnManager.pl2 = false;
                    break;
                case 3:
                    turnManager.pl3 = false;
                    break;
                case 4:
                    turnManager.pl4 = false;
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Índice de jogador inválido: " + playerIndex);
        }
    }
}
