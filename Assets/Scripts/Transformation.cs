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

    private bool isRobotActive;
    private bool isShieldActive;

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
        currentPosition = transform.position;
        LoadDefaultSkin(currentPosition);
    }

    public void ChangeSkin()
    {
        StartCoroutine(Change(currentSkin.transform.position));
    }

    private IEnumerator Change(Vector3 position)
    {
        DestroyCurrentSkin();
        LoadRobotSkin(position);
        yield return new WaitForSeconds(lifeTime);
        currentPosition = currentSkin.transform.position;
        DestroyCurrentSkin();
        LoadDefaultSkin(currentPosition);
    }

    private void LoadDefaultSkin(Vector3 position)
    {
        currentSkin = Instantiate(_skin1, transform);
        currentSkin.transform.position = position;
        currentSkin.GetComponent<PlayerControl>().IsRobotActionActive = isRobotActive;
    }
    private void LoadRobotSkin(Vector3 position)
    {
        currentSkin = Instantiate(_skin2, transform);
        currentSkin.transform.position = position;
        currentSkin.GetComponent<PlayerControl>().IsRobotActionActive = isRobotActive;
        currentSkin.GetComponent<Block>().IsShieldActionActive = isShieldActive;
    }
    private void DestroyCurrentSkin()
    {
        isRobotActive = currentSkin.GetComponent<PlayerControl>().IsRobotActionActive;
        isShieldActive = currentSkin.GetComponent<Block>().IsShieldActionActive;
        Destroy(currentSkin);
    }
}
