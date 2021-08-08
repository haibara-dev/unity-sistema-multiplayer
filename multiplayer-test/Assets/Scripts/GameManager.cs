using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks // Só vai começar o jogo quando todos os jogadores da lista estiverem conectados
{
    public static GameManager Instancia { get; private set; }
    
    [SerializeField] private string _localizacaoPrefab;
    [SerializeField] private Transform[] _spawns; 

    private int _jogadoresEmJogo = 0;
    private List<PlayerMovement> _jogadores;
    public List<PlayerMovement> Jogadores = new List<PlayerMovement>();

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

    private void Start()
    {
        photonView.RPC("AdicionaJogador", RpcTarget.AllBuffered);
        _jogadores = new List<PlayerMovement>();

        Instancia = GetComponent<GameManager>();
    }

    [PunRPC] // Lista de jogadores seja comum a todos os jogadores 
    private void AdicionaJogador()
    {
        _jogadoresEmJogo++;
        if(_jogadoresEmJogo == PhotonNetwork.PlayerList.Length)
        {
            CriaJogador();
        }
    }

    private void CriaJogador()
    {
        var jogadorObj = PhotonNetwork.Instantiate(_localizacaoPrefab, _spawns[Random.Range(0, _spawns.Length)].position, Quaternion.identity);
        var jogador = jogadorObj.GetComponent<PlayerMovement>();

        jogador.photonView.RPC("Inicializa", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
