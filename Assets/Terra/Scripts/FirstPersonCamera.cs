using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public Player player;
    public float xSen;
    public float ySen;

    private float mouseInputX;
    private float mouseInputY;

    private float xRotation;
    private float yRotation;
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void LookAround(InputAction.CallbackContext _context)
    {
        if(_context.performed)
        {
            mouseInputX = _context.ReadValue<Vector2>().x / 25;
            mouseInputY = _context.ReadValue<Vector2>().y / 25;
        }
    }
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 2, player.transform.position.z);
        float mouseX = mouseInputX * Time.deltaTime * xSen;
        float mouseY = mouseInputY * Time.deltaTime * ySen;
        mouseInputX = 0;
        mouseInputY = 0;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        player.gameObject.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
