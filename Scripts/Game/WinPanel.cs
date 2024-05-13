using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;



public class WinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winner,second,third,fourth;
    
    public TurnManager tm;
    public CountDown cd;
    public TotalTimer totalt;

    public SendData ds;    
        

    private int totalPlayers;
    private void OnEnable()
    {
        totalPlayers = tm.currentPlayers;
        SetWInner(totalPlayers);
        cd.isLock = true;
        totalt.cronometroRodando = false;
    }

    public void SetWInner(int aux)
    {

        if (aux == 1)
        {
            OnePlayer();
            second.text = "";
            third.text = "";
            fourth.text = "";
        }else if (aux == 2)
        {
            TwoPlayers();
            third.text = "";
            fourth.text = "";
        }

        else if(aux == 3)
        {
            ThreePlayers();
            fourth.text = "";
        }
        else
        {
            FourPlayers();
        }
    }

    public void OnePlayer()
    {
        winner.text = "Jogador Vencedor: " + tm.nick1 + " Com " + tm.scoreP1 + " pontos e: " + tm.moveP1 + " jogadas";
        if (PhotonNetwork.IsMasterClient)
        {
            ds.SendDataToDatabase(tm.nick1, tm.scoreP1,tm.moveP1,"1");
        }
        
    }

    public void TwoPlayers()
    {
        if(tm.scoreP1 > tm.scoreP2 || tm.moveP1 < tm.moveP2)
        {
            winner.text = "Jogador Vencedor: " + tm.nick1 + " Com " + tm.scoreP1 + " pontos e: " + tm.moveP1 + " jogadas";
            if (PhotonNetwork.IsMasterClient)
            {
                ds.SendDataToDatabase(tm.nick1, tm.scoreP1,tm.moveP1, "2");
            }
            second.text = " 2º colocado :" + tm.nick2 + " Com " + tm.scoreP2 + " pontos e: " + tm.moveP2 + " jogadas";
        }
        else
        {
            winner.text = "Jogador Vencedor: " + tm.nick2 + " Com " + tm.scoreP2 + " pontos e: " + tm.moveP2 + " jogadas";
            if (PhotonNetwork.IsMasterClient)
            {
                ds.SendDataToDatabase(tm.nick2, tm.scoreP2,tm.moveP2,"2");
            }
            
            second.text = " 2º colocado :" + tm.nick1 + " Com " + tm.scoreP1 + " pontos e: " + tm.moveP1 + " jogadas";
        }
    }

    public void ThreePlayers()
    {
        List<PlayerData> players = new List<PlayerData>
        {
            new PlayerData(tm.nick1, tm.scoreP1,tm.moveP1),
            new PlayerData(tm.nick2, tm.scoreP2, tm.moveP2),
            new PlayerData(tm.nick3, tm.scoreP3, tm.moveP3),
            
        };

        // Ordenar a lista de jogadores por pontuação (do maior para o menor)
        players.Sort((a, b) => b.score.CompareTo(a.score));

        // Mostrar os vencedores na tela
        for (int i = 0; i < players.Count; i++)
        {
            TextMeshProUGUI textComponent = null;
            switch (i)
            {
                case 0:
                    textComponent = winner;
                    break;
                case 1:
                    textComponent = second;
                    break;
                case 2:
                    textComponent = third;
                    break;
                case 3:
                    textComponent = fourth;
                    break;
            }

            if (textComponent != null)
            {
                textComponent.text = $"{i + 1}º Lugar: {players[i].name} com {players[i].score} pontos e {players[i].move} jogadas";
                // Enviar os dados dos vencedores para o banco de dados
               
            }

            if (i == 0)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    ds.SendDataToDatabase(players[i].name, players[i].score, players[i].move, "3");
                }
            }
        }
    }

    public void FourPlayers()
    {
        List<PlayerData> players = new List<PlayerData>
        {
            new PlayerData(tm.nick1, tm.scoreP1,tm.moveP1),
            new PlayerData(tm.nick2, tm.scoreP2, tm.moveP2),
            new PlayerData(tm.nick3, tm.scoreP3, tm.moveP3),
            new PlayerData(tm.nick4, tm.scoreP4, tm.moveP4),

        };

        // Ordenar a lista de jogadores por pontuação (do maior para o menor)
        players.Sort((a, b) => b.score.CompareTo(a.score));

        // Mostrar os vencedores na tela
        for (int i = 0; i < players.Count; i++)
        {
            TextMeshProUGUI textComponent = null;
            switch (i)
            {
                case 0:
                    textComponent = winner;
                    break;
                case 1:
                    textComponent = second;
                    break;
                case 2:
                    textComponent = third;
                    break;
                case 3:
                    textComponent = fourth;
                    break;
            }

            if (textComponent != null)
            {
                textComponent.text = $"{i + 1}º Lugar: {players[i].name} com {players[i].score} pontos e {players[i].move} jogadas";
                // Enviar os dados dos vencedores para o banco de dados

            }

            if (i == 0)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    ds.SendDataToDatabase(players[i].name, players[i].score, players[i].move, "4");
                }
                
            }
        }
    }

    private class PlayerData
    {
        public string name;
        public int score;        
        public int move;

        public PlayerData(string name, int score, int move)
        {
            this.name = name;
            this.score = score;
            this.move = move;
        }
    }
}
    

    

