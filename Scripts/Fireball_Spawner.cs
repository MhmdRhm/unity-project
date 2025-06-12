using UnityEngine;

public class Fireball_Spawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform kongTransform;
    static public bool thirdPhase = false;
    public float minTime = 3f;
    public float maxTime = 5f;

    private void Awake() {
        thirdPhase = false;
    }
    
    private void Start()
    {
        Invoke(nameof(Spawn), 0);
    }

    private void Spawn()
    {
        Debug.Log(thirdPhase);
        if (thirdPhase)
        {
            Instantiate(fireballPrefab, kongTransform.position, Quaternion.Euler(0, 0, -180));
        }
        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
}
