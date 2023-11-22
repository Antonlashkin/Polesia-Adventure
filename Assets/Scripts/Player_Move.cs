using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rigidBody;
    [SerializeField] private LayerMask solidObjectsLayer;
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
            transform.Translate(movement);
        }
    }
}
