using UnityEngine;

public class ChangeBackgroundScript : MonoBehaviour{
    
    public GameObject FirstLayer;
    // public GameObject[] SecondLayers;
    public GameObject SecondLayer_0;
    public GameObject SecondLayer_1;
    public GameObject SecondLayer_2;
    public GameObject SecondLayer_3;

    private int CurrentSecondLayer = 0;

    public void ChangeBackground(){
        // Deactivate the current second layer
        if (CurrentSecondLayer == 0){
            SecondLayer_0.SetActive(false);
            SecondLayer_1.SetActive(true);
            CurrentSecondLayer = 1;
        } else if (CurrentSecondLayer == 1){
            SecondLayer_1.SetActive(false);
            SecondLayer_2.SetActive(true);
            CurrentSecondLayer = 2;
        } else if (CurrentSecondLayer == 2){
            SecondLayer_2.SetActive(false);
            SecondLayer_3.SetActive(true);
            CurrentSecondLayer = 3;
        } else if (CurrentSecondLayer == 3){
            FirstLayer.SetActive(false);
            SecondLayer_3.SetActive(false);
        }
    }
}
