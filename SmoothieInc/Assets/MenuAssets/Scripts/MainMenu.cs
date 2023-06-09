using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;
//using UnityEditor.VersionControl;

public class MainMenu : MonoBehaviour
{
    [Header("Mixers")]
    public AudioMixer audioMixer;
    public AudioMixer smoothieMixer;

    [Header("Main Menu UI")]
    public GameObject hostButton;
    public GameObject clientButton;
    public GameObject creditsButton;
    public GameObject title;
    public GameObject mainMenuBackground;

    [Header("Join Menu UI")]
    public GameObject joinCodeInputBox;
    public TMP_InputField joinCodeInput;
    public GameObject joinButton;
    public GameObject joinCodeDisplay;
    public GameObject joinMenuBackground;
    public GameObject closeButton;
    public GameObject drivingControls;
    public GameObject giveCodeDirections;

    [Header("Tutorial UI")]
    public GameObject SmoothieTut0;
    public GameObject SmoothieTut1;
    public GameObject SmoothieTut2;
    public GameObject SmoothieTut3;

    private int tutSlide = 0;
    private bool isSmoothieTut = false;

    [Header("Object References")]
    public Camera truckCamera;
    public Camera smoothieCamera;
    public Camera menuCamera;

    public GameObject truckUI;
    public GameObject smoothieUI;
    public AudioSource driverMusic;
    public GameObject smoothieMusicGO;
    private AudioSource smoothieMusic;

    public MainGameController mainGameController;

    public GameObject dialogueMenu;

    public TestRelay testRelay;

    [Header("Pause Menu")]
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public static float volumeLevel = 1.0f;
    private Slider sliderVolumeCtrl;
    public GameObject currentUI;
    public GameObject credits;


    void Awake()
    {
        audioMixer.SetFloat("SFXVolume", -80.00f);
        GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
        if (sliderTemp != null)
        {
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
        smoothieMusic = smoothieMusicGO.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Hide Join Menu
        toggleJoinMenu(false, false);

        // Show Main Menu
        toggleMainMenu(false);

        dialogueMenu.SetActive(true);

        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

        GameisPaused = false;
    }

    public void StartMenu()
    {
        // Show Main Menu
        toggleMainMenu(true);
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
    }

    private void Update()
    {
        if (isSmoothieTut && (Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.Return)))
        {
            tutSlide++;
            if (tutSlide == 4)
            {
                isSmoothieTut = false;
                StartClient();
            }
            else
            {
                StartSmoothieTut(tutSlide);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    // enable or disable the join menu
    void toggleJoinMenu(bool enable, bool driver)
    {
        joinMenuBackground.SetActive(enable);
        if (enable)
        {
            if (!driver)
            {
                joinCodeInputBox.SetActive(enable);
                joinButton.SetActive(enable);
            }
            else
            {
                joinCodeDisplay.SetActive(enable);
                //closeButton.SetActive(enable);
                drivingControls.SetActive(enable);
                giveCodeDirections.SetActive(enable);
            }
        }
        else
        {
            giveCodeDirections.SetActive(enable);
            drivingControls.SetActive(enable);
            joinCodeInputBox.SetActive(enable);
            joinButton.SetActive(enable);
            joinCodeDisplay.SetActive(enable);
            //closeButton.SetActive(enable);
        }
    }

    // enable or disable the main menu
    void toggleMainMenu(bool enable)
    {
        hostButton.SetActive(enable);
        clientButton.SetActive(enable);
        creditsButton.SetActive(enable);
        title.SetActive(enable);
        mainMenuBackground.SetActive(enable);
    }

    public void StartHostMenu()
    {
        NetworkManager.Singleton.StartHost();
        testRelay.CreateRelay();
        toggleJoinMenu(true, true);
        toggleMainMenu(false);
    }

    public void StartHost()
    {
        menuCamera.gameObject.SetActive(false);
        truckCamera.gameObject.SetActive(true);
        truckUI.gameObject.SetActive(true);
        toggleJoinMenu(false, false);
        audioMixer.SetFloat("SFXVolume", 1.0f);
        if (!driverMusic.isPlaying)
        {
            driverMusic.Play();
        }
    }

    public void StartClientMenu()
    {
        toggleJoinMenu(true, false);
        toggleMainMenu(false);
    }

    public void StartSmoothieTut(int slideNum)
    {
        if (!smoothieMusic.isPlaying)
        {
            smoothieMusic.Play();
            audioMixer.SetFloat("TruckMaster", -80.00f);
        }

        isSmoothieTut = true;
        if (slideNum == 0)
        {
            toggleJoinMenu(false, false);
            SmoothieTut0.gameObject.SetActive(true);
        }
        else if (slideNum == 1)
        {
            SmoothieTut0.gameObject.SetActive(false);
            SmoothieTut1.gameObject.SetActive(true);
        }
        else if (slideNum == 2)
        {
            SmoothieTut1.gameObject.SetActive(false);
            SmoothieTut2.gameObject.SetActive(true);
        }
        else
        {
            SmoothieTut2.gameObject.SetActive(false);
            SmoothieTut3.gameObject.SetActive(true);
        }
    }

    public void StartClient()
    {
        SmoothieTut3.gameObject.SetActive(false);
        menuCamera.gameObject.SetActive(false);
        NetworkManager.Singleton.StartClient();
        smoothieCamera.gameObject.SetActive(true);
        smoothieUI.gameObject.SetActive(true);
        testRelay.JoinRelay(joinCodeInput.text);
    }

    private void FixedUpdate()
    {
        if (joinCodeInput.text.Length >= 6)
        {
            joinCodeInput.text = joinCodeInput.text.Substring(0, 6);
            joinButton.GetComponent<Button>().interactable = true;
        }
        else
            joinButton.GetComponent<Button>().interactable = false;
    }

    public void toggleCredits(bool enable)
    {
        if (enable)
        {
            credits.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;

        }
        if (!enable)
        {
            credits.SetActive(false);
            Time.timeScale = 1f;
            GameisPaused = false;

        }

    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsHost && clientId != NetworkManager.Singleton.LocalClientId)
        {
            StartHost();
            mainGameController.StartGame(210.0f);
        }

    }

    private void OnServerStarted()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            Debug.Log("Waiting for client");
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void CreditsBack()
    {
        credits.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void StartGame()
    {

    }

    public void SetLevel(float sliderValue)
    {
        smoothieMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        volumeLevel = sliderValue;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif

        //SceneManager.LoadScene(0);
    }

}
