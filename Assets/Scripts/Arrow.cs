using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void Start()
    {
    }

    private void OnTriggerStay()
    {
        AttachArrow();
    }

    void OnTriggerEnter(Collider other)
    {
        AttachArrow();
        //Debug.Log(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        AttachArrow();
        Monster monster = other.gameObject.GetComponentInChildren<Monster>();
        if (monster)
        {
            monster.SetStun();
        }
        //Debug.Log(other.collider.bounds);
    }

    private void AttachArrow()
    {
        var device = SteamVR_Controller.Input((int) ArrowManager.Instance.trackedObj.index);
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            ArrowManager.Instance.AttachBowToArrow(); //扣动trigger时进行attach操作
        }
    }
}