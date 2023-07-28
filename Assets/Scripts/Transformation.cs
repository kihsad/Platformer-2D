using System.Collections;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    //[SerializeField]
    //private GameObject _skin1, _skin2;
    [SerializeField]
    private float lifeTime;


    private PlayerControl _playerControl;
    private GameObject currentSkin;
    private Vector3 currentPosition;
    
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

    //private void Awake()
    //{
    //    currentSkin = _skin1;
    //    _skin2.SetActive(false);
    //    currentPosition = transform.position;
    //    //LoadDefaultSkin(currentPosition);
    //}

    //public void ChangeSkin()
    //{
    //    Debug.Log("transformation start");
    //    StartCoroutine(Change(currentSkin.transform.position));
    //}

    //private IEnumerator Change(Vector3 position)
    //{
    //    DestroyCurrentSkin();
    //    LoadNewSkin(position);
    //    yield return new WaitForSeconds(lifeTime);
    //    currentPosition = currentSkin.transform.position;
    //    DestroyCurrentSkin();
    //    LoadDefaultSkin(currentPosition);
    //}

    //private void LoadDefaultSkin(Vector3 position)
    //{
    //    currentSkin = _skin1;
    //    currentSkin.SetActive(true);
    //    currentSkin = Instantiate(_skin1, transform);
    //    currentSkin.transform.position = position;
    //}
    //private void LoadNewSkin(Vector3 position)
    //{
    //    currentSkin = _skin2;
    //    currentSkin.SetActive(true);
    //    currentSkin = Instantiate(_skin2, transform);
    //    currentSkin.transform.position = position;
    //}
    //private void DestroyCurrentSkin()
    //{
    //    //Destroy(currentSkin);
    //    currentSkin.SetActive(false);
    //}
}
