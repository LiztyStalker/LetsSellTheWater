//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.36373
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

#if UNITY_EDITOR
#define ENABLE_LOG
#endif

//로그 관리 - 오버로드

using UnityEngine;
public static class Debug
{

	public static bool isDebugBuild{
		get { return UnityEngine.Debug.isDebugBuild;}
	}

	[System.Diagnostics.Conditional("ENABLE_LOG")]
	public static void Log(object message){
		UnityEngine.Debug.Log (message);
	}

	[System.Diagnostics.Conditional("ENABLE_LOG")]
	public static void LogError(object message){
		UnityEngine.Debug.LogError (message);
	}

	[System.Diagnostics.Conditional("ENABLE_LOG")]
	public static void LogError(object message, UnityEngine.Object context){
		UnityEngine.Debug.LogError (message, context);
	}

	[System.Diagnostics.Conditional("ENABLE_LOG")]
	public static void LogWarning(object message){
		UnityEngine.Debug.LogWarning (message);
	}

	[System.Diagnostics.Conditional("ENABLE_LOG")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float duration = 0.0f, bool depthText = true){
		UnityEngine.Debug.DrawLine (start, end, color, duration, depthText);
	}

	[System.Diagnostics.Conditional("ENABLE_LOG")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color = default(Color), float duration = 0.0f, bool depthText = true){
		UnityEngine.Debug.DrawRay (start, dir, color, duration, depthText);
	}

	[System.Diagnostics.Conditional("ENABLE_LOG")]
	public static void Assert(bool condition){
		if (!condition)
			throw new System.Exception ();
	}
}


