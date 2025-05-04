using UnityEngine;

public class CameraScript : MonoBehaviour{

    public GameObject Player; // Reference to the Player object

    // Update is called once per frame
    void Update(){
        Vector3 position = transform.position; // Get the current position of the camera
        position.x = Player.transform.position.x; // Get the x position to the Player's x position
        transform.position = position; // Set the camera's position to the new position
    }
}
