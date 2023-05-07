using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.XR;

public class MatchTimer : NetworkBehaviour
{

  public NetworkVariable<float> RemainingTime = new NetworkVariable<float>(
    value:60.0f,
    NetworkVariableReadPermission.Everyone,
    NetworkVariableWritePermission.Owner
  );

    public TextMeshProUGUI RemainingTimeText;
    public TextMeshProUGUI RemainingTimeText2;

    public GameObject LoseScreen;
    public BooleanNetworkHandler networkHandler;
    public Text loseText;


    public bool isTimerStarted = false;


    private void Update() {

      if (IsHost && isTimerStarted) {
        RemainingTime.Value -= Time.deltaTime;
        if (RemainingTime.Value <= 0) {
          RemainingTime.Value = 0;
          EndMatchServerRpc();
        }
      }

      if (IsClient) {
        UpdateRemainingTimeText();
      }
    }

    private void UpdateRemainingTimeText()
    {
        int minutes = Mathf.FloorToInt(RemainingTime.Value / 60);
        int seconds = Mathf.FloorToInt(RemainingTime.Value % 60);
        RemainingTimeText.text = $"{minutes:D2}:{seconds:D2}";
        RemainingTimeText2.text = $"{minutes:D2}:{seconds:D2}";
    }

    [ServerRpc(RequireOwnership = false)]
    private void EndMatchServerRpc()
    {
        EndMatchClientRpc();
    }

    [ClientRpc]
    private void EndMatchClientRpc()
    {
        networkHandler.SetLostStatus(true);
        LoseScreen.gameObject.SetActive(true);
        loseText.text = "Looks like your customer is upset because they had to wait a long time. They're asking for a free smoothie to make up for it.";
    }

    public void ResetTimer()
    {
        if (IsHost) {
            RemainingTime.Value = 60.0f;
        }
    }

    public void StartTimer()
    {
      isTimerStarted = true;

    }



}
