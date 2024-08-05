using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimeIEnumerator
{
    public delegate void TimerCallback(float t);

    public static IEnumerator StartIEnumerator(float duration, TimerCallback callback)
    {
        // ����ʱ��
        float currentTime = 0.0f;

        // ��ֵ
        while (currentTime <= duration)
        {
            // �����ֵ
            float t = currentTime / duration;
            if (t >= 0.99)
            {
                t = 1;
                callback.Invoke(t);
            }
            else
            {
                callback.Invoke(t);
            }
        
            // ����ʱ��
            currentTime += Time.deltaTime;
            // �ȴ���һ֡
            yield return Time.deltaTime;
        }

    }
}
