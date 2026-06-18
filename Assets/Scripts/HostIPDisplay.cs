using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class HostIPDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text hostIPText;

    public void ShowHostIP()
    {
        string localIP = "No encontrada";

        foreach (IPAddress address in
                 Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (address.AddressFamily ==
                AddressFamily.InterNetwork)
            {
                localIP = address.ToString();
                break;
            }
        }

        hostIPText.text =
            "IP del Host: " + localIP + "\nPuerto: 7778";
    }

    public void HideHostIP()
    {
        hostIPText.text = "";
    }
}
