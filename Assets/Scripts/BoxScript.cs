using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxScript : MonoBehaviour{

    // Public variables to set in the Unity Inspector
    public float DragSpeed;
    public float MaxDistance;
    public GameObject Player;
    public GameObject GlowBox;
    public GameObject Ground;
    public LayerMask GroundLayer;
    
    // References to Unity components
    private Rigidbody2D Rb2D;
    private Collider2D Coll2D;
    private Animator Anim;
    private AudioSource AudioSrc;
    
    // Variables to store input values
    private bool BoxIsConnected = false;
    private bool BoxHasFallen = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        Rb2D = GetComponent<Rigidbody2D>();
        Coll2D = GetComponent<Collider2D>();
        Anim = GetComponent<Animator>();
        AudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if (BoxHasFallen) return; // If the box has fallen, skip the rest of the update
        if (GlowBox) CheckBoxIsGlowing();

        if (Input.GetKeyDown(KeyCode.E)){
            CheckBoxConnection();
        }

        if (BoxIsConnected && Input.GetKey(KeyCode.E)){
            DragBox();
        }
    }

    private void CheckBoxIsGlowing(){
        float distToGlowBox = Vector2.Distance(Player.transform.position, GlowBox.transform.position);

        bool isNearGlowBox = distToGlowBox < MaxDistance && !BoxIsConnected;

        GlowBox.SetActive(isNearGlowBox);
    }

    private void CheckBoxConnection(){
        float distToBox = Vector2.Distance(Player.transform.position, transform.position);

        if (distToBox < MaxDistance && !BoxIsConnected){
            BoxIsConnected = true;
            Debug.Log("Box Connected");
        }
    }

    private void CheckIfBoxShouldFall(){
        /*
         * Check if the box has ground beneath it using raycasting.
            * If there is no ground, set the box to dynamic and allow it to fall.
            * When the box falls, set the BoxHasFallen flag to true.
            * This will prevent the box from being dragged again until it is reset.
            * The raycast is casted from two points on the box to check for ground in the downward direction.
         */
        float RayCastLength = 0.2f;
        Vector2 leftOrigin = new Vector2(transform.position.x - 0.09f, transform.position.y);
        Vector2 rightOrigin = new Vector2(transform.position.x + 0.09f, transform.position.y);

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, RayCastLength, GroundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, RayCastLength, GroundLayer);

        Debug.DrawRay(leftOrigin, Vector2.down * RayCastLength, Color.red);
        Debug.DrawRay(rightOrigin, Vector2.down * RayCastLength, Color.red);

        if (!leftHit.collider && !rightHit.collider){
            AudioSrc.Play();
            if (Anim){
                Anim.SetTrigger("IsFalling");
                Invoke(nameof(RotateSpriteAfterFall), 0.09f);
            }
            Rb2D.bodyType = RigidbodyType2D.Dynamic;
            BoxHasFallen = true;
            Debug.Log("Box Has Fallen");
            // Change the background when the box stops falling
            FindFirstObjectByType<ChangeBackgroundScript>().ChangeBackground();
        }
    }

    private void DragBox(){
        Rb2D.bodyType = RigidbodyType2D.Kinematic;

        // Move the box towards the player
        Vector2 direction = Player.transform.position - transform.position;
        direction = new Vector2(direction.x, 0f).normalized;

        transform.position += (Vector3)direction * DragSpeed * Time.deltaTime;

        // Check if the box has ground beneath it
        CheckIfBoxShouldFall();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        // Check if the box has collided with the ground and set the animator parameter accordingly
        if (collision.gameObject.CompareTag("Ground") && BoxHasFallen){
            Rb2D.bodyType = RigidbodyType2D.Static;
            Debug.Log("Box is on the ground");
        }
    }

    private void RotateSpriteAfterFall(){
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }
}
