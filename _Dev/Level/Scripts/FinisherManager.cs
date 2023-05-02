using System;
using System.Collections;
using System.Collections.Generic;
using _Internal._Dev.Management.Scripts;
using Cinemachine;
using UnityEngine;

public class FinisherManager : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject rollerPuller;
    [SerializeField] private CinemachineVirtualCamera minigameCamera;
    private void Awake()
    {
        EventManager.AddListener<MinigameStartEvent>(OnMinigameStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MinigameStartEvent>(OnMinigameStart);
    }

    private void OnMinigameStart(MinigameStartEvent obj)
    {
        player = obj.Player.transform;
        player.SetParent(rollerPuller.transform);
        StartCoroutine(SetPlayerInPosition(1f));
    }

    private IEnumerator SetPlayerInPosition(float time)
    {
        Vector3 initPos = player.localPosition;
        Vector3 endPos = new Vector3(player.localPosition.x, 0,0);
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            player.localPosition = Vector3.Lerp(initPos, endPos, t / time);
            yield return null;
        }
        player.localPosition = endPos;
        minigameCamera.gameObject.SetActive(true);
        rollerPuller.GetComponent<Animator>().SetTrigger("MoveInPos");
    }
}
