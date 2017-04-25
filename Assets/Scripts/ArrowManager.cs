using System;
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

    private GameObject _currentArrow;
    private bool _isAttached = false; //判断是否被触发

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _currentArrow = null;
    }

    // Update is called once per frame
    void Update()
    {
        AttachArrow();
        PullString();
    }

    //当弓上没有箭的时候attach
    private void AttachArrow()
    {
        if (_currentArrow == null)
        {
            _currentArrow = Instantiate(ArrowPrefabs); //实体化箭
            _currentArrow.transform.parent = trackedObj.transform;
            _currentArrow.transform.localPosition = new Vector3(0.0f, -0.367f, 0.096f);
            _currentArrow.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, -70));
        }
    }

    public void AttachBowToArrow()
    {
        _currentArrow.transform.parent = AttachPoint.transform;
        _currentArrow.transform.localPosition = StartPoint.transform.localPosition +
                                                new Vector3(-0.47f, 0f, 0f);
        _currentArrow.transform.rotation = StartPoint.transform.rotation;
        _isAttached = true; //说明被触发了
    }

    //拉动弓弦的逻辑
    private void PullString()
    {
        if (_isAttached)
        {
            _currentArrow.GetComponent<Arrow>().AddDamage(Time.deltaTime * 100);
            float dist = -(trackedObj.transform.position - StringStartPoint.transform.position)
                .magnitude; //弓弦和手柄设备的距离差值
            AttachPoint.transform.localPosition = StringStartPoint.transform.localPosition +
                                                  new Vector3(1f * dist, 0f, 0f);
            var device = SteamVR_Controller.Input((int) trackedObj.index);
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Fire();
            }
        }
    }

    //射击
    private void Fire()
    {
        _currentArrow.transform.parent = null;
        Rigidbody rigidbody = _currentArrow.GetComponent<Rigidbody>();
        rigidbody.velocity = _currentArrow.transform.right * 20f; //设置速度
        rigidbody.maxAngularVelocity = 0;
        //rigidbody.rotation = Quaternion.Euler(Vector3.Angle(StringStartPoint.transform.position, StartPoint.transform.position));
        //rigidbody.freezeRotation = true;
        rigidbody.useGravity = true;
        BoxCollider collider = _currentArrow.GetComponent<BoxCollider>();
        //Debug.Log(collider.size);
        AttachPoint.transform.position = StringStartPoint.transform.position;
        _currentArrow = null; //发射出去以后
        _isAttached = false; //处于非触发的状态
        collider.isTrigger = false;
    }
}