using Unity.Netcode;
using UnityEngine;

public class PlayerCoinCollector : NetworkBehaviour
{
    [SerializeField] private GameObject carriedCoinVisual;

    public NetworkVariable<bool> HasCoin =
        new NetworkVariable<bool>(false);

    public NetworkVariable<int> Score =
        new NetworkVariable<int>(0);

    public override void OnNetworkSpawn()
    {
        HasCoin.OnValueChanged += OnCoinChanged;
        OnCoinChanged(false, HasCoin.Value);
    }

    public override void OnNetworkDespawn()
    {
        HasCoin.OnValueChanged -= OnCoinChanged;
    }

    private void OnCoinChanged(bool previousValue, bool newValue)
    {
        if (carriedCoinVisual != null)
            carriedCoinVisual.SetActive(newValue);
    }

    public bool TryTakeCoin()
    {
        if (!IsServer || HasCoin.Value)
            return false;

        GameTimer timer =
            FindFirstObjectByType<GameTimer>();

        if (timer == null || !timer.IsGameRunning)
            return false;

        HasCoin.Value = true;
        return true;
    }

    public void DepositCoin()
    {
        if (!IsServer || !HasCoin.Value)
            return;

        GameTimer timer =
            FindFirstObjectByType<GameTimer>();

        if (timer == null || !timer.IsGameRunning)
            return;

        HasCoin.Value = false;
        Score.Value++;
    }
}