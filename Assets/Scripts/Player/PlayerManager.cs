using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Move")]
    [SerializeField] private float speed;
    private Vector2 curinput;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask groundlayermask;
    [SerializeField] private LayerMask jumpBarlayermask;

    [Header("Look")]
    [SerializeField] private Transform cameracontainer;
    [SerializeField] private float minlook;
    [SerializeField] private float maxlook;
    private float camcurxrot;
    [SerializeField] private float looksensitivity;

    [Header("Stamina")]
    [SerializeField] private float jumpStamina;
    [SerializeField] private float passiveStamina;

    private Vector2 mousedelta;
    private bool candoublejump = false;

    public float PassiveStamina
    {
        get { return passiveStamina; }
    }

    public float JumpStamina
    {
        get { return jumpStamina; }
    }

    public bool CanDoubleJump
    {
        get { return candoublejump; }
        set { candoublejump = value; }
    }

    public float Jump
    {
        get { return jump; }
        set { jump = value; }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mousedelta = context.ReadValue<Vector2>();
    }

    void CameraLook()
    {
        camcurxrot += mousedelta.y * looksensitivity;
        camcurxrot = Mathf.Clamp(camcurxrot,minlook, maxlook);
        cameracontainer.localEulerAngles = new Vector3(-camcurxrot, 0, 0);
        transform.eulerAngles += new Vector3(0, mousedelta.x * looksensitivity, 0);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curinput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curinput = Vector2.zero; 
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded() && UIManager.Instance.TryJump())
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * jump, ForceMode.Impulse);
            return;
        }
        else if(context.phase == InputActionPhase.Started && IsJumpbar() && UIManager.Instance.TryJump())
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * jump * 4, ForceMode.Impulse);
            return;
        }
        if(context.phase == InputActionPhase.Started && candoublejump && UIManager.Instance.TryJump())
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * jump, ForceMode.Impulse);
            candoublejump = false;
        }
    }


    private void Move()
    {
        Vector3 dir = transform.forward * curinput.y + transform.right * curinput.x;
        dir *= speed;
        dir.y = GetComponent<Rigidbody>().velocity.y;
        GetComponent<Rigidbody>().velocity = dir;
    }


    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]{
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundlayermask))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsJumpbar()
    {
        Ray[] rays = new Ray[4]{
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, jumpBarlayermask))
            {
                return true;
            }
        }
        return false;
    }
}

