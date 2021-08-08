using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    
    public CharacterController controller;
    public float speed = 8f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private Player _photonPlayer;
    private int _id;

    [PunRPC]
    public void Inicializa(Player player)
    {
        _photonPlayer = player;
        _id = player.ActorNumber;

        print(GameManager.Instancia.gameObject.name);
        GameManager.Instancia.Jogadores.Add(this);

        if (!photonView.IsMine)
            controller.enabled = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // rotação bonitinha
            transform.rotation = Quaternion.Euler(0f, angle, 0f); // setar rotação

            controller.Move(direction * speed * Time.deltaTime);
        }

    }

}
