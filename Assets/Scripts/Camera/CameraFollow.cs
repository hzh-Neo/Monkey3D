using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    [Header("��������ƫ��")]
    public Vector3 offset;         // �������ҵ�ƫ����
    public float smoothSpeed = 0.125f;  // ���ƽ���ƶ����ٶ�

    void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.position;   // ����Ŀ��λ��
        Vector3 tagetPosition = new Vector3(desiredPosition.x, desiredPosition.y, 1) - offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, tagetPosition, smoothSpeed);  // ƽ���ƶ���Ŀ��λ��
        transform.position = smoothedPosition;  // �������λ��

        transform.LookAt(player.transform);  // ��ѡ��ʹ���ʼ�ճ������
    }

}
