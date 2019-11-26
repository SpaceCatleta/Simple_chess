using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess_Library;

namespace Шахматы
{


    public partial class Form1 : Form
    {
        public bool no_resize = true;
        public Size current_size;
        public ChessField chess_field;
        /// <summary>
        /// Хранит соотношения компонентов к форме
        /// (1)width (2)height (3)X (4)Y
        /// </summary>
        double[,] ratio;


        public Form1()
        {
            InitializeComponent();
            ratio = new double[4, Controls.Count];

            for (int i = 0; i < Controls.Count; i++)
            {
                ratio[0, i] = (double)Controls[i].Size.Width / Size.Width;
                ratio[1, i] = (double)Controls[i].Size.Height / Size.Height;
                ratio[2, i] = (double)Controls[i].Location.X / Size.Width;
                ratio[3, i] = (double)Controls[i].Location.Y / Size.Height;
            }

            chess_field = new ChessField();
            chess_field.indicatior = L_indicator;
            chess_field.CreateField(GB_MainField);

            no_resize = false;
            this.Size = new Size(1280, 720);
        }


        /// <summary>
        /// Обработка изменений размера формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (no_resize)
                return;
            
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Size = new Size((int)(Size.Width * ratio[0, i]), (int)(Size.Height * ratio[1, i]));
                Controls[i].Location = new Point((int)(Size.Width * ratio[2, i]), (int)(Size.Height * ratio[3, i]));
            }
            if (chess_field != null)
                chess_field.ResizeCells();

        }



        private void Button1_Click_1(object sender, EventArgs e)
        {
            
            Image image = Image.FromFile(@"chess_pieces\w_rook.png");
            ChessFigure test_figure = new ChessFigure(ChessColor.white, ChessType.rook, image);
            chess_field.chess_cells[0, 0].CurrentChess = test_figure;
        }

        private void B_Start_Click(object sender, EventArgs e)
        {
            chess_field.CreateFigures(@"chess_pieces");
            chess_field.PlaceFigures();
        }
    }
}
