using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnlocker : MonoBehaviour
{
    InputReader _reader;
    private void Awake() => _reader = FindObjectOfType<InputReader>();
    public void StartCameraRoutine(GameObject camera, float cameraDuration)
    {
        StartCoroutine(CameraRoutine(camera, cameraDuration));
    }
    IEnumerator CameraRoutine(GameObject camera, float cameraDuration)
    {
        _reader.Disable();
        camera.SetActive(true);
        yield return new WaitForSeconds(cameraDuration);
        camera.SetActive(false);
        _reader.Enable();
    }
}
