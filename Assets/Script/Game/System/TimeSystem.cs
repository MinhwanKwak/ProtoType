using UnityEngine;
using System.Collections;
using System;
using System.Globalization;
using UnityEngine.Networking;
using UniRx;

public class TimeSystem
{
    private static DateTime standardTime;
    public static DateTime TimeUTC => standardTime;	    //로그인 시간



    public IObservable<float> CreateCountDownObservable(float countTime) =>
        Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
            .Select(x => (countTime - x))
            .TakeWhile(x => x > 0);


    public static IEnumerator GetGoogleTime(Action onLoad)
    {
        var request = UnityWebRequest.Get("https://google.com");
        var async = request.SendWebRequest();
        yield return async;

        if (async.webRequest.result == UnityWebRequest.Result.Success)
        {
            var time = async.webRequest.GetResponseHeader("date");

            var dateTime = DateTime.ParseExact(time,
                                "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                CultureInfo.InvariantCulture.DateTimeFormat,
                                DateTimeStyles.AssumeUniversal);

            standardTime = dateTime.ToLocalTime();
        }
        else
        {
            standardTime = DateTime.UtcNow.ToLocalTime();
        }

        onLoad?.Invoke();
    }

    public static DateTime GetCurTime()
    {
        return TimeSystem.TimeUTC.AddSeconds(Time.realtimeSinceStartup);
    }

    public static DateTime GetConvertUSTimeFromString(string strTime)
    {
        IFormatProvider culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
        DateTime resultTime = default(DateTime);

        DateTime.TryParse(strTime, culture, System.Globalization.DateTimeStyles.None, out resultTime);

        return resultTime;


    }
}
