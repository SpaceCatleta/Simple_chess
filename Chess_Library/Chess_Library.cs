using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Chess_Library
{
    /// <summary>
    /// Хранит виды щахматных фигур
    /// </summary>
    public enum ChessType
    {
        king, queen, rook, knight, bishop, pawn
    }



    /// <summary>
    /// Хранит цвет фигуры
    /// </summary>
    public enum ChessColor
    {
        white, black
    }
    


    /// <summary>
    /// Предоставляет описание шахматной фигуры
    /// </summary>
    public class ChessFigure
    {
        /// <summary>
        /// Тип фиуры
        /// </summary>
        protected ChessType type;
        /// <summary>
        /// Изобпажение фигуры
        /// </summary>
        protected Image icon;
        /// <summary>
        /// Цвет фиуры
        /// </summary>
        protected ChessColor color;
        /// <summary>
        /// Занятое на доске поле
        /// </summary>
        public ChessCell OccupedField;
        /// <summary>
        /// Делала ли фигура уже ходы
        /// </summary>
        protected bool first_turn = true;

        /// <summary>
        /// Делала ли фигура ходы
        /// </summary>
        public bool FirstTurn
        {
            get { return first_turn; }
            set { first_turn = value; }
        }
        /// <summary>
        /// Возвращает изображение фигуры
        /// </summary>
        public Image Icon
        {
            get { return icon; }
        }
        /// <summary>
        /// Возвращает тип фигуры
        /// </summary>
        public ChessType Type
        {
            get { return type; }
        }
        /// <summary>
        /// Возвращает цвет фигуры
        /// </summary>
        public ChessColor Color
        {
            get { return color; }
        }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Color"> Цвет фигуры </param>
        /// <param name="Type"> Тип фигуры </param>
        /// <param name="Icon"> Изображение фигуры </param>
        public ChessFigure(ChessColor Color, ChessType Type, Image Icon)
        {
            color = Color;
            type = Type;
            icon = Icon;
        }
    }



    public class ChessCell : PictureBox
    {
        /// <summary>
        /// Положение по горизонтали
        /// </summary>
        public int X;
        /// <summary>
        /// Положение по вертикали
        /// </summary>
        public int Y;
        /// <summary>
        /// Занята ли клетка фигурой
        /// </summary>
        protected bool is_occuped;
        /// <summary>
        /// Фигура в клетке
        /// </summary>
        protected ChessFigure current_chess;
        

        /// <summary>
        /// Шахматная фигура, занимающая клетку
        /// </summary>
        public ChessFigure CurrentChess
        {
            get { return current_chess; }
            set
            {
                is_occuped = value != null ? true : false;
                Image = is_occuped ? value.Icon : null;
                if (is_occuped)
                    value.OccupedField = this;
                current_chess = value;
            }
        }
        /// <summary>
        /// Занято ли поле фигурой
        /// </summary>
        public bool IsOccuped
        {
            get { return is_occuped; }
        }
    }



    /// <summary>
    /// Описывает класс "шахматная доска",
    /// который производит все опирации в игре
    /// </summary>
    public class ChessField
    {
        Color WHITE_CELL = Color.SandyBrown;
        Color BLACK_CELL = Color.SaddleBrown;
        Color SELECTED_OCCUPED_CELL = Color.Yellow;
        Color SELECTED_FREE_CELL = Color.Gray;
        Color CELL_AVIABLE_FOR_MOVING = Color.Aqua;
        Color WHITE_CELL_AVIABLE_FOR_MOVING = Color.AliceBlue;
        Color BLACK_CELL_AVIABLE_FOR_MOVING = Color.LightSteelBlue;
        Color WARNING_CELL = Color.Red;

        
        int border_indent = 5;        
        double border_ratio_W;
        double border_ratio_H;

        /// <summary>
        /// GroupBox, в котором строится шахматное поле
        /// </summary>
        GroupBox border;
        /// <summary>
        /// Выбранное поле на доске
        /// </summary>
        ChessCell selected_cell;
        /// <summary>
        /// Массив всех полей доски
        /// </summary>
        public ChessCell[,] chess_cells = new ChessCell[8, 8];
        /// <summary>
        /// Массив всех фигр на доске
        /// </summary>
        public ChessFigure[,] chess_pieces = new ChessFigure[2, 16];
        /// <summary>
        /// Цвет фигур, ктоторые совершают ход
        /// </summary>
        protected ChessColor current_turn = ChessColor.white;
        /// <summary>
        /// Список клеток, выделенных для перемещения
        /// </summary>
        protected List<ChessCell> movement_cells = new List<ChessCell>();
        public Label indicatior;

        /// <summary>
        /// Возвращает цвет фигурм, делающих ход
        /// </summary>
        public ChessColor CurrentTurn
        {
            get { return current_turn; }
        }


        //=====КОНСТРУКТОРЫ=============================================

        /// <summary>
        /// Создание графического шахматоного поля
        /// </summary>
        /// <param name="FieldBorder"> GroupBox, в котором разместиться поле </param>
        public void CreateField(GroupBox FieldBorder)
        {
            border = FieldBorder;
            border_ratio_W = (double)border.Size.Width / (double)border.Parent.Size.Width;
            border_ratio_H = (double)border.Size.Height / (double)border.Parent.Size.Height;

            double square_size = Math.Min(FieldBorder.Size.Width, FieldBorder.Size.Height - FieldBorder.Font.Size);
            int len = (int)((square_size - border_indent * 2) / 8);

            Size cell_size = new Size(len, len);

            for (int i = 0, i_count = border_indent + (int)FieldBorder.Font.Size; i < 8; i++, i_count += len)
                for (int j = 0, j_count = border_indent; j < 8; j++, j_count += len)
                {
                    ChessCell new_cell = new ChessCell();
                    new_cell.SizeMode = PictureBoxSizeMode.StretchImage;
                    new_cell.Size = cell_size;
                    new_cell.Location = new Point(j_count, i_count);
                    new_cell.BackColor = (i + j) % 2 == 0 ? WHITE_CELL : BLACK_CELL;
                    new_cell.X = j;
                    new_cell.Y = i;
                    new_cell.Click += CellClick;

                    chess_cells[i, j] = new_cell;
                    FieldBorder.Controls.Add(new_cell);
                }
        }


        /// <summary>
        /// Зашружает шахматные фигуры
        /// </summary>
        /// <param name="folder"> Путь до изображений фигур </param>
        public void CreateFigures(string folder)
        {
            Image w_rook  = Image.FromFile(folder + @"\w_rook.png");            
            Image w_king = Image.FromFile(folder + @"\w_king.png");           
            Image w_queen = Image.FromFile(folder + @"\w_queen.png");            
            Image w_bishop = Image.FromFile(folder + @"\w_bishop.png");
            Image w_knight = Image.FromFile(folder + @"\w_knight.png");
            Image w_pawn = Image.FromFile(folder + @"\w_pawn.png");

            Image b_rook = Image.FromFile(folder + @"\b_rook.png");
            Image b_king = Image.FromFile(folder + @"\b_king.png");
            Image b_queen = Image.FromFile(folder + @"\b_queen.png");
            Image b_bishop = Image.FromFile(folder + @"\b_bishop.png");
            Image b_knight = Image.FromFile(folder + @"\b_knight.png");
            Image b_pawn = Image.FromFile(folder + @"\b_pawn.png");

            chess_pieces[0, 0] = new ChessFigure(ChessColor.white, ChessType.rook, w_rook);
            chess_pieces[0, 1] = new ChessFigure(ChessColor.white, ChessType.knight, w_knight);
            chess_pieces[0, 2] = new ChessFigure(ChessColor.white, ChessType.bishop,w_bishop);
            chess_pieces[0, 3] = new ChessFigure(ChessColor.white, ChessType.queen, w_queen);
            chess_pieces[0, 4] = new ChessFigure(ChessColor.white, ChessType.king, w_king);
            chess_pieces[0, 5] = new ChessFigure(ChessColor.white, ChessType.bishop, w_bishop);
            chess_pieces[0, 6] = new ChessFigure(ChessColor.white, ChessType.knight, w_knight);
            chess_pieces[0, 7] = new ChessFigure(ChessColor.white, ChessType.rook, w_rook);
            for (int i = 0; i < 8; i++)
                chess_pieces[0,8+i] = new ChessFigure(ChessColor.white, ChessType.pawn, w_pawn);


            for (int i = 0; i < 8; i++)
                chess_pieces[1, i] = new ChessFigure(ChessColor.black, ChessType.pawn, b_pawn);
            chess_pieces[1, 8] = new ChessFigure(ChessColor.black, ChessType.rook, b_rook);
            chess_pieces[1, 9] = new ChessFigure(ChessColor.black, ChessType.knight, b_knight);
            chess_pieces[1, 10] = new ChessFigure(ChessColor.black, ChessType.bishop, b_bishop);
            chess_pieces[1, 11] = new ChessFigure(ChessColor.black, ChessType.queen, b_queen);
            chess_pieces[1, 12] = new ChessFigure(ChessColor.black, ChessType.king, b_king);
            chess_pieces[1, 13] = new ChessFigure(ChessColor.black, ChessType.bishop, b_bishop);
            chess_pieces[1, 14] = new ChessFigure(ChessColor.black, ChessType.knight, b_knight);
            chess_pieces[1, 15] = new ChessFigure(ChessColor.black, ChessType.rook, b_rook);         
        }


        /// <summary>
        /// Помещает
        /// </summary>
        public void PlaceFigures()
        {
            for (int i = 0, Y, X; i < 2; i++) 
                for (int j = 0; j < 16; j++)
                {
                    Y = (i == 0 ? 0 : 6) + (j > 7 ? 1 : 0);
                    X = j > 7 ? j - 8 : j;
                    chess_cells[Y, X].CurrentChess = chess_pieces[i, j];
                }
                    
        }


        /// <summary>
        /// перемещает фигуру на доступное для этого поле
        /// </summary>
        /// <param name="old_cell"> Старое поле </param>
        /// <param name="new_cell"> Нвое поле </param>
        public void ReplaceFigure(ChessCell old_cell, ChessCell new_cell)
        {
            new_cell.CurrentChess = old_cell.CurrentChess;
            new_cell.CurrentChess.FirstTurn = false;
            old_cell.CurrentChess = null;
        }


        //=====ПОМЕТКА ДОСТУПНЫХ ДЛЯ ПЕРЕДВИЖЕНИЯ ПОЛЕЙ=================

        public void GetCellsForMovement(ChessFigure figure)
        {
            if (figure.Type == ChessType.pawn)
                GetPawnMovement(figure);
            else if (figure.Type == ChessType.rook)
                GetRookMovement(figure);
            else if (figure.Type == ChessType.bishop)
                GetBishopMovement(figure);
            else if (figure.Type == ChessType.queen)
                GetQueenMovement(figure);
            else if (figure.Type == ChessType.knight)
                GetKnightMovement(figure);
            else if (figure.Type == ChessType.king)
                GetKingMovement(figure);
        }

        public void GetKingMovement(ChessFigure king)
        {
            ColorCell(king.OccupedField.X + 1, king.OccupedField.Y);
            ColorCell(king.OccupedField.X - 1, king.OccupedField.Y);
            ColorCell(king.OccupedField.X, king.OccupedField.Y + 1);
            ColorCell(king.OccupedField.X, king.OccupedField.Y - 1);
            ColorCell(king.OccupedField.X + 1, king.OccupedField.Y + 1);
            ColorCell(king.OccupedField.X - 1, king.OccupedField.Y - 1);
            ColorCell(king.OccupedField.X + 1, king.OccupedField.Y - 1);
            ColorCell(king.OccupedField.X - 1, king.OccupedField.Y + 1);
        }

        public void GetQueenMovement(ChessFigure queen)
        {
            for (int i = 1; i < 8; i++)
                if (ColorCell(queen.OccupedField.X + i, queen.OccupedField.Y + i))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(queen.OccupedField.X - i, queen.OccupedField.Y + i))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(queen.OccupedField.X + i, queen.OccupedField.Y - i))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(queen.OccupedField.X - i, queen.OccupedField.Y - i))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(queen.OccupedField.X + i, queen.OccupedField.Y))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(queen.OccupedField.X, queen.OccupedField.Y + i))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(queen.OccupedField.X - i, queen.OccupedField.Y))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(queen.OccupedField.X, queen.OccupedField.Y - i))
                    break;
        }

        public void GetPawnMovement(ChessFigure pawn)
        {
            int modif = pawn.Color == ChessColor.white ? 1 : -1;

            if (pawn.FirstTurn)
                ColorCellPawn(pawn.OccupedField.X, pawn.OccupedField.Y + 2 * modif, false);
            ColorCellPawn(pawn.OccupedField.X, pawn.OccupedField.Y + modif, false);
            ColorCellPawn(pawn.OccupedField.X + 1, pawn.OccupedField.Y + modif, true);
            ColorCellPawn(pawn.OccupedField.X - 1, pawn.OccupedField.Y + modif, true);
        }

        public void GetBishopMovement(ChessFigure bishop)
        {
            for (int i = 1; i < 8; i++)
                if (ColorCell(bishop.OccupedField.X + i, bishop.OccupedField.Y + i))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(bishop.OccupedField.X - i, bishop.OccupedField.Y + i)) 
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(bishop.OccupedField.X + i, bishop.OccupedField.Y - i))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(bishop.OccupedField.X - i, bishop.OccupedField.Y - i))
                    break;
        }

        public void GetKnightMovement(ChessFigure knight)
        {
            ColorCell(knight.OccupedField.X + 2, knight.OccupedField.Y + 1);
            ColorCell(knight.OccupedField.X + 2, knight.OccupedField.Y - 1);
            ColorCell(knight.OccupedField.X - 2, knight.OccupedField.Y + 1);
            ColorCell(knight.OccupedField.X - 2, knight.OccupedField.Y - 1);
            ColorCell(knight.OccupedField.X + 1, knight.OccupedField.Y + 2);
            ColorCell(knight.OccupedField.X - 1, knight.OccupedField.Y + 2);
            ColorCell(knight.OccupedField.X + 1, knight.OccupedField.Y - 2);
            ColorCell(knight.OccupedField.X - 1, knight.OccupedField.Y - 2);
        }

        public void GetRookMovement(ChessFigure rook)
        {
            for (int i = 1; i < 8; i++)
                if (ColorCell(rook.OccupedField.X + i, rook.OccupedField.Y))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(rook.OccupedField.X, rook.OccupedField.Y + i))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(rook.OccupedField.X - i, rook.OccupedField.Y))
                    break;
            for (int i = 1; i < 8; i++)
                if (ColorCell(rook.OccupedField.X, rook.OccupedField.Y - i))
                    break;

        }

        public bool ColorCell(int X, int Y)
        {
            try
            {
                ChessCell cell = chess_cells[Y, X];

                if (cell.IsOccuped)
                    if (cell.CurrentChess.Color != current_turn)
                    {
                        movement_cells.Add(cell);
                        cell.BackColor = (cell.X + cell.Y) % 2 == 0 ? WHITE_CELL_AVIABLE_FOR_MOVING : BLACK_CELL_AVIABLE_FOR_MOVING;
                        return true;
                    }
                    else
                        return true;
                else
                {
                    movement_cells.Add(cell);
                    cell.BackColor = (cell.X + cell.Y) % 2 == 0 ? WHITE_CELL_AVIABLE_FOR_MOVING : BLACK_CELL_AVIABLE_FOR_MOVING;
                    return false;
                }
            }
            catch { }

            return true;
        }
            
        /// <summary>
        /// Помечает указанное поле, как доступное для перемещения
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="occupy_wanted"></param>
        public bool ColorCellPawn(int X, int Y, bool occupy_wanted)
        {
            try
            {
                ChessCell cell = chess_cells[Y, X];
                bool answer = cell.IsOccuped;

                if (cell.IsOccuped == occupy_wanted)
                {
                    movement_cells.Add(cell);
                    cell.BackColor = (cell.X + cell.Y) % 2 == 0 ? WHITE_CELL_AVIABLE_FOR_MOVING : BLACK_CELL_AVIABLE_FOR_MOVING;
                }
                return answer;
            }
            catch { }

            return true;
        }

        public void ClearCellsForMovement()
        {
            foreach(ChessCell cell in movement_cells)
            {
                cell.BackColor = (cell.X + cell.Y) % 2 == 0 ? WHITE_CELL : BLACK_CELL;
            }
            movement_cells.Clear();
        }


        //=====ИЗМЕНЕНИЕ РАЗМЕРА ПОЛЕЙ===================================

        /// <summary>
        /// Изменяет размер игрового поля
        /// </summary>
        public void ResizeMainField()
        {
            border.Size = new Size((int)(border.Parent.Size.Width * border_ratio_W), (int)(border.Parent.Size.Height * border_ratio_H));
            ResizeCells();
        }


        /// <summary>
        /// Изменяет размер клеток шахматной доски
        /// </summary>
        public void ResizeCells()
        {
            double square_size = Math.Min(border.Size.Width, border.Size.Height - border.Font.Size);
            int len = (int)((square_size - border_indent * 2) / 8);
            Size new_size = new Size(len, len);

            for (int i = 0, i_count = border_indent + (int)border.Font.Size; i < 8; i++, i_count += len)
                for (int j = 0, j_count = border_indent; j < 8; j++, j_count += len)
                {
                    chess_cells[i, j].Size = new_size;
                    chess_cells[i, j].Location = new Point(j_count, i_count);
                }
        }


        //=====НАЖАТИЕ НА ПОЛЕ===========================================

        public void NextTurn()
        {
            current_turn = current_turn == ChessColor.white ? ChessColor.black : ChessColor.white;
        }

        /// <summary>
        /// Обрабатывает выделение поля пользователем
        /// </summary>
        /// <param name="selected"></param>
        public void Cell_Select(ChessCell selected)
        {
            if (selected.BackColor == WHITE_CELL_AVIABLE_FOR_MOVING || selected.BackColor == BLACK_CELL_AVIABLE_FOR_MOVING)
            {
                ReplaceFigure(selected_cell, selected);
                selected_cell.BackColor = (selected_cell.X + selected_cell.Y) % 2 == 0 ? WHITE_CELL : BLACK_CELL;
                ClearCellsForMovement();
                selected_cell = null;
                NextTurn();
                if (indicatior != null)
                    indicatior.Text = current_turn == ChessColor.white ? "ходят белые" : "ходят чёрные";
                return;
            }
            if (selected_cell != null)
                selected_cell.BackColor = (selected_cell.X + selected_cell.Y) % 2 == 0 ? WHITE_CELL : BLACK_CELL;

            selected_cell = selected;
            ClearCellsForMovement();

            if (selected.IsOccuped && CurrentTurn == selected.CurrentChess.Color)
            {
                selected_cell.BackColor = SELECTED_OCCUPED_CELL;
                GetCellsForMovement(selected.CurrentChess);
                return;
            }
            else
                selected_cell.BackColor = SELECTED_FREE_CELL;
        }


        /// <summary>
        /// Обработка нажатия на кдетку доски
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CellClick(object sender, EventArgs e)
        {
            ChessCell self = (ChessCell)sender;
            Cell_Select(self);
        }
    }
}
