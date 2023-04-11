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

/*
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Services.Relay;

public class NetworkManagerCustom : MonoBehaviour
{
    public Button hostButton;
    public Button clientButton;

    public Camera truckCamera;
    public Camera smoothieCamera;

    public GameObject truckUI;
    public GameObject smoothieUI;

    [SerializeField] private InputField joinCodeInputField;
    [SerializeField] private Text joinCodeText;

    private void Start()
    {
        hostButton.onClick.AddListener(() =>
        {
            StartHost();
            truckCamera.gameObject.SetActive(true);
            truckUI.gameObject.SetActive(true);
        });

        clientButton.onClick.AddListener(() =>
        {
            StartClient();
            smoothieCamera.gameObject.SetActive(true);
            smoothieUI.gameObject.SetActive(true);
        });
    }

    public void StartHost()
    {
        StartCoroutine(RequestJoinCode());
    }

    private IEnumerator RequestJoinCode()
    {
        var request = new RelayServerAllocationRequest();
        var handle = RelayService.Instance.CreateRelayServer(request);
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var response = handle.Result;
            NetworkManager.Singleton.GetComponent<UnityTransport>().ServerListenPort = response.Port;
            joinCodeText.text = $"Join Code: {response.JoinCode}";
            NetworkManager.Singleton.StartHost();
        }
    }

    public void StartClient()
    {
        string joinCode = joinCodeInputField.text;
        if (!string.IsNullOrEmpty(joinCode))
        {
            StartCoroutine(ConnectWithJoinCode(joinCode));
        }
    }

    private IEnumerator ConnectWithJoinCode(string joinCode)
    {
        var request = new RelayJoinCodeRequest { JoinCode = joinCode };
        var handle = RelayService.Instance.JoinRelayServerWithJoinCode(request);
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var response = handle.Result;
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectAddress = response.IpAddress;
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectPort = response.Port;
            NetworkManager.Singleton.StartClient();
        }
    }
}
*/
