using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{
    private string message1 = "[[ [ 1 ] NEW MESSAGE(S) ]]";
    private string message2 = "My love,\n          \nShould this message reach you, I am near.              \nEngage ship’s Generative Pairing System.\n          \nI miss you.       \n- H ";
    private string message3 = "The GPS…       \nIt was damaged navigating those meteors.       \nI’ll have to pilot it manually.       \nIt will be most perilous, but I must try.       \nI’m on my way, my love!";
    private string message4 = "Now, let’s see…       \nIf I remember correctly,       \nthis thing is all about the Beat.       \nFeel the Beat, and the GPS will feel it too.";
    private string message5 = "The manual should help, it’s been a while.       \nTime to fire this thing up.";
    private char[] textArray;

    private Color playerColour;
    private Color partnerColour;

    public Text textComponent;
    public Button terminalButton;

    private int increment = 1;

    private void Start()
    {
        playerColour = Color.white;

        partnerColour.r = 254;
        partnerColour.g = 108;
        partnerColour.b = 144;
        partnerColour.a = 255;

        AssignMessage();
    }

    public void ChangeMessage()
    {
        Array.Clear(textArray, 0, textArray.Length - 1);
        textComponent.text = null;
        increment++;

        AssignMessage();
    }

    public void AssignMessage()
    {
        if (increment == 1)
        {
            textArray = message1.ToCharArray();
            textComponent.color = playerColour;
        }

        else if (increment == 2)
        {
            textArray = message2.ToCharArray();
            textComponent.color = playerColour;
        }

        else if (increment == 3)
        {
            textArray = message3.ToCharArray();
            textComponent.color = playerColour;
        }

        else if (increment == 4)
        {
            textArray = message4.ToCharArray();
            textComponent.color = playerColour;
        }

        else if (increment == 5)
        {
            textArray = message5.ToCharArray();
            textComponent.color = playerColour;
        }

        else if (increment > 5)
        {
            SceneManager.LoadScene("Cynthia's Gameplay");
        }

        else
        {
            Debug.Log("Increment Broke for Scroll Text!!!!");
        }

        StartCoroutine(PlayText());
    }

    public IEnumerator PlayText()
    {
        foreach (char c in textArray)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
