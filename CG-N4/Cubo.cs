/**
  Autor: Dalton Solano dos Reis
**/

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
namespace gcgcg
{
  internal class Cubo : ObjetoGeometria
  {
    private bool exibeVetorNormal = false;
    public Cubo(string rotulo, Objeto paiRef) : base(rotulo, paiRef)
    {      
      base.PontosAdicionar(new Ponto4D(-1, -1, 1)); // PtoA listaPto[0]
      base.PontosAdicionar(new Ponto4D(1, -1, 1)); // PtoB listaPto[1]
      base.PontosAdicionar(new Ponto4D(1, 1, 1)); // PtoC listaPto[2]
      base.PontosAdicionar(new Ponto4D(-1, 1, 1)); // PtoD listaPto[3]
      base.PontosAdicionar(new Ponto4D(-1, -1, -1)); // PtoE listaPto[4]
      base.PontosAdicionar(new Ponto4D(1, -1, -1)); // PtoF listaPto[5]
      base.PontosAdicionar(new Ponto4D(1, 1, -1)); // PtoG listaPto[6]
      base.PontosAdicionar(new Ponto4D(-1, 1, -1)); // PtoH listaPto[7]
    }
    
    protected override void DesenharObjeto()
    {       // Sentido anti-horário
        GL.Begin(PrimitiveType.Quads);
        // Face da frente
        GL.Color3(OpenTK.Color.Red);
        GL.Normal3(0, 0, 1);        
        GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
        GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
        GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
        GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
        // Face do fundo
        GL.Color3(OpenTK.Color.Green);
        GL.Normal3(0, 0, -1);
        GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
        GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
        GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
        GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
        // Face de cima
        GL.Color3(OpenTK.Color.Blue);
        GL.Normal3(0, 1, 0);
        GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
        GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
        GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
        GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
        // Face de baixo
        GL.Color3(OpenTK.Color.Yellow);
        GL.Normal3(0, -1, 0);
        GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
        GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
        GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
        GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
        // Face da direita
        GL.Color3(OpenTK.Color.Cyan);
        GL.Normal3(1, 0, 0);
        GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
        GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
        GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
        GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
        // Face da esquerda
        GL.Color3(OpenTK.Color.Magenta);
        GL.Normal3(-1, 0, 0);
        GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
        GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
        GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
        GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
        GL.End();

      // if (exibeVetorNormal) //TODO: acho que não precisa.
      //   ajudaExibirVetorNormal(); //TODO: acho que não precisa.
    }

    //TODO: melhorar para exibir não só a lsita de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Cubo: " + base.rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }

  }
}