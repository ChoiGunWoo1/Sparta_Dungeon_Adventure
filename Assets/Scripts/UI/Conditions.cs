using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conditions : MonoBehaviour
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

    private void Update()
    {
        UIbar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, minValue);
    }


}
