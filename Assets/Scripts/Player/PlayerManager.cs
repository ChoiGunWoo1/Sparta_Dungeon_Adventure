using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : Singleton<PlayerManager> // 플레이어의 움직임 및 상태 관리 + 카메라 움직임 관리
{
    [Header("Move")] // 플레이어의 이동과 관련한 변수
    [SerializeField] private float speed;
    private Vector2 curinput;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask groundlayermask;
    [SerializeField] private LayerMask jumpBarlayermask;

    [Header("Look")] // 카메라 움직임과 관련한 변수
    [SerializeField] private Transform cameracontainer;
    [SerializeField] private float minlook;
    [SerializeField] private float maxlook;
    private float camcurxrot;
    [SerializeField] private float looksensitivity;

    [Header("Stamina")] // 플레이어의 상태(스테미나)와 관련된 변수
    [SerializeField] private float jumpStamina;
    [SerializeField] private float passiveStamina;

    private Vector2 mousedelta; // 마우스의 이동정도
    private bool candoublejump = false; // 더블점프를 할수 있는가?(아이템을 먹었는가)

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
        Cursor.lockState = CursorLockMode.Locked; // 커서를 안보이게 함
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void OnLookInput(InputAction.CallbackContext context) // Input System 활용
    {
        mousedelta = context.ReadValue<Vector2>();
    }

    void CameraLook() // 마우스 움직임에 따라 회전
    {
        camcurxrot += mousedelta.y * looksensitivity;
        camcurxrot = Mathf.Clamp(camcurxrot,minlook, maxlook);
        cameracontainer.localEulerAngles = new Vector3(-camcurxrot, 0, 0);
        transform.eulerAngles += new Vector3(0, mousedelta.x * looksensitivity, 0);
    }

    public void OnMoveInput(InputAction.CallbackContext context) // Input System 활용
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

    public void OnJumpInput(InputAction.CallbackContext context) // Input System 활용
    {
        if(context.phase == InputActionPhase.Started && IsGrounded() && UIManager.Instance.TryJump())
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * jump, ForceMode.Impulse); // 점프를 시도했을때 땅에 있어야하고, tryjump가 true여야한다.
            return;
        }
        else if(context.phase == InputActionPhase.Started && IsJumpbar() && UIManager.Instance.TryJump())
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * jump * 4, ForceMode.Impulse); // 땅에 있지 않는다면 점프대에 있는지 확인, 점프대에 있다면 점프력을 2배로 하고 실행
            return;
        }
        if(context.phase == InputActionPhase.Started && candoublejump && UIManager.Instance.TryJump())
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * jump, ForceMode.Impulse); // 그것도 아니라면 -> 공중에 있다는것 -> 더블점프가 가능한지 여부를 체크하고 공중에서 점프를 수행
            candoublejump = false; // 더블점프를 한번만 실행시키기 위해 다시 변수를 false로 설정
        }
    }


    private void Move() // 매 프레임마다 실행될 이동 함수(update)
    {
        Vector3 dir = transform.forward * curinput.y + transform.right * curinput.x;
        dir *= speed;
        dir.y = GetComponent<Rigidbody>().velocity.y;
        GetComponent<Rigidbody>().velocity = dir;
    }


    private bool IsGrounded() // 땅인지 확인하는 함수
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

    private bool IsJumpbar() // 점프대인지 확인하는 함수
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

