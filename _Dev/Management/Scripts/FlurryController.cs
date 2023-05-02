
using UnityEngine;
using FlurrySDK;

public class FlurryController : MonoBehaviour
{

#if UNITY_ANDROID
    private string FLURRY_API_KEY = "SB9SQCBVRYHVHKYKFVXT";
#elif UNITY_IPHONE
    private string FLURRY_API_KEY = "SB9SQCBVRYHVHKYKFVXT";
#else
    private string FLURRY_API_KEY = null;
#endif

    void Start()
    {
        // Initialize Flurry.
        new Flurry.Builder()
            .WithCrashReporting(true)
            .WithLogEnabled(true)
            .WithLogLevel(Flurry.LogLevel.VERBOSE)
            .WithMessaging(true)
            .Build(FLURRY_API_KEY);
    }
}