using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _health;
    private ScreenShake _mainCamera;


    public float fadeDuration = 2.0f; // Time in seconds for the fade to complete

    //variables for fade color
    private Material material;
    private Color originalColor;
    private float elapsedTime = 0f;
    private Renderer _renderer;
    //[SerializeField] private Collider2D _collider;

    private void Start()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
        _renderer = GetComponent<Renderer>();

        if (_renderer != null) //grabs original color of renderer to fade material after death
        {
            material = _renderer.material;
            originalColor = material.color;
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
        if (_health <= 0)
        {
            Fade();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            _health--;

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
}

