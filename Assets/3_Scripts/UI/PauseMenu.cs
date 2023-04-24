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

    [SerializeField] private AK.Wwise.Event openEvent, closeEvent, hoverEvent, clickEvent, sliderEvent;
    private bool isActive;
    public const string masterVolume = "Master Volume";
    public const string musicVolume = "Music Volume";
    public const string inputType = "Input Type";
    public const string textShake = "Text Shake";

    void Start()
    {
        Close();
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
        GameController.instance.ShowCursor(true);
        openEvent.Post(gameObject);
        InitializeValues();
        EventSystem.current.SetSelectedGameObject(masterVolumeSlider.gameObject);
        if (HUDController.instance) HUDController.instance.ShowInputs(false);

    }

    private void InitializeValues()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolume, 100);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(musicVolume, 100);
        textShakeToggle.isOn = PlayerPrefs.GetInt(textShake, 1) != 0;
        inputTypeDropdown.value = PlayerPrefs.GetInt(inputType, 0);
    }

    public void Close()
    {
        isActive = false;
        if (PlayerController.instance) GameController.instance.ShowCursor(false);
        closeEvent.Post(gameObject);
        if (ControlToggle.isActive) ControlToggle.Unpause();
        if (HUDController.instance) HUDController.instance.ShowInputs(true);
        gameObject.SetActive(false);
    }

    void SetMasterVolume(float value)
    {
        sliderEvent.Post(gameObject);
        masterRTPC.SetGlobalValue(value);
        PlayerPrefs.SetFloat(masterVolume, value);
        PlayerPrefs.Save();
    }

    void SetMusicVolume(float value)
    {
        sliderEvent.Post(gameObject);
        musicRTPC.SetGlobalValue(value);
        PlayerPrefs.SetFloat(musicVolume, value);
        PlayerPrefs.Save();
    }

    void SetTextShake(bool value)
    {
        clickEvent.Post(gameObject);
        AlienVision.instance.textShake = value ? true : false;
        PlayerPrefs.SetInt(textShake, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    void QuitApplication()
    {
        clickEvent.Post(gameObject);
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void SetInputType(int value)
    {
        clickEvent.Post(gameObject);
        PlayerPrefs.SetInt(inputType, value);
        if(HUDController.instance) HUDController.instance.ChangeInputType((InputType)value);
        if (TutorialManager.instance) TutorialManager.instance.ChangeInputType((InputType)value);
        PlayerPrefs.Save();
    }
}
