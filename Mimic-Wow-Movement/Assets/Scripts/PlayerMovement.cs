using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float MoveSpeed = 50;
    private float _w,_a,_s,_d,_verticalSpeed,_horizontalSpeed;

    void Update()
    {
        _w = Input.GetKey(KeyCode.W) ?  1 : 0;
        _a = Input.GetKey(KeyCode.A) ? -1 : 0;
        _s = Input.GetKey(KeyCode.S) ? -1 : 0;
        _d = Input.GetKey(KeyCode.D) ?  1 : 0;
        
        _verticalSpeed = _a + _d;
        _horizontalSpeed = _w + _s;
        
        transform.position += new Vector3(MoveSpeed*_verticalSpeed*Time.deltaTime,0,MoveSpeed*_horizontalSpeed*Time.deltaTime);
    }
}
