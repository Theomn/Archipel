using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : SingletonMonoBehaviour<PauseMenu>
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private AK.Wwise.RTPC masterRTPC;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private AK.Wwise.RTPC musicRTPC;
    [SerializeField] private Toggle textShakeToggle;
    [SerializeField] private TMP_Dropdown inputTypeDropdown;
    [SerializeField] private Button backButton, quitButton;
    private bool isActive;
    private const string masterVolume = "Master Volume";
    private const string musicVolume = "Music Volume";
    private const string inputType = "Input Type";
    private const string textShake = "Text Shake";

    void Start()
    {
        Close();
        // Set initial values for the sliders and toggle
        masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolume, 100);
        masterRTPC.SetGlobalValue(PlayerPrefs.GetFloat(masterVolume, 100));
        musicVolumeSlider.value = PlayerPrefs.GetFloat(musicVolume, 100);
        musicRTPC.SetGlobalValue(PlayerPrefs.GetFloat(musicVolume, 100));
        textShakeToggle.isOn = PlayerPrefs.GetInt(textShake, AlienVision.instance.textShake? 1 : 0) != 0;
        AlienVision.instance.textShake = PlayerPrefs.GetInt(textShake, AlienVision.instance.textShake? 1 : 0) != 0;
        inputTypeDropdown.value = PlayerPrefs.GetInt(inputType, 1);

        // Set up event listeners for the sliders and toggle
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        textShakeToggle.onValueChanged.AddListener(SetTextShake);
        inputTypeDropdown.onValueChanged.AddListener(SetInputType);
        backButton.onClick.AddListener(Close);
        quitButton.onClick.AddListener(QuitApplication);
    }

    void Update()
    {
        if (isActive && Input.GetButtonDown(ButtonName.cancel))
        {
            Close();
        }
    }

    public void Open()
    {
        isActive = true;
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(masterVolumeSlider.gameObject);
    }

    public void Close()
    {
        isActive = false;
        if (ControlToggle.isActive) ControlToggle.Unpause();
        gameObject.SetActive(false);
    }

    void SetMasterVolume(float value)
    {
        masterRTPC.SetGlobalValue(value);
        PlayerPrefs.SetFloat(masterVolume, value);
        PlayerPrefs.Save();
    }

    void SetMusicVolume(float value)
    {
        musicRTPC.SetGlobalValue(value);
        PlayerPrefs.SetFloat(musicVolume, value);
        PlayerPrefs.Save();
    }

    void SetTextShake(bool value)
    {
        AlienVision.instance.textShake = value ? true : false;
        PlayerPrefs.SetInt(textShake, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    void QuitApplication()
    {
        Application.Quit();
    }

    public void SetInputType(int value)
    {
        PlayerPrefs.SetInt(inputType, value);
        PlayerPrefs.Save();
    }
}
