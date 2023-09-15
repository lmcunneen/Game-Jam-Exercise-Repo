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

    private Button trackedButton;
    private Slider trackedSlider;
    private Slider indicationSlider;
    private LevelMap map;
    private bool waitingForInput;
    private bool sliderUp;

    public enum ButtonType
    {
        bigButton,
        lever,
        simonSays,
        word,
        smallButton
    }

    public ButtonType type;

    private void Start()
    {
        map = FindObjectOfType<LevelMap>();
        switch (type)
        {
            case ButtonType.bigButton:
                trackedButton = interactableObject.GetComponent<Button>();
                break;
            case ButtonType.lever:
                trackedSlider = interactableObject.GetComponent<Slider>();
                break;
            case ButtonType.simonSays:
                trackedButton = interactableObject.GetComponent<Button>();
                break;
            case ButtonType.word:
                Debug.Log("Words are currently not implemented correctly.");
                break;
            case ButtonType.smallButton:
                trackedButton = interactableObject.GetComponent<Button>();
                indicationSlider = indicator.GetComponent<Slider>();
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

    public void IndicateInput()
    {
        switch (type)
        {
            case ButtonType.bigButton:
                StartCoroutine(bigButton());
                break;
            case ButtonType.lever:
                StartCoroutine(largeLever());
                break;
            case ButtonType.simonSays:
                StartCoroutine(bigButton());
                break;
            case ButtonType.word:
                Debug.Log("Words are currently not implemented.");
                break;
            case ButtonType.smallButton:
                StartCoroutine(smallButton());
                break;
            default:
                Debug.LogError("THE BUTTON FUCKING BROKEN");
                break;
        }
    }

    IEnumerator bigButton()
    {
        int flashCount = 0;
        bool indicatorOn = false;
        Image indicatorMat = indicator.GetComponent<Image>();
        while (flashCount < 6)
        {
            if (indicatorOn)
            {
                indicatorMat.color = oldColor;
                indicatorOn = false;
            }
            else
            {
                oldColor = indicatorMat.color;
                indicatorMat.color = Color.red;
                indicatorOn = true;
            }
            flashCount++;
            yield return new WaitForSeconds(0.42857142857f / 2);
        }
        

        float waitTime = 0.0f;
        while (waitTime < 0.12857142857f / 2)
        {
            waitTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("WaitingForInput...");
        waitingForInput = true;

        float buttonPressTime = 0.0f;
        while (buttonPressTime < 0.6 / 2)
        {
            buttonPressTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("Input Window over...");
        if (waitingForInput)
        {
            StopIndicateInput();
        }
        yield break;
    }

    IEnumerator smallButton()
    {

        for (float i = 0; i <= 1.71428571428 / 2; i += Time.deltaTime)
        {
            // set color with i as alpha
            indicationSlider.value = i / (1.71428571428f / 2);
            yield return null;
        }
        Debug.Log("WaitingForInput...");
        waitingForInput = true;

        float buttonPressTime = 0.0f;
        while (buttonPressTime < 0.4 / 2)
        {
            buttonPressTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("Input Window over...");
        if (waitingForInput)
        {
            StopIndicateInput();
        }
        yield break;
    }

    IEnumerator largeLever()
    {
        Image indicatorMat = indicator.GetComponent<Image>();
        oldColor = indicatorMat.color;
        float alpha = 0.0f;
        while (alpha <= 1)
        {
            indicatorMat.color = new Color(1, 1, 1, alpha);
            alpha += 0.1f;
            yield return new WaitForSeconds(0.42857142857f / 10);
        }
        indicatorMat.color = oldColor;
        Debug.Log("Waiting for input...");
        waitingForInput = true;

        float buttonPressTime = 0.0f;
        while (buttonPressTime < 0.4 / 2)
        {
            buttonPressTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("Input Window over...");
        if (waitingForInput)
        {
            StopIndicateInput();
        }
        yield break;
    }

    IEnumerator words()
    {

        yield return null;
    }

    public void StopIndicateInput()
    {
        if (!(type == ButtonType.smallButton))
        {
            Image indicatorMat = indicator.GetComponent<Image>();
            indicatorMat.color = oldColor;

        }

        map.OnFailedEvent();
        waitingForInput = false;
    }

    public void OnInteractableClick()
    {
        
        if (waitingForInput && type == ButtonType.bigButton)
        {
            Image indicatorMat = indicator.GetComponent<Image>();
            map.OnSuccessfulEvent();
            indicatorMat.color = oldColor;
            waitingForInput = false;
        }

        if (waitingForInput && type == ButtonType.smallButton)
        {
            map.OnSuccessfulEvent();
            waitingForInput = false;
        }
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
