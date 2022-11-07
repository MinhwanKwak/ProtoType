using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        var init = Addressables.InitializeAsync();
        yield return init;
        GameRoot.Load();
    }
}
