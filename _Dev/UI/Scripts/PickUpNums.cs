
using _Internal._Dev.Management.Scripts;
using UnityEngine;

public class PickUpNums : MonoBehaviour
{
    [SerializeField] private GameObject positivePickUp;
    [SerializeField] private GameObject negativePickUp;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        EventManager.AddListener<PickUpHair>(OnHairPickUp);
        EventManager.AddListener<PickUpObstacle>(OnObstaclePickUp);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PickUpHair>(OnHairPickUp);
        EventManager.RemoveListener<PickUpObstacle>(OnObstaclePickUp);
    }

    private void OnObstaclePickUp(PickUpObstacle obj)
    {
        SpawnText(negativePickUp);
    }

    private void OnHairPickUp(PickUpHair obj)
    {
        SpawnText(positivePickUp);
    }

    private void SpawnText(GameObject text)
    {
        Instantiate(text, transform).transform.localPosition =
            new Vector3(Random.Range(-rectTransform.rect.width/2, rectTransform.rect.width/2),
                Random.Range(-rectTransform.rect.height/2, rectTransform.rect.height/2) ,0);
    }
}
