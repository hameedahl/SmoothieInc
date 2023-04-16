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

    private NetworkVariable<int> valuesArrayNetwork0 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork1 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork2 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork3 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork4 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork5 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork6 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork7 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork8 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork9 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork10 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private NetworkVariable<int> valuesArrayNetwork11 = new NetworkVariable<int>(
      value:0,
      NetworkVariableReadPermission.Everyone,
      NetworkVariableWritePermission.Owner
    );

    private TruckManager truckManager;
    public NetworkUIManager nuim;
    private GameHandler gameHandler;
    public Transform target;

    public int[] tempValuesArray = new int[12];
    public int[] valuesArray;

    private void Start()
    {
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

    public int[] GetValuesArrayFromNetwork()
    {

      return tempValuesArray;
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

    [ServerRpc(RequireOwnership = false)]
    public void SmoothieStartServerRPC(ServerRpcParams serverRpcParams = default)
    {
      var clientId = serverRpcParams.Receive.SenderClientId;
      if (NetworkManager.ConnectedClients.ContainsKey(clientId))
      {
          nuim.StartHost();
      }
    }
}
