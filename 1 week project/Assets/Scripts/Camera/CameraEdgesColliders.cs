using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgesColliders : MonoBehaviour
{
    public Vector2 bottomLeft;
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public Vector2 topRight;

    float height;

    public BoxCollider2D left;
    public BoxCollider2D right;

    void Update()
    {
        bottomLeft = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        topLeft = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        bottomRight = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
        topRight = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.nearClipPlane));

        height = (topLeft.y - bottomLeft.y) * 1.5f;

        left.size = new Vector2(1f, height);
        right.size = new Vector2(1f, height);

        left.gameObject.transform.position = new Vector2(bottomLeft.x - 0.5f, bottomLeft.y + (topLeft.y - bottomLeft.y)/2);
        right.gameObject.transform.position = new Vector2(bottomRight.x + 0.5f, bottomRight.y + (topRight.y - bottomRight.y) / 2);
    }
}
