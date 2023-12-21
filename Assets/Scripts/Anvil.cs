using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{
    private int _speed;
    private int _randomNumber;
    void Start()
    {
        _randomNumber = Random.Range(0, 2);
        _speed = Random.Range(25, 45);
    }

    void Update()
    {
        if (_randomNumber == 0)
        {
            transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(-Vector3.forward * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
