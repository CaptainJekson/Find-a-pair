using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Popup : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private AudioSource _popupSound;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        OnPopup();
    }

    public void OnPopdown()
    {
        _animator.SetBool("State", true);
        StartCoroutine(WindowClosingDelay());
        _popupSound.Play();
    }


    private void OnPopup()
    {
        _animator.SetBool("State", false);
        _popupSound.Play();
    }

    private IEnumerator WindowClosingDelay()
    {
        yield return new WaitForSeconds(1.0f);
        _panel.SetActive(false);
    }
}
