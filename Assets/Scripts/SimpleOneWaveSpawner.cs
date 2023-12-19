using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOneWaveSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    class SpawnItem
    {
        [SerializeField] internal GameObject prefab;
        [SerializeField] internal Transform spawnLocation;
    }
    [SerializeField] SpawnItem[] spawn;
    void Start()
    {
        for (int i = 0; i < spawn.Length; i++)
        {
            SpawnItem item = spawn[i];
            item.prefab.SetActive(true);
            Instantiate(item.prefab, item.spawnLocation.position, item.spawnLocation.rotation);
        }
    }
}
