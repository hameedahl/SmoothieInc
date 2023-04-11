using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkArrivedStatus : NetworkBehaviour
{
    public GameObject truckManagerObject;
    public GameObject gameHandlerObject;

    [SerializeField]
    [NetworkVariable]
    private NetworkVariableBool arrivedNetworkVariable = new NetworkVariableBool();

    [SerializeField]
   [NetworkVariable]
   private NetworkVariableBool drinkFinishedNetworkVariable = new NetworkVariableBool();

   [SerializeField]
   [NetworkVariable]
   private NetworkVariableDouble playerScoreNetworkVariable = new NetworkVariableDouble();

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
