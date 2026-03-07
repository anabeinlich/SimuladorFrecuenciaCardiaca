using UnityEngine;
using UnityEngine.UI;

public class ECGVisual : MonoBehaviour
{
    [Header("Referencias")]
    public FCManager managerSimulador;
    public RawImage imagenECG;

    [Header("Configuración visual")]
    public float velocidadBase = 0.5f; // cuando hay 60 BPM

    void Update()
    {
        if (managerSimulador == null || imagenECG == null) return;

        float multiplicadorVelocidad = managerSimulador.fcFinal / 60f;

        float desplazamientoX = Time.time * (-velocidadBase) * multiplicadorVelocidad;

        imagenECG.uvRect = new Rect(desplazamientoX, 0, 1, 1);
    }
}
