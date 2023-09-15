using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAnim : MonoBehaviour
{
    public GameObject playerLoveship;
    public GameObject partnerLoveship;
    public GameObject completeLoveship;
    public GameObject canvas;
    
    public void ShipMergeAnim()
    {
        partnerLoveship.SetActive(false);
        completeLoveship.SetActive(true);
        canvas.SetActive(true);
        playerLoveship.SetActive(false);
    }
}
