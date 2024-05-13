using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalTimer : MonoBehaviour
{
    public TextMeshProUGUI cronometroText;
    public float tempoPassado = 0f;
    public bool cronometroRodando = false;

    
    private void Start()
    {
        IniciarCronometro();
    }

    void Update()
    {
        if (cronometroRodando)
        {
            tempoPassado += Time.deltaTime;
            AtualizarCronometro(tempoPassado);
        }
    }

   

    public void IniciarCronometro()
    {
        cronometroRodando = true;
    }



    void AtualizarCronometro(float tempo)
    {
        int minutos = Mathf.FloorToInt(tempo / 60);
        int segundos = Mathf.FloorToInt(tempo % 60);
        int milissegundos = Mathf.FloorToInt((tempo * 100) % 100);

        cronometroText.text = string.Format("{0:00}m:{1:00}s", minutos, segundos);
    }
}
