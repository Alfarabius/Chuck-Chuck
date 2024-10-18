using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;

    public GameObject Spawn()
    {
        return Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}
