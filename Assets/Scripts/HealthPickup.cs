using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public AudioClip healSound;
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0,180,0);

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable)
        {
            damageable.Heal(healthRestore);
            SoundManager.Instance.PlaySound(healSound);
            Destroy(gameObject);
        }    
    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
