using System;
using UnityEngine;

public abstract class MySubject
{
    public static MySubject Instance;
    
    public event Action PerformingAction;
    public event Action PerformedAction;

    private void Awake()
    {
        Instance ??= this;
    }
    
    public void OnActionPerformed()
    {
        PerformedAction?.Invoke();
    }
}

public class MyObserver : MonoBehaviour
{
    private void Start()
    {
        MySubject.Instance.PerformedAction += MySubject_PerformendAction;
    }

    public void MySubject_PerformendAction()
    {
        // Code ...
    }
}
