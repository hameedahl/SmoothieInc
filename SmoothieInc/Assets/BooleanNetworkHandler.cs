using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BooleanNetworkHandler : NetworkBehaviour
{
    public GameObject truckManagerObject;
    public GameObject gameHandlerObject;

    bool started = false;

    private NetworkVariable<bool> arrivedNetworkVariable = new NetworkVariable<bool>(
      value:false,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<bool> drinkFinishedNetworkVariable = new NetworkVariable<bool>(
      value:false,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );
    private NetworkVariable<double> playerScoreNetworkVariable = new NetworkVariable<double>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );
    private NetworkVariable<Vector3> destinationPos = new NetworkVariable<Vector3>(
      value:new Vector3(0,0,0),
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private TruckManager truckManager;
    private GameHandler gameHandler;
    public Transform target;

    private void Start()
    {
        truckManager = truckManagerObject.GetComponent<TruckManager>();
        gameHandler = gameHandlerObject.GetComponent<GameHandler>();
    }

    private void Update()
    {
      if (IsHost)
      {
          if(!started)
          {
            started = true;
            truckManager.host = true;
            destinationPos.Value = truckManager.NewOrder();
          }

          bool isTruckArrived = truckManager.GetArrivedStatus();
          arrivedNetworkVariable.Value = isTruckArrived;

          // bool isDrinkFinished = gameHandler.GetDrinkFinished();
          // drinkFinishedNetworkVariable.Value = isDrinkFinished;

          // double currentPlayerScore = gameHandler.GetPlayerScore();
          // playerScoreNetworkVariable.Value = currentPlayerScore;

      }
      else
      {
        if(destinationPos.Value.x != 0)
          target.position = destinationPos.Value;
      }
    }

    public bool GetArrivedStatus()
    {
        return arrivedNetworkVariable.Value;
    }

    public bool GetDrinkFinishedStatus()
    {
        return drinkFinishedNetworkVariable.Value;
    }

    public double GetPlayerScoreStatus()
    {
        return playerScoreNetworkVariable.Value;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetSmoothieServerRPC(bool finished, double points, ServerRpcParams serverRpcParams = default)
    {
      var clientId = serverRpcParams.Receive.SenderClientId;
      if (NetworkManager.ConnectedClients.ContainsKey(clientId))
      {
          var client = NetworkManager.ConnectedClients[clientId];
          drinkFinishedNetworkVariable.Value = finished;
          playerScoreNetworkVariable.Value = points;
      }
    }
}
