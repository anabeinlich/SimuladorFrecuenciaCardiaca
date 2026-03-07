using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Referencias 3D - Corazón")]
    public Light luzSpotCorazon;

    [Header("Referencias 3D - Pantalla")]
    public MeshRenderer monitorRenderer;
    public Material materialApagado;
    public Material materialEncendido;
    public int indiceMaterialPantalla = 0;

    [Header("Paneles de UI")]
    public GameObject panelSimuladorUI; 
    public GameObject panelDatosUI;     

    void Start()
    {
        if (luzSpotCorazon != null) luzSpotCorazon.enabled = false;
        if (panelSimuladorUI != null) panelSimuladorUI.SetActive(false);
        HoverPantallaExit(); 
    }

    //  BOTÓN CORAZÓN 
    public void HoverCorazonEnter()
    {
        if (luzSpotCorazon != null) luzSpotCorazon.enabled = true;
    }

    public void HoverCorazonExit()
    {
        if (luzSpotCorazon != null) luzSpotCorazon.enabled = false;
    }

    public void ClickCorazon()
    {
        SceneManager.LoadScene("HeartInspection");
    }

    // BOTÓN PANTALLA 
    public void HoverPantallaEnter()
    {
        if (monitorRenderer != null)
        {
            Material[] mats = monitorRenderer.materials;
            mats[indiceMaterialPantalla] = materialEncendido;
            monitorRenderer.materials = mats;
        }
    }

    public void HoverPantallaExit()
    {
        if (monitorRenderer != null)
        {
            Material[] mats = monitorRenderer.materials;
            mats[indiceMaterialPantalla] = materialApagado;
            monitorRenderer.materials = mats;
        }
    }

    public void ClickPantalla()
    {
        if (panelSimuladorUI != null) panelSimuladorUI.SetActive(true);
    }

    // BOTÓN DATOS
    public void ClickDatos()
    {
        if (panelDatosUI != null) panelDatosUI.SetActive(true);
    }

    // CERRAR PANELES / VOLVER
    public void CerrarPanelData()
    {
        if (panelDatosUI != null) panelDatosUI.SetActive(false);
    }
    public void CerrarPanelFC()
    {
        if (panelSimuladorUI != null) panelSimuladorUI.SetActive(false);
    }
}
