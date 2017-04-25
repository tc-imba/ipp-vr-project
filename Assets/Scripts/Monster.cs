using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int _stunCounter;

    private float _health;

    private float _time;


    void Start()
    {
        _time = -1;
        _stunCounter = 0;
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.mass = 6f;
        rigidbody.angularDrag = 0.02f;
        BoxCollider collidor = gameObject.AddComponent<BoxCollider>();
        Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
        collidor.center = renderer.bounds.center;
        collidor.size = renderer.bounds.size;
        float x = UnityEngine.Random.Range(-10f, 10f);
        float z = UnityEngine.Random.Range(-10f, 10f);
        //float z1 = ArrowManager.Instance.trackedObj.transform.position.z;
        gameObject.transform.localPosition = new Vector3(x, 10f, z);

        //float x1 = ArrowManager.Instance.AttachPoint.transform.position.x;


        double angle = Math.Atan2(z - 0f, x - 0f) * 180 / Math.PI;
        gameObject.transform.rotation = Quaternion.Euler(0, (float) angle, 0);


        int ran = UnityEngine.Random.Range(2, 6);
        _health = 100 * ran;

        //GUIText guitext = gameObject.AddComponent<GUIText>();
        //guitext.enabled = true;
        //guitext.text = _health.ToString();
    }

    private void Update()
    {
        //Debug.Log(gameObject.transform.localRotation);
        if (_stunCounter == 0)
        {
            float x1 = ArrowManager.Instance.trackedObj.transform.position.x;
            float z1 = ArrowManager.Instance.trackedObj.transform.position.z;
            float x2 = gameObject.transform.position.x;
            float z2 = gameObject.transform.position.z;
            double angle = Math.Atan2(z2 - z1, x2 - x1) * 180 / Math.PI;
            Quaternion rotation = Quaternion.Euler(0, (float) angle, 0);

            if (_time < 0) _time = Time.time;
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, (Time.time - _time) * 2f);
            if (_time >= Time.time + 0.5) _time = -1;
        }

        /*if (_protect)
        {
            if (gameObject.transform.position.y < 0)
                gameObject.transform.position += new Vector3(0f, 0.03f, 0f);
            else
                _protect = false;
        }*/
    }

    public void SetStun()
    {
        _stunCounter++;
        //Debug.Log(_stunCounter);
        StartCoroutine(UpdateStun());
    }

    private IEnumerator UpdateStun()
    {
        //Debug.Log(_stunCounter);
        yield return new WaitForSeconds(1f);
        _stunCounter--;
    }

    public void Hurt(float damage)
    {
        if (_health <= 0) return;
        _health -= damage;
        Debug.Log(_health);
        if (_health <= 0)
        {
            BoxCollider collidor = gameObject.GetComponent<BoxCollider>();
            Destroy(collidor);
            MonsterManager.Instance.CreateMonster();
            MonsterManager.Instance.CreateMonster();
            StartCoroutine(DeleteDelay(1f));
        }
    }

    private IEnumerator DeleteDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}