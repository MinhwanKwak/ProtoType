using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneSystem
{
    private SceneInstance curLoadInstance;
    private Dictionary<InGameType, string> dicScenePath = new Dictionary<InGameType, string>()
    {
        {InGameType.Main, "InGameTycoon"},
        {InGameType.Event, "InGameTycoon" }
    };

    public void ChangeScene(InGameType type, Action callback = null)
    {
        if(!dicScenePath.ContainsKey(type))
        {
            TpLog.LogError($"SceneSystem::ChangeScene type: {type.ToString()} is not found key");
            return;
        }        

        Addressables.LoadSceneAsync(dicScenePath[type]).Completed += (handle) => {
            curLoadInstance = handle.Result;
            callback?.Invoke();
            handle.Destroyed += (d) => {                                
                LocalizeString.Localizelist.Clear();
            };
        };
    }

    public void UnLoadScene(Action callback = null)
    {
        //single mode는 필요 없다
        if (!curLoadInstance.Equals(default(SceneInstance)))
            Addressables.UnloadSceneAsync(curLoadInstance).Completed += (handle) =>
            {
                callback?.Invoke();
            };
        else
            callback?.Invoke();
    }
}
