using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject first;
    private bool secondScreen = false;

    void Start()
    {
        first.SetActive(true);
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            first.SetActive(false);
            Time.timeScale = 1;
        }
    }
}