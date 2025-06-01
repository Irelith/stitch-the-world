using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public string MenuSelectionName = "MenuSelection";

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)){
            if (GameIsPaused){
                Debug.Log("GameIsPaused: " + GameIsPaused);
                ContinueGame();
            }else{
                PauseGame();
            }
        }
    }

    public void ContinueGame(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Debug.Log("Game is resumed.");
    }

    void PauseGame(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        
        Debug.Log("Game is paused.");
    }

    public void RestartLevel(){
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenuSelection(){
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(MenuSelectionName);
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Game has been closed.");
    }
}
