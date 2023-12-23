using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private UIManager _uiManager;

    [SerializeField] private float _mySpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bulletSpawnLocation;

    private float _sliderCooldownRemaining;
    private bool _canUseAnvil;
    private bool _anvilActive;

    [SerializeField] private GameObject _rudolph;
    private bool _isFrozen = false;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (_isFrozen == false)
        {
            Movement();
            RotatePlayertoMousePosition();
            Fire();
        }

        if (_canUseAnvil == false && !_anvilActive)
        {
            SliderIncrease();
        }

        if (_anvilActive == true)
        {
            SliderDecrease();
        }

        if (Input.GetKeyDown(KeyCode.Space) && _canUseAnvil)
        {
            Instantiate(_rudolph, transform.position, Quaternion.identity);
            _canUseAnvil = false;
            _anvilActive = true;
        }
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //_animator.SetTrigger("Throw");
            Instantiate(_bullet, _bulletSpawnLocation.transform.position, transform.rotation);
            //fixed bullet position jank by adding empty object to reference position instead, child of player object
        }

    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, vertical, 0);
        transform.Translate(direction * Time.deltaTime * _mySpeed, Space.World); //added Space.World to make movement disregard rotation

        //Clamp Player Within Screen Bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8, 8), transform.position.y, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4), 0);

        if (horizontal != 0)
        {
            _animator.SetBool("Walk", true);
        }
        if (vertical != 0)
        {
            _animator.SetBool("Walk", true);
        }





        //_animator.SetFloat("Walk", horizontal);
        //_animator.SetFloat("Walk", vertical);

    }

    private void RotatePlayertoMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get mouse coordinates in world position
        Vector2 direction = (mousePosition - transform.position).normalized; //Calculate direction from player to mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculate angle       
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90f), _rotationSpeed * Time.deltaTime);
        //Rotate player towards mouse at desired speed. -90 from angle to account for sprite facing up
    }

    private void SliderIncrease() //anvil increase slider over 25 seconds
    {
        float seconds = 25f;
        _uiManager.AnvilSlider(_sliderCooldownRemaining += 100 / seconds * Time.deltaTime);
        if (_sliderCooldownRemaining >= 100)
        {
            _canUseAnvil = true;
            _sliderCooldownRemaining = 100;
        }
    }
    private void SliderDecrease() //anvil decrease slider over 5 seconds
    {
        float seconds = 11f;
        _uiManager.AnvilSlider(_sliderCooldownRemaining -= 100 / seconds * Time.deltaTime);
        _anvilActive = true;

        if (_sliderCooldownRemaining <= 1)
        {
            _canUseAnvil = false;
            _anvilActive = false;
        }
    }

    IEnumerator PlayerFreeze()
    {
        _isFrozen = true;
        _animator.SetBool("Walk", false);
        _spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(1f);
        _spriteRenderer.color = Color.white;
        _isFrozen = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            StartCoroutine(PlayerFreeze());
            Destroy(other.gameObject);
        }
    }

}
