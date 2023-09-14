using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Inspectable]
public class ButtonHandler : MonoBehaviour
{
    public GameObject interactableObject;
    public GameObject indicator;

    private Color oldColor;
    private float timer = 0;
    private float timerComp;
    private float offsetTimer = 0;
    private float offsetComp;

    private Button trackedButton;
    private Slider trackedSlider;
    private LevelMap map;
    private bool waitingForInput;
    private bool sliderUp;
    private bool offsetIndicator = false;

    public enum ButtonType
    {
        button,
        slider,
        simonSays,
        word,
        greenButton
    }

    public ButtonType type;

    private void Start()
    {
        map = FindObjectOfType<LevelMap>();
        switch (type)
        {
            case ButtonType.button:
                trackedButton = interactableObject.GetComponent<Button>();
                break;
            case ButtonType.slider:
                trackedSlider = interactableObject.GetComponent<Slider>();
                break;
            case ButtonType.simonSays:
                trackedButton = interactableObject.GetComponent<Button>();
                break;
            case ButtonType.word:
                Debug.Log("Words are currently not implemented correctly.");
                break;
            case ButtonType.greenButton:
                trackedButton = interactableObject.GetComponent<Button>();
                break;
            default:
                Debug.LogError("THE BUTTON FUCKING BROKEN");
                break;
        }

        if (trackedButton != null)
        {
            trackedButton.onClick.AddListener(OnInteractableClick);
        }
        if (trackedSlider != null)
        {
            trackedSlider.onValueChanged.AddListener(OnInteractableSlide);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        offsetTimer += Time.deltaTime;

        if (timer >= timerComp && offsetIndicator)
        {
            StopIndicateInputNoFail();
        }
        if (offsetTimer >= offsetComp && !waitingForInput && offsetIndicator)
        {
            EnableInputWait();
        }

        if (timer >= timerComp && waitingForInput && !offsetIndicator)
        {
            StopIndicateInput();
        }

    }

    public void IndicateInput(float timeUntilInput, float timeOffset, bool offsetIndication = false)
    {
        timer = 0.0f;
        offsetTimer = 0.0f - timeUntilInput;
        Image indicatorMat = indicator.GetComponent<Image>();
        oldColor = indicatorMat.color;
        indicatorMat.color = Color.red;
        timerComp = timeUntilInput;
        offsetComp = timeOffset;
        offsetIndicator = offsetIndication;

        if (!offsetIndication)
        {
            waitingForInput = true;
        }
    }

    public void StopIndicateInputNoFail()
    {
        Image indicatorMat = indicator.GetComponent<Image>();
        indicatorMat.color = oldColor;
    }

    public void StopIndicateInput()
    {
        Image indicatorMat = indicator.GetComponent<Image>();
        indicatorMat.color = oldColor;
        map.OnFailedEvent();
        waitingForInput = false;
    }

    public void EnableInputWait()
    {
        timer = 0.0f;
        waitingForInput = true;
        offsetIndicator = false;
    }

    public void OnInteractableClick()
    {
        Image indicatorMat = indicator.GetComponent<Image>();
        if (0 < timer && timer < timerComp && waitingForInput)
        {
            map.OnSuccessfulEvent();
            indicatorMat.color = oldColor;
        }
        waitingForInput = false;
    }

    public void OnInteractableSlide(float sliderVal)
    {
        Image indicatorMat = indicator.GetComponent<Image>();
        if (sliderUp && sliderVal <= 10 && waitingForInput)
        {
            map.OnSuccessfulEvent();
            indicatorMat.color = oldColor;
            waitingForInput = false;
            sliderUp = false;
        }

        if (!sliderUp && sliderVal >= 90 && waitingForInput)
        {
            map.OnSuccessfulEvent();
            indicatorMat.color = oldColor;
            waitingForInput = false;
            sliderUp = true;
        }
    }
}
