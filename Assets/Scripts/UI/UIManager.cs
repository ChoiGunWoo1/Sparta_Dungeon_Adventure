using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> // UI���� �����ϴ� Ŭ����
{
    Conditions stamina; // ���׹̳� UI Ŭ����
    [SerializeField] private Image usingitemicon;

    public Image Usingitemicon
    {
        get { return usingitemicon; }
        set { usingitemicon = value; }
    }

    private void Start()
    {
        stamina = GetComponentInChildren<Conditions>(); // �ڽĵ��� condition�� ������
    }

    private void Update()
    {
        stamina.Add(PlayerManager.Instance.PassiveStamina * Time.deltaTime); // �ð��� ���������� ���׹̳� ȸ��
    }

    public bool TryJump() // ������ �ʿ��� ���׹̳��� �ִٸ� �Ҹ��ϰ� true�� ��ȯ
    {
        if(stamina.CurValue >= PlayerManager.Instance.JumpStamina)
        {
            stamina.Subtract(PlayerManager.Instance.JumpStamina);
            return true;
        }
        return false;
    }

    public void Onitemicon() // ������ Ȱ�� ������ �ѱ�
    {
        usingitemicon.gameObject.SetActive(true);
    }
    public void Offitemicon() // ������ Ȱ�� ������ ����
    {
        usingitemicon.gameObject.SetActive(false);
    }
}
