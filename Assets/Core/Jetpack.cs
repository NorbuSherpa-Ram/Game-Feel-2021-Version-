using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    private float jetpackTime;
    private float jetStength;
    private PlayerController player;
    private TrailRenderer myTrailRenderer;
    private float timer = 0;

    private Coroutine jetRoutine;
    private void Start()
    {
        player = PlayerController.Instance;
    }
    public void SetJetpack(float _jetTime, float _jetStength , TrailRenderer trailRenderer )
    {
        jetpackTime = _jetTime;
        jetStength = _jetStength;
        myTrailRenderer = trailRenderer;
    }

    public void StopJet()
    {
        if (jetRoutine != null) return;
        jetRoutine = StartCoroutine(JetpackFire());
    }
    public void LunchJetpack()
    {
        while (timer < jetpackTime)
        {
            timer += Time.deltaTime;
            myTrailRenderer.emitting = true;
            player.SetVelocity(player.myRb.velocity.x, jetStength);
        }

        if (jetRoutine != null) return;
        jetRoutine = StartCoroutine(JetpackFire());
    }

    private IEnumerator JetpackFire()
    {
        yield return new WaitForSeconds(.5f);
        timer = 0;
        jetRoutine = null;
        yield return new WaitForSeconds(.23f);
        myTrailRenderer.emitting = false;

    }



}
