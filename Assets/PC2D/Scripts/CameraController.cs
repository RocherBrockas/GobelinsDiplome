using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    public float cameraZpos = -10f;
    public float cameraXoffset = 5f;
    public float cameraYoffset = 1f;

    public float horizontalSpeed = 2f;
    public float verticalSpeed = 2f;

    private Transform _camera;
    private PlatformerMotor2D _playerController;

    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        _playerController = player.GetComponent<PlatformerMotor2D>();

        _camera = Camera.main.transform;

        _camera.position = new Vector3(player.transform.position.x + cameraXoffset,
            player.transform.position.y + cameraYoffset,
            player.transform.position.z + cameraZpos);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerController.facingLeft)
        {
            _camera.position = new Vector3(
                Mathf.Lerp(_camera.position.x, player.transform.position.x + cameraXoffset, horizontalSpeed * Time.deltaTime),
                Mathf.Lerp(_camera.position.y, player.transform.position.y + cameraYoffset, verticalSpeed * Time.deltaTime),
                cameraZpos);
        }
        else
        {
            _camera.position = new Vector3(
                Mathf.Lerp(_camera.position.x, player.transform.position.x - cameraXoffset, horizontalSpeed * Time.deltaTime),
                Mathf.Lerp(_camera.position.y, player.transform.position.y + cameraYoffset, verticalSpeed * Time.deltaTime),
                cameraZpos);
        } // Ajouter les up and down arrow pour regarder en haut et en bas
    }
}
