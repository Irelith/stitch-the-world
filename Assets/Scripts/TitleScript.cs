using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour{

    public string NextSceneName = "Level1";

    void Update(){
        Debug.Log("Expecting input to load the next scene...");
        if (Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("Input received, loading the next scene: " + NextSceneName);
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
