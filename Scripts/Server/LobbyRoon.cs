using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;


public class LobbyRoon : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI p1, p2,p3,p4,codeText;

    [SerializeField] private AudioClip enter,startAud;
    [SerializeField] private AudioSource aud;

    public GameObject startGameButtonText;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        codeText.text = PlayerPrefs.GetString("Code");
        SetPlayerName();

        if (PhotonNetwork.IsMasterClient)
        {
            startGameButtonText.gameObject.SetActive(true);
        }
    }

    public void Exit()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);

    }


    public void StartGame()
    {

        aud.PlayOneShot(startAud);
        Invoke("InitGame", 2.5f);

    }

    public void InitGame()
    {
        photonView.RPC("LoadNextScene", RpcTarget.All);
    }

    [PunRPC]
    private void LoadNextScene()
    {
        
        // Carrega a próxima cena para todos os jogadores
        PhotonNetwork.LoadLevel("GameScene"); 
    }



    // Chama o método SetPlayerName() sempre que houver uma mudança na sala
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        SetPlayerName();
        aud.PlayOneShot(enter);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetPlayerName();
    }

    public void SetPlayerName()
    {
        if (PhotonNetwork.InRoom)
        {
            // Obtém todos os jogadores na sala atual
            Player[] players = PhotonNetwork.PlayerList;

            
            if(players.Length == 1)
            {
                p1.text = players[0].NickName;
                p2.text = "Aguardando jogador 2";
                p3.text = "Aguardando jogador 3";
                p4.text = "Aguardando jogador 4";
            }
            else if(players.Length == 2)
            {
                p1.text = players[0].NickName;
                p2.text = players[1].NickName;
                p3.text = "Aguardando jogador 3";
                p4.text = "Aguardando jogador 4";
            }else if(players.Length == 3)
            {
                p1.text = players[0].NickName;
                p2.text = players[1].NickName;
                p3.text = players[2].NickName;
                p4.text = "Aguardando jogador 4";
            }else if(players.Length == 4)
            {
                p1.text = players[0].NickName;
                p2.text = players[1].NickName;
                p3.text = players[2].NickName;
                p4.text = players[3].NickName;
            }
        }
    }
}
