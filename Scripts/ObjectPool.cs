using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public enum BarrelType {
        Normal,
        Red
    };

    public List<GameObject> pooledBarrelObjects;
    public List<GameObject> pooledRedBarrelObjects;
    public GameObject barrelPrefab;
    public GameObject redBarrelPrefab;
    public int amountToPool = 10;

    private void Start() {
        pooledBarrelObjects = new List<GameObject>();
        pooledRedBarrelObjects = new List<GameObject>();
        GameObject barrel, redBarrel;
        for(int i = 0; i < amountToPool; i++) {
            barrel = Instantiate(barrelPrefab);
            barrel.SetActive(false);
            pooledBarrelObjects.Add(barrel);

            redBarrel = Instantiate(redBarrelPrefab);
            redBarrel.SetActive(false);
            pooledRedBarrelObjects.Add(redBarrel);
        }
    }

    public GameObject GetPooledObject(BarrelType type) {
        for(int i = 0; i < amountToPool; i++) {
            if(type == BarrelType.Normal) {
                if(!pooledBarrelObjects[i].activeInHierarchy) {
                    pooledBarrelObjects[i].SetActive(true);
                    return pooledBarrelObjects[i];
                }
            } else {
                if(!pooledRedBarrelObjects[i].activeInHierarchy) {
                    pooledRedBarrelObjects[i].SetActive(true);
                    return pooledRedBarrelObjects[i];
                }
            }
        }
        
        
        GameObject tmp = Instantiate(type == BarrelType.Normal ? barrelPrefab : redBarrelPrefab);
        pooledBarrelObjects.Add(tmp);
        return tmp;
    }
}
