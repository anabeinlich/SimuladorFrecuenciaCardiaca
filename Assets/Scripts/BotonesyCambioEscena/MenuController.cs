using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController: MonoBehaviour
{
    public void BotonEmpezar()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void BotonSalir()
    {
        Debug.Log("Saliste del simulador");
        Application.Quit();
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
