using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Package : MonoBehaviour
{
    [SerializeField] private GameObject groundDetector;
    bool isGDActive = true;
    [SerializeField] UnityEvent<Vector3> PackageOpened;

    private void Awake()
    {
        PackageOpened.AddListener(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SpawnCrystal);
    }

    void OnDestroy()
    {
        PackageOpened.Invoke(new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, 0f));
        PackageOpened.RemoveAllListeners();
    }

    private void OnTransformParentChanged()
    {
        isGDActive = !isGDActive;
        groundDetector.SetActive(isGDActive);
    }
}
