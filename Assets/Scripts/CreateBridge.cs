using UnityEngine;
using UnityEngine.SceneManagement;

public class Bridge : MonoBehaviour{

    // Public variables to configure in Unity
    public float MaxDistanceLeft;
    public float MaxDistanceRight;

    // Public variables to set in the Unity Inspector
    public GameObject Player;
    public GameObject GlowBridgeLeft;
    public GameObject GlowBridgeRight;
    public GameObject BridgeLeft;
    public GameObject BridgeRight;
    public GameObject BrokenBridge;
    // public GameObject SecondLayer_0;
    // public GameObject SecondLayer_1;
    public GameObject SecondCollider;

    // Variables to store input values
    private bool BridgeLeftConnected = false;
    private bool BridgeRightConnected = false;
    private bool BridgeCreated = false;
    private AudioSource AudioSrc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        AudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if (BridgeCreated) return; // If the bridge is already created, skip the rest of the update
        CheckBridgeIsGlowing();

        if (Input.GetKeyDown(KeyCode.E)){
            ConnectBridge();
        }

        if (BridgeLeftConnected && BridgeRightConnected && !BridgeCreated){
            CreateBridge();
            AudioSrc.Play();
            ChangeBackground();
        }
    }

    private void CheckBridgeIsGlowing(){
        float distToGlowBridgeLeft = Vector2.Distance(Player.transform.position, GlowBridgeLeft.transform.position);
        float distToGlowBridgeRight = Vector2.Distance(Player.transform.position, GlowBridgeRight.transform.position);

        bool isNearGlowBridgeLeft = distToGlowBridgeLeft < MaxDistanceLeft && !BridgeLeftConnected;
        bool isNearGlowBridgeRight = distToGlowBridgeRight < MaxDistanceRight && !BridgeRightConnected;

        GlowBridgeLeft.SetActive(isNearGlowBridgeLeft);
        GlowBridgeRight.SetActive(isNearGlowBridgeRight);
    }

    private void ConnectBridge(){
        float distToBridgeLeft = Vector2.Distance(Player.transform.position, BridgeLeft.transform.position);
        float distToBridgeRight = Vector2.Distance(Player.transform.position, BridgeRight.transform.position);

        if (distToBridgeLeft <= MaxDistanceLeft && !BridgeLeftConnected){
            BridgeLeftConnected = true;
            Debug.Log("Bridge Left Connected");
        }
        else if (distToBridgeRight <= MaxDistanceRight && !BridgeRightConnected){
            BridgeRightConnected = true;
            Debug.Log("Bridge Right Connected");
        }
    }

    private void CreateBridge(){
        GlowBridgeLeft.SetActive(false);
        GlowBridgeRight.SetActive(false);

        BridgeRight.SetActive(false);

        BrokenBridge.SetActive(true);
        BridgeCreated = true;
        Debug.Log("Bridge Created");
    }

    private void ChangeBackground(){
        SecondCollider.SetActive(false);
        FindFirstObjectByType<ChangeBackgroundScript>().ChangeBackground();
        Debug.Log("Background Changed");
    }
}
