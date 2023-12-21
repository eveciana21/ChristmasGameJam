using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudolph : MonoBehaviour
{
    private int _direction = 1;
    private int _randomNumber;
    private int _speed = 2;
    private float _startYPos;
    [SerializeField] private GameObject _anvil;


    void Start()
    {
        _randomNumber = Random.Range(0, 2);
        if (_randomNumber == 0)
        {
            transform.position = new Vector3(-11.5f, 2, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (_randomNumber == 1)
        {
            transform.position = new Vector3(11.5f, 2, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        _startYPos = transform.position.y;

        transform.position = new Vector3(transform.position.x, _startYPos + Mathf.Sin(Time.time * 1.5f), transform.position.z);

        StartCoroutine(DropAnvil());

    }

    void Update()
    {
        Movement();

        if (transform.position.x < -13)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.x > 13)
        {
            Destroy(this.gameObject);
        }
    }

    private void Movement()
    {
        if (_randomNumber == 0)
        {
            _direction = -1;
        }
        else if (_randomNumber == 1)
        {
            _direction = 1;
        }

        transform.Translate(Vector3.left * _speed * _direction * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, _startYPos + Mathf.Sin(Time.time * 1.5f), transform.position.z);
    }

    IEnumerator DropAnvil()
    {
        while (true)
        {
            Instantiate(_anvil, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }

    }
}
