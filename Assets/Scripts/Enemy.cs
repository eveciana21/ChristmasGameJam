using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _health;
    private ScreenShake _mainCamera;


    [SerializeField] private float fadeDuration = 1.0f; // Time in seconds for the fade to complete

    //variables for fade color
    private Material material;
    private Color originalColor;
    private float elapsedTime = 0f;
    private SpriteRenderer _renderer;
    private bool _isAlive;

    //target detection variables
    private float _distanceToClosestPresent = Mathf.Infinity;
    private float _distanceToPresent;
    private GameObject[] _allPresents;
    private GameObject _closestPresent;
    private bool _grabbedPresent = false;

    //border detection variables
    private float _distanceToClosestBorder = Mathf.Infinity;
    private float _distanceToBorder;
    private GameObject[] _allBorders;
    private GameObject _closestBorder;
    private bool _detectedBorder = false;

    private void Start()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
        _renderer = GetComponent<SpriteRenderer>();
        

        if (_renderer != null) //grabs original color of renderer to fade material after death
        {
            material = _renderer.material;
            originalColor = material.color;
        }

        DetectTarget();
    }

    void Update()
    {
        EnemyMovement();
        if (_health <= 0 && !_isAlive)
        {
            Fade();
        }
    }

    private void DetectTarget() //called at start and if initial found is destroyed
    {
        //finds all presents, checks distance of each against current position, closest distance object is stored in variable _closestPresent
        _allPresents = GameObject.FindGameObjectsWithTag("Present");
        if (_allPresents != null)
        {
            foreach (GameObject currentPresent in _allPresents)
            {
                _distanceToPresent = (currentPresent.transform.position - this.gameObject.transform.position).sqrMagnitude;

                if (_distanceToPresent < _distanceToClosestPresent)
                {
                    _distanceToClosestPresent = _distanceToPresent;
                    _closestPresent = currentPresent;
                }
            }
        }
    }

    private void DetectBorder()
    {
        //finds all borders, checks distance of each against current position, closest distance object is stored in variable _closestBorder
        _allBorders = GameObject.FindGameObjectsWithTag("Border");
        if (_allBorders != null)
        {
            foreach (GameObject currentBorder in _allBorders)
            {
                _distanceToBorder = (currentBorder.transform.position - this.gameObject.transform.position).sqrMagnitude;
                if (_distanceToBorder < _distanceToClosestBorder)
                {
                    _distanceToClosestBorder = _distanceToBorder;
                    _closestBorder = currentBorder;
                }
            }
        }

        _detectedBorder = true;
    }

    private void EnemyMovement()
    {

        if (_closestPresent != null && _grabbedPresent == false) //if detected present and not grabbed, move towards present
        {
            transform.position = Vector3.MoveTowards(transform.position, _closestPresent.transform.position, _moveSpeed * Time.deltaTime);
        }
        if (_closestPresent == null) //retargets closest present if initial becomes null or destroyed
        {
            DetectTarget();
        }
        if (_grabbedPresent == true) //when enemy grabs present, detects the closest boundary
        {
            DetectBorder();
        }
        if (_grabbedPresent == true && _detectedBorder == true) //if grabbed present and detected a border, move towards border
        {
            transform.position = Vector3.MoveTowards(transform.position, _closestBorder.transform.position, _moveSpeed / 2 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            _health--;
            StartCoroutine(ColorFlashOnDamage());

            if (_mainCamera != null)
            {
                _mainCamera.CameraShake();
            }

            if (_health <= 0)
            {
                GetComponent<Collider2D>().enabled = false;
                _moveSpeed = 0;
            }
            Destroy(other.gameObject);
        }

        if (other.tag == "Present")
        {
            other.gameObject.transform.parent = this.gameObject.transform;
            other.gameObject.transform.position = this.gameObject.transform.position;
            _grabbedPresent = true;
            //makes enemy "grab" present
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Present")
        {
            other.gameObject.transform.parent = null;
            _detectedBorder = false;
            _grabbedPresent = false;
            //resets grab interaction when exiting collider
        }
    }

    private void Fade() //Fade object over fadeDuration after death
    {
        if (material != null)
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time
            float lerpFactor = Mathf.Clamp01(elapsedTime / fadeDuration); //Calculate the lerp factor between 0 and 1 based on elapsed time and fade duration
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, 0f, lerpFactor)); // Lerp the alpha value of the color
            material.color = newColor; // Update the material color

            if (lerpFactor >= 1f) // if fade Complete, destroy object
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator ColorFlashOnDamage()
    {
        _isAlive = true;
        _renderer.enabled = false;
        yield return new WaitForSeconds(0.05f);
        _renderer.enabled = true;
        _isAlive = false;
    }
}

