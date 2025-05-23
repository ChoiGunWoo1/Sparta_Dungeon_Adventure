using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface Iiteminteractable // 아이템 오브젝트에 넣어야할 함수들을 정의
{
    public string GetDescription(); // 아이템 description을 반환하는 함수

    public void DoInteract(); // 상호작용을 실행하는 함수
}


public class ItemInteract : MonoBehaviour, Iiteminteractable // 아이템 데이터의 scriptable object를 담고있는 클래스 -> scriptable object데이터와 연결
{
    [SerializeField] private Scriptable itemdata;

    public string GetDescription()
    {
        return itemdata.Description();
    }

    public void DoInteract() // 해당되는 코루틴을 호출후 아이템을 보이지 않게함
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
