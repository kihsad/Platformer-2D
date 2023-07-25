using System.Collections;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    [SerializeField]
    private GameObject _skin1, _skin2;
    [SerializeField]
    private float lifeTime;

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
        currentSkin = _skin1;
        currentPosition = transform.position;
        LoadDefaultSkin(currentPosition);
    }

    public void ChangeSkin()
    {
        Debug.Log("transformation start");
        StartCoroutine(Change(currentSkin.transform.position));
    }

    private IEnumerator Change(Vector3 position)
    {
        DestroyCurrentSkin();
        LoadNewSkin(position);
        yield return new WaitForSeconds(lifeTime);
        currentPosition = currentSkin.transform.position;
        DestroyCurrentSkin();
        LoadDefaultSkin(currentPosition);
    }

    private void LoadDefaultSkin(Vector3 position)
    {
        currentSkin = _skin1;
        currentSkin.SetActive(true);
        //currentSkin = Instantiate(_skin1, transform);
        currentSkin.transform.position = position;
    }
    private void LoadNewSkin(Vector3 position)
    {
        currentSkin = _skin2;
        currentSkin.SetActive(true);
        //currentSkin = Instantiate(_skin2, transform);
        currentSkin.transform.position = position;
    }
    private void DestroyCurrentSkin()
    {
        //Destroy(currentSkin);
        currentSkin.SetActive(false);
    }
}
