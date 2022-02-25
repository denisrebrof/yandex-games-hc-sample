using UnityEngine;

namespace SDK.HappyTime.controller
{
    public class DebugLogHappyTimeController: IHappyTimeController
    {
        public void SetHappyTime(float intencity = 0.5f) => Debug.Log("Happy Time! intencity = " + intencity);
    }
}