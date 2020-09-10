using UnityEngine;

using System;
using System.Text;
using System.Collections;

namespace UnityUseful.IEnumeratorUtils
{
    public static class IEnumeratorUtils
    {
        static StringBuilder _string_builder = new StringBuilder();

        #region Enumerators

        public static IEnumerator ReverseTimer(int seconds, Action<int> OnSecondTick, Func<bool> break_condition, params Action[] finals)
        {
            var wait = new WaitForSeconds(1);

            while (seconds > 0 && !break_condition.Invoke())
            {
                OnSecondTick(seconds);

                seconds--;
                yield return wait;
            }

            foreach (var action in finals)
            {
                action();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="OnFrameTime">x->seconds, y-> Time.deltaTime </param>
        /// <param name="break_condition"></param>
        /// <param name="finals"></param>
        /// <returns></returns>
        public static IEnumerator ReverseTimer(float seconds, Action<float, float> OnFrameTime, Func<bool> break_condition, params Action[] finals)
        {
            while (seconds > 0 && !break_condition.Invoke())
            {
                seconds -= Time.deltaTime;
                OnFrameTime(seconds, Time.deltaTime);

                yield return null;
            }

            foreach (var action in finals)
            {
                action();
            }
        }
        public static IEnumerator ProgressTimer(float time, Action<float, float> prog_and_delta, bool unscaled = false, params Action[] finals)
        {
            var cur_time = 0f;

            while (time > cur_time)
            {
                var delta = unscaled ? Time.unscaledDeltaTime : Time.deltaTime;

                cur_time += delta;
                prog_and_delta(cur_time / time, delta);

                yield return null;
            }

            foreach (var action in finals)
            {
                action();
            }
        }
        public static IEnumerator Timer(float time, Action final)
        {
            yield return new WaitForSeconds(time);
            final?.Invoke();
        }
        public static IEnumerator CustomUpdate(float wait_time, Func<bool> condition, Action OnUpdate)
        {
            var wait = new WaitForSeconds(wait_time);

            while (condition())
            {
                yield return wait;
                OnUpdate();
            }

            Debug.Log("CustomUpdate End of work.");
        }
        public static IEnumerator WaitUntil(Func<bool> predicate, Action final)
        {
            yield return new WaitUntil(predicate);
            yield return null;

            final?.Invoke();
        }
        public static IEnumerator ChainIEnum(MonoBehaviour chain_parent, params IEnumerator[] actions)
        {
            foreach (IEnumerator action in actions)
            {
                yield return chain_parent.StartCoroutine(action);
            }
        }

#if UNITY_WEB_REQUEST
        public static IEnumerator Put(string url, byte[] data)
        {
            var www = UnityWebRequest.Put(url, data);
            www.chunkedTransfer = false;

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("PUT complete!");
            }
        } 
#endif

        #endregion

        public static void StopAllCoroutinesLogged(this MonoBehaviour mono, bool log_to_console = true)
        {
            if (log_to_console)
            {
                _string_builder.Clear();
                _string_builder.Append("<b>Utils.StopAllCoroutinesLogged</b> <color=red>Warning dangerous call!</color> obj: ");
                _string_builder.Append(mono.name);
                _string_builder.Append(" scene: ");
                _string_builder.Append(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

                var str = _string_builder.ToString();
                //$"<b>Utils.StopAllCoroutinesLogged</b> <color=red>Warning dangerous call!</color> obj: {mono.name} scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}";
                Debug.LogWarning(str, mono);
            }
            mono.StopAllCoroutines();
        }
    }
}
