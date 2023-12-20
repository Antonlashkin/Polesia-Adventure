using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float xMapSize = 15;
    [SerializeField] private float yMapSize = 10;
    [SerializeField] private LayerMask solidObjectsLayer;
    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        Vector2 movement = new Vector2 (deltaX, deltaY);
        movement = Vector2.ClampMagnitude(movement, speed);
        Vector2 point = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);
        //Debug.Log(transform.position);
        //rigidBody.MovePosition(position);
        if (Physics2D.OverlapBox(point, new Vector2(0.95f, 1.9f), 0f, solidObjectsLayer) == null)
        {
            if (transform.position.x > xMapSize && movement.x > 0)
            {
                movement.x = 0;
            }
            else if (transform.position.x < -xMapSize && movement.x < 0)
            {
                movement.x = 0;
            }
            if (transform.position.y > yMapSize && movement.y > 0)
            {
                movement.y = 0;
            }
            else if (transform.position.y < -yMapSize && movement.y < 0)
            {
                movement.y = 0;
            }
            transform.Translate(movement);
        }
    }
}
