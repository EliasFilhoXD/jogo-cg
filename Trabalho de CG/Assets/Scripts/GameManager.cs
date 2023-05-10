using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float startingTime = 400f; // 20 minutos
    public int startingPoint = 2000;
    private float currentTime;
    public TextMeshProUGUI textoTempo;
    public TextMeshProUGUI textoPonto;

    public int currentPoint;
    public AudioSource deathSound;
    bool truee = true;
    public AudioSource pontoAudio;
    public AudioSource danoAudio;

    bool final = false;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        currentPoint = startingPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (final != true){
            if (currentTime > 0f)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = 0f;
            }

            DisplayTime(currentTime);

            if(currentPoint <= 0){
                if(truee == true){
                    GameObject playerObject = GameObject.FindWithTag("Player");
                    Destroy(playerObject);
                    deathSound.Play();
                    textoPonto.text = "GAME OVER";
                    truee = false;
                    final = true;
                }
                
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
     float minutes = Mathf.FloorToInt(timeToDisplay / 60);
     float seconds = Mathf.FloorToInt(timeToDisplay % 60);

     textoTempo.text = "Tempo: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        Debug.Log("Texto atualizado: " + textoTempo.text);
    }

    public void AddPoint(int pontosAdicionados){
        currentPoint += pontosAdicionados;
        textoPonto.text = "Pontos: " + currentPoint;
        pontoAudio.Play();
    }

    public void RemovePoint(int pontosRemovidos){
        currentPoint -= pontosRemovidos;
        textoPonto.text = "Pontos: " + currentPoint;
        danoAudio.Play();
    }

    public void ResetGame() {
        currentPoint = startingPoint;
        textoPonto.text = "Pontos: " + currentPoint;
    }

    public void SetFinal(bool final){
        this.final = final;
        if (final == true){
            GameObject playerObject = GameObject.FindWithTag("Player");
            Destroy(playerObject);
            textoPonto.text = "PONTUAÇÂO FINAL: " + (int) currentPoint;
            textoTempo.text = "FIM DO JOGO";
        }

    }
}
