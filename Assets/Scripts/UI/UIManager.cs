using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    Conditions stamina;
    [SerializeField] private Image usingitemicon;

    public Image Usingitemicon
    {
        get { return usingitemicon; }
        set { usingitemicon = value; }
    }

    private void Start()
    {
        stamina = GetComponentInChildren<Conditions>();
    }

    private void Update()
    {
        stamina.Add(PlayerManager.Instance.PassiveStamina * Time.deltaTime);
    }

    public bool TryJump()
    {
        if(stamina.CurValue >= PlayerManager.Instance.JumpStamina)
        {
            stamina.Subtract(PlayerManager.Instance.JumpStamina);
            return true;
        }
        return false;
    }

    public void Onitemicon()
    {
        usingitemicon.gameObject.SetActive(true);
    }
    public void Offitemicon()
    {
        usingitemicon.gameObject.SetActive(false);
    }
}
