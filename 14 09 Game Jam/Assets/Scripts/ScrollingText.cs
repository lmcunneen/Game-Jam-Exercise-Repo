using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    private string message1 = "My love,\nShould this message reach you, I am near. Engage ship’s Generative Pairing System.\nI miss you.\n- H ";
    public string message2;
    public string message3;
    public string message4;
    private char[] textArray;

    public Text textComponent;

    private int increment = 1;

    private void Start()
    {
        AssignMessage();
    }

    public void AssignMessage()
    {
        if (increment == 1)
        {
            textArray = message1.ToCharArray();
        }

        else if (increment == 2)
        {
            textArray = message2.ToCharArray();
        }

        else if (increment == 3)
        {
            textArray = message3.ToCharArray();
        }

        else if (increment == 4)
        {
            textArray = message4.ToCharArray();
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
