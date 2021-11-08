using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SwappingAudioPlayer : MonoBehaviour
{
    [SerializeField] private Pointer _pointer;
    [SerializeField] private Swapper _swapper;
    [SerializeField] private AudioClip _pointing;
    [SerializeField] private AudioClip _swapping;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _pointer.Pointed += OnPointed;
        _swapper.Swapped += OnSwapped;
    }

    private void OnDisable()
    {
        _pointer.Pointed -= OnPointed;
        _pointer.Pointed -= OnSwapped;
    }

    private void OnPointed()
    {
        _audioSource.clip = _pointing;
        _audioSource.Play();
    }

    private void OnSwapped()
    {
        _audioSource.clip = _swapping;
        _audioSource.Play();
    }
}
