using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaScript : MonoBehaviour
{

    public Transform player;
    public float detectionRadius = 5.0f;
    public float Speed = 4.0f;
    private Rigidbody2D rb;
    private Vector2 movement;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
         if (distanceToPlayer < detectionRadius ){
            
             Vector2 direction = (player.position - transform.position).normalized;

            movement = new Vector2(direction.x, 0 );
            
        }
        else{
            movement = Vector2.zero;
        }

            rb.MovePosition(rb.position + movement * Speed * Time.deltaTime);

    }
}
