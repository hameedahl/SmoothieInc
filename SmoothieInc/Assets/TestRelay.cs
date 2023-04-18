using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;

public class TestRelay : MonoBehaviour
{

  public TMP_Text joinCodeDisplay;
  public TMP_InputField joinCodeInput;

  public BooleanNetworkHandler bnh;

  private async void Start() {
    await UnityServices.InitializeAsync();

    AuthenticationService.Instance.SignedIn += () => {
      Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
    };
    await AuthenticationService.Instance.SignInAnonymouslyAsync();
  }

  public async void CreateRelay() {
    try {
      Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2);

      string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
      joinCodeDisplay.text = joinCode;
      Debug.Log(joinCode);

      RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
      // NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
      //   allocation.RelayServer.IpV4,
      //   (ushort)allocation.RelayServer.Port,
      //   allocation.AllocationIdBytes,
      //   allocation.Key,
      //   allocation.ConnectionData
      // );

      NetworkManager.Singleton.StartHost();

    } catch (RelayServiceException e) {
      Debug.Log(e);
    }
  }

  public async void JoinRelay(string joinCode) {

    try {
      Debug.Log("Joining Relay with " + joinCode);
      joinCode = joinCode.Substring(0,6);
      JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

      RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);


      // NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
      //   joinAllocation.RelayServer.IpV4,
      //   (ushort)joinAllocation.RelayServer.Port,
      //   joinAllocation.AllocationIdBytes,
      //   joinAllocation.Key,
      //   joinAllocation.ConnectionData,
      //   joinAllocation.HostConnectionData
      // );

      NetworkManager.Singleton.StartClient();
      
    } catch (RelayServiceException e) {
      Debug.Log(e);
    }
  }



}
