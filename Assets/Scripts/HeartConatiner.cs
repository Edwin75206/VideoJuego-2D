using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartConatiner : MonoBehaviour
{public GameObject heartPrefab; // Prefab del corazón (imagen de corazón)
    public Transform heartContainer; // Contenedor de los corazones
    private List<GameObject> heartList = new List<GameObject>(); // Lista de corazones
    public int totalVidas = 3; // Total de vidas al inicio

    void Start()
    {
        CrearCorazones(totalVidas); // Crear los corazones iniciales
    }

    public void CrearCorazones(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            GameObject nuevoCorazon = Instantiate(heartPrefab, heartContainer);
            heartList.Add(nuevoCorazon);
        }
    }

    public void PerderCorazon()
    {
        if (heartList.Count > 0)
        {
            GameObject corazonAEliminar = heartList[heartList.Count - 1];
            heartList.RemoveAt(heartList.Count - 1);
            Destroy(corazonAEliminar);
        }
    }

    public void ReiniciarVidas(int cantidad)
    {
        foreach (GameObject corazon in heartList)
        {
            Destroy(corazon);
        }
        heartList.Clear();
        CrearCorazones(cantidad);
    }

    public void ActualizarVidas(int vidasRestantes)
    {
        // 🔴 Eliminar corazones si las vidas han disminuido
        while (heartList.Count > vidasRestantes)
        {
            GameObject corazonAEliminar = heartList[heartList.Count - 1];
            heartList.RemoveAt(heartList.Count - 1);
            Destroy(corazonAEliminar);
        }

        // 🟢 Añadir corazones si las vidas han aumentado
        while (heartList.Count < vidasRestantes)
        {
            GameObject nuevoCorazon = Instantiate(heartPrefab, heartContainer);
            heartList.Add(nuevoCorazon);
        }
    }
}
