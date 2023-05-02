using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowToyScreen : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private float activationTime;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinueButtonClick);
    }
    private void OnEnable()
    {
        StartCoroutine(Timer(activationTime));
    }
    private void OnContinueButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator Timer(float time)
    {
        for (float t = 0; t<time; t += Time.deltaTime)
        {
            yield return null;
        }
        continueButton.gameObject.SetActive(true);
    }
}
