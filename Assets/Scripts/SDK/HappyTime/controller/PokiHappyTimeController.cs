#if POKI_SDK

namespace SDK.HappyTime.controller
{
    public class PokiHappyTimeController: IHappyTimeController
    {
        [Inject] private PokiUnitySDK sdk;

        public void SetHappyTime(float intencity = 0.5f)
        {
            sdk.happyTime(intensity);
        }
    }
}
#endif
