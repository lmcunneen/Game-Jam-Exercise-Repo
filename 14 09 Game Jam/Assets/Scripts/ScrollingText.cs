using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    private string message1 = "[[ [ 1 ] NEW MESSAGE(S) ]]";
    private string message2 = "My love,\n          \nShould this message reach you, I am near.\nEngage ship’s Generative Pairing System.\n          \nI miss you.          - H ";
    private string message3;
    private string message4;
    private char[] textArray;

    private Color playerColour;
    private Color partnerColour;

    public Text textComponent;

    private int increment = 1;

    private void Start()
    {
        playerColour = Color.white;

        partnerColour.r = 254;
        partnerColour.g = 108;
        partnerColour.b = 144;

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
