using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class FCManager : MonoBehaviour
{
    [Header("UI - Textos Principales")]
    public TextMeshProUGUI fcText; 

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

        // Cálculo Final y Clamp (Límite fisiológico)
        fcFinal = fcBase + A + E + O + C + M;
        fcFinal = Mathf.Clamp(fcFinal, 40f, 180f);


        // ACTUALIZAR EL MONITOR 
        if (fcText != null) fcText.text = $"{Mathf.RoundToInt(fcFinal)} BPM";
    }
}
