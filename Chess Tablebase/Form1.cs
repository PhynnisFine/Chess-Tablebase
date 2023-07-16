namespace Chess_Tablebase
{
    public partial class frmBoard : Form
    {

        public static class myGlobals
        {
            public static Button[,] buttonArray = new Button[8, 8];
            public static int[,] board = new int[8, 8];
            public static int selectedPiece = 0;
            public static int ColToMove;
        }

        public frmBoard()
        {
            InitializeComponent();
        }


        private void frmBoard_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    myGlobals.board[i, j] = 0;
                }
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
            return 0;
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
            myGlobals.ColToMove = 1;
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

        private void btnGenPos_Click(object sender, EventArgs e)
        {
            string TableName;
            int[] Pieces = new int[4];     //MUST BE UPDATED IF 5 PIECE TABLEBASES ARE GENERATED
            int piecesIndex = 0;

            TableName = txtTableName.Text;
            Pieces[0] = 14;  //add white king
            Pieces[1] = 6;   //add black king
            piecesIndex = 2;

            for (int i = 0; i < TableName.Length; i++)
            {

            }
        }
    }
    }
    