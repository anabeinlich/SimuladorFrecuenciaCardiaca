using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;


public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference databaseReference;
    private bool firebaseListo = false;
    private string idSesion;

    [Serializable]
    public class RegistroConFecha
    {
        public string fecha; 
        public int numeroPaciente;
        public string hora;
        public string paciente;
        public int edad;
        public int bpm;
        public string parametros;

        public RegistroConFecha(RegistroPaciente datoLocal)
        {
            this.fecha = DateTime.Now.ToString("dd/MM/yyyy");
            this.numeroPaciente = datoLocal.numeroPaciente;
            this.hora = datoLocal.hora;
            this.paciente = datoLocal.paciente;
            this.edad = datoLocal.edad;
            this.bpm = datoLocal.bpm;
            this.parametros = datoLocal.parametros;
        }
    }

    void Start()
    {
        idSesion = "Sesion_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                databaseReference = FirebaseDatabase.GetInstance("https://simuladorfrecuenciacardiaca-default-rtdb.firebaseio.com/").RootReference;

                firebaseListo = true;
                Debug.Log("<color=#00aaff>Firebase Manager conectado y listo.</color>");
            }
            else
            {
                Debug.LogError("Error conectando Firebase: " + task.Result);
            }
        });
    }

    public void SubirDatosALaNube()
    {
        if (!firebaseListo)
        {
            Debug.LogError("Firebase no está listo.");
            return;
        }

        if (DataManager.Instancia == null || DataManager.Instancia.historial.Count == 0)
        {
            Debug.LogWarning("La tabla local está vacía, no hay qué subir.");
            return;
        }

        Debug.Log($"Subiendo {DataManager.Instancia.historial.Count} registros...");

        foreach (RegistroPaciente reg in DataManager.Instancia.historial)
        {
            RegistroConFecha datoFinal = new RegistroConFecha(reg);

            string json = JsonUtility.ToJson(datoFinal);

            databaseReference.Child("Sesiones").Child(idSesion).Child("Paciente_" + reg.numeroPaciente).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log($"¡Paciente #{reg.numeroPaciente} subido con éxito a la sesión {idSesion}!");
                }
                else
                {
                    Debug.LogError("Falló la subida: " + task.Exception);
                }
            });
        }
    }
}
