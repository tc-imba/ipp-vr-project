using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int _stunCounter;

    void Start()
    {
        _stunCounter = 0;
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.mass = 12f;
        rigidbody.angularDrag = 0.02f;
        BoxCollider collidor = gameObject.AddComponent<BoxCollider>();
        Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
        collidor.center = renderer.bounds.center;
        collidor.size = renderer.bounds.size;
        float x = UnityEngine.Random.Range(-10f, 10f);
        float z = UnityEngine.Random.Range(-10f, 10f);
        gameObject.transform.localPosition = new Vector3(x, 10f, z);
    }

    private void Update()
    {
        //Debug.Log(gameObject.transform.localRotation);
        if (_stunCounter == 0)
        {
            float x1 = 0f;
            float z1 = 0f;
            float x2 = gameObject.transform.position.x;
            float z2 = gameObject.transform.position.z;
            double angle = Math.Atan2(z2 - z1, x2 - x1);
            Quaternion rotation = Quaternion.Euler(0f, (float) angle, 0f);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, Time.time * 0.1f);
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
        Debug.Log(_stunCounter);
        StartCoroutine(UpdateStun());
    }

    private IEnumerator UpdateStun()
    {
        Debug.Log(_stunCounter);
        yield return new WaitForSeconds(1f);
        _stunCounter--;
    }
}