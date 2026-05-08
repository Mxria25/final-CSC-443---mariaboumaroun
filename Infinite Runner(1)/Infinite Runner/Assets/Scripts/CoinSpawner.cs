using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField, Range(0f, 1f)] private float spawnChance = 0.8f;

    private GameObject currentCoin;

    private void OnEnable()
    {
        SpawnOneRandomCoin();
    }

    private void OnDisable()
    {
        ClearCoin();
    }

    private void SpawnOneRandomCoin()
    {
        ClearCoin();

        if (coinPrefab == null || spawnPoints.Length == 0) return;

        if (Random.value > spawnChance) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform point = spawnPoints[randomIndex];

        currentCoin = Instantiate(coinPrefab, point.position, point.rotation, transform);
    }

    private void ClearCoin()
    {
        if (currentCoin != null)
        {
            Destroy(currentCoin);
            currentCoin = null;
        }
    }
}