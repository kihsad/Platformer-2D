using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;
    private float _currentFill;
    private float _currentValue;
    private float _overflow;

    [SerializeField]
    private float _lerpSpeed;
    public float MyMaxValue { get; set; }

    public bool IsFull
    {
        get { return content.fillAmount == 1; }
    }
    public float MyOverFlow
    {
        get
        {
            float tmp = _overflow;
            _overflow = 0;
            return tmp;
        }
    }
    public float MyCurrentValue
    {
        get
        {
            return _currentValue;
        }
        set
        {
            if (value > MyMaxValue)
            {
                _overflow = value - MyMaxValue;
                _currentValue = MyMaxValue;
            }
            else _currentValue = value;
            _currentFill = _currentValue / MyMaxValue;
        }
    }
    private void Start()
    {
        content = GetComponent<Image>();
        //MyMaxValue = 100;
    }

    private void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        if (_currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.MoveTowards(content.fillAmount, _currentFill, Time.deltaTime * _lerpSpeed);
        }
        Debug.Log(MyCurrentValue);
    }

    public void Reset()
    {
        content.fillAmount = 0;
    }

    public void Initialized(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
