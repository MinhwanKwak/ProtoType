using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using UnityEngine.UI;
using UniRx;
[UIPath("UI/Page/HUDQuest", true)]
public class HUDQuest : UIBase
{
    [SerializeField]
    private Text TimeText;

    private CompositeDisposable disposable = new CompositeDisposable();

    public void Init()
    {

        GameRoot.Instance.PlayTimeSystem.RemainTimeProperty.Subscribe(x => {
            var playtime = GameRoot.Instance.UserData.CurMode.CurPlayDateTime = GameRoot.Instance.UserData.CurMode.CurPlayDateTime.AddDays(1);
            TimeText.text = $"{playtime.Year}y{playtime.Month}m{playtime.Day}d";

        }).AddTo(disposable);
    }


    private void OnDestroy()
    {
        disposable.Clear();
    }
}
