using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// �÷��̾� ĳ������ ����.
// ���, �̵�, ����, ���.
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
                    // ���� �̵� ó��.
                }
                break;

            case CharacterState.Jump:
                {
                    // ���� ���� ó��.
                }
                break;

            case CharacterState.Die:
                {

                }
                break;
        }
    }
}
