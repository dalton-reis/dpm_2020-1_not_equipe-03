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
    public static Dictionary<Bloco, string> Instrucoes = new Dictionary<Bloco, string>();

    public string[] locais;
    public void SetSelected(string bloco)
    {
        Resultado.UltimoBlocoVisitado = string.Empty;
        Instrucoes = new Dictionary<Bloco, string>();
        bloco = bloco.Remove(0, 6);
        var find = new Caminhos();
        LabelBlocoSelecionado = GameObject.Find("LabelBlocoSelecionado").GetComponent<Text>();

        locais = find.FindPath(bloco).Split('>');
        var caminhos = new List<string>();
        Bloco ultimoBloco = null;
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
            var str = string.Empty;
            if (i + 1 == locais.Length - 1)
            {
                str = $"para chegar no seu destino, o bloco {proximoLocal.Nome}";
                ultimoBloco = proximoLocal;
            }
            else
            {
                str = $"até chegar no bloco {proximoLocal.Nome}";
            }
            if (!string.IsNullOrEmpty(caminhoX) && !string.IsNullOrEmpty(caminhoY))
                conector = " e ";

            var texto = string.Empty;
            if (local.PontosReferencia.ContainsKey(proximoLocal))
                texto = local.PontosReferencia.FirstOrDefault(p => p.Key == proximoLocal).Value + $" depois {(!string.IsNullOrEmpty(caminhoX) ? caminhoX : "ande")} {str}";
            else
                texto = $"{caminhoX}{conector}{caminhoY} " + str;

            Instrucoes.Add(local, texto);

        }
        Instrucoes.Add(ultimoBloco, "Este é o seu destino!");
        var menu = new Menu();
        menu.ChangeScene(ScenesNames.Resultado);
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
