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

    private TruckManager truckManager;
    private GameHandler gameHandler;

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
            truckManager.NewOrder();
          }

          bool isTruckArrived = truckManager.GetArrivedStatus();
          arrivedNetworkVariable.Value = isTruckArrived;

          bool isDrinkFinished = gameHandler.GetDrinkFinished();
          drinkFinishedNetworkVariable.Value = isDrinkFinished;

          double currentPlayerScore = gameHandler.GetPlayerScore();
          playerScoreNetworkVariable.Value = currentPlayerScore;
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
}
