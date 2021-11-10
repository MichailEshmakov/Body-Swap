using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Swapper _swapper;
    [SerializeField] private List<LevelParameters> _levelParameters;
    [SerializeField] private float _celebratingTime;
    [SerializeField] private Level _level;

    private List<Character> _characters;

    private int _currentLevel;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.CurrentLevel))
        {
            _currentLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevel);
            ResetCurrentLevelIfNeed();
        }
        else
        {
            _currentLevel = 0;
        }

        InitLevel();
    }


    private void OnEnable()
    {
        _level.Finishing += OnlevelFinishing;
    }

    private void OnDisable()
    {
        _level.Finishing -= OnlevelFinishing;
    }

    private void OnlevelFinishing()
    {
        StartCoroutine(WaitNextLevel());
    }

    private IEnumerator WaitNextLevel()
    {
        float timeLeft = _celebratingTime;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        SetNextLevel();
    }

    private void SetNextLevel()
    {
        _currentLevel++;
        ResetCurrentLevelIfNeed();
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevel, _currentLevel);
        InitLevel();
    }

    private void TrySetCharacters(int _levelIndex)
    {
        if (_levelIndex >= 0 && _levelIndex < _levelParameters.Count)
        {
            List<Character> characterPrefabs = _levelParameters[_levelIndex].CharacterPrefabs;
            List<Vector3> characterPositions = _levelParameters[_levelIndex].Positions;
            _characters = new List<Character>(characterPrefabs.Count);
            for (var i = 0; i < characterPrefabs.Count; i++)
            {
                Character newCharacter = Instantiate(characterPrefabs[i], characterPositions[i], Quaternion.identity);
                _characters.Add(newCharacter);
            }
        }
        else
        {
            Debug.LogError("Level index out of range");
        }
    }

    private void ResetCurrentLevelIfNeed()
    {
        if (_currentLevel >= _levelParameters.Count)
        {
            _currentLevel = 0;
        }
    }

    private void DestroyAllCharacters()
    {
        if (_characters != null)
        {
            foreach (Character character in _characters)
            {
                Destroy(character.gameObject);
            }
        }
    }

    private void InitLevel()
    {
        DestroyAllCharacters();
        TrySetCharacters(_currentLevel);
        _level.Init(_characters.Select(character => character.Celebrator).ToList(), _swapper);
        InitBodies();
    }

    private void InitBodies()
    {
        List<Body> bodies = _characters.Select(character => character.Body).ToList();
        _swapper.InitBodies(bodies, _level);
    }

    [ContextMenu("ResetCurrentLevelPrefs")]
    private void ResetCurrentLevelPref()
    {
        _currentLevel = 0;
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevel, _currentLevel);
        Debug.Log("Current level reset");
    }
}
