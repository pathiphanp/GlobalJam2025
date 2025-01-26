using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject selectHummanCamera;
    [SerializeField] GameObject lookStatusHumman;

    GameObject nowCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.cameraManager = this;
        nowCamera = selectHummanCamera;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLookAtHumman(Vector3 _posCamera)
    {
        if (_posCamera != Vector3.zero)
        {
            GameManager.instance.chooseTargetText.SetActive(false);
            _posCamera.x -= 0.6f;
            _posCamera.z = -10;
            lookStatusHumman.transform.position = _posCamera;
            ChangeCamera(lookStatusHumman);
            Invoke("OpenBtnQusetion", 3f);
        }
        else
        {
            ChangeCamera(selectHummanCamera);
            GameManager.instance.chooseTargetText.SetActive(true);
        }
    }
    void OpenBtnQusetion()
    {
        GameManager.instance.SetBtnQusetion(true);
    }
    void ChangeCamera(GameObject _camera)
    {
        nowCamera.SetActive(false);
        _camera.SetActive(true);
        nowCamera = _camera;
    }

}
