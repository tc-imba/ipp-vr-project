using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private void OnTriggerStay()
    {
        AttachArrow();
    }


    void OnTriggerEnter()
    {
        AttachArrow();
    }

    private void AttachArrow()
    {
        var device = SteamVR_Controller.Input((int)ArrowManager.Instance.TrackedObj.index);
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            ArrowManager.Instance.AttachBowToArrow(); //扣动trigger时进行attach操作
        }
    }
}
