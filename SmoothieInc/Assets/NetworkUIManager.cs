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
  public TMP_InputField joinCodeInput;

  public Camera truckCamera;
  public Camera smoothieCamera;

  public GameObject truckUI;
  public GameObject smoothieUI;


  public TestRelay testRelay;



  private void Awake()
  {
    //  hostButton.onClick.AddListener(() => {NetworkManager.Singleton.StartHost();
    //                                        truckCamera.gameObject.SetActive(true);
    //                                        truckUI.gameObject.SetActive(true);
    //                                        testRelay.CreateRelay();
    //                                         });

    //  clientButton.onClick.AddListener(() => {NetworkManager.Singleton.StartClient();
    //                                          smoothieCamera.gameObject.SetActive(true);
    //                                          smoothieUI.gameObject.SetActive(true);
    //                                          testRelay.JoinRelay(joinCodeInput.text);
    //                                         });
  }

  private void Start()
  {
    if(StaticClass.CrossSceneInformation == "Driver")
    {
      NetworkManager.Singleton.StartHost();
      truckCamera.gameObject.SetActive(true);
      truckUI.gameObject.SetActive(true);
      testRelay.CreateRelay();
    }
    if(StaticClass.CrossSceneInformation == "Smoothie")
    {
      NetworkManager.Singleton.StartClient();
      smoothieCamera.gameObject.SetActive(true);
      smoothieUI.gameObject.SetActive(true);
      testRelay.JoinRelay(joinCodeInput.text);
    }
  }

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

}
