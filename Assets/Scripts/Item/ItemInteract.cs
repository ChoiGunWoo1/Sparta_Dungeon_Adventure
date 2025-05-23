using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface Iiteminteractable // ������ ������Ʈ�� �־���� �Լ����� ����
{
    public string GetDescription(); // ������ description�� ��ȯ�ϴ� �Լ�

    public void DoInteract(); // ��ȣ�ۿ��� �����ϴ� �Լ�
}


public class ItemInteract : MonoBehaviour, Iiteminteractable // ������ �������� scriptable object�� ����ִ� Ŭ���� -> scriptable object�����Ϳ� ����
{
    [SerializeField] private Scriptable itemdata;

    public string GetDescription()
    {
        return itemdata.Description();
    }

    public void DoInteract() // �ش�Ǵ� �ڷ�ƾ�� ȣ���� �������� ������ �ʰ���
    {
        UIManager.Instance.Usingitemicon.sprite = itemdata.Getimage();
        if (itemdata.GetItemtype() == Itemtype.Doublejump)
        {
            StartCoroutine(InteractionManager.Instance.CanDoubleJump());
        }
        else
        {
            StartCoroutine(InteractionManager.Instance.JumpPowerTwice());
        }
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }




}
