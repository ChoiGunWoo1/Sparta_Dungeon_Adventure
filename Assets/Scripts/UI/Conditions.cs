using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conditions : MonoBehaviour // UI의 상태 클래스
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

    private void Update() // UIbar를 전체에 비례해 채움
    {
        UIbar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value) // 최대, 최소가 넘지 않게 계산
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, minValue);
    }


}
