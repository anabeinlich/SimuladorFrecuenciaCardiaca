using UnityEngine;
using UnityEngine.UI;

public class HeartIconAnimation : MonoBehaviour
{
    [Header("Referencias")]
    public FCManager managerSimulador; 
    public Image iconoCorazon; 

    [Header("Configuración del Latido")]
    public float escalaBase = 1f;        
    public float escalaMaxLatido = 1.15f; 
    public float duracionLatido = 0.2f;   

    private float timerIntervalo = 0f;
    private float timerAnimacion = 0f;
    private bool estaLatiendo = false;
    private Vector2 tamanoBaseOriginal;

    void Start()
    {
        if (iconoCorazon != null)
        {
            tamanoBaseOriginal = iconoCorazon.rectTransform.localScale;
        }
    }

    void Update()
    {
        if (managerSimulador == null || iconoCorazon == null) return;

        float intervalo = 60f / managerSimulador.fcFinal;

        timerIntervalo += Time.deltaTime;

        if (timerIntervalo >= intervalo)
        {
            timerIntervalo = 0f;
            estaLatiendo = true;
            timerAnimacion = 0f;
        }

        if (estaLatiendo)
        {
            timerAnimacion += Time.deltaTime;

            float porcentaje = timerAnimacion / duracionLatido;

            if (porcentaje < 0.5f)
            {
                float t = porcentaje / 0.5f; 
                float escalaActual = Mathf.Lerp(escalaBase, escalaMaxLatido, t);
                iconoCorazon.rectTransform.localScale = tamanoBaseOriginal * escalaActual;
            }
            else if (porcentaje < 1.0f)
            {
                float t = (porcentaje - 0.5f) / 0.5f; 
                float escalaActual = Mathf.Lerp(escalaMaxLatido, escalaBase, t);
                iconoCorazon.rectTransform.localScale = tamanoBaseOriginal * escalaActual;
            }
            else
            {
                iconoCorazon.rectTransform.localScale = tamanoBaseOriginal * escalaBase;
                estaLatiendo = false;
            }
        }
    }
}
