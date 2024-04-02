using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNarrationSystem : MonoBehaviour, IObserver
{
    [SerializeField] SubjectScript _playerSubject;

    public void OnNotify()
    {
        Debug.Log("Player has Been Notified");
    }

    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        _playerSubject?.RemoveObserver(this);
    }
}
