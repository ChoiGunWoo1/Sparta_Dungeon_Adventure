using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> // UI상태 관리하는 클래스
{
    Conditions stamina; // 스테미나 UI 클래스
    [SerializeField] private Image usingitemicon;

    public Image Usingitemicon
    {
        get { return usingitemicon; }
        set { usingitemicon = value; }
    }

    private void Start()
    {
        stamina = GetComponentInChildren<Conditions>(); // 자식들중 condition을 가져옴
    }

    private void Update()
    {
        stamina.Add(PlayerManager.Instance.PassiveStamina * Time.deltaTime); // 시간이 지날때마다 스테미나 회복
    }

    public bool TryJump() // 점프에 필요한 스테미나가 있다면 소모하고 true를 반환
    {
        if(stamina.CurValue >= PlayerManager.Instance.JumpStamina)
        {
            stamina.Subtract(PlayerManager.Instance.JumpStamina);
            return true;
        }
        return false;
    }

    public void Onitemicon() // 아이템 활성 아이콘 켜기
    {
        usingitemicon.gameObject.SetActive(true);
    }
    public void Offitemicon() // 아이템 활성 아이콘 끄기
    {
        usingitemicon.gameObject.SetActive(false);
    }
}
