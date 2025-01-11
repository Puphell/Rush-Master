using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Coin prefab�n� buraya drag and drop yapmal�s�n
    public int coinCount = 100; // Spawnlanacak coin say�s�

    // Coinlerin spawnlanaca�� z aral���
    private float minZ = -18031.88f;
    private float maxZ = -16586.7f;

    // Coinlerin spawnlanaca�� x aral���
    private float minX = -4f;
    private float maxX = 2.7f;

    // Y�ksekli�i ayarlamak i�in coinlerin y koordinat�
    public float yPosition = 2.446f; // Y�ksekli�i ayarlamak i�in

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
