using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    void Update()
    {
        // WEAPON ROTATION (FACE MOUSE)
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.up = direction;
    }
}
