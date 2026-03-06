using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RegistroPaciente
{
    public string hora;
    public string paciente;
    public int bpm;
    public string parametros; 
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instancia;

    public List<RegistroPaciente> historial = new List<RegistroPaciente>();

    void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GuardarRegistro(RegistroPaciente nuevoRegistro)
    {
        historial.Add(nuevoRegistro);
        Debug.Log("¡Dato guardado! Total en memoria: " + historial.Count);
    }
}
