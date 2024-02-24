using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


class Tank
{
    // 온갖 정보
    public float speed = 10.0f;
}

class FastTank: Tank
{

}


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;

    void Start()
    {
        Managers.input.MouseAction -= OnMouseClicked;
        Managers.input.MouseAction += OnMouseClicked;
    }


    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;
    
    void UpdateDie()
    {

    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            transform.LookAt(_destPos);
        }

        // 애니메이션
        Animator anim = GetComponent<Animator>();

    }

    void UpdateIdle()
    {
        // 애니메이션
        Animator anim = GetComponent<Animator>();

    }

    void Update()
    {

        switch(_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:    
                UpdateMoving();
                break;
            case PlayerState.Idle:
                 UpdateIdle();
                 break;
        }

    }

    void OnMouseClicked (Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
            // Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");
        }

    }
}
