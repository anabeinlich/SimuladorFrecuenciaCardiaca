using UnityEngine;
using TMPro;

public class FilaTabla : MonoBehaviour
{
    public TextMeshProUGUI txtHora;
    public TextMeshProUGUI txtPaciente;
    public TextMeshProUGUI txtBPM;
    public TextMeshProUGUI txtParametros;

    public void LlenarDatos(RegistroPaciente datos)
    {
        txtHora.text = datos.hora;
        txtPaciente.text = datos.paciente;
        txtBPM.text = datos.bpm.ToString() + " BPM";
        txtParametros.text = datos.parametros;
    }
}
