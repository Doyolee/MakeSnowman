using UnityEngine;

public class HeroUICameraController : MonoBehaviour
{

    public float cameraSpeed = 0.1f;

    Vector3 mouseStart;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
             mouseStart= Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {

            Vector3 direction=mouseStart-Input.mousePosition;
            Vector3 movedirection =Vector3.down*direction.y * cameraSpeed * Time.deltaTime;
            transform.position += movedirection;     
            mouseStart= Input.mousePosition;
        }
    }
}
