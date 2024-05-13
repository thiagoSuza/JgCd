using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviourPunCallbacks
{
    PhotonView view;

    public CountDown cd;

    public int pieceAvailable;

    [SerializeField] private List<Sprite> cardSprites;
    [SerializeField] private List<GameObject> cardObjects;

    [SerializeField] private List<Sprite> cardSpritesB;
    [SerializeField] private List<GameObject> cardObjectsB;

    


    [SerializeField] private Image img, imgA, imgB, imgA1;
    [SerializeField] private int indexA, indexB;

    [SerializeField] private GameObject panelA, panelB,shoewdPanel,winPanel,firstSelePanel;
    public GameObject btnA = null, btnB = null;

    public int playerTurn;

    public bool isLocked;

    [SerializeField] private TextMeshProUGUI nullText;

    [SerializeField] private AudioSource aud;
    [SerializeField] private AudioClip wrongAnswerAud;

    [SerializeField] GameObject pressBtn;

    private bool isC;

    private int checkIndex;
    int seed;
    int amountOfCards;
    // Start is called before the first frame update

    private void Awake()
    {
        
        pieceAvailable = cardObjects.Count;

        if (PhotonNetwork.IsMasterClient)
        {

            seed = Random.Range(int.MinValue, int.MaxValue);
            photonView.RPC("SyncSeed", RpcTarget.Others, seed);
        }



        if (PhotonNetwork.IsMasterClient)
        {
            amountOfCards = PlayerPrefs.GetInt("Card", 10);
            if (amountOfCards == 10)
            {
                pieceAvailable = 10;
                RemoveCards();
            }
            
        }

        
        view = GetComponent<PhotonView>();
       
    }

    [PunRPC]
    void SyncSeed(int newSeed)
    {
        // Sincroniza a semente em todos os clientes
        seed = newSeed;
        Random.InitState(seed);
    }
    void Start()
    {
       
        //SetRandonPosition();
        if (PhotonNetwork.IsMasterClient)
        {
            ShuffleCards();
            ShuffleCardsB();
        }

    }

    void RemoveCards()
    {
        for (int i = 0; i < 20; i++)
        {
            int x = Random.Range(0, cardSprites.Count);
            cardSprites.RemoveAt(x);
            cardSpritesB.RemoveAt(x);

            int xA = Random.Range(0, cardObjects.Count);
            int xB = Random.Range(0, cardObjectsB.Count);
            cardObjects[xA].SetActive(false);
            cardObjectsB[xB].SetActive(false);
            cardObjects.RemoveAt(xA);
            cardObjectsB.RemoveAt(xB);

            // Chama um RPC para notificar os outros jogadores sobre a remoção das cartas
            photonView.RPC("RemoveCardsRPC", RpcTarget.Others, x, xA, xB);
        }
    }

    [PunRPC]
    void RemoveCardsRPC(int x, int xA, int xB)
    {
        pieceAvailable = 10;
        // Remove as cartas nos outros clientes com base nos índices recebidos
        cardSprites.RemoveAt(x);
        cardSpritesB.RemoveAt(x);
        cardObjects[xA].SetActive(false);
        cardObjectsB[xB].SetActive(false);
        cardObjects.RemoveAt(xA);
        cardObjectsB.RemoveAt(xB);
    }

    void ShuffleCards()
    {
        List<int> randomIndices = new List<int>();

        // Generate a list of random indices
        for (int i = 0; i < cardSprites.Count; i++)
        {
            randomIndices.Add(i);
        }

        // Shuffle the list of random indices
        for (int i = 0; i < cardSprites.Count; i++)
        {
            int randomIndex = Random.Range(0, randomIndices.Count);
            int index = randomIndices[randomIndex];

            photonView.RPC("AssignCardSprite", RpcTarget.All, index, i);

            randomIndices.RemoveAt(randomIndex);
        }
    }

    [PunRPC]
    void AssignCardSprite(int spriteIndex, int cardIndex)
    {
        cardObjects[cardIndex].GetComponent<BtnController>().GetSprite(cardSprites[spriteIndex], spriteIndex);
    }
    void ShuffleCardsB()
    {
        List<int> randomIndices = new List<int>();

        // Generate a list of random indices
        for (int i = 0; i < cardSpritesB.Count; i++)
        {
            randomIndices.Add(i);
        }

        // Shuffle the list of random indices
        for (int i = 0; i < cardSpritesB.Count; i++)
        {
            int randomIndex = Random.Range(0, randomIndices.Count);
            int index = randomIndices[randomIndex];

            photonView.RPC("AssignCardSpriteB", RpcTarget.All, index, i);

            randomIndices.RemoveAt(randomIndex);
        }
    }

    [PunRPC]
    void AssignCardSpriteB(int spriteIndex, int cardIndex)
    {
        cardObjectsB[cardIndex].GetComponent<BtnController>().GetSprite(cardSpritesB[spriteIndex], spriteIndex);
    }


    /* public void SetRandonPosition()
     {
         for (int i = 0; i < gpA.Count; i++)
         {
             int randon = Random.Range(0,btnA.Count);
             btnA[randon].GetComponent<BtnController>().GetSprite(gpA[i],i);
             btnA.RemoveAt(randon);
         }
     }*/

    

    public void SetPanelA(int aux, string obj)
    {
        
        photonView.RPC("SetPanelAR", RpcTarget.All,aux,obj);
        
    }

    public void SetPanelB(int aux, string obj)
    {
        
        photonView.RPC("SetPanelBR", RpcTarget.All, aux, obj);
       
    }

    [PunRPC]
    public void SetPanelAR(int aux,string obj)
    {
        cd.isLock = true;
        img.enabled = true;
        indexA = aux;
        img.sprite = cardSprites[aux];
        imgA.sprite = cardSprites[aux];
        imgA1.sprite = cardSprites[aux];
        btnA = GameObject.Find(obj);
        
        btnA.GetComponent<BtnController>().ActiveImage();
        StartCoroutine(First());
        Invoke("ClosePanelA", 3.5f);
    }
    IEnumerator First()
    {
        yield return new WaitForSeconds(.5f);
        firstSelePanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        firstSelePanel.SetActive(false);
    }

    [PunRPC]
    public void SetPanelBR(int aux,string obj)
    {
        checkIndex = GetComponent<TurnManager>().playerTurn;
        cd.isLock = true;
        btnB = GameObject.Find(obj);
        btnB.GetComponent<BtnController>().ActiveImage();
        btnB.GetComponent<BtnController>().Disabel();
        indexB = aux;
        isLocked = true;
        imgB.sprite = cardSpritesB[aux];
        CheckAnswer();
    }
    public void ClosePanelA()
    {
        panelB.SetActive(true);
        btnA.GetComponent<BtnController>().DesactiveImge();
        isLocked = false;
        cd.currentTime = 15;
        cd.isLock = false;
        panelA.SetActive(false);
    }


    public void CheckAnswer()
    {
        if (indexA== indexB)
        {
            isC = true;
            if (GetComponent<TurnManager>().playerTurn == GetComponent<TurnManager>().playerIndex)
            {
                pressBtn.SetActive(true);
            }
            else
            {
                pressBtn.SetActive(false);
            }

            GetComponent<TurnManager>().AddScore();
            Invoke("CorrectPanel", 1.5f);
            Invoke("CorrectAnswer", 7f);            
            pieceAvailable--;
            if(pieceAvailable == 0)
            {
                isLocked = true;
                cd.isLock = true;
                Invoke("OpenWinPanel", 5f);
            }
        }
        else
        {
            if(GetComponent<TurnManager>().playerTurn == GetComponent<TurnManager>().playerIndex)
            {
                pressBtn.SetActive(true);
            }
            else
            {
                pressBtn.SetActive(false);
            }
            Invoke("WrongPanel", 1.5f);
           
            aud.PlayOneShot(wrongAnswerAud);
            Invoke("WrongAnswer", 7f);
        }
        

    }

    public void OpenWinPanel()
    {
        photonView.RPC("OpenWR", RpcTarget.All);
    }

    [PunRPC]
    void OpenWR()
    {
        winPanel.SetActive(true);
    }

    public void WrongPanel()
    {
        nullText.text = "O par não coincide.";
        shoewdPanel.SetActive(true);
    }
   
    public void CorrectPanel()
    {
        cd.currentTime = 15;
        shoewdPanel.SetActive(true);
    }

   

    public void WrongAnswer()
    {
        GetComponent<TurnManager>().AddIndex();
        isLocked = false;
        cd.currentTime = 15f;
        cd.isLock = false;
        photonView.RPC("WrongAnswerR", RpcTarget.All);
    }

    public void CorrectAnswer()
    {
        photonView.RPC("CorrectAnserR", RpcTarget.All);
    }

    [PunRPC]
    public void WrongAnswerR()
    {
         
            img.sprite = null;
            img.enabled = false;
            panelA.SetActive(true);
            indexA = 0;
            indexB = 0;
            btnA = null;
            btnB = null;
            shoewdPanel.SetActive(false);
            panelB.SetActive(false);


    }

    [PunRPC]
    public void CorrectAnserR()
    {
        if(btnA != null)
            btnA.SetActive(false);
        if (btnB != null)
            btnB.SetActive(false);
        panelA.SetActive(true);
        btnA = null;
        btnB = null;
        img.sprite = null;
        img.enabled = false;
        panelB.SetActive(false);
        shoewdPanel.SetActive(false);
        isLocked = false;
        cd.currentTime = 15f;
        cd.isLock = false;

    }

    public void BtnAction()
    {
        
        photonView.RPC("BtnActionR", RpcTarget.All);
        
    }


    [PunRPC]
    public void BtnActionR()
    {
        CancelInvoke("CorrectAnswer");
        CancelInvoke("WrongAnswer");

        if (isC)
        {
            if (btnA != null)
                btnA.SetActive(false);
            if (btnB != null)
                btnB.SetActive(false);
            panelA.SetActive(true);
            btnA = null;
            btnB = null;
            img.sprite = null;
            img.enabled = false;
            panelB.SetActive(false);
            isC = false;
           
        }
        else
        {
            GetComponent<TurnManager>().AddIndex();

            
            img.sprite = null;
            img.enabled = false;
            panelA.SetActive(true);
            indexA = 0;
            indexB = 0;
            btnA = null;
            btnB = null;
            panelB.SetActive(false);
        }

        isLocked = false;
        cd.currentTime = 15f;
        cd.isLock = false;
        shoewdPanel.SetActive(false);

    }
}
