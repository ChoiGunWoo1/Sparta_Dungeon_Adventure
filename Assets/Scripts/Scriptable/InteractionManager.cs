using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : Singleton<InteractionManager> // 플레이어의 상호작용을 관리하는 클래스
{
    private float checkRate = 0.05f;
    private float lastCheckTime = 0;
    [SerializeField] private float MaxCheckDistance;
    [SerializeField] private LayerMask itemlayer;
    [SerializeField] private TextMeshProUGUI promptTxt;

    private GameObject curInteractobject;
    private ItemInteract curitemdata;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }
    void Update() // 화면에 ray를 쏴서 아이템을 체크
    {
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, MaxCheckDistance, itemlayer))
            {
                if (hit.collider.gameObject != curInteractobject)
                {
                    curInteractobject = hit.collider.gameObject;
                    curitemdata = curInteractobject.GetComponent<ItemInteract>();
                    SetPrompt();
                }
            }
            else
            {
                curInteractobject = null;
                curitemdata = null;
                promptTxt.gameObject.SetActive(false);
            }
        }
    }

    public void SetPrompt() // 아이템 설명을 화면에 띄움
    {
        promptTxt.gameObject.SetActive(true);
        promptTxt.text = curitemdata.GetDescription();
    }

    public void OnInteractionInput(InputAction.CallbackContext context) // Input System 활용
    {
        if(context.phase == InputActionPhase.Started && curitemdata != null)
        {
            curitemdata.DoInteract(); // item에 해당되는 상호작용 실행
            curInteractobject = null;
            curitemdata = null;
            promptTxt.gameObject.SetActive(false);

        }
    }


    public IEnumerator CanDoubleJump() // 더블점프 코루틴(당근)
    {
        PlayerManager.Instance.CanDoubleJump = true;
        UIManager.Instance.Onitemicon();
        yield return new WaitForSeconds(5f);
        PlayerManager.Instance.CanDoubleJump = false;
        UIManager.Instance.Offitemicon();
        yield break;
    }

    public IEnumerator JumpPowerTwice() // 점프력 두배 코루틴(버섯)
    {
        PlayerManager.Instance.Jump *= 2;
        UIManager.Instance.Onitemicon();
        yield return new WaitForSeconds(5f);
        PlayerManager.Instance.Jump /= 2;
        UIManager.Instance.Offitemicon();
        yield break;
    }
}
