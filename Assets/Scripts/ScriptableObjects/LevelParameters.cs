using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Parameters", menuName = "Level Parameters", order = 51)]
public class LevelParameters : ScriptableObject
{
    [SerializeField] private List<Vector3> _positions;
    [SerializeField] private List<Character> _characterPrefabs;

    public List<Vector3> Positions => CopyList(_positions);
    public List<Character> CharacterPrefabs => CopyList(_characterPrefabs);

    private void OnValidate()
    {
        if (_characterPrefabs.Count > _positions.Count)
        {
            Debug.LogWarning("The number of models cannot be more than the number of positions");
            while (_characterPrefabs.Count <= _positions.Count)
            {
                _characterPrefabs.RemoveAt(_characterPrefabs.Count - 1);
            }
        }
    }

    private List<T> CopyList<T>(List<T> list)
    {
        T[] result = new T[list.Count];
        list.CopyTo(result);
        return result.ToList();
    }

    //public List<T> GetComponentsInModels<T>() where T : MonoBehaviour
    //{
    //    List<T> components = new List<T>(_characterPrefabs.Count);
    //    foreach (Model model in _characterPrefabs)
    //    {
    //        if (model.TryGetComponent(out T component))
    //        {
    //            components.Add(component);
    //        }
    //    }

    //    return components;
    //}


}
