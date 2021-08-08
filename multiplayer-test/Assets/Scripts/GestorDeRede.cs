using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GestorDeRede : MonoBehaviourPunCallbacks // Providencia métodos de callbacks pra gente saber o resultado da conexão
{
    public static GestorDeRede Instancia { get; private set; } 

    private void Awake()
    {
        if (Instancia != null && Instancia != this) // Caso jogador volte pra tela de menu e vai pra tela de jogo novamente, não vai instanciar 2 redes
        {
            gameObject.SetActive(false);
            return;
        }

        Instancia = this; 
        DontDestroyOnLoad(gameObject); // Rede não ser destruida ao passar da cena de menu pra cena de jogo
    }

    private void Start() // Chamar network (bagulhinho criado no Photon)
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conezão bem sucedida");
    }

    public void CriaSala(string nomeSala)
    {
        PhotonNetwork.CreateRoom(nomeSala);
    }

    public void EntraSala(string nomeSala)
    {
        PhotonNetwork.JoinRoom(nomeSala);
    }

    public void MudaNick(string nickname)
    {
        PhotonNetwork.NickName = nickname;
    }

    public string ObterListaDeJogadores()
    {
        var lista = "";
        foreach(var player in PhotonNetwork.PlayerList)
        {
            lista += player.NickName + "\n";
        }
        return lista;
    }

    public bool DonoDaSala()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public void SairDoLobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    public void ComecaJogo(string nomeCena)
    {
        PhotonNetwork.LoadLevel(nomeCena);
    }
}
