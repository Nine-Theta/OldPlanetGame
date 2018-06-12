using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper for the prefab and variables related to specific spawn types
/// </summary>
[System.Serializable]
public class SpawnerType
{
    public GameObject[] prefabs;
    public float delayBeforeSpawn = 14.0f;
    public float baseSpawnTime = 2.0f;
    public float spawnTimeDecreasePerSpawn = 0.0f;
    public float minSpawnTime = 2.0f;
    public float stopSpawningAt = 29.9f;

    private float _spawnTime = 0.0f;
    private float _totalTime = 0.0f;

    public bool ShouldSpawn
    { get { return (_spawnTime <= 0.0f && _totalTime < stopSpawningAt && delayBeforeSpawn <= 0.0f); } }

    public GameObject GetSpawnObject()
    {
        int spawnIndex = Mathf.FloorToInt(Random.Range(0.0f, (prefabs.Length - 1) + 0.99f));
        baseSpawnTime -= spawnTimeDecreasePerSpawn;
        if (baseSpawnTime > minSpawnTime)
            _spawnTime = baseSpawnTime;
        else
            _spawnTime = minSpawnTime;
        return prefabs[spawnIndex];
    }

    public void SubstractTimers(float deltaTime)
    {
        if (_totalTime >= stopSpawningAt) //Early exit for "inactive" spawnerTypes
            return;
        if (delayBeforeSpawn > 0.0f)
        {
            delayBeforeSpawn -= deltaTime;
        }
        else
        {
            _spawnTime -= deltaTime;
        }
        _totalTime += deltaTime;
    }
}

public class MinigameSpawnerScript : MonoBehaviour
{
    [SerializeField] private SpawnerType[] CloudSpawnList;
    [SerializeField] private float distanceFromCenter = 3.0f;
    [Tooltip("Sets spawned nodes' speed to this variable, or randomizes them from node variables if 0")]
    [SerializeField]
    private float _speed = 0.0f;
    //[SerializeField] private GameObject _prefab;


    void Start()
    {
        //_currentSpawnTime = _spawnMaxTime;
    }

    private void Update()
    {
        if (Time.unscaledDeltaTime >= 1.0f)
            return;
        for (int i = 0; i < CloudSpawnList.Length; i++)
        {
            CloudSpawnList[i].SubstractTimers(Time.unscaledDeltaTime);
            if (CloudSpawnList[i].ShouldSpawn)
            {
                GameObject newInstance = Instantiate(CloudSpawnList[i].GetSpawnObject(), transform);
                int dir = Mathf.FloorToInt(Random.Range(0.0f, 7.99f));
                Vector3 direction;
                switch(dir)
                {
                    default:
                        Debug.Log("Error in dirRandom, making it right.");
                        Debug.Log("Hehehe...");
                        Debug.Log("Get it? Right?");
                        Debug.Log("Because it defaults into the right direction.");
                        goto case 0;
                    case 0://Right
                        direction = new Vector3(1, 0);
                        break;
                    case 1://Right-Down
                        direction = new Vector3(0.707f, -0.707f);
                        break;
                    case 2://Down
                        direction = new Vector3(0, -1);
                        break;
                    case 3://Left-Down
                        direction = new Vector3(-0.707f, -0.707f);
                        break;
                    case 4://Left
                        direction = new Vector3(-1, 0);
                        break;
                    case 5://Left-Up
                        direction = new Vector3(-0.707f, 0.707f);
                        break;
                    case 6://Up
                        direction = new Vector3(0, 1);
                        break;
                    case 7://Right-Up
                        direction = new Vector3(0.707f, 0.707f);
                        break;
                }
                if (newInstance.GetComponent<TapNodeScript>() != null)
                {
                    newInstance.GetComponent<TapNodeScript>().SetDirection(direction);
                    newInstance.transform.localPosition = (direction * distanceFromCenter);
                    if (_speed != 0.0f)
                    {
                            newInstance.GetComponent<TapNodeScript>().SetSpeed(_speed);
                    }
                }
            }
        }
    }
}
