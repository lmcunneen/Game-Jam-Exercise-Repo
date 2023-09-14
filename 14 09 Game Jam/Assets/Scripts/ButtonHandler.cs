using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public GameObject interactableObject;
    public GameObject indicator;

    private Color oldColor;
    private float timer = 0;
    private float timerComp;

    private Button trackedButton;
    private Slider trackedSlider;

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
        if (timer >= timerComp)
        {
            StopIndicateInput();
        }

    }

    public void IndicateInput(float timeUntilInput)
    {
        timer = 0.0f;
        Material indicatorMat = indicator.GetComponent<Material>();
        oldColor = indicatorMat.color;
        indicatorMat.color = Color.red;
        timerComp = timeUntilInput;
    }

    public void StopIndicateInput()
    {
        Material indicatorMat = indicator.GetComponent<Material>();
        indicatorMat.color = oldColor;
    }

    public void OnInteractableClick()
    {

    }
}
