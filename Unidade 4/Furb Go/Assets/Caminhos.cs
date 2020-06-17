using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Caminhos : MonoBehaviour
{
    public static List<Bloco> blocos = new List<Bloco>();
    private bool achouCaminho = false;

    private List<Bloco> caminhos = new List<Bloco>();

    void Start()
    {

    }

    void PopularLista()
    {
        blocos.Clear();
        Bloco A = new Bloco("A", -3, 0);
        Bloco B = new Bloco("B", -2, 0);
        Bloco C = new Bloco("C", -1, 0);
        Bloco D = new Bloco("D", 0, 1); // Exceção
        Bloco E = new Bloco("E", 0, 0);
        Bloco F = new Bloco("F", 1, 0);
        Bloco G = new Bloco("G", 2, 0);
        Bloco H = new Bloco("H", 3, 0);
        Bloco I = new Bloco("I", -2, 1);
        Bloco J = new Bloco("J", 4, 0);
        Bloco K = new Bloco("K", 6, 0);
        Bloco L = new Bloco("L", 4, -2);
        Bloco M = new Bloco("M", -2, -3);
        Bloco N = new Bloco("N", 5, -1);
        Bloco Q = new Bloco("Q", -4, 1);
        Bloco R = new Bloco("R", -3, 2);
        Bloco S = new Bloco("S", -2, 2);
        Bloco T = new Bloco("T", -1, 2);
        Bloco U = new Bloco("U", -6, 2);
        Bloco V = new Bloco("V", -5, 3);
        Bloco W = new Bloco("W", -4, 3);
        A.Vizinhos = new List<Bloco>() { B, M };
        B.Vizinhos = new List<Bloco>() { I, A, C };
        C.Vizinhos = new List<Bloco>() { I, B, D };
        E.Vizinhos = new List<Bloco>() { F };
        D.Vizinhos = new List<Bloco>() { C, F };
        F.Vizinhos = new List<Bloco>() { E, G };
        G.Vizinhos = new List<Bloco>() { F, H, J, L, N, K };
        I.Vizinhos = new List<Bloco>() { B, C, T, S, R, Q };
        T.Vizinhos = new List<Bloco>() { I, S, Q };
        S.Vizinhos = new List<Bloco>() { T, R, I, W, V, U, Q };
        R.Vizinhos = new List<Bloco>() { Q, S, I, U};
        Q.Vizinhos = new List<Bloco>() { I, R, S, T };
        U.Vizinhos = new List<Bloco>() { V, R };
        V.Vizinhos = new List<Bloco>() { U, W, S };
        W.Vizinhos = new List<Bloco>() { V, S };
        H.Vizinhos = new List<Bloco>() { G, J, L, N, K };
        J.Vizinhos = new List<Bloco>() { H, G, L, N, K };
        L.Vizinhos = new List<Bloco>() { H, G, J, N, K };
        N.Vizinhos = new List<Bloco>() { H, G, J, L, K };
        K.Vizinhos = new List<Bloco>() { H, G, J, L, N };
        M.Vizinhos = new List<Bloco>() { A };
        I.PontosReferencia = new Dictionary<Bloco, string>
        {
            { R, PontosDeReferencia.IParaRSTQ },
            { S, PontosDeReferencia.IParaRSTQ },
            { T, PontosDeReferencia.IParaRSTQ },
            { Q, PontosDeReferencia.IParaRSTQ },
            { A, PontosDeReferencia.IParaABC },
            { B, PontosDeReferencia.IParaABC },
            { C, PontosDeReferencia.IParaABC }
        };
        A.PontosReferencia.Add(M, PontosDeReferencia.AParaM);


        blocos.AddRange(new[] { A, B, C, D, E, F, G, H, I, J, K, L, M, N, Q, R, S, T, U, V, W });
    }
    public string FindPath(string blocoDestino)
    {
        if (!string.IsNullOrEmpty(Init.qrCode))
        {
            achouCaminho = false;
            PopularLista();
            Bloco origem = blocos.Find(p => p.Nome == Init.qrCode);
            Bloco destino = blocos.Find(q => q.Nome == blocoDestino);
            caminhos.Add(origem);
            if (origem.Nome == destino.Nome)
                return "Você já está no seu destino!";

            SearchVizinhos(origem, destino);
            return string.Join(">", caminhos.Select(c => c.Nome));
        }
        return "Por favor escaneie o QrCode de localização mais proximo";
    }
    void SearchVizinhos(Bloco origem, Bloco destino)
    {
        if (!achouCaminho)
        {
            foreach (var vizinho in origem.Vizinhos)
            {
                if (vizinho.Nome == destino.Nome)
                {
                    achouCaminho = true;
                    caminhos.Add(vizinho);
                    break;
                }
                if (!caminhos.Contains(vizinho))
                {
                    caminhos.Add(vizinho);
                    SearchVizinhos(vizinho, destino);
                    if (achouCaminho)
                    {
                        break;
                    }
                }
            }
            if (!achouCaminho)
                caminhos.Remove(origem);
        }
    }
}


