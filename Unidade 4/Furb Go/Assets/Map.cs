using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public Text LabelBlocoSelecionado;
    public string BlocoSelecionado = string.Empty;

    public string[] locais;
    public void SetSelected(string bloco)
    {
        bloco = bloco.Remove(0, 6);
        var find = new Caminhos();
        LabelBlocoSelecionado = GameObject.Find("LabelBlocoSelecionado").GetComponent<Text>();

        locais = find.FindPath(bloco).Split('>');
        var caminhos = new List<string>();
        for (int i = 0; i < locais.Length - 1; i++)
        {
            var local = RetornarLocal(locais[i]);
            var proximoLocal = RetornarLocal(locais[i + 1]);
            var caminhoX = string.Empty;
            var caminhoY = string.Empty;

            if (local.OrdemX > proximoLocal.OrdemX)
                caminhoX = "ande para esquerda";
            if (local.OrdemX < proximoLocal.OrdemX)
                caminhoX = "ande para direita";
            if (local.OrdemX == proximoLocal.OrdemX)
                caminhoX = string.Empty;

            if (local.OrdemY > proximoLocal.OrdemY)
                caminhoY = "desça";
            if (local.OrdemY < proximoLocal.OrdemY)
                caminhoY = "suba";
            if (local.OrdemY == proximoLocal.OrdemY)
                caminhoY = string.Empty;

            var conector = string.Empty;
            var str = i + 1 == locais.Length - 1
                ? $"para chegar no seu destino, o bloco {proximoLocal.Nome}"
                : $"até chegar no bloco {proximoLocal.Nome}";
            if (!string.IsNullOrEmpty(caminhoX) && !string.IsNullOrEmpty(caminhoY))
                conector = " e ";

            if (local.PontosReferencia.ContainsKey(proximoLocal))
                caminhos.Add(local.PontosReferencia.FirstOrDefault(p => p.Key == proximoLocal).Value + $" depois {(!string.IsNullOrEmpty(caminhoX) ? caminhoX : "ande")} {str}");
            else
                caminhos.Add($"{caminhoX}{conector}{caminhoY} " + str);
        }
        foreach (var passo in caminhos)
            LabelBlocoSelecionado.text += passo + " \n";

    }

    private static Bloco RetornarLocal(string nome)
    {
        var local = Caminhos.blocos.FirstOrDefault(p => p.Nome == nome);
        return local;
    }

    void desenharCaminhoNoMapa()
    {
        //TODO

    }
}
