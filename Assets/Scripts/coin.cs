using Unity.Netcode;
using UnityEngine;

public class Coin : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer)
            return;

        PlayerCoinCollector player =
            other.GetComponentInParent<PlayerCoinCollector>();

        if (player != null && player.TryTakeCoin())
            NetworkObject.Despawn(true);
    }
}
