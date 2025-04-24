using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleGunShooting();
    }

    void HandleGunShooting() {
        if(Input.GetButtonDown("Fire1")) {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        }
    }
}
