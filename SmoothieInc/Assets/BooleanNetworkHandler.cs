using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BooleanNetworkHandler : NetworkBehaviour
{
    public GameObject truckManagerObject;
    public GameObject gameHandlerObject;
    public FillCard fillCard;

    bool started = false;

    // private NetworkVariable<bool> clientJoined = new NetworkVariable<bool>(
    //   value:false,
    //   NetworkVariableReadPermission.Everyone,
    //   NetworkVariableWritePermission.Owner
    // );

    public NetworkVariable<bool> arrivedNetworkVariable = new NetworkVariable<bool>(
      value:false,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<bool> finalWinNetworkVariable = new NetworkVariable<bool>(
      value:false,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<bool> lostNetworkVariable = new NetworkVariable<bool>(
      value:false,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<bool> drinkFinishedNetworkVariable = new NetworkVariable<bool>(
      value:false,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );
    public NetworkVariable<double> playerScoreNetworkVariable = new NetworkVariable<double>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<double> playerTipNetworkVariable = new NetworkVariable<double>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<double> playerBestScoreNetworkVariable = new NetworkVariable<double>(
      value: 0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<double> playerTotalMoneyNetworkVariable = new NetworkVariable<double>(
      value: 0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<double> playerBestTimeNetworkVariable = new NetworkVariable<double>(
      value: 0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<double> playerBestTimeMinNetworkVariable = new NetworkVariable<double>(
      value: 0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<double> playerBestTimeSecNetworkVariable = new NetworkVariable<double>(
      value: 0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<Vector3> destinationPos = new NetworkVariable<Vector3>(
      value:new Vector3(0,0,0),
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork0 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork1 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork2 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork3 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork4 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork5 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork6 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork7 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork8 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork9 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork10 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public NetworkVariable<int> valuesArrayNetwork11 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    public TruckManager truckManager;
    public NetworkUIManager nuim;
    public GameHandler gameHandler;
    public Transform target;

    public int[] tempValuesArray = new int[12];
    public int[] valuesArray;

    int diff = 3;

    private void Start()
    {
        diff = Random.Range(1,3);
        truckManager = truckManagerObject.GetComponent<TruckManager>();
        gameHandler = gameHandlerObject.GetComponent<GameHandler>();

        if (IsHost) {

          valuesArray = gameHandler.GetValuesArray();
          valuesArrayNetwork0.Value = valuesArray[0];
          valuesArrayNetwork1.Value = valuesArray[1];
          valuesArrayNetwork2.Value = valuesArray[2];
          valuesArrayNetwork3.Value = valuesArray[3];
          valuesArrayNetwork4.Value = valuesArray[4];
          valuesArrayNetwork5.Value = valuesArray[5];
          valuesArrayNetwork6.Value = valuesArray[6];
          valuesArrayNetwork7.Value = valuesArray[7];
          valuesArrayNetwork8.Value = valuesArray[8];
          valuesArrayNetwork9.Value = valuesArray[9];
          valuesArrayNetwork10.Value = valuesArray[10];
          valuesArrayNetwork11.Value = valuesArray[11];

            //Debug.Log("HIII");
        }
        tempValuesArray[0] = valuesArrayNetwork0.Value;
        tempValuesArray[1] = valuesArrayNetwork1.Value;
        tempValuesArray[2] = valuesArrayNetwork2.Value;
        tempValuesArray[3] = valuesArrayNetwork3.Value;
        tempValuesArray[4] = valuesArrayNetwork4.Value;
        tempValuesArray[5] = valuesArrayNetwork5.Value;
        tempValuesArray[6] = valuesArrayNetwork6.Value;
        tempValuesArray[7] = valuesArrayNetwork7.Value;
        tempValuesArray[8] = valuesArrayNetwork8.Value;
        tempValuesArray[9] = valuesArrayNetwork9.Value;
        tempValuesArray[10] = valuesArrayNetwork10.Value;
        tempValuesArray[11] = valuesArrayNetwork11.Value;

        fillCard.Initialize(tempValuesArray);
    }

    private void Update()
    {
      if (IsHost)
      {
          if(!started)
          {
            started = true;
            truckManager.host = true;
            destinationPos.Value = truckManager.NewOrder(3);
          }

          // bool isTruckArrived = truckManager.GetArrivedStatus();
          // arrivedNetworkVariable.Value = isTruckArrived;


          // bool isDrinkFinished = gameHandler.GetDrinkFinished();
          // drinkFinishedNetworkVariable.Value = isDrinkFinished;

          // double currentPlayerScore = gameHandler.GetPlayerScore();
          // playerScoreNetworkVariable.Value = currentPlayerScore;


          valuesArray = gameHandler.GetValuesArray();
          valuesArrayNetwork0.Value = valuesArray[0];
          valuesArrayNetwork1.Value = valuesArray[1];
          valuesArrayNetwork2.Value = valuesArray[2];
          valuesArrayNetwork3.Value = valuesArray[3];
          valuesArrayNetwork4.Value = valuesArray[4];
          valuesArrayNetwork5.Value = valuesArray[5];
          valuesArrayNetwork6.Value = valuesArray[6];
          valuesArrayNetwork7.Value = valuesArray[7];
          valuesArrayNetwork8.Value = valuesArray[8];
          valuesArrayNetwork9.Value = valuesArray[9];
          valuesArrayNetwork10.Value = valuesArray[10];
          valuesArrayNetwork11.Value = valuesArray[11];
        }
      else
      {
        if(destinationPos.Value.x != 0)
          target.position = destinationPos.Value;
      }

        tempValuesArray[0] = valuesArrayNetwork0.Value;
        tempValuesArray[1] = valuesArrayNetwork1.Value;
        tempValuesArray[2] = valuesArrayNetwork2.Value;
        tempValuesArray[3] = valuesArrayNetwork3.Value;
        tempValuesArray[4] = valuesArrayNetwork4.Value;
        tempValuesArray[5] = valuesArrayNetwork5.Value;
        tempValuesArray[6] = valuesArrayNetwork6.Value;
        tempValuesArray[7] = valuesArrayNetwork7.Value;
        tempValuesArray[8] = valuesArrayNetwork8.Value;
        tempValuesArray[9] = valuesArrayNetwork9.Value;
        tempValuesArray[10] = valuesArrayNetwork10.Value;
        tempValuesArray[11] = valuesArrayNetwork11.Value;

        fillCard.Initialize(tempValuesArray);
    }

    public void ResetOrders()
    {
        if(IsHost)
        {
            arrivedNetworkVariable.Value = false;
            drinkFinishedNetworkVariable.Value = false;
        }
    }

    public void SetDestination(Vector3 pos)
    {
      destinationPos.Value = pos;
    }

    public bool GetArrivedStatus()
    {
        return arrivedNetworkVariable.Value;
    }

    public void SetArrivedStatus(bool arrived)
    {
        arrivedNetworkVariable.Value = arrived;
    }

    public void SetLostStatus(bool lost)
    {
        lostNetworkVariable.Value = lost;
    }

    public bool GetLostStatus()
    {
        return lostNetworkVariable.Value;
    }

    public void SetFinalWin(bool win)
    {
        finalWinNetworkVariable.Value = win;
    }

    public bool GetFinalWin()
    {
        return finalWinNetworkVariable.Value;
    }

    public bool GetDrinkFinishedStatus()
    {
        return drinkFinishedNetworkVariable.Value;
    }

    public double GetPlayerScoreStatus()
    {
        return playerScoreNetworkVariable.Value;
    }

    public double GetPlayerTipStatus()
    {
      return playerTipNetworkVariable.Value;
    }

    public double GetBestScoreStatus()
    {
        return playerBestScoreNetworkVariable.Value;
    }

    public double GetTotalMoney()
    {
        return playerTotalMoneyNetworkVariable.Value;
    }

    public double GetBestTimeSec()
    {
        return playerBestTimeSecNetworkVariable.Value;
    }

    public double GetBestTimeMin()
    {
        return playerBestTimeMinNetworkVariable.Value;
    }

    public bool GetHostStatus()
    {
      if(IsHost)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public int[] GetValuesArrayFromNetwork()
    {
        return tempValuesArray;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetSmoothieServerRPC(bool finished, double points, double tip, double bestScore, double totalMoney, double bestTimeMin, double bestTimeSec, ServerRpcParams serverRpcParams = default)
    {
      var clientId = serverRpcParams.Receive.SenderClientId;
      if (NetworkManager.ConnectedClients.ContainsKey(clientId))
      {
          var client = NetworkManager.ConnectedClients[clientId];
          drinkFinishedNetworkVariable.Value = finished;
          playerScoreNetworkVariable.Value = points;
          playerTipNetworkVariable.Value = tip;
          playerBestScoreNetworkVariable.Value = bestScore;
          playerTotalMoneyNetworkVariable.Value = totalMoney;
          playerBestTimeMinNetworkVariable.Value = bestTimeMin;
          playerBestTimeSecNetworkVariable.Value = bestTimeSec;


            //playerBestTimeNetworkVariable.Value = bestTime;
        }
    }

    // [ServerRpc(RequireOwnership = false)]
    // public void SmoothieStartServerRPC(ServerRpcParams serverRpcParams = default)
    // {
    //   var clientId = serverRpcParams.Receive.SenderClientId;
    //   if (NetworkManager.ConnectedClients.ContainsKey(clientId))
    //   {
    //       Debug.Log("HI");

    //       nuim.StartHost();
    //   }
    // }
}
