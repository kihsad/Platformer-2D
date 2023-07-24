using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _shiled;
    [SerializeField]
    private float _shieldTime;

    public bool isActive;

    public bool IsShieldActionActive { get; set; }

    private void Awake()
    {
        _shiled.enabled = false;
    }
    public void SetActiveShield()
    {
        if (!IsShieldActionActive) return;
        if (!isActive)
        {
            StartCoroutine(Shield());
        }
    }

    private IEnumerator Shield()
    {
        isActive = true;
        _shiled.enabled = true;
        yield return new WaitForSeconds(_shieldTime);
        _shiled.enabled = false;
        isActive = false;
    }
   
}
