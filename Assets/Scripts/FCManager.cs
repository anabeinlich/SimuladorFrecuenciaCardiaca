using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class FCManager : MonoBehaviour
{
    [Header("UI - Textos Principales")]
    public TextMeshProUGUI fcText;
    public Image iconoCorazon;

    [Header("UI - Alertas")]
    public TextMeshProUGUI iconoAlertaOxigeno; 
    public TextMeshProUGUI textoParoCardiaco;  
    public RectTransform rectTransformECG;    

    [Header("UI - Textos de Valores (0 a 100)")]
    public TextMeshProUGUI textoValorActividad;
    public TextMeshProUGUI textoValorEstres;
    public TextMeshProUGUI textoValorOxigeno;

    [Header("UI - Controles")]
    public Slider sliderActividad;
    public Slider sliderEstres;
    public Slider sliderOxigeno;
    public TMP_Dropdown dropdownPaciente;
    public TMP_InputField inputEdad;
    public Toggle toggleMedica;

    [Header("Variables Internas")]
    public float fcFinal;

    void Start()
    {
        if (iconoAlertaOxigeno != null) iconoAlertaOxigeno.gameObject.SetActive(false);
        if (textoParoCardiaco != null) textoParoCardiaco.gameObject.SetActive(false);
        CalcularFC();
    }

    public void CalcularFC()
    {
        // ACTUALIZAR LOS TEXTOS DE LOS SLIDERS 
        if (textoValorActividad != null) textoValorActividad.text = sliderActividad.value.ToString("0");
        if (textoValorEstres != null) textoValorEstres.text = sliderEstres.value.ToString("0");
        if (textoValorOxigeno != null) textoValorOxigeno.text = sliderOxigeno.value.ToString("0");


        // FC Base según tipo de paciente
        float fcBase = dropdownPaciente.value switch
        {
            0 => 70f,  // Normal
            1 => 50f,  // Bradicárdico
            2 => 110f, // Taquicárdico
            _ => 70f
        };

        // Factores A, E, O 
        float A = (sliderActividad.value / 100f) * 25f;
        float E = (sliderEstres.value / 100f) * 20f;
        float O = (1f - (sliderOxigeno.value / 100f)) * 15f;

        // Factor de Edad (C)
        int edad = int.TryParse(inputEdad.text, out int res) ? res : 25;
        float factorEdad = edad < 30 ? 1.0f : (edad < 60 ? 0.8f : 0.6f);
        float C = (A + E + O) * (factorEdad - 1f);

        // Medicación (M)
        float M = toggleMedica.isOn ? -12f : 0f;

        // Cálculo Final y limites
        fcFinal = fcBase + A + E + O + C + M;
        fcFinal = Mathf.Clamp(fcFinal, 40f, 180f);

        // Hipoxia
        float oxigenoActual = sliderOxigeno.value;

        if (oxigenoActual <= 60f)
        {
            // PARO CARDÍACO (MENOS DE 60%) 
            fcFinal = 0f;
            fcText.text = "0 BPM";

            // "!" en rojo
            if (iconoAlertaOxigeno != null)
            {
                iconoAlertaOxigeno.gameObject.SetActive(true);
                iconoAlertaOxigeno.color = Color.red;
            }
            // Texto de Paro
            if (textoParoCardiaco != null) textoParoCardiaco.gameObject.SetActive(true);

            // Aplanar la onda 
            if (rectTransformECG != null) rectTransformECG.localScale = new Vector3(1f, 0.1f, 1f);
            if (iconoCorazon != null) iconoCorazon.color = Color.gray; 
        }
        else if (oxigenoActual <= 85f)
        {
            // ALERTA CRÍTICA (61% a 85%) 
            fcText.text = $"{Mathf.RoundToInt(fcFinal)} BPM";

            // "!" en amarillo
            if (iconoAlertaOxigeno != null)
            {
                iconoAlertaOxigeno.gameObject.SetActive(true);
                iconoAlertaOxigeno.color = Color.yellow;
            }
            // Ocultar Texto de Paro
            if (textoParoCardiaco != null) textoParoCardiaco.gameObject.SetActive(false);

            // Restaurar onda y corazón
            if (rectTransformECG != null) rectTransformECG.localScale = new Vector3(1f, 1f, 1f);
            if (iconoCorazon != null) iconoCorazon.color = Color.red;
        }
        else
        {
            // === ESTADO NORMAL (Más de 85%) ===
            fcText.text = $"{Mathf.RoundToInt(fcFinal)} BPM";

            // Ocultar alertas
            if (iconoAlertaOxigeno != null) iconoAlertaOxigeno.gameObject.SetActive(false);
            if (textoParoCardiaco != null) textoParoCardiaco.gameObject.SetActive(false);

            // Restaurar onda y corazón
            if (rectTransformECG != null) rectTransformECG.localScale = new Vector3(1f, 1f, 1f);
            if (iconoCorazon != null) iconoCorazon.color = Color.red;
        }

        // ACTUALIZAR EL MONITOR 
        if (fcText != null) fcText.text = $"{Mathf.RoundToInt(fcFinal)} BPM";
    }

    public void BotonGuardarDatos()
    {
        RegistroPaciente nuevo = new RegistroPaciente();

        nuevo.numeroPaciente = DataManager.Instancia.historial.Count + 1;
        nuevo.hora = System.DateTime.Now.ToString("HH:mm:ss");
        nuevo.paciente = dropdownPaciente.options[dropdownPaciente.value].text;

        int.TryParse(inputEdad.text, out int edadGuardada);
        nuevo.edad = edadGuardada == 0 ? 25 : edadGuardada;
        nuevo.bpm = Mathf.RoundToInt(fcFinal);

        string estadoMedicion = toggleMedica.isOn ? "Sí" : "No";

        nuevo.parametros = $"Act: {sliderActividad.value}% | Est: {sliderEstres.value}% | O2: {sliderOxigeno.value}% | Med: {estadoMedicion}";

        DataManager.Instancia.GuardarRegistro(nuevo);
    }
}
