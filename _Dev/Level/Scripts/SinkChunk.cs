using System.Collections;
using System.Collections.Generic;
using _Internal._Dev.Level.Scripts;
using _Internal._Dev.Roller.Scripts;
using UnityEngine;

public class SinkChunk : HazardousChunk
{
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private Vector2 spawnRect;
    [SerializeField] private Transform spawnCenter;
    public override void DoAction(RollerCollisionManager manager)
    {
        manager.HazardInteraction(true);
        List<AttachableObjects> attaches = manager.InteractionEffect(type);
        StartCoroutine(SinkEffect(attaches,delayTime));

        GetComponent<Collider>().enabled = false;
    }

    private IEnumerator SinkEffect(List<AttachableObjects> attaches ,float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var attach in attaches)
        {
            attach.rb.useGravity = false;
            attach.rb.isKinematic = true;
            Transform attachTransform = attach.transform;
            attachTransform.SetParent(spawnCenter);
            attachTransform.localPosition = new Vector3(Random.Range(-spawnRect.x, spawnRect.x),0,
                Random.Range(-spawnRect.y, spawnRect.y));
        }
    }
}
