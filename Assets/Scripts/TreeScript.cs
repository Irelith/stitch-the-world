using UnityEngine;

public class TreeScript : MonoBehaviour{

    // Public variables to set in the Unity Inspector
    public float DragSpeed;
    public float MaxDistance;
    public GameObject Player;
    public GameObject GlowTree;
    public GameObject Ground;
    public LayerMask GroundLayer;
    public GameObject BrokenTreeBottom;
    public GameObject BrokenTreeDown;
    
    // References to Unity components
    private Rigidbody2D Rb2D;
    private Collider2D Coll2D;
    private Animator Anim;
    private AudioSource AudioSrc;
    
    // Variables to store input values
    private bool TreeIsConnected = false;
    private bool TreeHasFallen = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        Rb2D = GetComponent<Rigidbody2D>();
        Coll2D = GetComponent<Collider2D>();
        Anim = GetComponent<Animator>();
        AudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if (TreeHasFallen) return; // If the Tree has fallen, skip the rest of the update
        if (GlowTree) CheckTreeIsGlowing();

        if (Input.GetKeyDown(KeyCode.E)){
            CheckTreeConnection();
        }

        if (TreeIsConnected && Input.GetKey(KeyCode.E)){
            DragTree();
        }
    }

    private void CheckTreeIsGlowing(){
        float distToGlowBox = Vector2.Distance(Player.transform.position, GlowTree.transform.position);

        bool isNearGlowBox = distToGlowBox < MaxDistance && !TreeIsConnected;

        GlowTree.SetActive(isNearGlowBox);
    }

    private void CheckTreeConnection(){
        float distToBox = Vector2.Distance(Player.transform.position, transform.position);

        if (distToBox < MaxDistance && !TreeIsConnected){
            TreeIsConnected = true;
            Debug.Log("Tree Connected");
            AudioSrc.Play();
        }
    }

    private void CheckIfTreeShouldFall(){
        /*
         * Check if the Tree has ground beneath it using raycasting.
            * If there is no ground, set the Tree to dynamic and allow it to fall.
            * When the Tree falls, set the TreeHasFallen flag to true.
            * This will prevent the Tree from being dragged again until it is reset.
            * The raycast is casted from two points on the Tree to check for ground in the downward direction.
         */
        float RayCastLength = 0.2f;
        Vector2 leftOrigin = new Vector2(transform.position.x - 0.09f, transform.position.y);
        Vector2 rightOrigin = new Vector2(transform.position.x + 0.09f, transform.position.y);

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, RayCastLength, GroundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, RayCastLength, GroundLayer);

        Debug.DrawRay(leftOrigin, Vector2.down * RayCastLength, Color.red);
        Debug.DrawRay(rightOrigin, Vector2.down * RayCastLength, Color.red);

        if (!leftHit.collider && !rightHit.collider){
            Rb2D.bodyType = RigidbodyType2D.Dynamic;
            TreeHasFallen = true;
            Debug.Log("Tree Has Fallen");
            BrokenTreeBottom.SetActive(true);
            if (Anim){
                Anim.SetTrigger("IsFalling");
                Invoke(nameof(RotateSpriteAfterFall), 0.09f);
            }
            BrokenTreeDown.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            Coll2D.enabled = false;
            GlowTree.SetActive(false);
            // Change the background when the Tree stops falling
            FindFirstObjectByType<ChangeBackgroundScript>().ChangeBackground();
        }
    }

    private void DragTree(){
        Rb2D.bodyType = RigidbodyType2D.Kinematic;

        // Move the Tree towards the player
        Vector2 direction = Player.transform.position - transform.position;
        direction = new Vector2(direction.x, 0f).normalized;

        transform.position += (Vector3)direction * DragSpeed * Time.deltaTime;

        // Check if the Tree has ground beneath it
        CheckIfTreeShouldFall();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        // Check if the Tree has collided with the ground and set the animator parameter accordingly
        if (collision.gameObject.CompareTag("Ground") && TreeHasFallen){
            Rb2D.bodyType = RigidbodyType2D.Static;
            Debug.Log("Tree is on the ground");
        }
    }

    private void RotateSpriteAfterFall(){
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }
}
