using UnityEngine;

public class AudioManagerScript : MonoBehaviour{
    public static AudioManagerScript Instance;
    public AudioSource AudioSrc;
    // public AudioClip background;

    private void Awake(){
        // Ensure that this GameObject is not destroyed when loading a new scene
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        AudioSrc = GetComponent<AudioSource>();
        // AudioSrc.clip = background;
        AudioSrc.Play();
    }

    // Update is called once per frame
    void Update(){
        
    }
}
