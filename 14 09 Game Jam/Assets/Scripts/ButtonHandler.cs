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

    private Button trackedButton;
    private Slider trackedSlider;
    private LevelMap map;
    private bool waitingForInput;

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
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timerComp && waitingForInput)
        {
            StopIndicateInput();
        }

    }

    public void IndicateInput(float timeUntilInput)
    {
        timer = 0.0f;
        Image indicatorMat = indicator.GetComponent<Image>();
        oldColor = indicatorMat.color;
        indicatorMat.color = Color.red;
        timerComp = timeUntilInput;
        waitingForInput = true;
    }

    public void StopIndicateInput()
    {
        Image indicatorMat = indicator.GetComponent<Image>();
        indicatorMat.color = oldColor;
        map.OnFailedEvent();
        waitingForInput = false;
    }

    public void OnInteractableClick()
    {
        Image indicatorMat = indicator.GetComponent<Image>();
        if (0 < timer && timer < timerComp)
        {
            map.OnSuccessfulEvent();
            indicatorMat.color = oldColor;
        }
        waitingForInput = false;
    }
}
