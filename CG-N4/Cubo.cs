/**
  Autor: Dalton Solano dos Reis
**/

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;
using System.Drawing.Imaging;
namespace gcgcg
{
    internal class Cubo : ObjetoGeometria
    {
        private bool exibeVetorNormal = false;
        private int texture;
        public Cubo(string rotulo, Objeto paiRef, Bitmap bitmap) : base(rotulo, paiRef)
        {
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);
            base.PontosAdicionar(new Ponto4D(-0.5, -0.5, 0.5)); // PtoA listaPto[0]
            base.PontosAdicionar(new Ponto4D(0.5, -0.5, 0.5)); // PtoB listaPto[1]
            base.PontosAdicionar(new Ponto4D(0.5, 0.5, 0.5)); // PtoC listaPto[2]
            base.PontosAdicionar(new Ponto4D(-0.5, 0.5, 0.5)); // PtoD listaPto[3]
            base.PontosAdicionar(new Ponto4D(-0.5, -0.5, -0.5)); // PtoE listaPto[4]
            base.PontosAdicionar(new Ponto4D(0.5, -0.5, -0.5)); // PtoF listaPto[5]
            base.PontosAdicionar(new Ponto4D(0.5, 0.5, -0.5)); // PtoG listaPto[6]
            base.PontosAdicionar(new Ponto4D(-0.5, 0.5, -0.5)); // PtoH listaPto[7]
        }

        protected override void DesenharObjeto()
        {
            // Sentido anti-horário
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.Quads); ;
            GL.Normal3(0, 0, 1);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
                                                                                                // Face do fundo
            GL.Normal3(0, 0, -1);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
                                                                                                // Face de cima
            GL.Normal3(0, 1, 0);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
                                                                                                // Face de baixo
            GL.Normal3(0, -1, 0);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
                                                                                                // Face da direita
            GL.Normal3(1, 0, 0);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
                                                                                                // Face da esquerda
            GL.Normal3(-1, 0, 0);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.End();
            GL.Disable(EnableCap.Texture2D);

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