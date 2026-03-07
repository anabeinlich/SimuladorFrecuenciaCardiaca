using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscena : MonoBehaviour
{
    public void VolverAlHospital()
    {
        SceneManager.LoadScene("MainScene");
    }
}
