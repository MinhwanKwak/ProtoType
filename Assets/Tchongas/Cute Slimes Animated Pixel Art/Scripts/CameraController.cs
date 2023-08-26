using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimatedCuteSlimesPack
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float maxY;
        [SerializeField] float minY;

        public void MoveCamera(float val){
            transform.position += new Vector3(0f,val,0f);

            if(transform.position.y >= maxY){
                transform.position = new Vector3(transform.position.x, maxY,transform.position.z);
            }else if(transform.position.y <= minY){
                transform.position = new Vector3(transform.position.x, minY,transform.position.z);
            }
        }
    }
}