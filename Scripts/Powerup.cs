using UnityEngine;

public class Powerup : MonoBehaviour
{

    public float rotationSpeed = 90f;

    private void Update() {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
   
}
