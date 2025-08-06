using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// 플레이어 캐릭터의 상태.
// 대기, 이동, 점프, 사망.
public enum CharacterState
{
    Idle,
    Move,
    Jump,
    Die,
}

public class StateMachine : MonoBehaviour
{
    CharacterState state = CharacterState.Idle;

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if(moveInput != 0.0f)
        {
            state = CharacterState.Move;
        }

        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            state = CharacterState.Jump;
        }

        UpdateState();
    }

    void UpdateState()
    {
        switch (state)
        {
            case CharacterState.Idle:
                {

                }
                break;

            case CharacterState.Move:
                {
                    // 실제 이동 처리.
                }
                break;

            case CharacterState.Jump:
                {
                    // 실제 점프 처리.
                }
                break;

            case CharacterState.Die:
                {

                }
                break;
        }
    }
}
