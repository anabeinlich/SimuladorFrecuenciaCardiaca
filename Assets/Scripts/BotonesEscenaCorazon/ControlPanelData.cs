using UnityEngine;
using UnityEngine.UI;

public class ControlPanelData : MonoBehaviour
{
    public GameObject panelDatosUI;

    public void ClickDatos()
    {
        if (panelDatosUI != null) panelDatosUI.SetActive(true);
    }

    public void CerrarPanelData()
    {
        if (panelDatosUI != null) panelDatosUI.SetActive(false);
    }
}
