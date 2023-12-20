using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private float _shakeDuration;
    private float _xShake, _yShake;

    void Start()
    {
        transform.position = new Vector3(0, 0, -10);
    }

    public void CameraShake()
    {
        StartCoroutine(CameraShakeStrength());
    }

    IEnumerator CameraShakeStrength()
    {
        Vector3 originalPos = transform.position;
        _shakeDuration = Time.time + 0.2f;

        while (_shakeDuration > Time.time)
        {
            _xShake = Random.Range(-0.05f, 0.05f);
            _yShake = Random.Range(-0.05f, 0.05f);
            transform.position = new Vector3(_xShake, _yShake, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        transform.position = originalPos;
    }
}
