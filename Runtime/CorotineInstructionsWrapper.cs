using UnityEngine;

using System;
using System.Collections;

using static UnityUseful.IEnumeratorUtils.IEnumeratorUtils;

namespace UnityUseful.CorotineInstructionsWrappers
{
    public static class CIWrappers
    {
        #region CoroutineWrappers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OnSecondTick">Seconds left(integer)</param>
        /// <param name="OnConditionCheck"></param>
        /// <param name="OnFinish"></param>
        /// <returns></returns>
        public static Coroutine ReverseTimerV(this MonoBehaviour mono, int seconds, Action<int> OnSecondTick, Func<bool> OnConditionCheck, params Action[] OnFinish)
        {
            return mono.StartCoroutine(ReverseTimer(seconds, OnSecondTick, OnConditionCheck, OnFinish));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OnFrameTime">1 - Seconds left, 2 - Time.deltaTime</param>
        /// <param name="OnConditionCheck"></param>
        /// <param name="OnFinish"></param>
        /// <returns></returns>
        public static Coroutine ReverseTimerV(this MonoBehaviour mono, float seconds, Action<float, float> OnFrameTime, Func<bool> break_condition, params Action[] finals)
        {
            return mono.StartCoroutine(ReverseTimer(seconds, OnFrameTime, break_condition, finals));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="time"></param>
        /// <param name="prog_and_delta">1 - progress[0..1], 2 - deltaTime(scaled or unscaled, depends on next argument)</param>
        /// <param name="unscaled"></param>
        /// <param name="finals"></param>
        /// <returns></returns>
        public static Coroutine ProgressTimerV(this MonoBehaviour mono, float time, Action<float, float> prog_and_delta,bool unscaled = false, params Action[] finals)
        {
            return mono.StartCoroutine(ProgressTimer(time, prog_and_delta, unscaled, finals));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">How much to wait?</param>
        /// <param name="final">OnComplete callback</param>
        /// <returns></returns>
        public static Coroutine TimerV(this MonoBehaviour mono, float time, Action final)
        {
            return mono.StartCoroutine(Timer(time, final));
        }
        
        public static Coroutine CustomUpdateV(this MonoBehaviour mono, float time, Func<bool> condition, Action OnUpdate)
        {
            return mono.StartCoroutine(CustomUpdate(time, condition, OnUpdate));
        }
        
        public static Coroutine WaitUntilV(this MonoBehaviour mono, Func<bool> predicate, Action final)
        {
            return mono.StartCoroutine(WaitUntil(predicate, final));
        }
        
        public static Coroutine ChainV(this MonoBehaviour mono, params IEnumerator[] enums)
        {
            return mono.StartCoroutine(ChainIEnum(mono, enums));
        }

        #endregion
    }
}
