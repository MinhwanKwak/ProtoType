using System.Diagnostics;

public class TpLog
{
	[Conditional("TREEPLLA_LOG")]
	public static void Log(string message)
	{
		UnityEngine.Debug.Log(message);
	}

	[Conditional("TREEPLLA_LOG")]
	public static void LogWarning(string message)
	{
		UnityEngine.Debug.LogWarning(message);
	}

	[Conditional("TREEPLLA_LOG")]
	public static void LogError(string message)
	{
		UnityEngine.Debug.LogError(message);
	}
}