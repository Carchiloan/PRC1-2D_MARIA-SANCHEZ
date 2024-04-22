using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    public float velocity = 1f;
    private float multiplicador = 15f;

    private float multiplicadorSalto = 8f;

    private bool puedoSaltar = true;

    public static bool miraDerecha = true;

    private Rigidbody2D rb;

    private Animator animatorController;

    GameObject respawn;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>( );

        animatorController = this.GetComponent<Animator>();

        //transform.position = new Vector3( -9f, 0f, 0);

        //Respawn
        respawn = GameObject.Find("Respawn");
        //Respawnear();
        transform.position = respawn.transform.position;
        //transform.position = respawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.estoyMuerto) return;

        float miDeltaTime = Time.deltaTime;

        //movimiento personaje
         float movTeclas = Input.GetAxis("Horizontal"); //(a -1f - d 1f)
        //float movTeclasY = Input.GetAxis("Vertical"); //(a -1f - d 1f)
        
        rb.velocity = new Vector2(movTeclas*multiplicador, rb.velocity.y);


        //FLIP <--
        if(movTeclas < 0){
            this.GetComponent<SpriteRenderer>().flipX = true;
            miraDerecha = false;
        }else if(movTeclas > 0){
            this.GetComponent<SpriteRenderer>().flipX = false;
            miraDerecha = true;
        }

        //Animation Walking
         if(movTeclas != 0){
            animatorController.SetBool("activaCamina", true);
        }else{
            animatorController.SetBool("activaCamina", false);
        }
        

        //Salto
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,0.5f);

        Debug.DrawRay(transform.position, Vector2.down, Color.red);

        if(hit){
            puedoSaltar = true;
            Debug.Log(hit.collider.name);    
        }else{
            puedoSaltar = false;
        }

        if(Input.GetKeyDown(KeyCode.Space)&& puedoSaltar){
            rb.AddForce(
                new Vector2(0,multiplicadorSalto),
                ForceMode2D.Impulse
                );
                puedoSaltar = false;
        }
        //comprobar si me he salido de la pantalla
        if(transform.position.y <= -7){
            Respawnear();
        }   

        //0 vidas
        if(GameManager.vidas <= 0)
        {
            GameManager.estoyMuerto = true;
        }
    }


    public void Respawnear( ){

        Debug.Log("vidas: "+GameManager.vidas);
        GameManager.vidas = GameManager.vidas -1;
        Debug.Log("vidas: "+GameManager.vidas);

        transform.position = respawn.transform.position;
    }

}
        
    /*
    void OnCollisionEnter2D() {
        puedoSaltar = true;
        Debug.Log("Collision");
      
    }*/
    

