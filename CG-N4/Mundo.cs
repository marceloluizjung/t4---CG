/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Gizmo
#define CG_Privado

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;
using System.Drawing;


//TODO: arrumar o id dos objetos usando char letra = 'A'; letra++;
//TODO: ter mais objetos geométricos: esfera
//TODO: arrumar objeto cone
//TODO: ter iluminação
//TODO: ter textura
//TODO: ter texto 2D
//TODO: ter um mapa em 2D
//TODO: ler arquivo OBJ/MTL
//TODO: ter audio
//TODO: usar DisplayList
//TODO: Seleciona Alpha
//TODO: Unproject
namespace gcgcg
{
    class Mundo : GameWindow
    {
        private static Mundo instanciaMundo = null;

        private Mundo(int width, int height) : base(width, height) { }

        public static Mundo GetInstance(int width, int height)
        {
            if (instanciaMundo == null)
                instanciaMundo = new Mundo(width, height);
            return instanciaMundo;
        }

        private CameraPerspective camera = new CameraPerspective();
        protected List<Objeto> objetosLista = new List<Objeto>();
        private ObjetoGeometria objetoSelecionado = null;
        private bool bBoxDesenhar = false;
        int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
        private Poligono objetoNovo = null;
        private String objetoId = "A";
        private Retangulo obj_Retangulo;
        private Cubo obj_Cubo;
        private Cilindro obj_Cilindro;
        private Esfera obj_Esfera;
        private Cone obj_Cone;
        private Bitmap barreira = new Bitmap("barreira.png");
        private Bitmap chao = new Bitmap("chao.png");
        private Bitmap cerca = new Bitmap("cerca.png");
        private Bitmap tankFront = new Bitmap("tank-front.png");
        private Bitmap tankLeft = new Bitmap("tank-left.png");
        private Bitmap tankRight = new Bitmap("tank-right.png");
        private Bitmap tankBack = new Bitmap("tank-back.png");
        private Bitmap tankInimigoFront = new Bitmap("tank-inimigo-front.png");
        private Bitmap tankInimigoLeft = new Bitmap("tank-inimigo-left.png");
        private Bitmap tankInimigoRight = new Bitmap("tank-inimigo-right.png");
        private Bitmap tankInimigoBack = new Bitmap("tank-inimigo-back.png");


        private Cubo inimgo;
        private Cubo personagem;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Console.WriteLine(" --- Ajuda / Teclas: ");
            Console.WriteLine(" [  H     ] mostra teclas usadas. ");

            //Chão
            obj_Cubo = new Cubo("Chao", null, chao);
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(1000, 1, 1000);
            obj_Cubo.TranslacaoXYZ(3, -1, 2);

            //Cerca
            obj_Cubo = new Cubo("Cerca1", null, cerca);
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(30, 100, 1000);
            obj_Cubo.TranslacaoXYZ(485, 55, 0);

            obj_Cubo = new Cubo("Cerca2", null, cerca);
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(1000, 100, 30);
            obj_Cubo.TranslacaoXYZ(0, 55, -485);

            obj_Cubo = new Cubo("Cerca3", null, cerca);
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(30, 100, 1000);
            obj_Cubo.TranslacaoXYZ(-485, 55, 0);

            obj_Cubo = new Cubo("Cerca4", null, cerca);
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(1000, 100, 30);
            obj_Cubo.TranslacaoXYZ(0, 55, 485);

            //Barreira
            obj_Cubo = new Cubo("Barreira1", null, barreira);
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(100, 100, 100);
            obj_Cubo.TranslacaoXYZ(420, 55, -298);

            obj_Cubo = new Cubo("Barreira2", null, barreira);
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(100, 100, 100);
            obj_Cubo.TranslacaoXYZ(-420, 55, 298);

            obj_Cubo = new Cubo("Barreira3", null, barreira);
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(650, 100, 100);
            obj_Cubo.TranslacaoXYZ(0, 55, 0);

            //Personagens
            //Inimigos
            obj_Cubo = new Cubo("Inimigo01", null, tankInimigoBack);
            this.inimgo = obj_Cubo;
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(100, 1, 100);
            obj_Cubo.TranslacaoXYZ(0, 55, -350);

            //Eu
            obj_Cubo = new Cubo("Personagem", null, tankFront);
            this.personagem = obj_Cubo;
            objetosLista.Add(obj_Cubo);
            obj_Cubo.EscalaXYZ(100, 1, 100);
            obj_Cubo.TranslacaoXYZ(0, 55, 350);

            objetoSelecionado = obj_Cubo;

            camera.Eye = new Vector3(0f, 1400, 850);
            camera.At = new Vector3(0f, 0f, 0f);
            camera.Near = 100f;
            camera.Far = 2000.0f;

            GL.ClearColor(0, 0, 0, 255);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            // GL.Disable(EnableCap.CullFace);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(camera.Fovy, Width / (float)Height, camera.Near, camera.Far);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 modelview = Matrix4.LookAt(camera.Eye, camera.At, camera.Up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
#if CG_Gizmo
            Sru3D();
#endif
            for (var i = 0; i < objetosLista.Count; i++)
                objetosLista[i].Desenhar();
            if (bBoxDesenhar && (objetoSelecionado != null))
                objetoSelecionado.BBox.Desenhar();
            this.SwapBuffers();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.H)
                Utilitario.AjudaTeclado();
            else if (e.Key == Key.Escape)
                Exit();
            else if (e.Key == Key.E)
            {
                Console.WriteLine("--- Objetos / Pontos: ");
                for (var i = 0; i < objetosLista.Count; i++)
                {
                    Console.WriteLine(objetosLista[i]);
                }
            }
            else if (e.Key == Key.O)
                bBoxDesenhar = !bBoxDesenhar;
            else if (e.Key == Key.Enter)
            {
                if (objetoNovo != null)
                {
                    objetoNovo.PontosRemoverUltimo();   // N3-Exe6: "truque" para deixar o rastro
                    objetoSelecionado = objetoNovo;
                    objetoNovo = null;
                }
            }
            else if (e.Key == Key.Space)
            {
                if (objetoNovo == null)
                {
                    objetoNovo = new Poligono(objetoId + 1, null);
                    objetosLista.Add(objetoNovo);
                    objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));
                    objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));  // N3-Exe6: "troque" para deixar o rastro
                }
                else
                    objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));
            }
            else if (objetoSelecionado != null)
            {
                if (e.Key == Key.M)
                    Console.WriteLine(objetoSelecionado.Matriz);
                else if (e.Key == Key.P)
                    Console.WriteLine(objetoSelecionado);
                else if (e.Key == Key.I)
                    objetoSelecionado.AtribuirIdentidade();
                //TODO: não está atualizando a BBox com as transformações geométricas
                else if (e.Key == Key.Left)
                {
                    objetoSelecionado = this.personagem;
                    Cubo cubo = this.personagem;
                    cubo.updateImage(tankLeft);
                    objetoSelecionado.TranslacaoXYZ(-10, 0, 0);
                }
                else if (e.Key == Key.Right)
                {
                    objetoSelecionado = this.personagem;
                    Cubo cubo = this.personagem;
                    cubo.updateImage(tankRight);
                    objetoSelecionado.TranslacaoXYZ(10, 0, 0);
                }
                else if (e.Key == Key.Up)
                {
                    objetoSelecionado = this.personagem;
                    Cubo cubo = this.personagem;
                    cubo.updateImage(tankFront);
                    objetoSelecionado.TranslacaoXYZ(0, 0, -10);
                }
                else if (e.Key == Key.Down)
                {
                    objetoSelecionado = this.personagem;
                    Cubo cubo = this.personagem;
                    cubo.updateImage(tankBack);
                    objetoSelecionado.TranslacaoXYZ(0, 0, 10);
                }
                else if (e.Key == Key.A)
                {
                    objetoSelecionado = this.inimgo;
                    Cubo cubo = this.inimgo;
                    cubo.updateImage(tankInimigoLeft);
                    objetoSelecionado.TranslacaoXYZ(-10, 0, 0);
                }
                else if (e.Key == Key.D)
                {
                    objetoSelecionado = this.inimgo;
                    Cubo cubo = this.inimgo;
                    cubo.updateImage(tankInimigoRight);
                    objetoSelecionado.TranslacaoXYZ(10, 0, 0);
                }
                else if (e.Key == Key.W)
                {
                    objetoSelecionado = this.inimgo;
                    Cubo cubo = this.inimgo;
                    cubo.updateImage(tankInimigoFront);
                    objetoSelecionado.TranslacaoXYZ(0, 0, -10);
                }
                else if (e.Key == Key.S)
                {
                    objetoSelecionado = this.inimgo;
                    Cubo cubo = this.inimgo;
                    cubo.updateImage(tankInimigoBack);
                    objetoSelecionado.TranslacaoXYZ(0, 0, 10);
                }
                else if (e.Key == Key.Number8)
                    objetoSelecionado.TranslacaoXYZ(0, 0, 10);
                else if (e.Key == Key.Number9)
                    objetoSelecionado.TranslacaoXYZ(0, 0, -10);
                else if (e.Key == Key.PageUp)
                    objetoSelecionado.EscalaXYZ(2, 2, 2);
                else if (e.Key == Key.PageDown)
                    objetoSelecionado.EscalaXYZ(0.5, 0.5, 0.5);
                else if (e.Key == Key.Home)
                    objetoSelecionado.EscalaXYZBBox(0.5, 0.5, 0.5);
                else if (e.Key == Key.End)
                    objetoSelecionado.EscalaXYZBBox(2, 2, 2);
                else if (e.Key == Key.Number1)
                    objetoSelecionado.Rotacao(10);
                else if (e.Key == Key.Number2)
                    objetoSelecionado.Rotacao(-10);
                else if (e.Key == Key.Number3)
                    objetoSelecionado.RotacaoZBBox(10);
                else if (e.Key == Key.Number4)
                    objetoSelecionado.RotacaoZBBox(-10);
                else if (e.Key == Key.Number0)
                    objetoSelecionado = null;
                else if (e.Key == Key.X)
                    objetoSelecionado.TrocaEixoRotacao('x');
                else if (e.Key == Key.Y)
                    objetoSelecionado.TrocaEixoRotacao('y');
                else if (e.Key == Key.Z)
                    objetoSelecionado.TrocaEixoRotacao('z');
                else
                    Console.WriteLine(" __ Tecla não implementada.");
            }
            else
                Console.WriteLine(" __ Tecla não implementada.");
        }

        //TODO: não está considerando o NDC
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            mouseX = e.Position.X; mouseY = 600 - e.Position.Y; // Inverti eixo Y
            if (objetoNovo != null)
            {
                objetoNovo.PontosUltimo().X = mouseX;
                objetoNovo.PontosUltimo().Y = mouseY;
            }
        }

#if CG_Gizmo
        private void Sru3D()
        {
            GL.LineWidth(1);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
            GL.End();
        }
#endif
    }
    class Program
    {
        static void Main(string[] args)
        {
            Mundo window = Mundo.GetInstance(600, 600);
            window.Title = "CG-N4";
            window.Run(1.0 / 60.0);
        }
    }
}
