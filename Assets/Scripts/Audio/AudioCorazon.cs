using UnityEngine;

public class AudioCorazon : MonoBehaviour
{
    public AudioSource audioLatido;

    public int bpmActual;
    private float timer = 0f;

    void Start()
    {
        bpmActual = PlayerPrefs.GetInt("BPM_Elegido", 60);
    }

    void Update()
    {
        if (bpmActual <= 0) return;

        float intervalo = 60f / bpmActual;
        timer += Time.deltaTime;

        if (timer >= intervalo)
        {
            if (audioLatido != null) audioLatido.Play();
            timer = 0f;
        }
    }
}
