using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _listaDeJogadores;
    [SerializeField] private Button _comecarJogo;

    [PunRPC]
    public void AtualizaLista()
    {
        _listaDeJogadores.text = GestorDeRede.Instancia.ObterListaDeJogadores();
        _comecarJogo.interactable = GestorDeRede.Instancia.DonoDaSala(); 
    }


}
