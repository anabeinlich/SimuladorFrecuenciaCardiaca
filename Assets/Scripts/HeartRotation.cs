using UnityEngine;

public class HeartRotation : MonoBehaviour
{
    public float velocidadRotacion = 5f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * velocidadRotacion;
            float rotY = Input.GetAxis("Mouse Y") * velocidadRotacion;

            transform.Rotate(Vector3.up, -rotX, Space.World);
            transform.Rotate(Vector3.right, rotY, Space.World);
        }
    }
}

