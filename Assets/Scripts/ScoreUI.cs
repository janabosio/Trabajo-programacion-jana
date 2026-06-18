using System.Text;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Update()
    {
        PlayerCoinCollector[] players =
            FindObjectsByType<PlayerCoinCollector>(
                FindObjectsSortMode.None
            );

        StringBuilder text = new StringBuilder();

        foreach (PlayerCoinCollector player in players)
        {
            text.AppendLine(
                "Jugador " + (player.OwnerClientId + 1) +
                ": " + player.Score.Value
            );
        }

        scoreText.text = text.ToString();
    }
}