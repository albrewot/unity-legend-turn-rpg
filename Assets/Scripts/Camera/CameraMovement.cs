using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour {
    //Atributes
    //Serialized Fields
    [SerializeField] public Transform targetPlayer;
    [SerializeField] public float smoothing;
    [SerializeField] public Tilemap sceneMap;

    //Non-Serialized
    private Vector3 minBoundary;
    private Vector3 maxBoundary;
    private float halfHeight;
    private float halfWidth;

    //Methods
    public void MakeCameraFollowPlayer() {
        if (transform.position != targetPlayer.position) {
            Vector3 targetPosition = new Vector3(targetPlayer.position.x, targetPlayer.position.y, transform.position.z);
            Vector3 desiredPosition = Vector3.Lerp(transform.position, targetPosition, smoothing);
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBoundary.x, maxBoundary.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBoundary.y, maxBoundary.y);
            transform.position = desiredPosition;
        }
    }

    public void SetBoundaries() {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        Vector3 offset = new Vector3(halfWidth, halfHeight, 0f);

        minBoundary = sceneMap.localBounds.min + offset;
        maxBoundary = sceneMap.localBounds.max - offset;

        SetPlayerBoundaries();
    }

    public void SetPlayerBoundaries() {
        PlayerController.instance.SetBoundaries(sceneMap.localBounds.min, sceneMap.localBounds.max);
    }

    public void FindPlayer() {
        if(targetPlayer == null) {
            targetPlayer = FindObjectOfType<PlayerController>().transform;
        }
    }

    // Start is called before the first frame update
    void Start() {
        FindPlayer();
        SetBoundaries();
    }

    // Update is called once per frame
    void FixedUpdate() {
        FindPlayer();
        MakeCameraFollowPlayer();
    }
}
