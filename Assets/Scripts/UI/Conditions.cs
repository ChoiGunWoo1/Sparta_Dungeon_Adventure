using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conditions : MonoBehaviour // UI�� ���� Ŭ����
{
    private float curValue;
    [SerializeField] float startValue;
    [SerializeField] float maxValue;
    [SerializeField] float minValue;
    [SerializeField] Image UIbar;

    public float CurValue
    {
        get { return curValue; }
    }

    private void Start()
    {
        curValue = startValue;
    }

    private void Update() // UIbar�� ��ü�� ����� ä��
    {
        UIbar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value) // �ִ�, �ּҰ� ���� �ʰ� ���
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, minValue);
    }


}
