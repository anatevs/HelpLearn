using Mirror;
using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class MatchTimer : NetworkBehaviour
    {
        public event Action OnTimerStarted;

        public event Action<double> OnRemainTimeChanged;

        public event Action OnTimerEnded;

        [SyncVar(hook = nameof(HookChangeRemainTime))]
        private double _remainTime;

        [Server]
        public void StartTimer(float matchTime)
        {
            OnTimerStarted?.Invoke();

            ChangeRemain(matchTime);

            var endTime = NetworkTime.time + _remainTime;

            StartCoroutine(RechargeCoroutine(endTime));
        }

        [Server]
        private void ChangeRemain(double remain)
        {
            _remainTime = remain;

            if (isServer)
            {
                OnRemainTimeChanged?.Invoke(_remainTime);
            }
        }

        private void HookChangeRemainTime(double oldValue, double newValue)
        {
            if (isServer)
            {
                return;
            }

            OnRemainTimeChanged?.Invoke(newValue);
        }

        private IEnumerator RechargeCoroutine(double endTime)
        {
            var remain = endTime - NetworkTime.time;

            while (remain > 0)
            {
                ChangeRemain(remain);

                remain = endTime - NetworkTime.time;
                remain = Math.Max(0, remain);

                yield return null;
            }

            ChangeRemain(remain);

            OnTimerEnded?.Invoke();
        }
    }
}