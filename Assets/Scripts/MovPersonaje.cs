using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
//public float velocity = 1f;
    public float multiplicador = 15f;

    private float multiplicadorSalto = 8f;

    private bool puedoSaltar = true;

    private bool activaSaltoFixed = false;

    float movTeclas;

    public bool miraDerecha = true;

    private Rigidbody2D rb;

    private Animator animatorController;

    GameObject respawn;

    bool soyAzul;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>( );

        animatorController = this.GetComponent<Animator>();

        //Respawn
        respawn = GameObject.Find("Respawn");
        transform.position = respawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.estoyMuerto) return;

        float miDeltaTime = Time.deltaTime;

        //movimiento personaje
          movTeclas = Input.GetAxis("Horizontal"); //(a -1f - d 1f)
        //float movTeclasY = Input.GetAxis("Vertical"); //(a -1f - d 1f)
        


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
            //Debug.Log(hit.collider.name);    
        }else{
            puedoSaltar = false;
        }

        if(Input.GetKeyDown(KeyCode.Space)&& puedoSaltar){
            activaSaltoFixed = true;
        }
        
        //PuedoSaltarFixed
        /*{
            rb.AddForce(
                new Vector2(0,multiplicadorSalto),
                ForceMode2D.Impulse
                );
                puedoSaltar = false;
        }*/


        //comprobar si me he salido de la pantalla
        if(transform.position.y <= -7){
            AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.fxDead);
            Respawnear();
        }   

        //0 vidas
        if(GameManager.vidas <= 0)
        {
            GameManager.estoyMuerto = true;
        }
    }

     void FixedUpdate() {
                rb.velocity = new Vector2(movTeclas*multiplicador, rb.velocity.y);

                if(activaSaltoFixed == true){
                    rb.AddForce(
                    new Vector2(0,multiplicadorSalto),
                    ForceMode2D.Impulse
                    );
                    activaSaltoFixed = false;
                }

    }


    public void Respawnear( ){

        Debug.Log("vidas: "+GameManager.vidas);
        GameManager.vidas = GameManager.vidas -1;
        Debug.Log("vidas: "+GameManager.vidas);

        transform.position = respawn.transform.position;
    }

    public void CambiarColor(){

        if(soyAzul){
        this.GetComponent<SpriteRenderer>( ).color = Color.white;
        }else{
            this.GetComponent<SpriteRenderer>( ).color = Color.white;

        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log(col.gameObject.name);

        if(col.gameObject.name == "Tunel"){
            //disparo tunnel
            AudioManager.Instance.IniciarEfectoTunel();
        }

        if(col.gameObject.name == "Burbuja"){
            //disparo tunnel
            AudioManager.Instance.IniciarEfectoBurbuja();
        }

        
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Tunel"){
            AudioManager.Instance.IniciarEfectoDefault();
        }

        if(col.gameObject.name == "Burbuja"){
            AudioManager.Instance.IniciarEfectoDefault();
        }
    }

}
        
    /*
    void OnCollisionEnter2D() {
        puedoSaltar = true;
        Debug.Log("Collision");
      
    }*/
    

