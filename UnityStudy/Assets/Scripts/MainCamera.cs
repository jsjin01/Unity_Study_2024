using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject Target;               // 플레이어

    public float offsetX = 0.0f;            // 카메라의 x좌표
    public float offsetY = 7.0f;           // 카메라의 y좌표
    public float offsetZ = -15.0f;          // 카메라의 z좌표

    //Vector3으로 한번에 받아서 더하는 것도 하나의 방법 

    public float CameraSpeed = 10.0f;       // 카메라의 속도
    Vector3 TargetPos;                      // 타겟의 위치

    void Update()
    {
        TargetPos = new Vector3(Target.transform.position.x + offsetX, Target.transform.position.y + offsetY, Target.transform.position.z + offsetZ);
        // 플레이어 좌표에 카메라의 좌표를 더하여 카메라의 위치를 변경

        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
        // 카메라의 움직임을 부드럽게 하는 함수(Lerp)
        /*.Lerp(회전각 a에서, 회전각 b까지, t의 속도로 회전)
           우리의 코드를 보면 "현재 회전각(transform.rotation)에서 바라보는 방향(TargetPos) 까지 
           우리가 지정한 회전속도(Time.deltaTime?* CameaSpeed) (deltaTime은 속도 보정값)로 회전한다"*/
    }
}
