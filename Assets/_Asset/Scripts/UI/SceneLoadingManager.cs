using System;

public static class SceneLoadingManager
{
    public static event Action OnLoadingComplete;

    public static void NotifyLoadingComplete()
    {
        OnLoadingComplete?.Invoke();
    }
}