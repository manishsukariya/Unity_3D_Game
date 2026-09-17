using UnityEngine;
using System.Collections.Generic;

public class Vehicles : MonoBehaviour
{
    public List<GameObject> vehicles = new List<GameObject>();

    public List<GameObject> spawnPoints0 = new List<GameObject>();
    public List<GameObject> spawnPoints90 = new List<GameObject>();
    public List<GameObject> spawnPoints180 = new List<GameObject>();
    public List<GameObject> spawnPoints270 = new List<GameObject>();

    public List<List<GameObject>> points = new List<List<GameObject>>();

    public float spawnInterval = 3f;

    private float spawnTimer;

    void Start()
    {
        points.Add(spawnPoints0);
        points.Add(spawnPoints90);
        points.Add(spawnPoints180);
        points.Add(spawnPoints270);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnVehicle();
            spawnTimer = 0f;
        }
    }

    void SpawnVehicle()
    {
        if (vehicles.Count == 0)
            return;

        int direction = Random.Range(0, points.Count);

        List<GameObject> selectedPoints = points[direction];

        List<GameObject> availablePoints = new List<GameObject>();

        foreach (GameObject pointObject in selectedPoints)
        {
            SpawnPoint point = pointObject.GetComponent<SpawnPoint>();

            if (point != null && point.isAvailable)
            {
                availablePoints.Add(pointObject);
            }
        }

        if (availablePoints.Count == 0)
            return;

        GameObject selectedPoint =
            availablePoints[Random.Range(0, availablePoints.Count)];

        SpawnPoint selectedSpawnPoint =
            selectedPoint.GetComponent<SpawnPoint>();

        GameObject selectedVehicle =
            vehicles[Random.Range(0, vehicles.Count)];

        GameObject spawnedVehicle = Instantiate(
            selectedVehicle,
            selectedPoint.transform.position,
            selectedPoint.transform.rotation
        );

        spawnedVehicle.transform.localScale = Vector3.one * 0.7f;

        selectedSpawnPoint.StartCooldown();
    }
}