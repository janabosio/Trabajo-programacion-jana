using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;
    private Vector3 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
        // Los jugadores remotos son controlados por NetworkTransform.
        rb.isKinematic = !IsOwner;

        if (IsOwner && Camera.main != null)
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition =
                new Vector3(0, 10, -10);

            Camera.main.transform.localRotation =
                Quaternion.Euler(45, 0, 0);
        }
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical).normalized;
    }

    private void FixedUpdate()
    {
        if (!IsOwner)
            return;

        Vector3 newPosition =
            rb.position + movement * speed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }
}