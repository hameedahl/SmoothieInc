using UnityEngine;
using Unity.Netcode;

public class MinimapNetworkObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minimapNetworkObjectPrefab;

    private void Start()
    {
        if (NetworkManager.Singleton.IsListening)
        {
            Instantiate(minimapNetworkObjectPrefab);
        }
    }
}
