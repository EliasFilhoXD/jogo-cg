using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilScript : MonoBehaviour
{
    public int value;
    public AudioSource danoAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other){
        if(other.tag == "Player"){
               Debug.LogWarning("Entrou");
            FindObjectOfType<GameManager>().RemovePoint(value);
            danoAudio.Play();

        }
    }
}
