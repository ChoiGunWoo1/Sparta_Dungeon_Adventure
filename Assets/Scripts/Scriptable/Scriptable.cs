using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Itemtype // ������ Ÿ��(��������, ���� 2��)
{
    Doublejump,
    JumpScaleUp
}

[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]
public class Scriptable : ScriptableObject // ��ũ���ͺ� ������Ʈ(�������� ����)
{
    [Header("Info")] // ������ ����
    [SerializeField] private Itemtype itemtype;
    [SerializeField] private GameObject image; // ������
    [SerializeField] private Sprite icon; // ������
    [SerializeField] private string description;

    public string Description() // �� private ���� get
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
