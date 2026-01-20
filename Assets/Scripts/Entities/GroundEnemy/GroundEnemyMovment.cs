using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GroundEnemyManager core;

    [Header("Salto automático")]
    public float jumpForce = 7f;       // fuerza del salto
    public float jumpCooldown = 2f;    // cada cuántos segundos salta
    private float jumpTimer;

    private void Awake()
    {
        core = GetComponent<GroundEnemyManager>();
        if (core == null)
            Debug.LogError("GroundEnemyManager no encontrado en " + gameObject.name);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        // Movimiento horizontal simple
        float dir = core.facingRight ? 1f : -1f;
        core.rb.AddForce(Vector2.right * dir * core.speed);
    }

    void Jump()
    {
        jumpTimer += Time.fixedDeltaTime;

        if (jumpTimer >= jumpCooldown)
        {
            core.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Borde"))
            core.facingRight = !core.facingRight;
    }
}
