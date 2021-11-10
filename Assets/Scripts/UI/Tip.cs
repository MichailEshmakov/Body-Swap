using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tip : MonoBehaviour
{
    [SerializeField] private Pointer _pointer;
    [SerializeField] private Swapper _swapper;
    [SerializeField] private Level _level;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private string _beforeSelectionTip;
    [SerializeField] private string _afterSelectionTip;

    private void OnEnable()
    {
        _level.Inited += OnLevelInited;
        if (_level.CurrentState != Level.State.Initializing)
        {
            SubscribeToDependencies();
        }
    }

    private void OnDisable()
    {
        _level.Inited -= OnLevelInited;
        UnsubscribeFromDependencies();
    }

    private void OnLevelInited()
    {
        UnsubscribeFromDependencies();
        SubscribeToDependencies();
        _label.text = _beforeSelectionTip;
        _label.gameObject.SetActive(true);
    }

    private void OnPointed()
    {
        _label.text = _afterSelectionTip;
    }

    private void OnSwapped()
    {
        UnsubscribeFromDependencies();
        _label.gameObject.SetActive(false);
    }

    private void OnSwappedWithSame()
    {
        _label.text = _beforeSelectionTip;
    }

    private void SubscribeToDependencies()
    {
        _pointer.Pointed += OnPointed;
        _swapper.Swapped += OnSwapped;
        _swapper.SwappedWithSame += OnSwappedWithSame;
    }

    private void UnsubscribeFromDependencies()
    {
        _pointer.Pointed -= OnPointed;
        _swapper.Swapped -= OnSwapped;
        _swapper.SwappedWithSame -= OnSwappedWithSame;
    }
}
