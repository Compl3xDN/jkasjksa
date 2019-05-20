using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SimulacionLocalizador
{
    public partial class Form1 : Form
    {
        private SerialPort Puerto;
        private string DatosEntrantes;
        Graphics g;
        Pen verde = new Pen(Color.LightGreen, 4);

        public Form1()
        {
            InitializeComponent();
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Point[] Puntos = new Point[4];
            Puntos[0] = new Point(550, 560);
            Puntos[1] = new Point(545, 502);
            Puntos[2] = new Point(540, 490);
            Puntos[3] = new Point(535, 560);
            g.DrawPolygon(verde, Puntos);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            g = CreateGraphics();
        }

        private void Trazar_Click(object sender, EventArgs e)
        {
            Puerto = new SerialPort();
            Puerto.BaudRate = 9600;
            string NombrePuerto = "COM13";
            Puerto.PortName = NombrePuerto;
            Puerto.Parity = Parity.None;
            Puerto.DataBits = 8;
            Puerto.StopBits = StopBits.One;
            Puerto.DataReceived += Puerto_DatosRecibidos;

            try
            {
                Puerto.Open();
                Datos.Text = "";
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erroorrrrrrrrrrrrrr");
            }
        }

        public void Puerto_DatosRecibidos(object sender, SerialDataReceivedEventArgs e)
        {
            DatosEntrantes = Puerto.ReadLine();
            this.Invoke(new EventHandler(MostrarInformacionEvento));
            this.Invoke(new EventHandler(TomarDatos));
        }

        public void MostrarInformacionEvento(object sender, EventArgs e)
        {
            Datos.AppendText(DatosEntrantes);
        }

        public void TomarDatos(object sender, EventArgs e)
        {
            String PatronDejarX1 = (@"Z[-0-9]+Z");
            String PatronDejarX2 = (@"Y[-0-9]+Y");
            String PatronDejarX3 = (@"X+");

            String PatronDejarY1 = (@"Z[-0-9]+Z");
            String PatronDejarY2 = (@"X[-0-9]+X");
            String PatronDejarY3 = (@"Y+");

            string DatosJuntos;
            string[] DatosEnBruto = Datos.Text.Split('\t');
            int hola;
            hola = 0;
            foreach (string equistexto in DatosEnBruto)
            {
                DatosJuntos = string.Concat(equistexto);
                string[] TomarValoresX = Regex.Split(DatosJuntos, PatronDejarX1);
                foreach (string texto2 in TomarValoresX)
                {
                    string datosX = string.Concat(texto2);
                    textBox1.AppendText(datosX);
                    string[] NumerosX = Regex.Split(datosX, PatronDejarX2);
                    foreach (string texto3 in NumerosX)
                    {
                        string numeritosX = string.Concat(texto3);
                        textBox2.AppendText(numeritosX);
                        string[] FinalX = Regex.Split(numeritosX, PatronDejarX3);
                        
                        foreach (string texto4 in FinalX)
                        {
                            string Xtexto = string.Concat(texto4);
                            textBox3.AppendText(Xtexto);
                            int hola2 = 600;
                            //*Esto es para convertir los numeros en string, a int    
                            string[] X = Xtexto.Split('\n');
                            int[] arr2 = Array.ConvertAll(X, s => int.Parse(s));

                            label1.Text = arr2.ToString();
                            g.DrawLine(verde, hola2, hola, hola, hola2);           
                        }
                    }
                } 
            }

            /*foreach (var yetexto in DatosEnBruto)
            {
                DatosJuntos = string.Concat(yetexto);
                string[] TomarValoresY = Regex.Split(DatosJuntos, PatronDejarY1);
                foreach (string yetexto2 in TomarValoresY)
                {
                    string datosY = string.Concat(yetexto2);
                    textBox1.AppendText(datosY);
                    string[] NumerosY = Regex.Split(datosY, PatronDejarY2);
                    foreach (string yetexto3 in NumerosY)
                    {
                        string numeritosY = string.Concat(yetexto3);
                        textBox2.AppendText(numeritosY);
                        string[] FinalX = Regex.Split(numeritosY, PatronDejarY3);
                        foreach (string yetexto4 in FinalX)
                        {
                            string Ytexto = string.Concat(yetexto4);
                            textBox3.AppendText(Ytexto);
                            label1.Text = Ytexto;
                        }
                    }
                }
            }*/
        }

        private void Parar_Click(object sender, EventArgs e)
        {
            Puerto.Close();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Point[] Puntos = new Point[4];
            Puntos[0] = new Point(150, 36);
            Puntos[1] = new Point(145, 42);
            Puntos[2] = new Point(140, 49);
            Puntos[3] = new Point(135, 56);
            g.DrawPolygon(verde, Puntos);
        }
    }
}
