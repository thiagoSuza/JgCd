using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnController : MonoBehaviour
{
    [SerializeField] private Image img;
    public int index = 0;

    public int selector;

    public GameManager gm;
    public TurnManager tm;

    public AudioSource aud;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DesactiveImge", 0f);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            ActiveImage();
        }
    }

    private void OnDisable()
    {
        DesactiveImge();
    }

    public void GetSprite(Sprite sprite, int aux)
    {
        index = aux;
        img.sprite = sprite;
    }

    public void DesactiveImge()
    {
        img.enabled = false;
    }

    public void ActiveImage()
    {
        img.enabled = true;
    }

    public void DefineIndex()
    {
        aud.Play();
        if(tm.playerTurn == tm.playerIndex && !gm.isLocked)
        {
            gm.isLocked = true;
            
            if (selector == 0)
            {
                
                tm.AddMove();
                gm.SetPanelA(index, gameObject.name);
                
            }
            else
            {
              
                gm.SetPanelB(index, gameObject.name);
                Invoke("DesactiveImge", 4f);
            }
        }
        
    }

    public void Disabel()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(3f);
        DesactiveImge();
    }

    
}
