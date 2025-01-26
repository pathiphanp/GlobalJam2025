using System.Collections;
using UnityEngine;

public class OnObjectSelection : MonoBehaviour
{
    Coroutine chanageCamera;
    bool onLook = false;
    private void OnMouseDown()
    {
        onLook = !onLook;
        if (onLook)
        {
            if (chanageCamera != null)
            {
                StopCoroutine(chanageCamera);
            }
            GameManager.instance.cameraManager.SetLookAtHumman(this.transform.position);
            GameManager.instance.hummanDescription = this.GetComponent<HummanDescription>();
        }
        else
        {
            chanageCamera = StartCoroutine(DelayLookHumman());
            GameManager.instance.hummanDescription = null;
        }
    }

    IEnumerator DelayLookHumman()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.cameraManager.SetLookAtHumman(Vector3.zero);
        chanageCamera = null;
    }
}
