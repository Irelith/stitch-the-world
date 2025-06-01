using System.Collections;
using UnityEngine;
using TMPro;

public class RepairWire : MonoBehaviour{
    
    // Public variables to configure in Unity
    public float MaxDistanceLeft;
    public float MaxDistanceDown;
    public float MaxDistanceRight;

    // Public variables to set in the Unity Inspector
    public GameObject Player;
    public GameObject SecondCollider;
    public GameObject SingleBrokenLamp;
    public GameObject SingleIluminatedLamp;
    public GameObject DoubleBrokenLamp;
    public GameObject DoubleIluminatedLamp;

    // Glows
    public GameObject GlowBrokenWirePowerLine;
    public GameObject GlowBrokenWire;
    public GameObject GlowBrokenWireLamp;

    // Wires
    public GameObject WirePowerLine;
    public GameObject WireLamp;
    public GameObject BrokenWire;
    public GameObject RepairedWire;

    // Variables to store input values
    private bool WirePowerLineConnected = false;
    private bool WireConnected = false;
    private bool WireLampConnected = false;
    private bool WireCreated = false;
    private AudioSource AudioSrc;
    private bool isPlayerInRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        AudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if (WireCreated) return; // If the Wire is already created, skip the rest of the update
        CheckWireIsGlowing();

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)){
            ConnectWire();
        }

        if (!WireCreated && WirePowerLineConnected && WireConnected && WireLampConnected){
            CreateWire();
            AudioSrc.Play();
            ChangeBackground();
        }
    }

    private void CheckWireIsGlowing(){
        float distToGlowBrokenWirePowerLine = Vector2.Distance(Player.transform.position, GlowBrokenWirePowerLine.transform.position);
        float distToGlowBrokenWire = Vector2.Distance(Player.transform.position, GlowBrokenWire.transform.position);
        float distToGlowBrokenWireLamp = Vector2.Distance(Player.transform.position, GlowBrokenWireLamp.transform.position);

        bool isNearGlowBrokenWirePowerLine = distToGlowBrokenWirePowerLine < MaxDistanceLeft && !WirePowerLineConnected;
        bool isNearGlowBrokenWire = distToGlowBrokenWire < MaxDistanceDown && !WireConnected;
        bool isNearGlowBrokenWireLamp = distToGlowBrokenWireLamp < MaxDistanceRight && !WireLampConnected;

        GlowBrokenWirePowerLine.SetActive(isNearGlowBrokenWirePowerLine);
        GlowBrokenWire.SetActive(isNearGlowBrokenWire);
        GlowBrokenWireLamp.SetActive(isNearGlowBrokenWireLamp);
    }

    private void ConnectWire(){
        float distToWirePowerLine = Vector2.Distance(Player.transform.position, WirePowerLine.transform.position);
        float distToWireLamp = Vector2.Distance(Player.transform.position, WireLamp.transform.position);
        float distToWire = Vector2.Distance(Player.transform.position, BrokenWire.transform.position);

        if (distToWirePowerLine <= MaxDistanceLeft && !WirePowerLineConnected){
            WirePowerLineConnected = true;
            Debug.Log("Power Line Wire Connected");
        }
        else if (distToWire <= MaxDistanceDown && !WireConnected){
            WireConnected = true;
            Debug.Log("Wire Connected");
        }
        else if (distToWireLamp <= MaxDistanceRight && !WireLampConnected){
            WireLampConnected = true;
            Debug.Log("Lamp Wire Connected");
        }
    }

    private void CreateWire(){
        GlowBrokenWirePowerLine.SetActive(false);
        GlowBrokenWire.SetActive(false);
        GlowBrokenWireLamp.SetActive(false);

        BrokenWire.SetActive(false);
        RepairedWire.SetActive(true);
        WireCreated = true;

        SingleBrokenLamp.SetActive(false);
        SingleIluminatedLamp.SetActive(true);
        DoubleBrokenLamp.SetActive(false);
        DoubleIluminatedLamp.SetActive(true);
        SecondCollider.SetActive(false);
        
        Debug.Log("Wire Repaired");
    }

    private void ChangeBackground(){
        FindFirstObjectByType<ChangeBackgroundScript>().ChangeBackground();
        Debug.Log("Background Changed");
    }
    
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            isPlayerInRange = false;
        }
    }
}
