using UnityEngine;

public class Barrel_Spawner : MonoBehaviour
{
    public float minTime = 1f;
    public float maxTime = 3f;
    private ObjectPool pool;
    static public bool secondPhase = false;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPool>().GetComponent<ObjectPool>();
        secondPhase = false;
    }

    private void Start() {
        Invoke(nameof(Spawn), 1);
    }

    private void Spawn() {
        GameObject barrel;
        if (secondPhase)
            barrel = pool.GetPooledObject(ObjectPool.BarrelType.Red);
        else
            barrel = pool.GetPooledObject(ObjectPool.BarrelType.Normal);
        barrel.transform.position = transform.position;

        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
}
