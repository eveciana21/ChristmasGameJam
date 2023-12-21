using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private int _randomValue;
    private Vector3 _direction;
    private void Start()
    {
        _randomValue = Random.Range(0, 15);

        if (_randomValue == 5) // if random value generated is 5, will throw a yellow snow ball
        {
            GetComponent<SpriteRenderer>().material.color = Color.yellow;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
