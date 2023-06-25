using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0,75,0);
    public float timeToFade = 1f;

    private RectTransform _textTransform;
    private TextMeshProUGUI _textMeshPro;

    private float _timeElapsed = 0f;
    private Color _startColor;

    private void Awake()
    {
        _textTransform = GetComponent<RectTransform>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _startColor = _textMeshPro.color;
    }

    private void Update()
    {
        _textTransform.position += moveSpeed * Time.deltaTime;
        _timeElapsed += Time.deltaTime;
        if(_timeElapsed < timeToFade)
        {
            float fadeAlpha = _startColor.a * (1 - _timeElapsed / timeToFade);
            _textMeshPro.color = new Color(_startColor.r, _startColor.g, _startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
