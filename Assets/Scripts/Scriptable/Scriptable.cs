using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Itemtype // 아이템 타입(더블점프, 점프 2배)
{
    Doublejump,
    JumpScaleUp
}

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]
public class Scriptable : ScriptableObject // 스크립터블 오브젝트(아이템의 정보)
{
    [Header("Info")] // 아이템 정보
    [SerializeField] private Itemtype itemtype;
    [SerializeField] private GameObject image; // 프리팹
    [SerializeField] private Sprite icon; // 아이콘
    [SerializeField] private string description;

    public string Description() // 각 private 변수 get
    {
        return description;
    }

    public Itemtype GetItemtype()
    {
        return itemtype;
    }

    public Sprite Getimage()
    {
        return icon;
    }
}
