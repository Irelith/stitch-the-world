using UnityEngine;
using UnityEngine.SceneManagement;

public class TentScript : MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // public Image FadeImage;
    // public float FadeDuration = 1f;
    public string NextSceneName = "LevelCompleted";

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            // StartCoroutine(FadeOutAndLoadScene());
            SceneManager.LoadScene(NextSceneName);
        }
    }

    // private IEnumerator FadeOutAndLoadScene(){
    //     float timer = 0f;
    //     Color color = FadeImage.color;
        
    //     while (timer < FadeDuration){
    //         timer += Time.deltaTime;
    //         float changeColor = Mathf.Clamp01(timer / FadeDuration);
    //         FadeImage.color = new Color(color.r, color.g, color.b, changeColor);
    //         yield return null;
    //     }
    //     SceneManager.LoadScene(NextSceneName);
    // }
}
