using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayTimeSystem
{

    public IReactiveProperty<int> RemainTimeProperty = new ReactiveProperty<int>();

    private float deltaTime = 0f;

    

    public void Update()
    {
        if (deltaTime < 1f)
        {
            deltaTime += Time.deltaTime;
            return;
        }

        deltaTime -= 1f;

        RemainTimeProperty.Value += 1;
    }

}
