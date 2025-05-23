using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
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
    void Update()
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

    public void SetPrompt()
    {
        promptTxt.gameObject.SetActive(true);
        promptTxt.text = curitemdata.GetDescription();
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curitemdata != null)
        {
            Destroy(curInteractobject);
            DoInteract();
            curInteractobject = null;
            curitemdata = null;
            promptTxt.gameObject.SetActive(false);

        }
    }

    void DoInteract()
    {
        if(curitemdata.Itemdata.GetItemtype() == Itemtype.Doublejump)
        {
            StartCoroutine(CanDoubleJump());
        }
        else
        {
            StartCoroutine(JumpPowerTwice());
        }
    }

    private IEnumerator CanDoubleJump()
    {
        PlayerManager.Instance.CanDoubleJump = true;
        UIManager.Instance.Usingitemicon.sprite = curitemdata.Itemdata.Getimage();
        UIManager.Instance.Onitemicon();
        yield return new WaitForSeconds(5f);
        PlayerManager.Instance.CanDoubleJump = false;
        UIManager.Instance.Offitemicon();
        yield break;
    }

    private IEnumerator JumpPowerTwice()
    {
        PlayerManager.Instance.Jump *= 2;
        UIManager.Instance.Usingitemicon.sprite = curitemdata.Itemdata.Getimage();
        UIManager.Instance.Onitemicon();
        yield return new WaitForSeconds(5f);
        PlayerManager.Instance.Jump /= 2;
        UIManager.Instance.Offitemicon();
        yield break;
    }
}
