using UnityEngine;

namespace VoidLess.Game.UniSpecific
{
    public class CameraFaceRotator : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
        }
    }
}
