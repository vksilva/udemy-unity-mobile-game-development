using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private float secondsBetweenAsteroids = 0.5f;
    [SerializeField] private Vector2 asteroidForceRange;
    [SerializeField] private Camera mainCamera;

    private float timer;
    private Stack<Asteroid> pool = new Stack<Asteroid>();

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnAsteroid();
            timer += secondsBetweenAsteroids;
        }
    }

    private void Awake()
    {
        Asteroid.returnToPool = asteroid =>
        {
            pool.Push(asteroid);
            asteroid.gameObject.SetActive(false);
        };
    }

    private void SpawnAsteroid()
    {
        int side = Random.Range(0, 4);

        Vector2 spawPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (side)
        {
            case 0:
                // Left
                spawPoint.Set(0f, Random.value);
                direction.Set(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                // Right
                spawPoint.Set(1f, Random.value);
                direction.Set(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                // Bottom 
                spawPoint.Set(Random.value, 0f);
                direction.Set(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                // Top
                spawPoint.Set(Random.value, 1f);
                direction.Set(Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawPoint);
        worldSpawnPoint.z = 0f;

        Asteroid asteroidInstance = null;

        if (pool.Count > 0)
        {
            asteroidInstance = pool.Pop();
            asteroidInstance.gameObject.SetActive(true);
            asteroidInstance.transform.position = worldSpawnPoint;
            asteroidInstance.transform.rotation = CreateRandomRotation();
        }
        else
        {
            asteroidInstance = Instantiate(
                asteroidPrefab,
                worldSpawnPoint,
                CreateRandomRotation());
        }

        Rigidbody asteroidRigidbody = asteroidInstance.GetComponent<Rigidbody>();
        asteroidRigidbody.velocity = direction.normalized * Random.Range(asteroidForceRange.x, asteroidForceRange.y);
    }

    private static Quaternion CreateRandomRotation()
    {
        return Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }
}