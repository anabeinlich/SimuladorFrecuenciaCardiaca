using UnityEngine;

public class GestorTablaUI : MonoBehaviour
{
    public GameObject prefabFila; 
    public Transform contenedorFilas;

    void OnEnable()
    {
        ActualizarTabla();
    }

    public void ActualizarTabla()
    {
        foreach (Transform hijo in contenedorFilas)
        {
            Destroy(hijo.gameObject);
        }

        foreach (RegistroPaciente reg in DataManager.Instancia.historial)
        {
            GameObject nuevaFila = Instantiate(prefabFila, contenedorFilas);

            nuevaFila.GetComponent<FilaTabla>().LlenarDatos(reg);
        }
    }
}
