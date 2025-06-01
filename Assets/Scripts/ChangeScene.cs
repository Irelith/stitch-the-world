using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour{

    public string NextSceneName;
    [SerializeField] private GameObject pauseMenuUI;

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(NextSceneName);
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Game has been closed.");
    }
}
