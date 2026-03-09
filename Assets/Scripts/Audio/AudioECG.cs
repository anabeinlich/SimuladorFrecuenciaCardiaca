using UnityEngine;

public class AudioECG : MonoBehaviour
{
    public AudioSource audioBeep;
    public AudioSource audioParoCardiaco;

    private int bpmActual = 60;
    private float timer = 0f;
    private bool enParo = false;

    void Update()
    {
        if (bpmActual <= 0)
        {
            if (!enParo) ActivarAlarmaParo();
            if (audioParoCardiaco.time >= 3f)
            {
                audioParoCardiaco.time = 0f;
            }
            return;
        }
        else
        {
            if (enParo) DesactivarAlarmaParo();
        }

        float intervalo = 60f / bpmActual;
        timer += Time.deltaTime;

        if (timer >= intervalo)
        {
            if (audioBeep != null) audioBeep.Play();
            timer = 0f;
        }
    }

    public void RecibirBPMCalculado(int nuevoBPM)
    {
        bpmActual = nuevoBPM;

        PlayerPrefs.SetInt("BPM_Elegido", bpmActual);
        PlayerPrefs.Save();
    }

    void ActivarAlarmaParo()
    {
        enParo = true;
        if (audioBeep != null) audioBeep.Stop();
        if (audioParoCardiaco != null && !audioParoCardiaco.isPlaying)
        {
            audioParoCardiaco.loop = true;
            audioParoCardiaco.Play();
        }
    }

    void DesactivarAlarmaParo()
    {
        enParo = false;
        if (audioParoCardiaco != null) audioParoCardiaco.Stop();
    }
}
