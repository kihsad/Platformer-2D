using System.Collections;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;

    private PlayerControl _playerControl;
    
    private static Transformation instance;
    public static Transformation Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Transformation>();
            }
            return instance;
        } 
    }

    private void Awake()
    {
        _playerControl = GetComponent<PlayerControl>();
    }

    public void ChangeSkin()
    {
        StartCoroutine(ChangeCoroutine(_playerControl));
    }

    private IEnumerator ChangeCoroutine(PlayerControl playerControl)
    {
        var defaultColor = playerControl.gameObject.GetComponentInChildren<ObjectForChange>().GetComponent<SpriteRenderer>().color;
        playerControl.gameObject.GetComponentInChildren<ObjectForChange>().GetComponent<SpriteRenderer>().color = new Color(1F, 0.3F, 0.4F, 1F);
        playerControl.transform.localScale = new Vector3(-1.6F, 1.6F, 1.6F);
        yield return new WaitForSeconds(lifeTime);
        playerControl.gameObject.GetComponentInChildren<ObjectForChange>().GetComponent<SpriteRenderer>().color = defaultColor;
        playerControl.transform.localScale = new Vector3(1F, 1F, 1F);
    }

}
