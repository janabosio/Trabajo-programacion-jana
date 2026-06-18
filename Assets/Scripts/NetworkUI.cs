using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField ipInput;

    private const ushort Port = 7778;

    public void StartHost()
    {
        if (NetworkManager.Singleton.IsListening)
            return;

        UnityTransport transport =
            NetworkManager.Singleton.GetComponent<UnityTransport>();

        transport.SetConnectionData(
            "127.0.0.1",
            Port,
            "0.0.0.0"
        );

        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        if (NetworkManager.Singleton.IsListening)
            return;

        string ip = ipInput.text.Trim();

        if (string.IsNullOrEmpty(ip))
            ip = "127.0.0.1";

        UnityTransport transport =
            NetworkManager.Singleton.GetComponent<UnityTransport>();

        transport.SetConnectionData(ip, Port);

        Debug.Log("Conectando a " + ip + ":" + Port);

        NetworkManager.Singleton.StartClient();
    }
}