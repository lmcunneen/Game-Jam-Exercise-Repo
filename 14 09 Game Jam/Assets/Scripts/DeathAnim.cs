using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnim : MonoBehaviour
{
    public GameObject canvas;
    public Animator animator;

    public void OpenDeathMenu()
    {
        canvas.SetActive(true);
        animator.enabled = false;
    }
}
