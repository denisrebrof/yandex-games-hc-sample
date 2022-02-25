#if CRAZY_SDK
using CrazyGames;
using Zenject;
namespace SDK.HappyTime.controller
{
    public class CrazyHappyTimeController : IHappyTimeController
    {
        [Inject] private CrazySDK sdk;

        public void SetHappyTime(float intencity = 0.5f) => sdk.HappyTime();
    }
}
#endif