    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptVida : MonoBehaviour
{
    // Referencia al script del jugador
    private ScriptJugador scriptJugador;

    void Start()
    {
        // Encuentra al objeto que tiene el componente ScriptJugador
        scriptJugador = GameObject.FindWithTag("Jugador").GetComponent<ScriptJugador>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador")) 
        {
            scriptJugador.AñadirVida(); // Llama a la nueva función para añadir una vida
            Debug.Log("Se ha otorgado una vida. Vidas actuales: " + scriptJugador.vidas);
            Destroy(gameObject); // Destruye el objeto de vida
        }
    }   
}
