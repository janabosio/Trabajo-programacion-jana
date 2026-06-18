using Unity.Netcode;
using UnityEngine;

public class DepositZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (NetworkManager.Singleton == null ||
            !NetworkManager.Singleton.IsServer)
            return;

        PlayerCoinCollector player =
            other.GetComponentInParent<PlayerCoinCollector>();

        if (player != null)
            player.DepositCoin();
    }
}
