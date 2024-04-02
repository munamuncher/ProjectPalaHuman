using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SubjectScript : MonoBehaviour
{

    [SerializeField]
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        Debug.Log("observer hase been added");
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
    public void notifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnNotify();
        }
    }


}
