using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMap : MonoBehaviour
{
    [Serializable, Inspectable]
    public class ButtonEvent
    {
        public string name;
        public ButtonHandler button;
        public float executionTime;

        public ButtonEvent(ButtonHandler button, float executionTime)
        { 
            this.button = button;
            this.executionTime = executionTime;
        }
    }

    public List<ButtonEvent> buttonEvents;
    public float timeElapsed;
    public int currentEvent = 0;
    public bool mapFinished = false;

    public int life = 3;
    public GameObject[] lives;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (buttonEvents[currentEvent].executionTime * 0.42857142857 <= timeElapsed && !mapFinished)
        {
            buttonEvents[currentEvent].button.IndicateInput();
            if (buttonEvents.Count > currentEvent + 1) currentEvent += 1;
            else mapFinished = true;
        }
    }

    public void OnFailedEvent()
    {
        lives[life].gameObject.SetActive(false);
        life--;
        Debug.Log("YOU FUCKED UP DAWG! You have this much life left: " + life);


        if (life == 0)
        {
            SceneManager.LoadScene("Death");
        }
    }

    public void OnSuccessfulEvent()
    {
        Debug.Log("YOU DID IT!!");

        if (mapFinished)
        {
            SceneManager.LoadScene("End Custscene");
        }
    }
}

