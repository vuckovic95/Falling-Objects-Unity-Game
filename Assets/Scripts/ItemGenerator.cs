using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using System;

public class ItemGenerator : MonoBehaviour
{
    Action SpawnItemAction;

    [Inject]
    PoolManager _poolManager;

    [BoxGroup("Element Spawners Holder")]
    [SerializeField]
    private Transform _elementSpawnersHolder;

    [BoxGroup("Items Holder")]
    [SerializeField]
    private Transform _itemsHolder;

    [BoxGroup("Elements")]
    [SerializeField]
    private List<ItemScriptableObject> _itemScriptableObjects = new List<ItemScriptableObject>();

    [BoxGroup("Element Speed Range")]
    [SerializeField]
    private float _minSpeed;
    [BoxGroup("Element Speed Range")]
    [SerializeField]
    private float _maxSpeed;

    private List<GameObject> _items = new List<GameObject>();
    private List<Transform> _elementSpawners = new List<Transform>();
    private List<Transform> _elementSpawnersHelper = new List<Transform>();

    private float RESOLUTION_FACTOR = 2.38f;
 
    private void Awake()
    {
        PopulateElementSpawners();
    }

    void Start()
    {
        SpawnElementSpawners();
        PopulateItemsAndSetIntoTheHolder();
        SubscribeToActions();
        StartSpawning(); // brisemo
    }

    private void SubscribeToActions()
    {
        Actions.StartGameAction += ResetAllItems;
        Actions.StartGameAction += StartSpawning;
        SpawnItemAction += SpawnItemHelper;
    }

    private void PopulateElementSpawners()
    {
        foreach (Transform spawner in _elementSpawnersHolder)
        {
            _elementSpawners.Add(spawner);
        }
    }

    private void PopulateItemsAndSetIntoTheHolder()
    {
        int numberOfItems = _poolManager.GetPool["Element"].Count;

        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject go = _poolManager.GetElement();
            go.transform.SetParent(_itemsHolder);
            go.transform.localScale = Vector3.one;
            go.SetActive(false);
            _items.Add(go);
        }
    }

    private void ResetAllItems()
    {
        foreach (GameObject item in _items)
        {
            item.SetActive(false);
        }
    }

    private void SpawnElementSpawners()
    {
        float distanceBetweenFirstAndLastSpawner = (Screen.width / RESOLUTION_FACTOR) * 2;
        float distanceBetweenSpawners = distanceBetweenFirstAndLastSpawner / (_elementSpawners.Count - 1);

        List<Transform> helperSpawnersList = new List<Transform>();
        foreach (Transform spawner in _elementSpawners)
        {
            helperSpawnersList.Add(spawner);
        }

        helperSpawnersList[0].localPosition = new Vector3((-Screen.width / RESOLUTION_FACTOR), helperSpawnersList[0].localPosition.y, helperSpawnersList[0].localPosition.z);
        helperSpawnersList.RemoveAt(0);
        helperSpawnersList[0].localPosition = new Vector3(Screen.width / RESOLUTION_FACTOR, helperSpawnersList[0].localPosition.y, helperSpawnersList[0].localPosition.z);
        helperSpawnersList.RemoveAt(0);


        for (int i = 0; i < helperSpawnersList.Count; i++)
        {
            if(i == 0)
            {
                helperSpawnersList[i].localPosition = new Vector3(_elementSpawners[0].localPosition.x + distanceBetweenSpawners, _elementSpawners[0].localPosition.y, _elementSpawners[0].localPosition.z);
            }
            else
            {
                helperSpawnersList[i].localPosition = new Vector3(helperSpawnersList[i - 1].localPosition.x + distanceBetweenSpawners, helperSpawnersList[i].localPosition.y, helperSpawnersList[i].localPosition.z);
            }
        }
    }

    private void GetItemFromPoolAndSetProperties(Transform spawner)
    {
        GameObject item = _poolManager.GetElement();
        Item itemComponent = item.GetComponent<Item>();
        SetItemSpeed(itemComponent);
        item.SetActive(true);
        item.transform.SetParent(spawner);
        item.transform.localPosition = Vector3.zero;

        int random = UnityEngine.Random.Range(0, _itemScriptableObjects.Count);
        itemComponent.Object = _itemScriptableObjects[random];
        itemComponent.SetProperties();
    }


    private void SpawnItemHelper()
    {
        while (true)
        {
            if(_elementSpawnersHelper.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, _elementSpawnersHelper.Count);
                Transform currentSpawner = _elementSpawnersHelper[random];

                GetItemFromPoolAndSetProperties(currentSpawner);
                _elementSpawnersHelper.Remove(currentSpawner);
                break;
            }
            else
            {
                foreach (Transform spawner in _elementSpawners)
                {
                    _elementSpawnersHelper.Add(spawner);
                }
            }
        }
    }

    private IEnumerator Timer(float time)
    {
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            SpawnItemAction?.Invoke();
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 4));
        }
    }

    private void StartSpawning()
    {
        StartCoroutine(Timer(120));
    }

    private void SetItemSpeed(Item item)
    {
        float speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);

        item.Speed = speed;
    }
}
