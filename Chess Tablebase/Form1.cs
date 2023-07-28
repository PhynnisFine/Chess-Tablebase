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
            public static int selectedPiece = -1;
            public static int ColToMove = 8;
            public static List<board> givenPositions = new List<board>();
            public static Position[,] Tablebase = new Position[3,262144];
            public static int selectedSquare = -1;
            public static string[] tables = new string[] {"KQvK", "KRvK", "KPvK" };
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

        public int readTablebase(string filename, int tableNum)
        {
            //files need to be stored 4 levels up so that they show up on the github page
            filename = "..\\..\\..\\..\\Tablebase_files\\" + filename;

            try
            {
                StreamReader sr = new StreamReader(filename);

                for (int i = 0; i < 262144; i++)
                {
                    myGlobals.Tablebase[tableNum, i] = new Position();
                    myGlobals.Tablebase[tableNum, i].WhiteEval = 9999;
                    myGlobals.Tablebase[tableNum, i].BlackEval = 9999;
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
                            myGlobals.Tablebase[tableNum, tableIndex].board = copyPos(board);
                            myGlobals.Tablebase[tableNum, tableIndex].WhiteEval = Convert.ToInt16(parts[1]);
                            myGlobals.Tablebase[tableNum, tableIndex].BlackEval = Convert.ToInt16(parts[2]);
                        }
                    }

                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception p)
            {
                MessageBox.Show("Exception: " + p.Message);
            }

            return 0;
        }

        //creates 2d array of button, reads tablebase from text files
        private void frmBoard_Load(object sender, EventArgs e)
        {
            myGlobals.ColToMove = 0;
            readTablebase("KQvK.txt", 0);
            readTablebase("KRvK.txt", 1);
            //readTablebase("KPvK.txt", 2);
            MessageBox.Show("Finished reading tablebase from files");

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

                if (gap != 0)
                {
                    FEN += Convert.ToString(gap);
                }
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

        //-------------------UI Buttons (board squares + pieces)------------------------//
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
            myGlobals.selectedSquare = -1;
        }

        private void btnWKnight_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 10;
            pieceSelected();
            btnWKnight.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnWBishop_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 11;
            pieceSelected();
            btnWBishop.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnWRook_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 12;
            pieceSelected();
            btnWRook.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnWQueen_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 13;
            pieceSelected();
            btnWQueen.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnWKing_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 14;
            pieceSelected();
            btnWKing.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnBPawn_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 1;
            pieceSelected();
            btnBPawn.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnBKnight_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 2;
            pieceSelected();
            btnBKnight.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnBBishop_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 3;
            pieceSelected();
            btnBBishop.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnBRook_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 4;
            pieceSelected();
            btnBRook.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnBQueen_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 5;
            pieceSelected();
            btnBQueen.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        private void btnBKing_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 6;
            pieceSelected();
            btnBKing.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }
        //should be btnDelete
        private void button1_Click(object sender, EventArgs e)
        {
            myGlobals.selectedPiece = 0;
            pieceSelected();
            button1.BackColor = Color.Gray;
            myGlobals.selectedSquare = -1;
        }

        public int buttonClicked(int y,int x)
        {
            if (myGlobals.selectedSquare == -1)
            {
                if (myGlobals.selectedPiece != -1)
                {
                    myGlobals.board[y, x] = myGlobals.selectedPiece;
                    updateDisplay(myGlobals.board);
                }
                else
                {
                    if (myGlobals.board[y, x] != 0)
                    {
                        myGlobals.selectedSquare = y * 8 + x;
                    }
                }
            }
            else
            {
                int piece = myGlobals.board[myGlobals.selectedSquare / 8, myGlobals.selectedSquare % 8];
                if (myGlobals.board[y, x] == 0 || myGlobals.board[y, x] / 8 != piece / 8)
                {
                    myGlobals.board[y, x] = piece;
                    myGlobals.board[myGlobals.selectedSquare / 8, myGlobals.selectedSquare % 8] = 0;
                    updateDisplay(myGlobals.board);
                }
                myGlobals.selectedSquare = -1;
            }

            myGlobals.selectedPiece = -1;
            pieceSelected();

            return 0;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;

            buttonClicked(y, x);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            int x = 1;
            int y = 0;
            buttonClicked(y, x);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            int x = 2;
            int y = 0;
            buttonClicked(y, x);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            int x = 3;
            int y = 0;
            buttonClicked(y, x);

        }

        private void btn4_Click(object sender, EventArgs e)
        {
            int x = 4;
            int y = 0;
            buttonClicked(y, x);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            int x = 5;
            int y = 0;
            buttonClicked(y, x);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            int x = 6;
            int y = 0;
            buttonClicked(y, x);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            int x = 7;
            int y = 0;
            buttonClicked(y, x);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 1;
            buttonClicked(y, x);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            int x = 1;
            int y = 1;
            buttonClicked(y, x);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            int x = 2;
            int y = 1;
            buttonClicked(y, x);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            int x = 3;
            int y = 1;
            buttonClicked(y, x);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            int x = 4;
            int y = 1;
            buttonClicked(y, x);
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            int x = 5;
            int y = 1;
            buttonClicked(y, x);
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            int x = 6;
            int y = 1;
            buttonClicked(y, x);
        }

        private void btn15_Click(object sender, EventArgs e)
        {
            int x = 7;
            int y = 1;
            buttonClicked(y, x);
        }

        private void btn16_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 2;
            buttonClicked(y, x);
        }

        private void btn17_Click(object sender, EventArgs e)
        {
            int x = 1;
            int y = 2;
            buttonClicked(y, x);
        }

        private void btn18_Click(object sender, EventArgs e)
        {
            int x = 2;
            int y = 2;
            buttonClicked(y, x);
        }

        private void btn19_Click(object sender, EventArgs e)
        {
            int x = 3;
            int y = 2;
            buttonClicked(y, x);
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            int x = 4;
            int y = 2;
            buttonClicked(y, x);
        }

        private void btn21_Click(object sender, EventArgs e)
        {
            int x = 5;
            int y = 2;
            buttonClicked(y, x);
        }

        private void btn22_Click(object sender, EventArgs e)
        {
            int x = 6;
            int y = 2;
            buttonClicked(y, x);
        }

        private void btn23_Click(object sender, EventArgs e)
        {
            int x = 7;
            int y = 2;
            buttonClicked(y, x);
        }

        private void btn24_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 3;
            buttonClicked(y, x);
        }

        private void btn25_Click(object sender, EventArgs e)
        {
            int x = 1;
            int y = 3;
            buttonClicked(y, x);
        }

            private void btn26_Click(object sender, EventArgs e)
        {
            int x = 2;
            int y = 3;
            buttonClicked(y, x);
        }

        private void btn27_Click(object sender, EventArgs e)
        {
            int x = 3;
            int y = 3;
            buttonClicked(y, x);
        }

        private void btn28_Click(object sender, EventArgs e)
        {
            int x = 4;
            int y = 3;
            buttonClicked(y, x);
        }

        private void btn29_Click(object sender, EventArgs e)
        {
            int x = 5;
            int y = 3;
            buttonClicked(y, x);
        }

        private void btn30_Click(object sender, EventArgs e)
        {
            int x = 6;
            int y = 3;
            buttonClicked(y, x);
        }

        private void btn31_Click(object sender, EventArgs e)
        {
            int x = 7;
            int y = 3;
            buttonClicked(y, x);
        }

        private void btn32_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 4;
            buttonClicked(y, x);
        }

        private void btn33_Click(object sender, EventArgs e)
        {
            int x = 1;
            int y = 4;
            buttonClicked(y, x);
        }

        private void btn34_Click(object sender, EventArgs e)
        {
            int x = 2;
            int y = 4;
            buttonClicked(y, x);
        }

        private void btn35_Click(object sender, EventArgs e)
        {
            int x = 3;
            int y = 4;
            buttonClicked(y, x);
        }

            private void btn36_Click(object sender, EventArgs e)
        {
            int x = 4;
            int y = 4;
            buttonClicked(y, x);
        }

        private void btn37_Click(object sender, EventArgs e)
        {
            int x = 5;
            int y = 4;
            buttonClicked(y, x);
        }

        private void btn38_Click(object sender, EventArgs e)
        {
            int x = 6;
            int y = 4;
            buttonClicked(y, x);
        }

        private void btn39_Click(object sender, EventArgs e)
        {
            buttonClicked(4, 7);
        }

        private void btn40_Click(object sender, EventArgs e)
        {
            buttonClicked(5, 0);
        }

        private void btn41_Click(object sender, EventArgs e)
        {
            buttonClicked(5, 1);
        }

        private void btn42_Click(object sender, EventArgs e)
        {
            buttonClicked(5, 2);
        }

        private void btn43_Click(object sender, EventArgs e)
        {
            buttonClicked(5, 3);
        }

        private void btn44_Click(object sender, EventArgs e)
        {
            buttonClicked(5, 4);
        }

        private void btn45_Click(object sender, EventArgs e)
        {
            buttonClicked(5, 5);
        }

        private void btn46_Click(object sender, EventArgs e)
        {
            buttonClicked(5, 6);
        }

        private void btn47_Click(object sender, EventArgs e)
        {
            buttonClicked(5, 7);
        }

        private void btn48_Click(object sender, EventArgs e)
        {
            buttonClicked(6, 0);
        }

        private void btn49_Click(object sender, EventArgs e)
        {
            buttonClicked(6, 1);
        }

        private void btn50_Click(object sender, EventArgs e)
        {
            buttonClicked(6, 2);
        }

        private void btn51_Click(object sender, EventArgs e)
        {
            buttonClicked(6, 3);
        }

        private void btn52_Click(object sender, EventArgs e)
        {
            buttonClicked(6, 4);
        }

        private void btn53_Click(object sender, EventArgs e)
        {
            buttonClicked(6, 5);
        }

        private void btn54_Click(object sender, EventArgs e)
        {
            buttonClicked(6, 6);
        }

        private void btn55_Click(object sender, EventArgs e)
        {
            buttonClicked(6, 7);
        }

        private void btn56_Click(object sender, EventArgs e)
        {
            buttonClicked(7, 0);
        }

        private void btn57_Click(object sender, EventArgs e)
        {
            buttonClicked(7, 1);
        }

        private void btn58_Click(object sender, EventArgs e)
        {
            buttonClicked(7, 2);
        }

        private void btn59_Click(object sender, EventArgs e)
        {
            buttonClicked(7, 3);
        }

        private void btn60_Click(object sender, EventArgs e)
        {
            buttonClicked(7, 4);
        }

        private void btn61_Click(object sender, EventArgs e)
        {
            buttonClicked(7, 5);
        }

        private void btn62_Click(object sender, EventArgs e)
        {
            buttonClicked(7, 6);
        }

        private void btn63_Click(object sender, EventArgs e)
        {
            buttonClicked(7, 7);
        }

        //--------------------------------Tablebase creation functions----------------------------//

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

        public string PiecesToTableName(List<int> Pieces)
        {
            string tableName = "";
            for (int i = 0; i < Pieces.Count; i++)
            {
                if (Pieces[i] > 8)
                {
                    tableName += pieceNumToString(Pieces[i]);
                }
            }

            tableName += "v";
            for (int i = 0; i < Pieces.Count; i++)
            {
                if (Pieces[i] < 8)
                {
                    tableName += pieceNumToString(Pieces[i]+8);
                }
            }

            return tableName;
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
                                if (piece != 9 && piece != 1)  //if piece is not a pawn
                                {
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
                                }
                                else 
                                {
                                    positionsIndex += 8;
                                    for (int pieceY = 1; pieceY < 7; pieceY++)
                                    {
                                        for (int pieceX = 0; pieceX < 8; pieceX++) 
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
                                    positionsIndex += 8;
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

        public List<board> tryMove(List<board> Positions, int[,] oldboard, int y, int x, int ychange, int xchange, int col)
        {
            int[,] board = copyPos(oldboard);

            if (y+ychange >=0 && y + ychange <=7 && x+xchange >=0 && x+xchange <=7 && board[y + ychange, x + xchange] == 0)
            {
                board[y + ychange, x + xchange] = board[y, x];
                board[y, x] = 0;
                Positions.Add(addPos(board));
            }

                return Positions;
        }

        //used for finding squares that the opposite king can move to
        public int[,] maskCol(int[,] board, int col)
        {
            int[,] mask = new int[8, 8];
            int pawnDirection = (4 - col) / 4;

            for (int y = 0; y<8; y++)
            {
                for (int x =0; x <8; x++)
                {
                    switch (board[y,x] - col)
                    {
                        case 1:
                            //pawn
                            mask = trymask(y, x, pawnDirection, 1, board, mask, col);
                            mask = trymask(y, x, pawnDirection, -1, board, mask, col);
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

        //generates table index for one table (one set of pieces) while it is being generated
        public int generateIndex(int[,] board)
        {
            int tableIndex = 0;
            int squared64 = 4096;
            bool bishopOrKnight = false;
            List<int> pieces = new List<int> ();
            List<int> piecePos = new List<int>();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y,x] != 0 && board[y, x] != null)
                    {
                        pieces.Add (board[y,x]);
                        piecePos.Add(y * 8 + x);
                        if (board[y, x] == 2 || board[y, x] == 3 || board[y,x] == 10 || board[y,x] == 11)
                        {
                            bishopOrKnight = true;
                        }
                    }
                }
            }

            if (piecePos.Count == 2 || (piecePos.Count == 3 && bishopOrKnight))
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
                            //MessageBox.Show("White King at " + Convert.ToString(piecePos[i]));
                            break;
                        case 6:
                            tableIndex += piecePos[i] * 64;
                            //MessageBox.Show("Black King at " + Convert.ToString(piecePos[i]));
                            break;
                        default:
                            tableIndex += piecePos[i];
                            //MessageBox.Show("Piece at " + Convert.ToString(piecePos[i]));
                            break;
                    }
                }
            }

            return tableIndex;
        }

        //generates tablebase index (for all sets of pieces that have been generated) while a position is being searched for
        public int[] generateTBIndex(int[,] board)
        {
            int[] tableBaseIndex = new int[2];
            int tableIndex = generateIndex(board);
            int tableNum = 0;

            if (tableIndex != -1)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        switch (board[y,x])
                        {
                            case 13:
                                tableNum = 0;
                                break;
                            case 12:
                                tableNum = 1;
                                break;
                            case 9:
                                tableNum = 2;
                                break;
                        }
                    }
                }
            }

            tableBaseIndex[0] = tableNum;
            tableBaseIndex[1] = tableIndex;

            return tableBaseIndex;
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

            if (kingPos != -1)
            {
                if (mask[kingPos / 8, kingPos % 8] == 1)
                {
                    check = true;
                }
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
            int pawnDirection = (4-col) / 4;
            int[,] enemyAttackMask = maskCol(board, (col +8) %16);
            int[,] myPieceMask = maskPieces(board, col);

            if (!inCheck(board, enemyAttackMask, col))
            {

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        switch (board[y,x] - col)
                            {
                            case 1:             //PAWN
                                int[,] enemyPieceMask = maskPieces(board, (col + 8) % 16);

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
                                else if (board[y + pawnDirection, x] == 0)    //move 1 forward
                                {
                                    GeneratedPositions = movePiece(GeneratedPositions, board, y, x, y + pawnDirection, x);

                                    if ((y - pawnDirection) % 7 == 0 && board[y + 2*pawnDirection, x] == 0)      //move 2 forward
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
                                //knight (not needed until 4 pieces)
                                GeneratedPositions = tryMove(GeneratedPositions, board, y, x, 2, 1, col);
                                GeneratedPositions = tryMove(GeneratedPositions, board, y, x, 1, 2, col);
                                GeneratedPositions = tryMove(GeneratedPositions, board, y, x, -2, 1, col);
                                GeneratedPositions = tryMove(GeneratedPositions, board, y, x, -1, 2, col);
                                GeneratedPositions = tryMove(GeneratedPositions, board, y, x, 2, -1, col);
                                GeneratedPositions = tryMove(GeneratedPositions, board, y, x, 1, -2, col);
                                GeneratedPositions = tryMove(GeneratedPositions, board, y, x, -2, -1, col);
                                GeneratedPositions = tryMove(GeneratedPositions, board, y, x, -1, -2, col);
                                break;
                            case 3:
                                //bishop (not needed until 4 pieces)
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 1, 1, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, -1, 1, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, 1, -1, myPieceMask);
                                GeneratedPositions = moveDirection(GeneratedPositions, board, y, x, -1, -1, myPieceMask);
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

        //only requirement is not be next to enemy king *(and not to reveal a check) **(and to block checks), can move out of check
        public List<board> undoKingMoves(List<board> positions, int[,] board, int y, int x, int enemykingy, int enemykingx, int col)
        {
            if (Math.Abs(y - enemykingy) > 2 || Math.Abs(x - enemykingx) > 2)
            {
                for (int ychange = -1; ychange < 2; ychange++)
                {
                    for (int xchange = -1; xchange < 2; xchange++)
                    {
                        if (x + xchange > -1 && x + xchange < 8 && y + ychange > -1 && y + ychange < 8 && !(xchange ==0 && ychange ==0) && board[y + ychange, x + xchange] == 0)
                        {
                                int[,] newboard = copyPos(board);
                                newboard[y + ychange, x + xchange] = newboard[y, x];
                                newboard[y, x] = 0;
                                int[,] newMask = maskCol(newboard, col);

                                if (!inCheck(newboard, newMask, (col + 8) % 16))
                                    {
                                    positions.Add(addPos(newboard));
                                }
                            
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
                                int[,] newboard = copyPos(board);
                                newboard[y + ychange, x + xchange] = newboard[y, x];
                                newboard[y, x] = 0;
                                int[,] newMask = maskCol(newboard, col);

                                if (!inCheck(newboard, newMask, (col + 8) % 16))
                                {
                                    positions.Add(addPos(newboard));
                                }
                            }
                        }
                    }
                }
            }
            return positions;
        }
        //essentailly the same as making all moves forward, but you cannot be chekcing the other player after your undone move
        //assumes you are not in check to start with
        //Very definitely needs recoding if i ever work on this again
        public List<board> UndoAllMoves(int[,] board, string colour)
        {
            List<board> GeneratedPositions = new List<board>();
            int col = 0;
            int pawndirection = 1;
            if (colour == "white")
            {
                col = 8;
                pawndirection = -1;
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
                            if (board[y-pawndirection, x] == 0)
                            {
                                if (!(otherkingy - (y - pawndirection) == -1 && Math.Abs(otherkingx - x) == 1)) //if you are not putting the other king in check (should make an undo move function if i need to use it for knights)
                                    {
                                    GeneratedPositions = movePiece(GeneratedPositions, board, y, x, y - pawndirection, x);
                                }
                                if ((y - 3*pawndirection) % 7 == 0 && board[y - 2* pawndirection, x] == 0)
                                {
                                    if (!(otherkingy - (y - 2*pawndirection) == -1 && Math.Abs(otherkingx - x) == 1))
                                    {
                                        GeneratedPositions = movePiece(GeneratedPositions, board, y, x, y - pawndirection, x);
                                    }
                                    GeneratedPositions = movePiece(GeneratedPositions, board, y, x, y - 2*pawndirection, x);
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
                            case 6: //king (can do blocking moves even if other king already in check)
                                GeneratedPositions = undoKingMoves(GeneratedPositions, board, y, x, otherkingy, otherkingx, col);

                                break;
                        }
                    }
                }



            return GeneratedPositions;
        }

        public Position[] GenerateMovesToTable(Position[] allPositions, List<int> Pieces, int startPiece, int endPiece, int col)
        {
            string tableName = PiecesToTableName(Pieces);
            int tableNum = 0;
            int tableIndex = 0;

            for (int i = 0; i < myGlobals.tables.Length; i++)
            {
                if(tableName == myGlobals.tables[i])
                {
                    tableNum = i;
                }
            }

            if (endPiece != 0)   //promote pawn
            {
                int pawnDirection = (4 - col) / 4;

                for (int i = 0; i < allPositions.Length; i++)
                {
                    if (allPositions[i].WhiteEval != 9999)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                if (allPositions[i].board[y, x] == startPiece)
                                {
                                    if ((y + pawnDirection) % 7 == 0 && allPositions[i].board[y + pawnDirection, x] == 0)   //does not yet consider capture promotions
                                    {
                                        if (x == 0)
                                        {
                                            if (allPositions[i].board[y + pawnDirection, x + 1] != 6)
                                            {
                                                allPositions[i].board[y, x] = 0;
                                                allPositions[i].board[y + pawnDirection, x] = endPiece;
                                                tableIndex = generateIndex(allPositions[i].board);

                                                if (tableIndex == 98818)
                                                {
                                                    updateDisplay(allPositions[i].board);
                                                    MessageBox.Show(Convert.ToString(allPositions[i].WhiteEval));
                                                    MessageBox.Show(Convert.ToString(myGlobals.Tablebase[tableNum, tableIndex].BlackEval - 1));
                                                }

                                                if (myGlobals.Tablebase[tableNum, tableIndex].BlackEval - 1 > allPositions[i].WhiteEval && col == 8)
                                                {
                                                    allPositions[i].WhiteEval = myGlobals.Tablebase[tableNum, tableIndex].BlackEval - 1;
                                                    if (endPiece == 12)
                                                    {
                                                        MessageBox.Show("damn");
                                                    }
                                                }

                                                allPositions[i].board[y, x] = startPiece;
                                                allPositions[i].board[y + pawnDirection, x] = 0;

                                            }
                                        }
                                        else if (x == 7)
                                        {
                                            if (allPositions[i].board[y + pawnDirection, x - 1] != 6)
                                            {
                                                allPositions[i].board[y, x] = 0;
                                                allPositions[i].board[y + pawnDirection, x] = endPiece;
                                                tableIndex = generateIndex(allPositions[i].board);

                                                if (myGlobals.Tablebase[tableNum, tableIndex].BlackEval - 1 > allPositions[i].WhiteEval && col == 8)
                                                {
                                                    allPositions[i].WhiteEval = myGlobals.Tablebase[tableNum, tableIndex].BlackEval - 1;
                                                }


                                                allPositions[i].board[y, x] = startPiece;
                                                allPositions[i].board[y + pawnDirection, x] = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (allPositions[i].board[y + pawnDirection, x + 1] != 6 && allPositions[i].board[y + pawnDirection, x - 1] != 6)
                                            {

                                                allPositions[i].board[y, x] = 0;
                                                allPositions[i].board[y + pawnDirection, x] = endPiece;
                                                tableIndex = generateIndex(allPositions[i].board);


                                                if (myGlobals.Tablebase[tableNum, tableIndex].BlackEval-1  > allPositions[i].WhiteEval && col == 8)
                                                {
                                                    allPositions[i].WhiteEval = myGlobals.Tablebase[tableNum, tableIndex].BlackEval - 1;
                                                }


                                                allPositions[i].board[y, x] = startPiece;
                                                allPositions[i].board[y + pawnDirection, x] = 0;
                                            }
                                        }
                                        

                                    }
                                }
                            }
                        }
                    }
                }
            }
            else     //take start piece
            {

            }

            return allPositions;
        }

        public List<board> findAllMIX(Position[] allPositions, int x)
        {
            List<board> MIXPositions = new List<board> ();

            if (x % 2 == 0)
            {
                for (int i = 0; i < allPositions.Length; i++)
                {
                    if (allPositions[i].BlackEval == 1000 - x)
                    {
                        MIXPositions.Add(addPos(allPositions[i].board));
                    }
                }
            }
            else
            {
                for (int i = 0; i < allPositions.Length; i++)
                {
                    if (allPositions[i].WhiteEval == 1000 - x)
                    {
                        MIXPositions.Add(addPos(allPositions[i].board));
                    }
                }
            }

            return MIXPositions;
        }
        private void btnGenPos_Click(object sender, EventArgs e)
        {
            int cubed64 = 262144;
            string TableName = txtTableName.Text;
            List<int> Pieces;
            Position[] AllPositions = new Position[cubed64];    //64^3
            List<board> MateinX = new List<board>();
            List<board> MateinXPlus1 = new List<board>();
            List<board> PMateinX = new List<board>(); //p for potentialMateinX
            bool dependencies = false;
            bool foundAny = false;

            int X = 0;
            int[,] whiteMask = new int[8, 8];

            for (int i = 0; i < AllPositions.Length; i++)
            {
                AllPositions[i] = new Position();
            }

            //generate all positions for the given table (set of pieces)

            Pieces = TableNameToPieces(TableName);

            GenerateAllPositions(Pieces, ref AllPositions);

            //find all invalid positions (positions where the side that is not about to move is in check)

            int[,] Wmask;
            int[,] Bmask;
            for (int i = 0; i < AllPositions.Length; i++)
            {
                Wmask = maskCol(AllPositions[i].board, 8);
                Bmask = maskCol(AllPositions[i].board, 0);

                if (inCheck(AllPositions[i].board, Wmask, 0))
                {
                    AllPositions[i].WhiteEval = 9999;
                    //updateDisplay(AllPositions[i].board);
                    //MessageBox.Show("white to move");
                }
                if (inCheck(AllPositions[i].board, Bmask, 8))
                {
                    AllPositions[i].BlackEval = 9999;
                }
            }

            //find all checmates for white (+M# or 1000 stored in Black(ToMove)Eval)
            //white should be the only side with a piece (for 3 pieces), and so the only side that can checkmate

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
                        foundAny = true;
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

            //work out which dependencies are relevant and GenerateMovesToTable

            int startPiece = 0;
            int endPiece = 0;

            for (int i = 0; i < Pieces.Count; i++)
            {
                switch (Pieces[i])
                {
                    case 9:
                        dependencies = true;
                        startPiece = Pieces[i];
                        endPiece = 13;
                        Pieces[i] = endPiece;
                        AllPositions = GenerateMovesToTable(AllPositions, Pieces, startPiece, endPiece, 8);
                        endPiece = 12;
                        Pieces[i] = endPiece;
                        AllPositions = GenerateMovesToTable(AllPositions, Pieces, startPiece, endPiece, 8);
                        if (Pieces.Count > 3)
                        {
                            //do same for bishop and knight
                        }
                        Pieces[i] = startPiece;
                        break;
                    //all other cases are just one piece gets taken

                }
            }


            //now all winning positions have been found: undoAllMoves, UndoAllMoves, GenAllMoves, repeat

            while (MateinX.Count > 0 || !foundAny)
            {
                //MessageBox.Show("Mate in " + Convert.ToString(X) + " positions = " + Convert.ToString(MateinX.Count));
                List<board> newPositions = new List<board>();                                 
                MateinXPlus1.Clear();

                if (dependencies)
                {
                    MateinXPlus1 = findAllMIX(AllPositions, X + 1);
                }


                for (int XIndex = 0; XIndex < MateinX.Count; XIndex++)        //add all M1 positions to MIX+1, and then update the white to move evaluations        
                {
                    newPositions = UndoAllMoves(MateinX[XIndex].pos, "white");

                    MateinXPlus1 = merge(MateinXPlus1, newPositions, AllPositions, 8);                   
                }

              if (MateinXPlus1.Count != 0 && !foundAny)
                {
                    foundAny = true;
                }

               // MessageBox.Show("Mate in " + Convert.ToString(X+1) + " positions = " + Convert.ToString(MateinXPlus1.Count));

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

             // MessageBox.Show("Potential mate in " + Convert.ToString(X+2) + " positions = " + Convert.ToString(PMateinX.Count));

                if (dependencies)
                {
                    MateinX = findAllMIX(AllPositions, X + 2);
                }

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

                if (MateinX.Count != 0 && !foundAny)
                {
                    foundAny = true;
                }

                X += 2;
               //MessageBox.Show("Done for x = " + Convert.ToString(X));
            }


            //write to file

            StreamWriter sw = new StreamWriter(TableName + ".txt");
            string FEN = "";

            for (int i = 0; i < AllPositions.Length; i++)
            {
                if (AllPositions[i].BlackEval != 9999 || AllPositions[i].WhiteEval != 9999)
                {
                    FEN = BoardToFEN(AllPositions[i].board);
                    sw.WriteLine(FEN + "," + Convert.ToString(AllPositions[i].WhiteEval) + "," + Convert.ToString(AllPositions[i].BlackEval));
                }
            }
            sw.Close(); 

            MessageBox.Show("Finished writing to file");
            updateDisplay(myGlobals.board);
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
            int[,] mask = maskCol(myGlobals.board, 8);
            if (inCheck(myGlobals.board, mask, 0))
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

        //--------------------------------UI Position Evaluation Button-----------------------//

        //assumes only 1 piece has moved (no castling / en pasant)
        public int[,] flip(int[,] board)
        {
            int[,] flippedBoard = new int[8, 8];

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x] != 0)
                    {
                        flippedBoard[7 - y, x] = (board[y, x] + 8)%16;
                    }
                }
            }

            return flippedBoard;
        }
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
            int[,] mask;

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
            else if (board1[y2, x2] == board2[y1, x1] && board2[y1, x1] != 0)
            {
                startx = x2;
                starty = y2;
                finalx = x1;
                finaly = y1;
            }
            //pawn was promoted (FUNCTION ENDS IN THIS ELSE{}, sorry about the goto END)
            else
            {
               if (board2[y2, x2] != 0)
                {
                    startx = x1;
                    starty = y1;
                    finaly = y2;
                    finalx = x2;
                    Name += columns[finalx] + Convert.ToString(8 - finaly);
                }
                else
                {
                    startx = x2;
                    starty = y2;
                    finalx = x1;
                    finaly = y1;
                    Name += columns[finalx] + Convert.ToString(8 - finaly);
                }
                Name += "=";
                Name += pieceNumToString(board2[finaly, finalx]);

                col = (board2[finaly, finalx] / 8) * 8;
                mask = maskCol(board2, (col + 8) % 16);

                if (inCheck(board2, mask, (col + 8) % 16))
                {
                    Name += "+";
                }
                return Name;
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

            mask = maskCol(board2, col);

                    if (inCheck(board2, mask, (col + 8) % 16))
                    {
                        Name += "+";
                    }

            return Name;
        }
        public int displayMoves(int[,] board, bool flipB) // broken
        {
            string colour = "black";
            string nameMove = "";
            string Evaluation = "";
            int score;
            string[] names = new string[128];
            int[] scores = new int[128];
            int[] tableBaseIndex;

            if (myGlobals.ColToMove == 8)
            {
                colour = "white";
            }

            List<board> nextPositions = GenerateAllMoves(board, colour);

            for (int i = 0; i< nextPositions.Count; i++)
            {
                //updateDisplay(nextPositions[i].pos);
                nameMove = moveName(board, nextPositions[i].pos);
                if (flipB)
                {
                    tableBaseIndex = generateTBIndex(flip(nextPositions[i].pos));
                }
                else
                {
                    tableBaseIndex = generateTBIndex(nextPositions[i].pos);
                }

                if (nextPositions[i].tableIndex != -1)
                {
                    if (colour == "black")
                    {
                        score = myGlobals.Tablebase[tableBaseIndex[0], tableBaseIndex[1]].WhiteEval;
                    }
                    else
                    {
                        score = myGlobals.Tablebase[tableBaseIndex[0], tableBaseIndex[1]].BlackEval;
                    }
                }
                else
                {
                    score = -1;
                }
                names[i] = nameMove;
                if (flipB)
                {
                    scores[i] = -1*score;
                }
                else
                {
                    scores[i] = score;
                }
            }

            int tempInt;
            string tempString;

            for (int i = 0; i < nextPositions.Count-1; i++)   //bubble sort OMEGALUL
            {
                for (int j = 0; j < nextPositions.Count-1; j++)
                {
                    if (scores[j] < scores[j + 1] && myGlobals.ColToMove == 8)
                    {

                        tempInt = scores[j];
                        scores[j] = scores[j + 1];
                        scores[j + 1] = tempInt;
                        tempString = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = tempString;
                    }
                    else if (scores[j+1] < scores[j] && myGlobals.ColToMove == 0)
                    {
                        tempInt = scores[j +1];
                        scores[j+1] = scores[j];
                        scores[j] = tempInt;
                        tempString = names[j+1];
                        names[j+1] = names[j];
                        names[j] = tempString;
                    }

                }
            }

            string ToAdd;

            for (int i = 0; i < nextPositions.Count; i++)
            {
                ToAdd = "";
                switch (scores[i])
                {
                    case -1:
                        Evaluation = "Insufficient Material";
                        break;
                    case 0:
                        Evaluation = "Draw";
                        break;
                    case 1000:
                        Evaluation = "Checkmate";
                        break;
                    case -9999:
                        Evaluation = "wtf";
                        break;
                    default:
                        if (flipB)
                        {
                            Evaluation = "DTM " + Convert.ToString(1000 + scores[i]);
                        }
                        else
                        {
                            Evaluation = "DTM " + Convert.ToString(1000 - scores[i]);
                        }
                        break;
                }

                ToAdd += names[i];

                for (int j = 0; j < (6 - names[i].Length); j++)
                {
                    ToAdd += "   ";
                }

                ToAdd += Evaluation;

                lstMoveEvals.Items.Add(ToAdd);
            }

            return 0;
        }

        private void btnEvaluate_Click(object sender, EventArgs e)   //not broken apart from display moves
        {
            bool flipBoard = false;
            int[] tableBaseIndex;
            int Eval;
            int[,] board = copyPos(myGlobals.board);
            int[,] flippedBoard = flip(board);

            for (int y = 0; y <8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y,x] < 8 && board[y,x] != 0 && board[y, x]!=6)
                    {
                        flipBoard = true;
                    }
                }
            }

            if (flipBoard)
            {
                tableBaseIndex = generateTBIndex(flippedBoard);
            }
            else
            {
                tableBaseIndex = generateTBIndex(board);
            }

            lstMoveEvals.Items.Clear();

            if (tableBaseIndex[1] != -1)
            {
                if (myGlobals.Tablebase[tableBaseIndex[0], tableBaseIndex[1]].WhiteEval != 9999 || myGlobals.Tablebase[tableBaseIndex[0], tableBaseIndex[1]].BlackEval != 9999)
                {
                    lblWhiteEval.Text = Convert.ToString(myGlobals.Tablebase[tableBaseIndex[0], tableBaseIndex[1]].WhiteEval);
                    lblBlackEval.Text = Convert.ToString(myGlobals.Tablebase[tableBaseIndex[0], tableBaseIndex[1]].BlackEval);
                    if ((myGlobals.ColToMove == 8 && !flipBoard) || (myGlobals.ColToMove == 0 && flipBoard))
                    {
                        Eval = myGlobals.Tablebase[tableBaseIndex[0], tableBaseIndex[1]].WhiteEval;
                    }
                    else
                    {
                        Eval = myGlobals.Tablebase[tableBaseIndex[0], tableBaseIndex[1]].BlackEval;
                    }
                }
                else
                {
                    Eval = 9999;
                }


                switch (Eval)
                {
                    case 0:
                        lblEval.Text = "Draw";
                        displayMoves(myGlobals.board, flipBoard);
                        break;
                    case 1000:
                        if (flipBoard)
                        {
                            lblEval.Text = "Black won by checkmate";

                        }
                        else
                        {
                            lblEval.Text = "White won by checkmate";
                        }
                        break;
                    case 9999:
                    case -9999:
                        lblEval.Text = "Invalid position";
                        break;
                    default:
                        if (!flipBoard)
                        {
                            lblEval.Text = "White is winning:    DTM " + Convert.ToString(1000 - Eval);
                        }
                        else
                        {
                            lblEval.Text = "Black is winning:    DTM " + Convert.ToString(1000 - Eval);
                        }
                        displayMoves(myGlobals.board, flipBoard);
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

        private void btnFlip_Click(object sender, EventArgs e)
        {
            updateDisplay(flip(myGlobals.board));
        }
    }
    }
    
    