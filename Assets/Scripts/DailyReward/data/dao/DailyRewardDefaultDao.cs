using System;
using UnityEngine;

namespace DailyReward.data.dao
{
    [CreateAssetMenu(fileName = "DailyRewardDefaultDao", menuName = "DailyReward/DailyRewardDefaultDao")]
    public class DailyRewardDefaultDao : ScriptableObject, PlayerPrefsDailyRewardRepository.IDailyRewardDao
    {
        [SerializeField] private int defaultRewardAmount = 0;
        [Header("Cooldown")] [SerializeField] private int cooldownDays = 0;
        [SerializeField] private int cooldownHours = 0;
        [SerializeField] private int cooldownMinutes = 0;
        [SerializeField] private int cooldownSeconds = 0;
        public int GetRewardAmount() => defaultRewardAmount;

        public TimeSpan GetRewardCooldown()
        {
            return new TimeSpan(cooldownDays * 24 + cooldownHours, cooldownMinutes, cooldownSeconds);
        }
    }
}