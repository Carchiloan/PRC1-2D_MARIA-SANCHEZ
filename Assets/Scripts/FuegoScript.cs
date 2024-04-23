using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoScript : MonoBehaviour
{
    GameObject personaje;

    bool bolaDerecha = true;

    public float speedBala = 5.0f;

    float queHoraEs;

    // Start is called before the first frame update
    void Start()
    {
        personaje = GameObject.Find("Personaje");
        bolaDerecha = personaje.GetComponent<MovPersonaje>().miraDerecha;
    }

    // Update is called once per frame
    void Update()
    {
       if(bolaDerecha){
        transform.Translate(speedBala*Time.deltaTime, 0, 0, Space.World);

       }else{ 
        transform.Translate((speedBala*Time.deltaTime)*-1, 0, 0, Space.World);
        }

        if(Time.time >= queHoraEs+tiempoDestruccion){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        //col.gameObject.name;
        //Debug.Log(col.gameObject.name.StartWith("Fantasma"));

        /*if(col.gameObject.name.StratWith("Fantasma")){
            Destroy(col.gameObject);
        }*/

        if(col.gameObject.tag == "Enemigo"){
            Destroy(col.gameObject);

            GameManager.muertes +=1;

            Destroy(this.gameObject);
        }
    }
}
