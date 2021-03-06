﻿using UnityEngine;
using Leap;

public class SuperHands : MonoBehaviour
{
    public Hand hand;
    // Use this for initialization
    void Start() { }

    public float grabThresh = 0.98f;
    public float letgoThresh = 0.02f;
    public SuperGrab holding;
    // Update is called once per frame
    void Update()
    {

        var handModel = GetComponent<HandModel>();
        if (handModel == null)
            return;

        hand = GetComponent<HandModel>().GetLeapHand();

        if (hand == null)
            return;

        var palmPos = hand.PalmPosition.ToVector3();
        var thumb = hand.Fingers[0];
        if(thumb != null) palmPos = thumb.TipPosition.ToVector3();

        var direction = (palmPos - Camera.main.transform.position).normalized;

        if (holding == null)
        {
            RaycastHit hover;
            if (!Physics.SphereCast(palmPos, 0.1f, direction, out hover, 10f))
                return;
            var superGrab = hover.transform.GetComponent<SuperGrab>();
            if (superGrab == null)
                return;
            if (hand.PinchStrength > grabThresh)
            {
                holding = superGrab;
                superGrab.Grab();
            }
        }

        else
        {
            if (hand.PinchStrength < letgoThresh)
            {
                holding.Drop();
                holding = null;
            }
        }

        if (holding != null)
        {
            var handDist = Vector3.Distance(palmPos, Camera.main.transform.position);
            var normalizedHandDist = Mathf.InverseLerp(.2f, .5f, handDist);

            holding.transform.position = palmPos + direction * Mathf.Lerp(minHoldDist,maxHoldDist, normalizedHandDist);
            holding.transform.forward = direction;
        }
    }
    public float minHoldDist = 1;
    public float maxHoldDist = 3;
}
