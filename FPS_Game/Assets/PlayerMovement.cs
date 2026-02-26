using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Vector2 move;

    public float gravMult;
    private bool jumping;
    public float grav;
    private float vertVelocity;

    public float mouseSens;
    public float maxPitch;
    public float zoom;
    public float zoomSpeed;

    private float camPitch;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

        jumping = Input.GetButtonDown("Jump");

        Movement();
        Grav();
        CamMovement();
    }

    void Movement()
    {
        Vector3 dir = (transform.forward * move.y + transform.right * move.x).normalized * speed * Time.fixedDeltaTime;

        dir += Vector3.up * vertVelocity * Time.fixedDeltaTime;

        controller.Move(dir);
    }

    void Grav()
    {
        if (controller.isGrounded)
        {
            if (vertVelocity < 0)
            {
                vertVelocity = -2;
            }

            if (jumping)
            {
                vertVelocity = Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y);
            }
        } else
        {
            if (vertVelocity < grav)
            {
                float gravityMult = 1;
                if (controller.velocity.y < -1)
                {
                    gravityMult = gravMult;
                }
                vertVelocity += Physics.gravity.y * gravityMult * Time.fixedDeltaTime;
            }
        }
    }

    void CamMovement()
    {
        float yaw = Input.GetAxis("Mouse X") * mouseSens;
        transform.Rotate(0, yaw, 0);

        camPitch += -Input.GetAxis("Mouse Y") * mouseSens;
        camPitch = Mathf.Clamp(camPitch, -maxPitch, maxPitch);
        Camera.main.transform.localRotation = Quaternion.Euler(camPitch, 0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeFOV(60, zoom);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ChangeFOV(zoom, 60);
        }
    }

    void ChangeFOV(float from, float to)
    {
        Camera.main.fieldOfView = Mathf.Lerp(from, to, zoomSpeed);
    }
}
