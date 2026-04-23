using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public Player player;
    public float xSensitivity;
    public float ySensitivity;

    private float xRotation;
    private float yRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
   
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        float mouseX = Mouse.current.position.ReadValue().x * xSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.position.ReadValue().y * ySensitivity * Time.deltaTime;

        yRotation += mouseX;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        player.gameObject.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        transform.rotation = Quaternion.Euler(-xRotation, yRotation, 0f);
    }
}
