using System;
using UnityEngine;

public class GoogleAdmobAd : ScriptableObject
{
    [TextArea(2, 5), SerializeField] private string Id = "ca-app-pub-2277678404900326/8172759670";
    [TextArea(2, 5), SerializeField] private string TestId = "ca-app-pub-3940256099942544/1033173712";


    protected virtual void Request() => throw new NotImplementedException();
    public virtual bool TryShow() => throw new NotImplementedException();
    public virtual void TryShow_() => throw new NotImplementedException();

    protected string GetId()
    {
        return TestId;
    }
}
