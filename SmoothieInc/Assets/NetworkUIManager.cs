using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class NetworkUIManager : MonoBehaviour
{

  public Button hostButton;
  public Button clientButton;
  public GameObject clientButtonObject;
  public GameObject joinCode;
  public GameObject joinCodeInputObject;
  public TMP_InputField joinCodeInput;
  public GameObject background;
  public BooleanNetworkHandler bnh;
  public GameObject joinCodeLabel;
  public GameObject closeButton;

  public Camera truckCamera;
  public Camera smoothieCamera;

  public GameObject truckUI;
  public GameObject smoothieUI;

  public bool developerMode = true;
  public bool developerIsDriver = true;

  public TestRelay testRelay;



  private void Awake()
  {
     hostButton.onClick.AddListener(() => {NetworkManager.Singleton.StartHost();
                                           truckCamera.gameObject.SetActive(true);
                                           truckUI.gameObject.SetActive(true);
                                           testRelay.CreateRelay();
                                            });

     clientButton.onClick.AddListener(() => {NetworkManager.Singleton.StartClient();
                                             smoothieCamera.gameObject.SetActive(true);
                                             smoothieUI.gameObject.SetActive(true);
                                             testRelay.JoinRelay(joinCodeInput.text);
                                            });
  }

  private void Start()
  {
    // StartCoroutine(waitThenStart());
  }

  // public void StartHost()
  // {
  //   Debug.Log("Client Conntected");
  //   truckUI.gameObject.SetActive(true);
  //   background.SetActive(false);
  //   joinCode.SetActive(false);
  //   closeButton.SetActive(false);
  //   joinCode.SetActive(false);
  //   joinCodeLabel.SetActive(false);
  // }

  // public void StartClient()
  // {
  //   NetworkManager.Singleton.StartClient();
  //   smoothieUI.gameObject.SetActive(true);
  //   testRelay.JoinRelay(joinCodeInput.text);
  //   background.SetActive(false);
  //   clientButtonObject.SetActive(false);
  //   joinCodeInputObject.SetActive(false);

  //   bnh.SmoothieStartServerRPC();
  // }

  private void FixedUpdate()
  {
    if(joinCodeInput.text.Length >= 6)
    {
        joinCodeInput.text = joinCodeInput.text.Substring(0,6);
        clientButton.interactable = true;
    }
    else
      clientButton.interactable = false;
  }

  // public IEnumerator waitThenStart()
  // {
  //   yield return new WaitForSeconds(0.1f);
  //   if(developerMode)
  //   {
  //     if(developerIsDriver)
  //     {
  //       StaticClass.CrossSceneInformation = "Driver";
  //     }
  //     else
  //     {
  //       StaticClass.CrossSceneInformation = "Smoothie";
  //     }
  //   }
  //   if(StaticClass.CrossSceneInformation == "Driver")
  //   {
  //     StartCoroutine(WaitThenCreate());
  //     NetworkManager.Singleton.StartHost();
  //     truckCamera.gameObject.SetActive(true);
  //     joinCode.SetActive(true);
  //     joinCodeLabel.SetActive(true);
  //     closeButton.SetActive(true);
  //   }
  //   if(StaticClass.CrossSceneInformation == "Smoothie")
  //   {
  //     smoothieCamera.gameObject.SetActive(true);
  //     clientButtonObject.SetActive(true);
  //     joinCodeInputObject.SetActive(true);
  //   }
  // }

  // IEnumerator WaitThenCreate()
  // {
  //   yield return new WaitForSeconds(1f);
  //   testRelay.CreateRelay();
  // }

}
