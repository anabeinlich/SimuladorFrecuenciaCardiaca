using UnityEngine;
using TMPro;

public class FilaTabla : MonoBehaviour
{
    public TextMeshProUGUI txtNumero;
    public TextMeshProUGUI txtHora;
    public TextMeshProUGUI txtPaciente;
    public TextMeshProUGUI txtEdad;
    public TextMeshProUGUI txtBPM;
    public TextMeshProUGUI txtParametros;

    public void LlenarDatos(RegistroPaciente datos)
    {
        txtNumero.text = "#" + datos.numeroPaciente.ToString();
        txtHora.text = datos.hora;
        txtPaciente.text = datos.paciente;
        txtEdad.text = datos.edad.ToString();
        txtBPM.text = datos.bpm.ToString() + " BPM";
        txtParametros.text = datos.parametros;
    }
}
