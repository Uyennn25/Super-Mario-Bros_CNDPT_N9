using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed;
    public Vector2 direction = Vector2.left;
    
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    public float oldSpeed;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnBecameVisible()
    {
        speed = oldSpeed;
    }
    
    private void OnBecameInvisible()
    {
        speed = 0;
        rigidbody.velocity = Vector2.zero;
    }
    
    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);


        if (rigidbody.Raycast(direction))
        {
            direction = -direction;
        }

        if (rigidbody.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
        //rigidbody.velocity = velocity;

    }
}
