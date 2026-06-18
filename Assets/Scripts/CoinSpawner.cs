using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class CoinSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject coinPrefab;
    [SerializeField] private float spawnInterval = 2f;

    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float minZ = -8f;
    [SerializeField] private float maxZ = 8f;
    [SerializeField] private float altura = 0.5f;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
            StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        while (true)
        {
            if (NetworkManager.Singleton == null ||
                !NetworkManager.Singleton.IsListening)
            {
                yield break;
            }

            GameTimer timer =
                FindFirstObjectByType<GameTimer>();

            if (timer != null && timer.IsGameRunning)
            {
                Vector3 position = new Vector3(
                    Random.Range(minX, maxX),
                    altura,
                    Random.Range(minZ, maxZ)
                );

                NetworkObject newCoin =
                    Instantiate(
                        coinPrefab,
                        position,
                        Quaternion.identity
                    );

                newCoin.Spawn();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
