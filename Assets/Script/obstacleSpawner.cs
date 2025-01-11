using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Obstacle prefabýný buraya drag and drop yapmalýsýn
    public int obstacleCount = 100; // Spawnlanacak obstacle sayýsý
    public float minDistance = 5f; // Minimum distance between obstacles
    public float minPortalDistance = 10f; // Minimum distance from portals

    // Obstacle'larýn spawnlanacaðý z aralýðý
    private float minZ = -18031.88f;
    private float maxZ = -16586.7f;

    // Obstacle'larýn spawnlanacaðý x aralýðý
    private float minX = -4f;
    private float maxX = 2.7f;

    // Yüksekliði ayarlamak için obstacle'larýn y koordinatý
    public float yPosition = 0.17f; // Yüksekliði ayarlamak için

    private List<Vector3> spawnedPositions = new List<Vector3>();
    private List<Vector3> portalPositions = new List<Vector3>();

    void Start()
    {
        FindPortals();
        SpawnObstacles();
    }

    void FindPortals()
    {
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
        foreach (GameObject portal in portals)
        {
            portalPositions.Add(portal.transform.position);
        }
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 spawnPosition;

            // Keep generating new positions until a suitable one is found
            do
            {
                float randomX = Random.Range(minX, maxX);
                float randomZ = Random.Range(minZ, maxZ);
                spawnPosition = new Vector3(randomX, yPosition, randomZ);
            }
            while (!IsPositionValid(spawnPosition));

            spawnedPositions.Add(spawnPosition);

            Quaternion spawnRotation = Quaternion.Euler(0, 53, 0);

            Instantiate(obstaclePrefab, spawnPosition, spawnRotation);
        }
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 pos in spawnedPositions)
        {
            if (Vector3.Distance(pos, position) < minDistance)
            {
                return false;
            }
        }

        foreach (Vector3 portalPos in portalPositions)
        {
            if (Vector3.Distance(portalPos, position) < minPortalDistance)
            {
                return false;
            }
        }

        return true;
    }
}