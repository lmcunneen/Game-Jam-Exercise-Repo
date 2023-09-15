using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    [Serializable, Inspectable]
    public class ButtonEvent
    {
        public string name;
        public ButtonHandler button;
        public float executionTime;
        public float indicationTime;

        public ButtonEvent(ButtonHandler button, float executionTime, float indicationTime)
        { 
            this.button = button;
            this.executionTime = executionTime;
            this.indicationTime = indicationTime;
        }
    }

    public List<ButtonEvent> buttonEvents;
    public float timeElapsed;
    public int currentEvent = 0;
    public bool mapFinished = false;
    public bool offsetIndication;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (buttonEvents[currentEvent].executionTime - buttonEvents[currentEvent].indicationTime <= timeElapsed && !mapFinished)
        {
            buttonEvents[currentEvent].button.IndicateInput(buttonEvents[currentEvent].indicationTime);
            if (buttonEvents.Count > currentEvent + 1) currentEvent += 1;
            else mapFinished = true;
        }
    }

    public void OnFailedEvent()
    {
        Debug.Log("YOU FUCKED UP DAWG");
    }

    public void OnSuccessfulEvent()
    {
        Debug.Log("YOU DID IT!!");
    }
}

