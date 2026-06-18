using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameTimer : NetworkBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameObject endPanel;

    public NetworkVariable<int> TimeLeft =
        new NetworkVariable<int>(90);

    private bool started;
    private bool ended;
    private double endTime;

    public bool IsGameRunning
    {
        get { return started && !ended; }
    }

    private void Update()
    {
        if (IsServer)
            UpdateTimer();

        UpdateTimerText();
    }

    private void UpdateTimer()
    {
         if (NetworkManager.Singleton == null)
        return;

        if (!started &&
            NetworkManager.Singleton.ConnectedClientsIds.Count >= 2)
        {
            started = true;
            endTime =
                NetworkManager.Singleton.ServerTime.Time + 90;
        }

        if (!started || ended)
            return;

        TimeLeft.Value = Mathf.Max(0, Mathf.CeilToInt(
            (float)(endTime -
            NetworkManager.Singleton.ServerTime.Time)
        ));

        if (TimeLeft.Value <= 0)
        {
            ended = true;
            ShowWinner();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = TimeLeft.Value / 60;
        int seconds = TimeLeft.Value % 60;

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void ShowWinner()
    {
        PlayerCoinCollector[] players =
            FindObjectsByType<PlayerCoinCollector>(
                FindObjectsSortMode.None
            );

        string result;

        if (players.Length == 0)
        {
            result = "Partida terminada";
        }
        else if (players.Length == 1)
        {
            result = "¡Ganó el jugador 1!";
        }
        else if (players[0].Score.Value >
                 players[1].Score.Value)
        {
            result = "¡Ganó el jugador " +
                     (players[0].OwnerClientId + 1) + "!";
        }
        else if (players[1].Score.Value >
                 players[0].Score.Value)
        {
            result = "¡Ganó el jugador " +
                     (players[1].OwnerClientId + 1) + "!";
        }
        else
        {
            result = "¡Empate!";
        }

        ShowWinnerClientRpc(result);
    }

    [ClientRpc]
    private void ShowWinnerClientRpc(string result)
    {
        resultText.text = result;
        endPanel.SetActive(true);
    }
}