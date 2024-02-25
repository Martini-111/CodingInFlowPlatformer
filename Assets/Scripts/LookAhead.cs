using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAhead : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Player;
    private Vector2 PlayerVelocity;
    private Rigidbody2D rb;
    private Camera mainCamera;

    [SerializeField] private float ZoomDelta = 3f;
    [SerializeField] private Transform player;
    private float MinZoom;
    private float MaxZoom;
    [SerializeField] private float speed = 1.5f;

    private void Start()
    {
        Player = GameObject.Find("Player");
        rb = Player.GetComponent<Rigidbody2D>();
        mainCamera = gameObject.GetComponent<Camera>();
        MinZoom = mainCamera.orthographicSize;
        MaxZoom = MinZoom + ZoomDelta;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        PlayerVelocity = rb.velocity;
        //Debug.Log(PlayerVelocity.x);
        //Debug.Log("MaxZoom = " + MaxZoom + " MinZoom = " + MinZoom + "Mathf.Abs(PlayerVelocity.x) = " + Mathf.Abs(PlayerVelocity.x));
        float maxSize = Mathf.Clamp(Mathf.Abs(PlayerVelocity.x), MinZoom, MaxZoom);
        float newSize = Mathf.MoveTowards(mainCamera.orthographicSize, maxSize, speed * Time.deltaTime);
        mainCamera.orthographicSize = newSize;
 
    }
}
