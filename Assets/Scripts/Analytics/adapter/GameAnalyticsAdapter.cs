#if GAME_ANALYTICS
using System.Collections.Generic;
using Analytics.ads;
using Analytics.levels;
using Analytics.screens;
using Analytics.session.domain;
using Analytics.settings;
using GameAnalyticsSDK;
using UnityEngine;

namespace Analytics.adapter
{
    public class GameAnalyticsAdapter : AnalyticsAdapter
    {
        private static string defaultPostfix = "Undefined";

        public override void SendAdEvent(AdAction action, AdType type, AdProvider provider, IAdPlacement placement)
        {
            Debug.Log(" SendAdEvent: " + action + ' ' + type);
            var gAAction = action switch
            {
                AdAction.Request => GAAdAction.Request,
                AdAction.Show => GAAdAction.Show,
                AdAction.Failure => GAAdAction.FailedShow,
                _ => GAAdAction.Undefined
            };
            var gAType = type switch
            {
                AdType.Rewarded => GAAdType.RewardedVideo,
                AdType.Interstitial => GAAdType.Interstitial,
                _ => GAAdType.Undefined
            };
            GameAnalytics.NewAdEvent(gAAction, gAType, provider.ToString(), placement.GetName());
        }

        public override void SendSettingsEvent(SettingType type, string val)
        {
            Debug.Log("SendSettingsEvent: " + type + ' ' + val);
            var eventName = "Setting_" + type switch
            {
                SettingType.SoundToggle => "Sound",
                _ => defaultPostfix
            };
            var param = new Dictionary<string, object> { { "value", val } };
            GameAnalytics.NewDesignEvent(eventName, param);
        }

        public override void SendScreenEvent(string screenName, ScreenAction action)
        {
            Debug.Log("SendScreenEvent: " + screenName + ' ' + action);
            var eventName = "Screen_" + action switch
            {
                ScreenAction.Open => "Open",
                ScreenAction.Close => "Close",
                _ => defaultPostfix
            };
            var param = new Dictionary<string, object> { { "screenName", screenName } };
            GameAnalytics.NewDesignEvent(eventName, param);
        }

        public override void SendLevelEvent(LevelPointer levelPointer, LevelEvent levelEvent)
        {
            Debug.Log("SendLevelEvent: " + levelPointer.LevelId + ' ' + levelEvent);
            if (levelEvent == LevelEvent.Load)
            {
                SendLoadLevelEvent(levelPointer);
                return;
            }

            var gAProgressionStatus = levelEvent switch
            {
                LevelEvent.Start => GAProgressionStatus.Start,
                LevelEvent.Fail => GAProgressionStatus.Fail,
                LevelEvent.Complete => GAProgressionStatus.Complete,
                _ => GAProgressionStatus.Undefined
            };
            ;
            GameAnalytics.NewProgressionEvent(gAProgressionStatus, "Level_" + levelPointer.LevelId);
        }

        public override void SendSessionEvent(SessionEvent sessionEvent, LevelPointer currentLevelPointer)
        {
            Debug.Log("SendSessionEvent: " + sessionEvent);
            var param = new Dictionary<string, object> { { "level", currentLevelPointer.LevelId } };
            var eventName = sessionEvent switch
            {
                SessionEvent.Start => "Game Session Started",
                SessionEvent.Quit => "Quit",
                _ => defaultPostfix
            };
            GameAnalytics.NewDesignEvent(eventName, param);
        }

        public override void SendErrorEvent(string error)
        {
            Debug.Log("SendErrorEvent: " + error);
            GameAnalytics.NewErrorEvent(GAErrorSeverity.Error, error);
        }

        public override void SetPlayerId(string id)
        {
            Debug.Log("Set player id: " + id);
            GameAnalytics.SetCustomId(id);
            GameAnalytics.Initialize();
        }

        public override void InitializeWithoutPlayerId()
        {
            Debug.Log("InitializeWithoutPlayerId");
            GameAnalytics.Initialize();
        }

        public override void SendFirstOpenEvent()
        {
            Debug.Log("SendFirstOpenEvent");
            GameAnalytics.NewDesignEvent("FirstOpen");
        }

        public override void SendPurchasedEvent(long purchaseId)
        {
            Debug.Log("SendPurchasedEvent");
            GameAnalytics.NewResourceEvent(
                GAResourceFlowType.Sink,
                "coins",
                1,
                defaultPostfix,
                purchaseId.ToString()
            );
        }
        
        public override void SendBalanceAddedEvent(int amount)
        {
            Debug.Log("SendBalanceAddedEvent");
            GameAnalytics.NewResourceEvent(
                GAResourceFlowType.Source,
                "coins",
                amount,
                defaultPostfix,
                "coins"
            );
        }

        private void SendLoadLevelEvent(LevelPointer levelPointer)
        {
            var param = new Dictionary<string, object> { { "levelId", levelPointer.LevelId } };
            GameAnalytics.NewDesignEvent("ManualLoadLevel", param);
        }
    }
}
#endif