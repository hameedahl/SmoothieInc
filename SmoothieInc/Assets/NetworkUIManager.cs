using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkUIManager : MonoBehaviour
{

  public Button hostButton;
  public Button clientButton;

  public Camera truckCamera;
  public Camera smoothieCamera;

  public GameObject truckUI;
  public GameObject smoothieUI;

  private void Awake()
  {

     hostButton.onClick.AddListener(() => {NetworkManager.Singleton.StartHost();
                                           truckCamera.gameObject.SetActive(true);
                                           truckUI.gameObject.SetActive(true);
                                            });
     clientButton.onClick.AddListener(() => {NetworkManager.Singleton.StartClient();
                                             smoothieCamera.gameObject.SetActive(true);
                                             smoothieUI.gameObject.SetActive(true);

                                            });


  }

}
