using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

public class MatchTimer : NetworkBehaviour
{

  public NetworkVariable<float> RemainingTime = new NetworkVariable<float>(
    value:60.0f,
    NetworkVariableReadPermission.Everyone,
    NetworkVariableWritePermission.Owner
  );

    public TextMeshProUGUI RemainingTimeText;

    private void Update() {
      if (IsHost) {
        RemainingTime.Value -= Time.deltaTime;
        if (RemainingTime.Value <= 0) {
          RemainingTime.Value = 0;
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
    }


}
