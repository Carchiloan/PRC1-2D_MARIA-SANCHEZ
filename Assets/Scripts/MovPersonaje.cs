using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    public float multiplicador = 5f;

    public float multiplicadorSalto = 5f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>( );
    }

    // Update is called once per frame
    void Update()
    {
        float movTeclas = Input.GetAxis("Horizontal"); //(a -1f - d 1f)

        float miDeltaTime = Time.deltaTime;

        Debug.Log(Time.deltaTime);

        transform.Translate(
            movTeclas*(Time.deltaTime*multiplicador),
            0,
            0
        );

        //Salto
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(
                new Vector2(0,multiplicadorSalto),
                ForceMode2D.Impulse
                );
        }

    }
}
