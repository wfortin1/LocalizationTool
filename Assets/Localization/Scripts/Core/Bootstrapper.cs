using UnityEngine;

namespace Localization
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute()
        {
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Localization/LocalizationManager")));
        }
    }
}
