using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Body))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Celebrator : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;

    private Body _body;
    private Animator _animator;
    private AudioSource _audioSource;

    public event UnityAction Celebrated;

    private void Awake()
    {
        _body = GetComponent<Body>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _body.Completed += OnBodyCompleted;
    }

    private void OnDisable()
    {
        _body.Completed -= OnBodyCompleted;
    }

    private void OnBodyCompleted()
    {
        _animator.SetTrigger(AnimatorCharacterController.Params.Completed);
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        _confetti.Play();
        _audioSource.Play();
        Celebrated?.Invoke();
    }
}
