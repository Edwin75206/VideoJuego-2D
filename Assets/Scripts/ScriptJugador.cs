using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Importar la librería para gestionar escenas


public class ScriptJugador : MonoBehaviour
{
    

    public int vidas = 3;
    public Rigidbody2D cuerpoJugador;
    public bool MirandoDerecha = true;
    public bool enSuelo = false;
    public bool Perdiste = false;
    public Animator miAnimacion;
    public float velocidad = 7;
    public float fuerzaSalto = 6;
    public bool atacando;
    public bool recibiendoDanio ;
    public bool muerto;

    public AudioSource sonidoAtacar;
    public AudioSource sonidoSaltar;

    public float fuerzaRebote = 10f;
    public Canvas canvas;

    public HeartConatiner heartContainer; // Referencia al HeartContainer

    // Start is called before the first frame update
    void Start()
    {
       cuerpoJugador = GetComponent<Rigidbody2D>();
    
                canvas.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!muerto)
        {
            if (Input.GetKey("z") && !atacando && enSuelo){
                Atacando();
                sonidoAtacar.Play();
            } 
            float horizontal = Input.GetAxis("Horizontal");
            Voltear(horizontal);
            
        miAnimacion.SetFloat("Velocidad", Mathf.Abs(horizontal));

            if(!atacando){
                 Mover();
            }
        }


        

        
        miAnimacion.SetBool("Suelo",enSuelo);
        miAnimacion.SetBool("Perdiste", Perdiste);
        miAnimacion.SetBool("Atacando", atacando);
        miAnimacion.SetBool("recibeDanio", recibiendoDanio);
        miAnimacion.SetBool("muerto", muerto);
    }

    private void Voltear(float horizontal){
        if(horizontal > 0 && !MirandoDerecha || horizontal < 0 && MirandoDerecha){
            MirandoDerecha = !MirandoDerecha;
            Vector3 LaEscala = transform.localScale;
            LaEscala.x *= -1;
            transform.localScale = LaEscala;
        }
    }

    public void Mover(){
        if(Input.GetKey("d") || Input.GetKey("right")){
            cuerpoJugador.velocity = new Vector2(velocidad, cuerpoJugador.velocity.y);
        } 
        else if (Input.GetKey("a") || Input.GetKey("left")){
            cuerpoJugador.velocity = new Vector2(-velocidad, cuerpoJugador.velocity.y);
        }

        if(Input.GetKey("space") && enSuelo && !recibiendoDanio){
            cuerpoJugador.velocity = new Vector2(cuerpoJugador.velocity.x, fuerzaSalto);
            sonidoSaltar.Play();
        }

        
    }

    

    public void Atacando()
    {
        atacando = true;
        
    }

    public void DesactivaAtaque(){
        atacando = false; 
    }


    public void RecibeDanio(Vector2 direccion , int cantDanio){
        if(!recibiendoDanio)
        {
            recibiendoDanio = true;
            vidas -= cantDanio; 

            heartContainer.ActualizarVidas(vidas); // Actualiza los corazones


            if (vidas<=0)
            {
                muerto = true; 
                canvas.gameObject.SetActive(true);



            } 
            else if(!muerto)
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                cuerpoJugador.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
        }

    }
    public void DesactivaDanio(){
        recibiendoDanio = false;
        cuerpoJugador.velocity = Vector2.zero;
    }

    
    
     void OnTriggerEnter2D(Collider2D c1){
        if(c1.tag == "Nubes"){
            enSuelo = true;
        } 
        if(c1.tag == "Agua"){   //ESTE ES PARA MATAR AL JUGADOR
            vidas -- ;
            Debug.Log("Vidas: " + vidas );
            Perdiste = true;
            cuerpoJugador.constraints = RigidbodyConstraints2D.FreezeAll; 
        }
        if(c1.tag == "Perro"){
            Destroy(c1.gameObject);
        }
        
        if(c1.tag == "Perdiste"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena
        }
    }

     void OnTriggerExit2D(Collider2D c2){
        if(c2.tag == "Nubes"){
            enSuelo = false;
        }
    }


    // ➕ Método para añadir vidas 
    public void AñadirVida()
    {
        vidas++; // Aumentar la cantidad de vidas
        heartContainer.ActualizarVidas(vidas); // Actualizar los corazones
        Debug.Log("Vida añadida. Vidas actuales: " + vidas);
    }
}
