using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkArrivedStatus : NetworkBehaviour
{
    public GameObject truckManagerObject;

    [SerializeField]
    [NetworkVariable]
    private NetworkVariableBool arrivedNetworkVariable = new NetworkVariableBool();

    private TruckManager truckManager;

    private void Start()
    {
        truckManager = truckManagerObject.GetComponent<TruckManager>();
    }

    private void Update()
    {
        if (IsServer)
        {
            bool isTruckArrived = truckManager.GetArrivedStatus();
            arrivedNetworkVariable.Value = isTruckArrived;
        }
    }

    public bool GetArrivedStatus()
    {
        return arrivedNetworkVariable.Value;
    }
}
