using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _shield;
    [SerializeField]
    private float _shieldTime;

    public bool isActive;

    //public bool IsShieldActionActive { get; set; }

    private void Awake()
    {
        _shield.enabled = false;
    }
    public void SetActiveShield(Damageable damageable)
    {
        if (!isActive)
        {
            StartCoroutine(Shield());
        }
    }

    private IEnumerator Shield()
    {
        isActive = true;
        _shield.enabled = true;
        yield return new WaitForSeconds(_shieldTime);
        _shield.enabled = false;
        isActive = false;
    }
   
}
