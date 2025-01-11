using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Coin prefabýný buraya drag and drop yapmalýsýn
    public int coinCount = 100; // Spawnlanacak coin sayýsý

    // Coinlerin spawnlanacaðý z aralýðý
    private float minZ = -18031.88f;
    private float maxZ = -16586.7f;

    // Coinlerin spawnlanacaðý x aralýðý
    private float minX = -4f;
    private float maxX = 2.7f;

    // Yüksekliði ayarlamak için coinlerin y koordinatý
    public float yPosition = 2.446f; // Yüksekliði ayarlamak için

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        for (int i = 0; i < coinCount; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 spawnPosition = new Vector3(randomX, yPosition, randomZ);

            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
