using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _enemyBulletSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");

        if (_player == null)
        {
            Debug.LogError("Couldn't find player");
        }

        RotateToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        transform.Translate(Vector3.up * _enemyBulletSpeed * Time.deltaTime);
    }

    private void RotateToPlayer()
    {
        //rotation
        Quaternion _rotateTarget = Quaternion.LookRotation(Vector3.forward, (_player.transform.position - transform.position).normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotateTarget, 1000f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }
    }


}
