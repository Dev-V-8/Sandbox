using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

using Random = UnityEngine.Random;
public class EntitiesSpawner : MonoBehaviour
{
    [Header("Spawner")]
   
    [SerializeField] private int spawnCount = 30;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnRadius = 30f;
    [SerializeField] private int difficultyBonus = 5;

    [Header("Asteroids")]
    [SerializeField] float minSpeed = 4f;
    [SerializeField] float maxSpeed = 12f;

    private float spawnTimer;

    private EntityManager entityManager;

    [SerializeField] private Mesh enemyMesh;
    [SerializeField] private Material enemyMaterial;

    private bool canSpawn;

    [SerializeField] private GameObject alienPrefab;
    [SerializeField] private GameObject asteroidPrefab;

    private Entity enemyEntityPrefab;

    private void Start()
    {

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);

        enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroidPrefab, settings);

        SpawnWave();
    }

    private void SpawnWave()
    {
        NativeArray<Entity> enemyArray = new NativeArray<Entity>(spawnCount, Allocator.Temp);

        for (int i = 0; i < enemyArray.Length; i++)
        {
            enemyArray[i] = entityManager.Instantiate(enemyEntityPrefab);

            entityManager.SetComponentData(enemyArray[i], new Translation { Value = RandomPointOnCircle(spawnRadius) });

            float3 asteroidDirection = new float3();
            asteroidDirection.x = Random.Range(-1.0f, 1.0f);
            asteroidDirection.y = Random.Range(-1.0f, 1.0f);
            entityManager.SetComponentData(enemyArray[i], new AsteroidMovementData { speed = Random.Range(minSpeed, maxSpeed), direction = asteroidDirection });
        }

        enemyArray.Dispose();

        spawnCount += difficultyBonus;
    }

    private float3 RandomPointOnCircle(float radius)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized * radius;

        return new float3(randomPoint.x, randomPoint.y, 0.0f ) + (float3)GameManager.GetPlayerPosition();
    }

    public void StartSpawn()
    {
        canSpawn = true;
    }

    private void Update()
    {
        if (!canSpawn || GameManager.IsGameOver())
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnInterval)
        {
            SpawnWave();
            spawnTimer = 0;
        }
    }
}
