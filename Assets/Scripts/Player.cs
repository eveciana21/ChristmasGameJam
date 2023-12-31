using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _mySpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _bullet;


    void Update()
    {
        Movement();
        RotatePlayertoMousePosition();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(_bullet, transform.position, transform.rotation);
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, vertical, 0);
        transform.Translate(direction * Time.deltaTime * _mySpeed);

        //Clamp Player Within Screen Bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8, 8), transform.position.y, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4), 0);
    }

    private void RotatePlayertoMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get mouse coordinates in world position
        Vector2 direction = (mousePosition - transform.position).normalized; //Calculate direction from player to mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculate angle       
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90f), _rotationSpeed * Time.deltaTime);
        //Rotate player towards mouse at desired speed. -90 from angle to account for sprite facing up
    }
}
