using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour{

    // Public variables to set in the Unity Inspector
    public float JumpForce;
    public float MoveSpeed;
    public GameObject GroundDeath;

    // References to Unity components
    private Rigidbody2D Rb2D;
    private Animator Anim;
    private Collider2D Coll2D;

    // Variables to store input values
    private float Horizontal;
    private bool IsGrounded;
    private bool PlayerAlive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        Rb2D = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Coll2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update(){
        if (!PlayerAlive) return;
        Horizontal = Input.GetAxisRaw("Horizontal");
        
        ChangeDirection(Horizontal);
        
        Anim.SetBool("isRunning", Horizontal != 0.0f);

        CheckIsGrounded();

        if (IsGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))){
            Jump();
        }
        Die();
    }

    private void FixedUpdate(){
        Rb2D.linearVelocity = new Vector2(Horizontal * 5f * MoveSpeed, Rb2D.linearVelocity.y);
        // float vertical = Input.GetAxisRaw("Vertical");

        // Vector2 direction = new Vector2(Horizontal, 0).normalized;
        // rigidbody2D.velocity = new Vector2(direction.x * 5f, rigidbody2D.velocity.y);
    }

    private void ChangeDirection(float Horizontal){
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void CheckIsGrounded(){
        // Check if the player is grounded and set the Anim parameter accordingly
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f)){
            IsGrounded = true;
        }else IsGrounded = false;
    }

    private void Jump(){
        // Here remains the logic for jumping
        Rb2D.AddForce(Vector2.up * JumpForce);
    }

    private void Die(){
        if(Coll2D.IsTouchingLayers(LayerMask.GetMask("groundDeath"))){
            PlayerAlive = false;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
