using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour{

    public string NextSceneName = "Level1";

    // private Animator anim;
    
    // void Start(){
    //     anim = GetComponent<Animator>();
    // }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
