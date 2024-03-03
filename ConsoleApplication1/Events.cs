class MySubject
{
    private event Action PerformingAction;
    private event Action PerformedAction;
    
    public static void OnActionPerformed()
    {
        PerformedAction?.Invoke();
    }
}

class MyObserver
{
    private void Start()
    {
        MySubject.OnActionPerformed() += MySubject_ActionPerformed;
    }

    private void MySubject_ActionPerformed() { /* Blah blah */ }
    
}