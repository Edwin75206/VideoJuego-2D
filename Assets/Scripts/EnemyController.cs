using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform player;
    public float detectionRadius = 5.0f;
    public float Speed = 4.0f;
    public int vida = 3;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;
    private bool recibiendoDanio;
    public float fuerzaRebote = 5f;
    private bool playerVivo;
    private Animator animator;
    public AudioSource sonidoMorir;


    public bool atacando;
    private bool muerto;
    public float tamX = -6;
    public float tamY = 6;
    public float tamz = 6;
    


    // Start is called before the first frame update
    void Start()
    {
        playerVivo = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerVivo && !muerto)
        {
            Movimiento();
            Atacando();
            
            
            if(!atacando){
                 Movimiento();
            }
        }
        animator.SetBool("EnMovimiento", enMovimiento);
        animator.SetBool("muerto" , muerto);
        animator.SetBool("Atacando", atacando);
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Jugador"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x , 0);
            ScriptJugador playerScript = collision.gameObject.GetComponent<ScriptJugador>(); 

            playerScript.RecibeDanio(direccionDanio, 1);
            playerVivo = !playerScript.muerto;
            //collision.gameObject.GetComponent<ScriptJugador>().RecibeDanio(direccionDanio, 1);

            if (!playerVivo){
                enMovimiento = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Espada"))
        {
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x , 0);
            RecibeDanio(direccionDanio, 1);
        }

        
    }

    
    public void Atacando()
    {
        atacando = true;
     
        
    }

    public void DesactivaAtaque(){
        atacando = false; 
    }


    private void Movimiento(){

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius ){
            
             Vector2 direction = (player.position - transform.position).normalized;
            
            if(direction.x < 0){
                transform.localScale = new Vector3(tamX, tamY,tamz);
            }
            if(direction.x > 0){
                transform.localScale = new Vector3(tamY, tamY ,tamz);
            }
            movement = new Vector2(direction.x, 0 );

            enMovimiento = true;

        }
        else{
            movement = Vector2.zero;
            enMovimiento = false;
        }

        if (!recibiendoDanio)
        {
            rb.MovePosition(rb.position + movement * Speed * Time.deltaTime);
        }

    }
    
    public void RecibeDanio(Vector2 direccion , int cantDanio){
        if(!recibiendoDanio)
        {
            vida -= cantDanio;
            recibiendoDanio = true;
            if (vida <= 0)
            {
                muerto = true;
                enMovimiento = false ;
            }
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
                StartCoroutine(DesactivaDanio());    
            }
        }

    }
    public void ReproducirMuerte(){
        
        sonidoMorir.Play();
        Debug.Log("Esta Reproduciendo");
    }
    
    IEnumerator DesactivaDanio(){       //CORRUTINA CREADA PARA QUE SE PARE EL RECIBIENDO DANIO
        yield return new WaitForSeconds(0.4f);
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }

    public void DeleteBody(){
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
