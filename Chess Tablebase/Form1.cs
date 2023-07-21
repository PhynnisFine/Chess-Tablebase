using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static Chess_Tablebase.frmBoard;

namespace Chess_Tablebase
{
    public partial class frmBoard : Form
    {

        public static class myGlobals
        {
            public static Button[,] buttonArray = new Button[8, 8];
            public static int[,] board = new int[8, 8];
            public static int selectedPiece = 0;
            public static int ColToMove = 8;
            public static List<board> givenPositions = new List<board>();
            public static Position[] Tablebase = new Position[262144];
        }

        public class Position
        {
            public int[,] board = new int[8,8];
            public int WhiteEval=0;
            public int BlackEval=0;
        }

        public class board
        {
            public int[,] pos = new int[8, 8];
            public int tableIndex = new int();
        }

        public frmBoard()
        {
            InitializeComponent();
        }

        public int ArrayLength(int[] array)
        {
            int Length = 0;

            return (Length);
        }

        //creates 2d array of button, reads tablebase from text files
        private void frmBoard_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader("KQvK.txt");

                for (int i = 0; i < myGlobals.Tablebase.Length; i++)
                {
                    myGlobals.Tablebase[i] = new Position();
                }

                string line = sr.ReadLine();
                string[] parts;
                string FEN;
                int[,] board = new int[8, 8];
                int tableIndex;

                while (line != null)
                {
                    parts = line.Split(",");

                    FEN = parts[0];

                    board = FENToBoard(FEN);

                    tableIndex = generateIndex(board);
                    if (parts != null)
                    {
                        if (parts.Length == 3)
                        {
                            myGlobals.Tablebase[tableIndex].board = copyPos(board);
                            myGlobals.Tablebase[tableIndex].WhiteEval = Convert.ToInt16(parts[1]);
                            myGlobals.Tablebase[tableIndex].BlackEval = Convert.ToInt16(parts[2]);
                        }
                    }

                    line = sr.ReadLine();
                }
                MessageBox.Show("Finished reading tablebase from text file");
            }
            catch (Exception p)
            {
                MessageBox.Show("Exception: " + p.Message);
            }

            myGlobals.buttonArray[0, 0] = btn0;
            myGlobals.buttonArray[0, 1] = btn1;
            myGlobals.buttonArray[0, 2] = btn2;
            myGlobals.buttonArray[0, 3] = btn3;
            myGlobals.buttonArray[0, 4] = btn4;
            myGlobals.buttonArray[0, 5] = btn5;
            myGlobals.buttonArray[0, 6] = btn6;
            myGlobals.buttonArray[0, 7] = btn7;
            myGlobals.buttonArray[1, 0] = btn8;
            myGlobals.buttonArray[1, 1] = btn9;
            myGlobals.buttonArray[1, 2] = btn10;
            myGlobals.buttonArray[1, 3] = btn11;
            myGlobals.buttonArray[1, 4] = btn12;
            myGlobals.buttonArray[1, 5] = btn13;
            myGlobals.buttonArray[1, 6] = btn14;
            myGlobals.buttonArray[1, 7] = btn15;
            myGlobals.buttonArray[2, 0] = btn16;
            myGlobals.buttonArray[2, 1] = btn17;
            myGlobals.buttonArray[2, 2] = btn18;
            myGlobals.buttonArray[2, 3] = btn19;
            myGlobals.buttonArray[2, 4] = btn20;
            myGlobals.buttonArray[2, 5] = btn21;
            myGlobals.buttonArray[2, 6] = btn22;
            myGlobals.buttonArray[2, 7] = btn23;
            myGlobals.buttonArray[3, 0] = btn24;
            myGlobals.buttonArray[3, 1] = btn25;
            myGlobals.buttonArray[3, 2] = btn26;
            myGlobals.buttonArray[3, 3] = btn27;
            myGlobals.buttonArray[3, 4] = btn28;
            myGlobals.buttonArray[3, 5] = btn29;
            myGlobals.buttonArray[3, 6] = btn30;
            myGlobals.buttonArray[3, 7] = btn31;
            myGlobals.buttonArray[4, 0] = btn32;
            myGlobals.buttonArray[4, 1] = btn33;
            myGlobals.buttonArray[4, 2] = btn34;
            myGlobals.buttonArray[4, 3] = btn35;
            myGlobals.buttonArray[4, 4] = btn36;
            myGlobals.buttonArray[4, 5] = btn37;
            myGlobals.buttonArray[4, 6] = btn38;
            myGlobals.buttonArray[4, 7] = btn39;
            myGlobals.buttonArray[5, 0] = btn40;
            myGlobals.buttonArray[5, 1] = btn41;
            myGlobals.buttonArray[5, 2] = btn42;
            myGlobals.buttonArray[5, 3] = btn43;
            myGlobals.buttonArray[5, 4] = btn44;
            myGlobals.buttonArray[5, 5] = btn45;
            myGlobals.buttonArray[5, 6] = btn46;
            myGlobals.buttonArray[5, 7] = btn47;
            myGlobals.buttonArray[6, 0] = btn48;
            myGlobals.buttonArray[6, 1] = btn49;
            myGlobals.buttonArray[6, 2] = btn50;
            myGlobals.buttonArray[6, 3] = btn51;
            myGlobals.buttonArray[6, 4] = btn52;
            myGlobals.buttonArray[6, 5] = btn53;
            myGlobals.buttonArray[6, 6] = btn54;
            myGlobals.buttonArray[6, 7] = btn55;
            myGlobals.buttonArray[7, 0] = btn56;
            myGlobals.buttonArray[7, 1] = btn57;
            myGlobals.buttonArray[7, 2] = btn58;
            myGlobals.buttonArray[7, 3] = btn59;
            myGlobals.buttonArray[7, 4] = btn60;
            myGlobals.buttonArray[7, 5] = btn61;
            myGlobals.buttonArray[7, 6] = btn62;
            myGlobals.buttonArray[7, 7] = btn63;

            btnWhiteToMove.BackColor = Color.LightGray;
            btnBlackToMove.BackColor = Color.LightGray;
        }

        //Displays a 2d array 'board' on the UI
        public int updateDisplay(int[,] board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    switch (board[i, j] % 8)
                    {
                        case 0:
                            myGlobals.buttonArray[i, j].Text = "";
                            break;
                        case 1:
                            myGlobals.buttonArray[i, j].Text = "P";
                            break;
                        case 2:
                            myGlobals.buttonArray[i, j].Text = "N";
                            break;
                        case 3:
                            myGlobals.buttonArray[i, j].Text = "B";
                            break;
                        case 4:
                            myGlobals.buttonArray[i, j].Text = "R";
                            break;
                        case 5:
                            myGlobals.buttonArray[i, j].Text = "Q";
                            break;
                        case 6:
                            myGlobals.buttonArray[i, j].Text = "K";
                            break;
                    }
                    if (board[i, j] > 8)
                    {
                        myGlobals.buttonArray[i, j].ForeColor = Color.White;
                    }
                    else
                    {
                        myGlobals.buttonArray[i, j].ForeColor = Color.Black;
                    }

                }
            }

            //MessageBox.Show("Updated Display");
            return 0;
        }

        public string pieceNumToString(int num)
        {
            string[] pieces = new string[] { "X", "p", "n", "b", "r", "q", "k", "X", "X", "P", "N", "B", "R", "Q", "K" };

            return pieces[num];
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblEval_Click(object sender, EventArgs e)
        {

        }

        //Converts FEN (string) to board (2d array)
        public int[,] FENToBoard(string FEN)
        {
            int[,] board = new int[8, 8];
            int k = 0;
            int x = -1;
            int y = 0;

            for (int i = 0; i < 8; i++)    //initialise board
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = 0;
                }
            }

            while (FEN.Substring(k, 1) != " " && k < FEN.Length)
            {
                switch (FEN.Substring(k, 1))
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                        x += Convert.ToInt16(FEN.Substring(k, 1));   //move right by the number of squares (one less than required)
                        break;
                    case "/":
                        y++;          //move down a row, and reset x
                        x = -1;       //x is -1 so that the same shift of 1 can be applied for the first peice in a row and all the others
                        break;
                    case "p":
                        x++;
                        board[y, x] = 1;
                        break;
                    case "n":
                        x++;
                        board[y, x] = 2;
                        break;
                    case "b":
                        x++;
                        board[y, x] = 3;
                        break;
                    case "r":
                        x++;
                        board[y, x] = 4;
                        break;
                    case "q":
                        x++;
                        board[y, x] = 5;
                        break;
                    case "k":
                        x++;
                        board[y, x] = 6;
                        break;
                    case "P":
                        x++;
                        board[y, x] = 9;
                        break;
                    case "N":
                        x++;
                        board[y, x] = 10;
                        break;
                    case "B":
                        x++;
                        board[y, x] = 11;
                        break;
                    case "R":
                        x++;
                        board[y, x] = 12;
                        break;
                    case "Q":
                        x++;
                        board[y, x] = 13;
                        break;
                    case "K":
                        x++;
                        board[y, x] = 14;
                        break;
                }
                k++;
            }
            k++;
            if (FEN.Substring(k, 1) == "w")
            {
                myGlobals.ColToMove = 1;
                btnWhiteToMove.BackColor = Color.Gray;
                btnBlackToMove.BackColor = Color.LightGray;
            }
            else
            {
                btnWhiteToMove.BackColor = Color.LightGray;
                btnBlackToMove.BackColor = Color.Gray;
            }

            return board;
        }

        public string BoardToFEN(int[,] board)
        {
            string FEN = "";
            int gap = 0;

            for (int y = 0; y < 8; y++)
            {
                gap = 0;
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x] == 0)
                    {
                        gap++;

                    }
                    else
                    {
                        if (gap != 0)
                        {
                            FEN += Convert.ToString(gap);
                            gap = 0;
                        }
                        switch (board[y, x])
                        {
                            case 1:
                                FEN += "p";
                                break;
                            case 2:
                                FEN += "n";
                                break;
                            case 3:
                                FEN += "b";
                                break;
                            case 4:
                                FEN += "r";
                                break;
                            case 5:
                                FEN += "q";
                                break;
                            case 6:
                                FEN += "k";
                                break;
                            case 9:
                                FEN += "P";
                                break;
                            case 10:
                                FEN += "N";
                                break;
                            case 11:
                                FEN += "B";
                                break;
                            case 12:
                                FEN += "R";
                                break;
                            case 13:
                                FEN += "Q";
                                break;
                            case 14:
                                FEN += "K";
                                break;
                        }
                    }
                }
                FEN += Convert.ToString(gap);
                FEN += "/";
            }
            FEN += " ";

            if (myGlobals.ColToMove == 8)
            {
                FEN += "w";
            }
            else
            {
                FEN += "b";
            }

            return FEN;
        }

        //user inputs FEN, position is displayed
        private void btnFENInput_Click(object sender, EventArgs e)
        {
            string FEN;

            FEN = txtFENInput.Text;

            myGlobals.board = FENToBoard(FEN);

            updateDisplay(myGlobals.board);
        }

        private void btnWhiteToMove_Click(object sender, EventArgs e)
        {
            btnWhiteToMove.BackColor = Color.Gray;
            btnBlackToMove.BackColor = Color.LightGray;
            myGlobals.ColToMove = 8;
        }

        private void btnBlackToMove_Click(object sender, EventArgs e)
        {
            btnWhiteToMove.BackColor = Color.LightGray;
            btnBlackToMove.BackColor = Color.Gray;
            myGlobals.ColToMove = 0;
        }

        //Resets button colors to show which one has been selected
        public int pieceSelected()
            {
            btnWPawn.BackColor = Color.SaddleBrown;
            btnWKnight.BackColor = Color.SaddleBrown;
            btnWBishop.BackColor = Color.SaddleBrown;
            btnWRook.BackColor = Color.SaddleBrown;
            btnWQueen.BackColor = Color.SaddleBrown;
            btnWKing.BackColor = Color.SaddleBrown;
            btnBPawn.BackColor = Color.BurlyWood;
            btnBKnight.BackColor = Color.BurlyWood;
            btnBBishop.BackColor = Color.BurlyWood;
            btnBRook.BackColor = Color.BurlyWood;
            btnBQueen.BackColor = Color.BurlyWood;
            btnBKing.BackColor = Color.BurlyWood;
            button1.BackColor = Color.BurlyWood;
            return (0);
            }
        private void btnWPawn_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 9;
            pieceSelected();
            btnWPawn.BackColor = Color.Gray;
        }

        private void btnWKnight_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 10;
            pieceSelected();
            btnWKnight.BackColor = Color.Gray;
        }

        private void btnWBishop_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 11;
            pieceSelected();
            btnWBishop.BackColor = Color.Gray;
        }

        private void btnWRook_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 12;
            pieceSelected();
            btnWRook.BackColor = Color.Gray;
        }

        private void btnWQueen_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 13;
            pieceSelected();
            btnWQueen.BackColor = Color.Gray;
        }

        private void btnWKing_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 14;
            pieceSelected();
            btnWKing.BackColor = Color.Gray;
        }

        private void btnBPawn_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 1;
            pieceSelected();
            btnBPawn.BackColor = Color.Gray;
        }

        private void btnBKnight_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 2;
            pieceSelected();
            btnBKnight.BackColor = Color.Gray;
        }

        private void btnBBishop_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 3;
            pieceSelected();
            btnBBishop.BackColor = Color.Gray;
        }

        private void btnBRook_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 4;
            pieceSelected();
            btnBRook.BackColor = Color.Gray;
        }

        private void btnBQueen_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 5;
            pieceSelected();
            btnBQueen.BackColor = Color.Gray;
        }

        private void btnBKing_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 6;
            pieceSelected();
            btnBKing.BackColor = Color.Gray;
        }
        //should be btnDelete
        private void button1_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 0;
            pieceSelected();
            button1.BackColor = Color.Gray;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            myGlobals.board[0, 0] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            myGlobals.board[0, 1] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            myGlobals.board[0, 2] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            myGlobals.board[0, 3] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            myGlobals.board[0, 4] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            myGlobals.board[0, 5] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            myGlobals.board[0, 6] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            myGlobals.board[0, 7] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            myGlobals.board[1, 0] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            myGlobals.board[1, 1] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            myGlobals.board[1, 2] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            myGlobals.board[1, 3] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            myGlobals.board[1, 4] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            myGlobals.board[1, 5] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            myGlobals.board[1, 6] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn15_Click(object sender, EventArgs e)
        {
            myGlobals.board[1, 7] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn16_Click(object sender, EventArgs e)
        {
            myGlobals.board[2, 0] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn17_Click(object sender, EventArgs e)
        {
            myGlobals.board[2, 1] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn18_Click(object sender, EventArgs e)
        {
            myGlobals.board[2, 2] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn19_Click(object sender, EventArgs e)
        {
            myGlobals.board[2, 3] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            myGlobals.board[2, 4] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn21_Click(object sender, EventArgs e)
        {
            myGlobals.board[2, 5] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn22_Click(object sender, EventArgs e)
        {
            myGlobals.board[2, 6] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn23_Click(object sender, EventArgs e)
        {
            myGlobals.board[2, 7] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn24_Click(object sender, EventArgs e)
        {
            myGlobals.board[3, 0] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn25_Click(object sender, EventArgs e)
        {
            myGlobals.board[3, 1] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn26_Click(object sender, EventArgs e)
        {
            myGlobals.board[3, 2] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn27_Click(object sender, EventArgs e)
        {
            myGlobals.board[3, 3] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn28_Click(object sender, EventArgs e)
        {
            myGlobals.board[3, 4] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn29_Click(object sender, EventArgs e)
        {
            myGlobals.board[3, 5] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn30_Click(object sender, EventArgs e)
        {
            myGlobals.board[3, 6] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn31_Click(object sender, EventArgs e)
        {
            myGlobals.board[3, 7] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn32_Click(object sender, EventArgs e)
        {
            myGlobals.board[4, 0] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn33_Click(object sender, EventArgs e)
        {
            myGlobals.board[4, 1] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn34_Click(object sender, EventArgs e)
        {
            myGlobals.board[4, 2] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn35_Click(object sender, EventArgs e)
        {
            myGlobals.board[4, 3] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn36_Click(object sender, EventArgs e)
        {
            myGlobals.board[4, 4] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn37_Click(object sender, EventArgs e)
        {
            myGlobals.board[4, 5] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn38_Click(object sender, EventArgs e)
        {
            myGlobals.board[4, 6] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn39_Click(object sender, EventArgs e)
        {
            myGlobals.board[4, 7] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn40_Click(object sender, EventArgs e)
        {
            myGlobals.board[5, 0] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn41_Click(object sender, EventArgs e)
        {
            myGlobals.board[5, 1] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn42_Click(object sender, EventArgs e)
        {
            myGlobals.board[5, 2] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn43_Click(object sender, EventArgs e)
        {
            myGlobals.board[5, 3] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn44_Click(object sender, EventArgs e)
        {
            myGlobals.board[5, 4] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn45_Click(object sender, EventArgs e)
        {
            myGlobals.board[5, 5] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn46_Click(object sender, EventArgs e)
        {
            myGlobals.board[5, 6] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn47_Click(object sender, EventArgs e)
        {
            myGlobals.board[5, 7] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn48_Click(object sender, EventArgs e)
        {
            myGlobals.board[6, 0] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn49_Click(object sender, EventArgs e)
        {
            myGlobals.board[6, 1] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn50_Click(object sender, EventArgs e)
        {
            myGlobals.board[6, 2] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn51_Click(object sender, EventArgs e)
        {
            myGlobals.board[6, 3] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn52_Click(object sender, EventArgs e)
        {
            myGlobals.board[6, 4] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn53_Click(object sender, EventArgs e)
        {
            myGlobals.board[6, 5] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn54_Click(object sender, EventArgs e)
        {
            myGlobals.board[6, 6] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn55_Click(object sender, EventArgs e)
        {
            myGlobals.board[6, 7] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn56_Click(object sender, EventArgs e)
        {
            myGlobals.board[7, 0] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn57_Click(object sender, EventArgs e)
        {
            myGlobals.board[7, 1] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn58_Click(object sender, EventArgs e)
        {
            myGlobals.board[7, 2] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn59_Click(object sender, EventArgs e)
        {
            myGlobals.board[7, 3] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn60_Click(object sender, EventArgs e)
        {
            myGlobals.board[7, 4] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn61_Click(object sender, EventArgs e)
        {
            myGlobals.board[7, 5] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn62_Click(object sender, EventArgs e)
        {
            myGlobals.board[7, 6] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btn63_Click(object sender, EventArgs e)
        {
            myGlobals.board[7, 7] = myGlobals.selectedPiece;
            updateDisplay(myGlobals.board);
        }

        private void btnClearBoard_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    myGlobals.board[i, j] = 0;
                }
            }
            updateDisplay(myGlobals.board);
        }

        //takes a string like KQvK and converts it to a list of pieces
        public List<int> TableNameToPieces(string TableName)
        {
            List<int> Pieces = new List<int>();
            int colour = 8;

            Pieces.Add(14);  //add white king
            Pieces.Add(6);   //add black king

            for (int i = 0; i < TableName.Length; i++)
            {
                switch (TableName.Substring(i, 1))
                {
                    case "P":
                        Pieces.Add(1 + colour);
                        break;
                    case "N":
                        Pieces.Add(2 + colour);
                        break;
                    case "B":
                        Pieces.Add(3 + colour);
                        break;
                    case "R":
                        Pieces.Add(4 + colour);
                        break;
                    case "Q":
                        Pieces.Add(5 + colour);
                        break;
                    case "v":
                        colour = 0;
                        break;
                }
            }

            //MessageBox.Show("Pieces: "+ Pieces[0] + " " + Pieces[1] + " " + Pieces[2]);
            return (Pieces);
        }

        //Deep copying in c# is the absolute worst thing to exist
        //I swear i spent about 2 hours trying to work out wtf was going wrong and trying to fix it, this seems like the best solution (speed wise anyway)
        public Position copyBoard(int[,] board)
        {
            Position copyPos = new Position();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    copyPos.board[y, x] = board[y, x];
                }
            }

            return (copyPos);
        }

        public int[,] copyPos(int[,] board)
        {
            int[,] copiedPos = new int[8, 8];
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    copiedPos[y, x] = board[y, x];
                }
            }

            return copiedPos;
        }

        //MUST BE CHANGED FOR TABLES ABOVE 3 PIECES
        public int GenerateAllPositions(List<int> Pieces, ref Position[] AllPositions)
        {
            int[,] board = new int[8, 8];
            int wKing = Convert.ToInt16(Pieces[0]);
            int bKing = Convert.ToInt16(Pieces[1]);
            int piece = Convert.ToInt16(Pieces[2]);
            int positionsIndex = 0;

            //6 nested for loops HELLL YEEAAAH (eww)

            for (int wKingY = 0; wKingY < 8; wKingY++)
            {
                for (int wKingX = 0; wKingX < 8; wKingX++)     //First piece (white king)
                {
                    board[wKingY, wKingX] = wKing;

                    for (int bKingY = 0; bKingY < 8; bKingY++)
                    {
                        for (int bKingX = 0; bKingX < 8; bKingX++)        //2nd piece (black king), cannot touch / be on same square as white king
                        {
                            if (Math.Abs(bKingY - wKingY) > 1 || Math.Abs(bKingX - wKingX) > 1)
                            {
                                board[bKingY, bKingX] = bKing;

                                for (int pieceY = 0; pieceY < 8; pieceY++)
                                {
                                    for (int pieceX = 0; pieceX < 8; pieceX++)      //3rd piece (any piece), cannot be on same square as other 2 pieces
                                    {
                                        if ((pieceY != wKingY || pieceX != wKingX) && (pieceY != bKingY || pieceX != bKingX))
                                        {
                                            board[pieceY, pieceX] = piece;

                                            AllPositions[positionsIndex].board = copyPos(board);

                                            //updateDisplay(board);
                                            //MessageBox.Show(Convert.ToString(positionsIndex));

                                            board[pieceY, pieceX] = 0;
                                        }
                                        positionsIndex++;
                                    }
                                }
                                board[bKingY, bKingX] = 0;
                            }
                            else
                            {
                                positionsIndex += 64;
                            }
                        }
                    }
                    board[wKingY, wKingX] = 0;
                }
            }

            //updateDisplay(AllPositions[129].board);

            //MessageBox.Show(Convert.ToString(positionsIndex), " positions have been checked");
            return (0);
        }

        public int[,] maskdirection(int y, int x, int ydirection, int xdirection, int[,] board, int[,] mask)
        {
            int d = 1;
            int enemyKing = 6;
            if (board[y,x] < 8)
            {
                enemyKing = 14;
            }

            while (x + xdirection*d > -1 && x + xdirection * d < 8 && y + ydirection*d > -1 && y + ydirection * d <8) //while still on board
            {
                mask[y + ydirection * d, x + xdirection * d] = 1;
                if (board[y + ydirection * d, x + xdirection * d] == 0 || board[y + ydirection * d, x + xdirection * d] == enemyKing) //x-ray enemay king, it cant just step back 1
                {
                    d++;
                }
                else 
                {
                    d += 999; //how to use a goto without actually writing goto
                }
            }

            return (mask);
        }

        public int[,] trymask(int y, int x, int ymove, int xmove, int[,] board, int[,] mask, int col)
        {
            if (y+ymove <8 && y +ymove >-1 && x+xmove <8 && x+xmove >-1 )
            {
                mask[y + ymove, x + xmove] = 1;
            }

            return (mask);
        }

        //used for finding squares that the opposite king can move to
        public int[,] maskCol(int[,] board, int col)
        {
            int[,] mask = new int[8, 8];

            for (int y = 0; y<8; y++)
            {
                for (int x =0; x <8; x++)
                {
                    switch (board[y,x] - col)
                    {
                        case 1:
                            //pawn
                            mask = trymask(y, x, -1, 1, board, mask, col);
                            mask = trymask(y, x, -1, -1, board, mask, col);
                            break;
                        case 2:
                            //knight (all 8 cases considered seperately to make debugging easier, should have used a function)

                            mask = trymask(y, x, 2, 1, board, mask, col);
                            mask = trymask(y, x, 1,2, board, mask, col);
                            mask = trymask(y, x, -2, 1, board, mask, col);
                            mask = trymask(y, x, -1, 2, board, mask, col);
                            mask = trymask(y, x, 2, -1, board, mask, col);
                            mask = trymask(y, x, 1, -2, board, mask, col);
                            mask = trymask(y, x, -2, -1, board, mask, col);
                            mask = trymask(y, x, -1, -2, board, mask, col);
                            break;
                        case 3:
                            //Bishop

                            //this used to be 50 lines long, i love functions
                            mask = maskdirection(y, x, -1, -1, board, mask);
                            mask = maskdirection(y, x, -1, 1, board, mask);
                            mask = maskdirection(y, x, 1, -1, board, mask);
                            mask = maskdirection(y, x, 1, 1, board, mask);

                            break;
                        case 4:
                            //rook
                            mask = maskdirection(y, x, -1, 0, board, mask);
                            mask = maskdirection(y, x, 0, -1, board, mask);
                            mask = maskdirection(y, x, 1, 0, board, mask);
                            mask = maskdirection(y, x, 0, 1, board, mask);
                            break;

                        case 5:
                            //quin (ne quin ne king ne master)

                            mask = maskdirection(y, x, -1, -1, board, mask);
                            mask = maskdirection(y, x, -1, 1, board, mask);
                            mask = maskdirection(y, x, 1, -1, board, mask);
                            mask = maskdirection(y, x, 1, 1, board, mask);
                            mask = maskdirection(y, x, -1, 0, board, mask);
                            mask = maskdirection(y, x, 0, -1, board, mask);
                            mask = maskdirection(y, x, 1, 0, board, mask);
                            mask = maskdirection(y, x, 0, 1, board, mask);
                            break;
                        case 6:
                            //king
                            mask = trymask(y, x, 1, 1, board, mask, col);
                            mask = trymask(y, x, 1, 0, board, mask, col);
                            mask = trymask(y, x, 1, -1, board, mask, col);
                            mask = trymask(y, x, 0, 1, board, mask, col);
                            mask = trymask(y, x, 0, -1, board, mask, col);
                            mask = trymask(y, x, -1, 1, board, mask, col);
                            mask = trymask(y, x, -1, 0, board, mask, col);
                            mask = trymask(y, x, -1, -1, board, mask, col);
                            break;
                    }
                        
                }
            }

            return (mask);
        }
        public int[,] maskPieces(int[,] board, int col)
        {
            int[,] mask = new int[8,8];
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y,x] - col > 0 && board[y, x] - col < 8)
                    {
                        mask[y, x] = 1;
                    }
                }
            }

            return mask;
        }

        //used for checking if the opposite colour king can move to any of those squares (mask and checkmate functions are seperate because mask can be used for other thing)
        public bool checkmate(int[,] board, int[,] mask, int col)
        {
            bool checkmate = true;
            int kingy = 0;
            int kingx = 0;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y,x] == 14 - col)
                    {
                        kingy = y;
                        kingx = x;
                    }
                }
            }

            if (kingy > 0)
            {
                if (mask[kingy - 1, kingx] == 0) //and if that square isnt occupied by a black piece already
                {
                    checkmate = false;
                }
                if (kingx > 0)
                {
                    if (mask[kingy - 1, kingx - 1] == 0)
                    {
                        checkmate = false;
                    }
                }
                if (kingx < 7)
                {
                    if (mask[kingy - 1, kingx + 1] == 0)
                    {
                        checkmate = false;
                    }
                }
            }

            if (kingy <7)
            {
                if (mask[kingy + 1, kingx] == 0)
                {
                    checkmate = false;
                }
                if (kingx > 0)
                {
                    if (mask[kingy + 1, kingx - 1] == 0)
                    {
                        checkmate = false;
                    }
                }
                if (kingx < 7)
                {
                    if (mask[kingy + 1, kingx + 1] == 0)
                    {
                        checkmate = false;
                    }
                }
            }

            if (kingx > 0)
            {
                if (mask[kingy, kingx - 1] == 0)
                {
                    checkmate = false;
                }
            }
            if (kingx <7)
            {
                if (mask[kingy, kingx + 1] == 0)
                {
                    checkmate = false;
                }
            }

            if (mask[kingy, kingx] == 0)
            {
                checkmate = false;
            }

            if (checkmate)
            {
                //run a function that checks whether you can block or take, not required for 3 pieces
            }

            return (checkmate);
        }

        //ONLY FOR 3 PIECES genereates the index of the position in the table based on the location of pieces in the position
        public int generateIndex(int[,] board)
        {
            int tableIndex = 0;
            int squared64 = 4096;
            List<int> pieces = new List<int> ();
            List<int> piecePos = new List<int>();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y,x] != 0)
                    {
                        pieces.Add (board[y,x]);
                        piecePos.Add(y * 8 + x);
                    }
                }
            }
            if (piecePos.Count == 2)
            {
                tableIndex = -1;
            }
            else
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    switch (pieces[i])
                    {
                        case 14:
                            tableIndex += piecePos[i] * squared64;
                            break;
                        case 6:
                            tableIndex += piecePos[i] * 64;
                            break;
                        default:
                            tableIndex += piecePos[i];
                            break;
                    }
                }
            }

            return tableIndex;
        }
        
        //Combines 2 lists + prevents duplicates and prevents positions being added that have a better eval already
        public List<board> merge(List<board> listA, List<board> listB, Position[] AllPositions, int col)
        {
            int j = 0;
            bool duplicate;

            if (col == 8)
            {
                for (int i = 0; i < listB.Count; i++)
                {
                    if (listB[i].tableIndex != -1)
                    {
                        if (AllPositions[listB[i].tableIndex].WhiteEval == 0)
                        {
                            j = 0;
                            duplicate = false;
                            while (!duplicate && j < listA.Count)
                            {
                                if (listA[j].tableIndex == listB[i].tableIndex)
                                    duplicate = true;
                                j++;
                            }

                            if (!duplicate)
                            {
                                listA.Add(listB[i]);
                                
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < listB.Count; i++)
                {
                    if (listB[i].tableIndex != -1)
                    {
                        if (AllPositions[listB[i].tableIndex].BlackEval == 0)
                        {
                            j = 0;
                            duplicate = false;
                            while (!duplicate && j < listA.Count)
                            {
                                if (listA[j].tableIndex == listB[i].tableIndex)
                                    duplicate = true;
                                j++;
                            }

                            if (!duplicate)
                            {
                                listA.Add(listB[i]);
                            }
                        }
                    }
                }
            }

            return listA;
        }

        //i wonder what this one does
        public int findPiece(int[,] board, int piece)
        {
            int Pos = 0;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y,x] == piece)
                    {
                        Pos = y * 8 + x;
                        return Pos;
                    }
                }
            }

                    return -1;
        }

        //checks if one side in a position is in check
        public bool inCheck(int[,] board, int [,] mask, int col)
        {
            bool check = false;
            int kingPos = findPiece(board, col + 6);

            if (mask[kingPos/8, kingPos%8] == 1)
            {
                check = true;
            }

            return check;
        }

        //move a piece from one sqyare to another
        public List<board> movePiece(List<board> positions,int[,] oldboard, int y1, int x1, int y2, int x2)
        {
            int[,] board = copyPos(oldboard);
            board[y2, x2] = board[y1, x1];
            board[y1, x1] = 0;
            positions.Add(addPos(board));
            //MessageBox.Show(Convert.ToString(positions.Last().tableIndex));
            return positions;
        }

        public board addPos(int[,] position)
        {
            board board = new board();
            board.pos = copyPos(position);
            board.tableIndex = generateIndex(board.pos);

            return board;
        }

        //promote a pawn and output all resulting positions
        public List<board> promote(List<board> positions, int[,] oldboard, int y1, int x1, int y2, int x2)
        {
            int[,] board = copyPos(oldboard);
            int col = board[y1, x1] - 1;
            board[y1, x1] = 0;
            board[y2, x2] = 2 + col;
            positions.Add(addPos(board));
            board[y2, x2] = 3 + col;
            positions.Add(addPos(board));
            board[y2, x2] = 4 + col;
            positions.Add(addPos(board));
            board[y2, x2] = 5 + col;
            positions.Add(addPos(board));

            return positions;
        }

        public List<board> kingMoves(List<board> positions, int[,] oldboard, int[,] enemyMask, int[,] mypiecemask, int y, int x)
        {
            for (int ychange = -1; ychange < 2; ychange ++)
            {
                for (int xchange = -1; xchange < 2; xchange++)
                {
                    if (x + xchange > -1 && x + xchange < 8 && y + ychange > -1 && y + ychange < 8 && !(xchange == 0 && ychange == 0) && mypiecemask[y + ychange, x + xchange] == 0 && enemyMask[y + ychange, x + xchange] == 0)
                    {
                        positions = movePiece(positions, oldboard, y, x, y + ychange, x + xchange);
                    }
                }
            }

            return positions;
        }

        public List<board> moveDirection(List<board> positions, int[,] board, int y, int x, int ydirection, int xdirection, int[,] ownpieces)
        {
            int d = 1;

            while (x + xdirection * d > -1 && x + xdirection * d < 8 && y + ydirection * d > -1 && y + ydirection * d < 8 && ownpieces[y + ydirection * d, x + xdirection * d] == 0) //while still on board
            {

                positions = movePiece(positions, board, y, x, y + ydirection * d, x + xdirection * d);
                if (board[y + ydirection * d, x + xdirection * d] == 0) 
                {
                    d++;
                }
                else
                {
                    d += 999; //how to use a goto without actually writing goto
                }
            }

            return positions;
        }

        //REMEMBER TO ADD PINNED FUNCTION FOR 4+ PIECES!!
        public List<board> GenerateAllMoves(int[,] board, string colour)
        {
            List<board> GeneratedPositions = new List<board>();
            int col = 0;
            if (colour == "white")
            {
                col = 8;
            }
            int pawnDirection = (col - 4) / 4;
            int[,] enemyAttackMask = maskCol(board, (col +8) %16);
            int[,] myPieceMask = maskPieces(board, col);
            int[,] enemyPieceMask = maskPieces(board, (col + 8) % 16);

            if (!inCheck(board, enemyAttackMask, col))
            {

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        switch (board[y,x] - col)
                            {
                            case 1:             //PAWN
                                if ((y + pawnDirection)% 7 == 0)  //if about to promote pawn
                                {
                                    if (board[y + pawnDirection, x] == 0)
                                    {
                                        GeneratedPositions = promote(GeneratedPositions, board, y, x, y + pawnDirection, x);
                                    }

                                    if (enemyPieceMask[y + pawnDirection, x-1] ==1)   //promote + take
                                    {
                                        GeneratedPositions = promote(GeneratedPositions, board, y, x, y + pawnDirection, x-1);
                                    }

                                    if (enemyPieceMask[y + pawnDirection, x + 1] == 1)
                                    {
                                        GeneratedPositions = promote(GeneratedPositions, board, y, x, y + pawnDirection, x + 1);
                                    }
                                }

                                if (board[y + pawnDirection, x] == 0)    //move 1 forward
                                {
                                    GeneratedPositions = movePiece(GeneratedPositions, board, y, x, y + pawnDirection, x);

                                    if ((y + pawnDirection) % 7 == 0 && board[y + 2*pawnDirection, x] == 0)      //move 2 forward
                                    {
                                        GeneratedPositions = movePiece(GeneratedPositions, board, y, x, y + 2*pawnDirection, x);
                                    }
                                }
                                if (x > 0)
                                {
                                    if (enemyPieceMask[y + pawnDirection, x - 1] == 1)       //take
                                    {
                                        GeneratedPositions = movePiece(GeneratedPositions, board, y, x, y + pawnDirection, x - 1);
                                    }
                                }

                                if (x < 7)
                                {
                                    if (enemyPieceMask[y + pawnDirection, x + 1] == 1)
                                    {
                                        GeneratedPositions = movePiece(GeneratedPositions, board, y, x, y + pawnDirection, x + 1);
                                    }
                                }

                                break;

                            case 2:
                                //kngiht (not needed until 4 pieces)
                                break;
                            case 3:
                                //bishop (not needed until 4 pieces)
                                break;
                            case 4:      //THE ROOOOOOOOK
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 1, 0, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, -1, 0, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 0, 1, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 0, -1, myPieceMask);
                                break;
                            case 5:      //THE QUEEEEEEN
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 1, 0, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, -1, 0, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 0, 1, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 0, -1, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 1, 1, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, -1, 1, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 1, -1, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, -1, -1, myPieceMask);
                                break;
                            case 6: //king
                                GeneratedPositions = kingMoves(GeneratedPositions, board, enemyAttackMask, myPieceMask, y, x);
                                break;
                        }
                    }
                }

            }
            else
            {
                int kingPos = findPiece(board, col + 6);
                int y = kingPos / 8;
                int x = kingPos % 8;

                //move
                GeneratedPositions = kingMoves(GeneratedPositions, board, enemyAttackMask,myPieceMask, y, x);

                //take or block


            }

            return GeneratedPositions;
        }

        //needs to be updated for 4 pieces since blocking is possible
        public bool undoMovesInCheck(int[,] board, int kingy, int kingx, int piecey, int piecex)
        {
            bool check = false;
            int[,] mask = new int[8, 8];

            switch (board[piecey, piecex])
            {
                case 1:
                    if (kingy +1 == piecey && (kingx -1 == piecex || kingx + 1 == piecex))
                    {
                        check = true;
                    }
                    break;
                case 9:
                    if (kingy - 1 == piecey && (kingx - 1 == piecex || kingx + 1 == piecex))
                    {
                        check = true;
                    }
                    break;
                case 2:
                case 10:
                    break;
                case 3:
                case 11:
                    break;
                case 4:
                case 12:
                    mask = maskdirection(piecey, piecex, -1, 0, board, mask);
                    mask = maskdirection(piecey, piecex, 0, -1, board, mask);
                    mask = maskdirection(piecey, piecex, 1, 0, board, mask);
                    mask = maskdirection(piecey, piecex, 0, 1, board, mask);
                    break;
                case 5:
                case 13:
                    mask = maskdirection(piecey, piecex, -1, -1, board, mask);
                    mask = maskdirection(piecey, piecex, -1, 1, board, mask);
                    mask = maskdirection(piecey, piecex, 1, -1, board, mask);
                    mask = maskdirection(piecey, piecex, 1, 1, board, mask);
                    mask = maskdirection(piecey, piecex, -1, 0, board, mask);
                    mask = maskdirection(piecey, piecex, 0, -1, board, mask);
                    mask = maskdirection(piecey, piecex, 1, 0, board, mask);
                    mask = maskdirection(piecey, piecex, 0, 1, board, mask);
                    break;
            }

            if (mask[kingy, kingx] == 1)
            {
                check = true;
            }

            return check;
        }

        //moves a sliding piece in one 1 direction, checking that no checks occur
        public List<board> undoMoveDirection(List<board> positions, int[,] board, int y, int x, int ydirection, int xdirection, int kingy, int kingx)
        {
            int d = 1;
            int enemyKing = board[kingy, kingx];
            int piece = board[y, x];
            board[y, x] = 0;

            while (x + xdirection * d > -1 && x + xdirection * d < 8 && y + ydirection * d > -1 && y + ydirection * d < 8 && (board[y + ydirection * d, x + xdirection * d] == 0)) //while still on board
            {
                board[y + ydirection * d, x +xdirection * d] = piece;

                if (!undoMovesInCheck(board, kingy, kingx, y + ydirection * d, x + xdirection * d))
                {
                    positions.Add(addPos(board));
                }

                board[y + ydirection * d, x + xdirection * d] = 0;
                d++;
            }
            board[y, x] = piece;

            return positions;
        }

        //only requirement is not be next to enemy king, can have moved out of check
        public List<board> undoKingMoves(List<board> positions, int[,] board, int y, int x, int enemykingy, int enemykingx)
        {
            if (Math.Abs(y - enemykingy) > 2 || Math.Abs(x - enemykingx) > 2)
            {
                for (int ychange = -1; ychange < 2; ychange++)
                {
                    for (int xchange = -1; xchange < 2; xchange++)
                    {
                        if (x + xchange > -1 && x + xchange < 8 && y + ychange > -1 && y + ychange < 8 && !(xchange ==0 && ychange ==0) && board[y + ychange, x + xchange] == 0)
                        {
                            positions = movePiece(positions, board, y, x, y + ychange, x + xchange);
                        }
                    }
                }
            }
            else
            {
                for (int ychange = -1; ychange < 2; ychange++)
                {
                    for (int xchange = -1; xchange < 2; xchange++)
                    {
                        if (x + xchange > -1 && x + xchange < 8 && y + ychange > -1 && y + ychange < 8 && !(xchange == 0 && ychange == 0) && board[y + ychange, x + xchange] == 0)
                        {
                            if (Math.Abs((y + ychange) - enemykingy) > 1 || Math.Abs((x + xchange) - enemykingx) > 1)
                            {
                                positions = movePiece(positions, board, y, x, y + ychange, x + xchange);
                            }
                        }
                    }
                }
            }
            return positions;
        }
        //essentailly the same as making all moves forward, but you cannot be chekcing the other player after your undone move
        //assumes you are not in check to start with
        public List<board> UndoAllMoves(int[,] board, string colour)
        {
            List<board> GeneratedPositions = new List<board>();
            int col = 0;
            if (colour == "white")
            {
                col = 8;
            }
            int otherKingPos = findPiece(board, (col + 8) % 16 + 6);
            int otherkingy = otherKingPos / 8;
            int otherkingx = otherKingPos % 8;
            int[,] attackMask = maskCol(board, col);

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        switch (board[y, x] - col)
                        {
                            case 1:
                                //gimme a minute
                                break;
                            case 2:
                                //kngiht (not needed until 4 pieces)
                                break;
                            case 3:
                                //bishop (not needed until 4 pieces)
                                break;
                            case 4:      //THE ROOOOOOOOK
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, 1, 0, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, -1, 0, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, 0, 1, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, 0, -1, otherkingy, otherkingx);
                                break;
                            case 5:      //THE QUEEEEEEN
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, 1, 0, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, -1, 0, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, 0, 1, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, 0, -1, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, 1, 1, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, -1, 1, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, 1, -1, otherkingy, otherkingx);
                                GeneratedPositions = undoMoveDirection(GeneratedPositions, board, y, x, -1, -1, otherkingy, otherkingx);
                                break;
                            case 6: //king
                            if (!inCheck(board, attackMask, (col + 8) % 16))
                            {
                                GeneratedPositions = undoKingMoves(GeneratedPositions, board, y, x, otherkingy, otherkingx);
                            }
                                break;
                        }
                    }
                }



            return GeneratedPositions;
        }


        private void btnGenPos_Click(object sender, EventArgs e)
        {
            int cubed64 = 262144;
            string TableName = txtTableName.Text;
            List<int> Pieces;
            Position[] AllPositions = new Position[cubed64];    //64^3          

            for (int i = 0; i < AllPositions.Length; i++)
            {
                AllPositions[i] = new Position();
            }

            Pieces = TableNameToPieces(TableName);

            GenerateAllPositions(Pieces, ref AllPositions);

            //find all checmates for white (+M# or 1000 stored in Black(ToMove)Eval)
            //white should be the only side with a piece, and so the only side that can checkmate


            List<board> MateinX = new List<board>();
            List<board> MateinXPlus1 = new List<board>();
            List<board> PMateinX = new List<board>(); //p for potentialMateinX

            int X = 0;
            int[,] whiteMask = new int[8, 8];
         
            for (int i = 0; i< AllPositions.Length; i++)
            {
                int kingPos = findPiece(AllPositions[i].board, 6);

                //if black king is found
                if (kingPos !=-1)
                {
                    whiteMask = maskCol(AllPositions[i].board, 8);

                    if (checkmate(AllPositions[i].board, whiteMask, 8))
                        {
                        //put the position in MateinX (X = 0 atm)
                        
                        MateinX.Add(addPos(AllPositions[i].board));
                        AllPositions[i].BlackEval = 1000;
                        
                    }
                }
                else
                {
                    //see notepad doc for evaluation definitions (9999 = illegal). if black king not found, position was not generated in generating phase so its illegal
                    AllPositions[i].WhiteEval = 9999;
                    AllPositions[i].BlackEval = 9999;
                }
            }

            //now all checkmate positions have been found: undoAllMoves, UndoAllMoves, GenAllMoves, repeat

            while (MateinX.Count > 0)
            {
                MessageBox.Show("Mate in " + Convert.ToString(X) + " positions = " + Convert.ToString(MateinX.Count));
                List<board> newPositions = new List<board>();                                 
                MateinXPlus1.Clear();
                
                for (int XIndex = 0; XIndex < MateinX.Count; XIndex++)        //add all M1 positions to MIX+1, and then update the white to move evaluations        
                {
                    newPositions = UndoAllMoves(MateinX[XIndex].pos, "white");

                    MateinXPlus1 = merge(MateinXPlus1, newPositions, AllPositions, 8);                   
                }

                MessageBox.Show("Mate in " + Convert.ToString(X+1) + " positions = " + Convert.ToString(MateinXPlus1.Count));

                MateinX.Clear();
                PMateinX.Clear();
                newPositions.Clear();

                for (int XPlus1Index = 0; XPlus1Index < MateinXPlus1.Count; XPlus1Index++)
                {
                    if (MateinXPlus1[XPlus1Index].tableIndex != -1)
                    {
                            AllPositions[MateinXPlus1[XPlus1Index].tableIndex].WhiteEval = 1000 - (X + 1);

                            newPositions = UndoAllMoves(MateinXPlus1[XPlus1Index].pos, "black");

                            PMateinX = merge(PMateinX, newPositions, AllPositions, 0);
                    }

                }

                MessageBox.Show("Potential mate in " + Convert.ToString(X+2) + " positions = " + Convert.ToString(PMateinX.Count));

                for (int PXIndex = 0; PXIndex < PMateinX.Count; PXIndex++)
                {
                        int bestEval = 1000 - (X + 1);
                        bool WillAdd = true;

                        newPositions = GenerateAllMoves(PMateinX[PXIndex].pos, "black");

                        int i = 0;
                        while (i < newPositions.Count && WillAdd)
                        {
                            if (newPositions[i].tableIndex != -1)
                            {
                                if (AllPositions[newPositions[i].tableIndex].WhiteEval < bestEval)   // for black '<' = better than, for white '>' = better than (less is better for black, visa versa
                                {
                                    WillAdd = false;
                                }
                            }
                            else
                            {
                                WillAdd = false;
                            }
                            i++;
                        }

                        if (WillAdd)
                        {
                            if (!MateinX.Contains(PMateinX[PXIndex]))
                            {
                                MateinX.Add(PMateinX[PXIndex]);
                                AllPositions[PMateinX[PXIndex].tableIndex].BlackEval = 1000 - (X + 2);
                            updateDisplay(AllPositions[PMateinX[PXIndex].tableIndex].board);
                            lblBlackEval.Text = Convert.ToString(AllPositions[PMateinX[PXIndex].tableIndex].BlackEval);
                            lblWhiteEval.Text = Convert.ToString(AllPositions[PMateinX[PXIndex].tableIndex].WhiteEval);
                        }
                        }                 
                }

                X += 2;
               MessageBox.Show("Done for x = " + Convert.ToString(X));
            }

            updateDisplay(AllPositions[46753].board);
            lblBlackEval.Text = Convert.ToString(AllPositions[30003].BlackEval);
            lblWhiteEval.Text = Convert.ToString(AllPositions[30003].WhiteEval);

            StreamWriter sw = new StreamWriter("KRvK.txt");
            string FEN = "";

            for (int i = 0; i < AllPositions.Length; i++)
            {
                if (AllPositions[i].WhiteEval != 9999)
                {
                    FEN = BoardToFEN(AllPositions[i].board);
                    sw.WriteLine(FEN + "," + Convert.ToString(AllPositions[i].WhiteEval) + "," + Convert.ToString(AllPositions[i].BlackEval));
                }
            }
            MessageBox.Show("Finished writing to file");
        }

        //-----------------TEST BUTTONS------------------------------------//
        private void btnMaskTest_Click(object sender, EventArgs e)
        {
            int[,] mask = new int[8, 8];

            mask = maskCol(myGlobals.board, 8);
            updateDisplay(mask);
        }

        private void btncmtest_Click(object sender, EventArgs e)
        {
            int[,] mask = new int[8, 8];
            bool CM;

            mask = maskCol(myGlobals.board, 8);

            CM = checkmate(myGlobals.board, mask, 8);

            if (CM)
            {
                MessageBox.Show("Checkmate, I think you'll find");
            }
            else
            {
                MessageBox.Show("nah");
            }


        }

        private void btnIndexTest_Click(object sender, EventArgs e)
        {
            int Index = generateIndex(myGlobals.board);

        }

        private void btnMovePieceTest_Click(object sender, EventArgs e)
        {
            int[,] board = myGlobals.board;
            List<board> positions = new List<board>();
            updateDisplay(board);
            MessageBox.Show("1");

            positions.Add(addPos(board));
            positions = movePiece(positions, board, 0, 0, 1, 1);

            updateDisplay(board);
            MessageBox.Show("2");
            updateDisplay(positions[1].pos);
            MessageBox.Show("3");
        }

        private void btnCheckTest_Click(object sender, EventArgs e)
        {

            updateDisplay(myGlobals.board);
            if (undoMovesInCheck(myGlobals.board, 2,1,1,0))
            {
                MessageBox.Show("Jaque!");
            }
            else
            {
                MessageBox.Show("ReallyMad, no Jaque!");
            }
            updateDisplay(myGlobals.board);
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            List<board> nextMoves = GenerateAllMoves(myGlobals.board, "black");

            for (int i = 0; i < nextMoves.Count; i++)
            {
                updateDisplay(nextMoves[i].pos);
                MessageBox.Show(Convert.ToString(i));
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            List<board> nextMoves = UndoAllMoves(myGlobals.board, "black");

            for (int i = 0; i < nextMoves.Count; i++)
            {
                updateDisplay(nextMoves[i].pos);
                MessageBox.Show(Convert.ToString(i));
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            board nextBoard = addPos(myGlobals.board);
            nextBoard.tableIndex = generateIndex(nextBoard.pos);
            MessageBox.Show(Convert.ToString(nextBoard.tableIndex));
            bool dupe = false;

            for (int i = 0; i < myGlobals.givenPositions.Count; i++)
            {
                if (myGlobals.givenPositions[i].tableIndex == nextBoard.tableIndex)
                {
                    dupe = true;
                }
            }

            if (!dupe)
            {
                myGlobals.givenPositions.Add(nextBoard);
                MessageBox.Show("New position " + Convert.ToString(myGlobals.givenPositions.Count));
            }
            else
            {
                MessageBox.Show("Positions already in list " + Convert.ToString(myGlobals.givenPositions.Count));
            }
        }

        //assumes only 1 piece has moved (no castling / en pasant)
        public string moveName(int[,] board1, int[,] board2)
        {
            string[] columns = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", };
            string Name = "";
            int col;
            int x1 = -1;
            int y1 = -1;
            int x2 = -1;
            int y2 = -1;
            int startx = 0;
            int starty = 0;
            int finalx = 0;
            int finaly = 0;
            int piece = 0;
            string pieceLetter;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board1[y, x] != board2[y, x])
                    {
                        if (x1 == -1)
                        {
                            x1 = x;
                            y1 = y;
                        }
                        else
                        {
                            x2 = x;
                            y2 = y;
                        }
                    }
                }
            }
            if (board1[y1, x1] == board2[y2, x2] && board2[y2, x2] != 0)
            {
                startx = x1;
                starty = y1;
                finalx = x2;
                finaly = y2;
            }
            else
            {
                startx = x2;
                starty = y2;
                finalx = x1;
                finaly = y1;
            }


                piece = board2[finaly, finalx];
                col = (piece / 8) * 8;
                if(piece == 1 || piece == 9)
                {
                    pieceLetter = "";
                }
                else
                {
                    pieceLetter = pieceNumToString((piece%8) + 8);
                }


                Name += pieceLetter;      // add piece letter e.g. Q for queen

            if (board1[finaly, finalx] != 0)          //if move was a capture, add an x
                {
                    if (pieceLetter != "") //if its not a pawn otherwise it gets weird
                    {
                        Name += "x";
                    }
                    else
                    {
                        Name += columns[startx] + "x";                     
                    }
                }

                Name += columns[finalx] + Convert.ToString(8 - finaly); //add final position of piece

            int[,] mask = maskCol(board2, col);

                    if (inCheck(board2, mask, (col + 8) % 16))
                    {
                        Name += "+";
                    }

            return Name;
        }
        public int displayMoves(int[,] board)
        {
            string colour = "black";
            string nameMove = "";
            string Evaluation = "";
            int score;
            string[] names = new string[128];
            int[] scores = new int[128];

            if(myGlobals.ColToMove == 8)
            {
                colour = "white";
            }

            List<board> nextPositions = GenerateAllMoves(board, colour);

            for (int i = 0; i< nextPositions.Count; i++)
            {
                //updateDisplay(nextPositions[i].pos);
                nameMove = moveName(board, nextPositions[i].pos);

                if (nextPositions[i].tableIndex != -1)
                {
                    if (colour == "black")
                    {
                        score = myGlobals.Tablebase[nextPositions[i].tableIndex].WhiteEval;
                    }
                    else
                    {
                        score = myGlobals.Tablebase[nextPositions[i].tableIndex].BlackEval;
                    }
                }
                else
                {
                    score = 0;
                }
                names[i] = nameMove;
                scores[i] = score;
            }

            int tempInt;
            string tempString;

            for (int i = 0; i < nextPositions.Count-2; i++)   //bubble sort OMEGALUL
            {
                for (int j = 0; j < nextPositions.Count-1; j++)
                {
                    if (scores[j] < scores[j+1])
                    {
                        tempInt = scores[j];
                        scores[j] = scores[j + 1];
                        scores[j + 1] = tempInt;
                        tempString = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = tempString;
                    }
                }
            }

            for (int i = 0; i < nextPositions.Count; i++)
            {

                switch (scores[i])
                {
                    case 0:
                        Evaluation = "Draw";
                        break;
                    case 1000:
                        Evaluation = "Checkmate";
                        break;
                    default:
                        Evaluation = "DTM " + Convert.ToString(1000 - scores[i]);
                        break;
                }

                lstMoveNames.Items.Add(names[i]);
                lstMoveEvals.Items.Add(Evaluation);
                
            }

                return 0;
        }

        private void btnEvaluate_Click(object sender, EventArgs e)
        {
            int tableIndex = generateIndex(myGlobals.board);
            int Eval;

            lstMoveEvals.Items.Clear();
            lstMoveNames.Items.Clear();

            if (tableIndex != -1)
            {
                if (myGlobals.Tablebase[tableIndex] != null)
                {
                    lblWhiteEval.Text = Convert.ToString(myGlobals.Tablebase[tableIndex].WhiteEval);
                    lblBlackEval.Text = Convert.ToString(myGlobals.Tablebase[tableIndex].BlackEval);
                }

                if (myGlobals.ColToMove == 8)
                {
                    Eval = myGlobals.Tablebase[tableIndex].WhiteEval;
                }
                else
                {
                    Eval = myGlobals.Tablebase[tableIndex].BlackEval;
                }

                switch (Eval)
                {
                    case 0:
                        lblEval.Text = "Draw / illegal position";
                        break;
                    case 1000:
                        lblEval.Text = "White won by checkmate";
                        break;
                    case 9999:
                        lblEval.Text = "Illegal position";
                        break;
                    default:
                        lblEval.Text = "White is winning:    DTM " + Convert.ToString(1000 - Eval);
                        displayMoves(myGlobals.board);

                        break;
                }
            }
            else
            {
                lblEval.Text = "Draw by insufficient material";
            }



        }

        private void btnUndoW_Click(object sender, EventArgs e)
        {
            List<board> nextMoves = UndoAllMoves(myGlobals.board, "white");

            for (int i = 0; i < nextMoves.Count; i++)
            {
                updateDisplay(nextMoves[i].pos);
                MessageBox.Show(Convert.ToString(i));
            }
            MessageBox.Show("done");
        }
    }
    }
    
    