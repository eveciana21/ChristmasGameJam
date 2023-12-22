using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text textBox;
    [SerializeField] private string fullText;
    [SerializeField] private float delay = 0.1f;

    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private GameObject _exitDialogBoxButton;

    private void Start()
    {
        _exitDialogBoxButton.SetActive(false);
        _dialogBox.SetActive(false);
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            textBox.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(delay);

            if (i == fullText.Length)
            {
                _exitDialogBoxButton.SetActive(true);
            }
        }
    }

    public void EnableDialogBox()
    {
        _dialogBox.SetActive(true);
        StartCoroutine(TypeText());
    }


}
