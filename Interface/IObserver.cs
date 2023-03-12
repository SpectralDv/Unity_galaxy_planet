using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IObserver
{
    //public IObserver(){}
    void UpdateObserver();
}

public interface IObservable
{
    //public IObservable(){}
    void AddObserver(IObserver o);
    void RemoveObserver();
    void Notify();
}
