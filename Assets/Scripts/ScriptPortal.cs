using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class ScriptPortal : MonoBehaviour
{
    public AudioSource sonidoTeletransportar;

    [SerializeField] private string nombreEscena = "nivel2"; // Nombre de la escena a la que quieres cambiar

    // Detecta si el jugador entra en el portal
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador")) // Verifica si el objeto que colisionó tiene la etiqueta "Player"
        {
            ChangeScene();
            Debug.Log("Si pega");
            sonidoTeletransportar.Play();
        }
    }

    // Cambiar de escena
    private void ChangeScene()
    {
        SceneManager.LoadScene(nombreEscena);
    }
 }
