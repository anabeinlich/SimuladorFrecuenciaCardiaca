using UnityEngine;

public class AnimacionLatido3D : MonoBehaviour
{
    [Header("Configuración Visual")]
    public float escalaBase = 1f;        
    public float escalaMaxLatido = 1.15f;
    public float duracionLatido = 0.2f;

    private int bpmActual = 70;
    private float timerIntervalo = 0f;
    private float timerAnimacion = 0f;
    private bool estaLatiendo = false;
    private Vector3 tamanoOriginal;

    void Start()
    {
        tamanoOriginal = transform.localScale;

        if (DataManager.Instancia != null && DataManager.Instancia.historial.Count > 0)
        {
            int ultimoIndice = DataManager.Instancia.historial.Count - 1;
            bpmActual = DataManager.Instancia.historial[ultimoIndice].bpm;

            Debug.Log($"El corazón 3D late a {bpmActual} BPM, del paciente #{DataManager.Instancia.historial[ultimoIndice].numeroPaciente}");
        }
    }

    void Update()
    {
        if (bpmActual <= 0) return;

        float intervalo = 60f / bpmActual;
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
                transform.localScale = tamanoOriginal * escalaActual;
            }
            else if (porcentaje < 1.0f)
            {
                float t = (porcentaje - 0.5f) / 0.5f;
                float escalaActual = Mathf.Lerp(escalaMaxLatido, escalaBase, t);
                transform.localScale = tamanoOriginal * escalaActual;
            }
            else
            {
                transform.localScale = tamanoOriginal * escalaBase;
                estaLatiendo = false;
            }
        }
    }
}
