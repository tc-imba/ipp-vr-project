using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;
    public GameObject ArrowPrefabs;
    public static ArrowManager Instance;
    public GameObject AttachPoint;
    public GameObject StartPoint;
    public GameObject StringStartPoint;

    private GameObject CurrentArrow;
    private bool isAttached = false; //判断是否被触发

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }
	// Update is called once per frame
	void Update ()
    {
        AttachArrow();
        PullString();
    }

    //当弓上没有箭的时候attach
    private void AttachArrow()
    {
        if (CurrentArrow == null)
        {
            CurrentArrow = Instantiate(ArrowPrefabs); //实体化箭
            CurrentArrow.transform.parent = trackedObj.transform;
            CurrentArrow.transform.localPosition = new Vector3(0.0f, -0.367f, 0.096f);
            CurrentArrow.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, -70));
        }
    }

    public void AttachBowToArrow()
    {
        CurrentArrow.transform.parent = AttachPoint.transform;
        CurrentArrow.transform.localPosition = StartPoint.transform.localPosition;
        CurrentArrow.transform.rotation = StartPoint.transform.rotation;
        isAttached = true; //说明被触发了
    }

    //拉动弓弦的逻辑
    private void PullString()
    {
        if (isAttached)
        {
            float dist = - (trackedObj.transform.position - StringStartPoint.transform.position).magnitude; //弓弦和手柄设备的距离差值
            AttachPoint.transform.localPosition = StringStartPoint.transform.localPosition + new Vector3(1f * dist, 0f, 0f);

            var device = SteamVR_Controller.Input((int)trackedObj.index);
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Fire();
            }
        }
    }

    //射击
    private void Fire()
    {
        CurrentArrow.transform.parent = null;
        Rigidbody rigidbody = CurrentArrow.GetComponent<Rigidbody>();
        rigidbody.velocity = CurrentArrow.transform.right* 20f; //设置速度
        rigidbody.useGravity = true;
        AttachPoint.transform.position = StringStartPoint.transform.position;
        CurrentArrow = null; //发射出去以后
        isAttached = false; //处于非触发的状态
    }
}
