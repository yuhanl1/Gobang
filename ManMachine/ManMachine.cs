using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace ManMachine
{//Written by LiuYuhan, 20150502-201505.
    public partial class ManMachine : Form
    {
        //Define public variable and constant.
        #region defineVariable
        //Define Chess Pieces.
        public const int White = 0;
        public const int Black = 1;
        public const int Empty = 2;
        public const int lengthOFTree = 2000000;
        #region chessboard
        //Define Chess Board.
        public int[,] chessBoard = new int[15, 15] { 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }, 
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 } };
        #endregion
        //Define Human/Machine Color.manColor is the Human Color.
        public int manColor = Black;
        //Define the number of procedures.
        public int totalProcedure = 0;
        //Define the depth of the treeview.
        public int depth = 0;
        //Define a chess board to store the result.
        public int[,] resultchessBoard = new int[15, 15];
        //Define a chess board to store the temp data.
        public int[,] tempchessBoard = new int[15, 15];
        //Define an array contains chessBoards to record every step.
        public int[ , ,] everystep = new int[225,15,15];
        //Define tree node of tree structure.
        public struct TreeNode
        {
            public int[,] nodeboard;
            public int nodedepth;
            public int nodescore;
            public int parentnode;
        }
        //Define Tree.
        TreeNode[] tree = new TreeNode[lengthOFTree];
        //ArrayList tree = new ArrayList();
        //Define edged attributes of current occation.
        public int upEdge=14,newupEdge;
        public int downEdge=0,newdownEdge;
        public int leftEdge=14,newleftEdge;
        public int rightEdge=0,newrightEdge;
        //Define the evaluated score.
        public int score = 0;
        //Define WU SHOU N DA number of chess pieces.
        public int fifthpiecenumber = 0;
        public int kaiju = 0;
        public int blackx41, blacky41, blackx42, blacky42, blackx43, blacky43, blackx44, blacky44;
        public int countnidaye;
        #endregion
        public ManMachine()
        {
            InitializeComponent();
        }
        #region UIDesign
        //Draw the initial board.
        public void drawBoard()
        {
            Graphics g = Board.CreateGraphics();
            //Draw vertical Line.
            for (int i = 0; i < 15; i++ )
                g.DrawLine(new Pen(Color.Black, 2), 12 + 34 * i, 12, 12 + 34 * i, 488);
            //Draw horizonal Line.
            for (int j = 0; j < 15; j++)
                g.DrawLine(new Pen(Color.Black, 2), 12, 12 + 34 * j, 488, 12 + 34 * j);
            //Draw TIAN YUAN on the center of the board.
            g.DrawArc(new Pen(Color.Black,4), new Rectangle(7 * 34 + 12-3, 7 * 34 + 12-3, 6, 6), 0, 360);
            //Draw XING on the side of the board.
            g.DrawArc(new Pen(Color.Black, 3), new Rectangle(3 * 34 + 12 - 2, 3 * 34 + 12 - 2, 4, 4), 0, 360);
            g.DrawArc(new Pen(Color.Black, 3), new Rectangle(3 * 34 + 12 - 2, 11 * 34 + 12 - 2, 4, 4), 0, 360);
            g.DrawArc(new Pen(Color.Black, 3), new Rectangle(11 * 34 + 12 - 2, 3 * 34 + 12 - 2, 4, 4), 0, 360);
            g.DrawArc(new Pen(Color.Black, 3), new Rectangle(11 * 34 + 12 - 2, 11 * 34 + 12 - 2, 4, 4), 0, 360);
        }
        private void buttonBegin_Click(object sender, EventArgs e)
        {
            Board.Update();
            pictureBoxMan.Update();
            pictureBoxComputer.Update();
            drawBoard();
            System.Drawing.SolidBrush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.WhiteSmoke);
            System.Drawing.SolidBrush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            Graphics m = pictureBoxMan.CreateGraphics();
            Graphics n = pictureBoxComputer.CreateGraphics();
            copyChessToEverystep(0);
            if (manColor == White)
            {
                chessBoard[7, 7] = Black;//First Black Piece must be at TIAN YUAN.
                m.FillEllipse(whiteBrush, new Rectangle(0, 0, 30, 30));
                n.FillEllipse(blackBrush, new Rectangle(0, 0, 30, 30));
                totalProcedure++;
                copyChessToEverystep(totalProcedure);
                machineGo();
                //MessageBox.Show("chessBoard");
            }
            else if (manColor == Black)
            {
                chessBoard[7, 7] = Empty;
                m.FillEllipse(blackBrush, new Rectangle(0, 0, 30, 30));
                n.FillEllipse(whiteBrush, new Rectangle(0, 0, 30, 30));
            }
            drawChessPieces();
        }
        //According to Array chessBoard to Draw the Chess Pieces on the board.
        public void drawChessPieces()
        {
            Graphics g = Board.CreateGraphics();
            g.Clear(this.BackColor);
            drawBoard();
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    if (chessBoard[i, j] == White)
                        drawWhiteChessPiece(i, j);
                    else if (chessBoard[i, j] == Black)
                        drawBlackChessPiece(i, j);
        }
        //Draw black and white Chess Piece.
        public void drawBlackChessPiece(int x,int y)
        {
            Graphics g = Board.CreateGraphics();
            System.Drawing.SolidBrush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            g.FillEllipse(blackBrush, new Rectangle(x * 34 + 12 - 12, y * 34 + 12 - 12, 24, 24));
        }
        public void drawWhiteChessPiece(int x, int y)
        {
            Graphics g = Board.CreateGraphics();
            System.Drawing.SolidBrush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.WhiteSmoke);
            g.FillEllipse(whiteBrush, new Rectangle(x * 34 + 12 - 12, y * 34 + 12 - 12, 24, 24));
        }
        #endregion
        #region REN JI JIAO HU
        //Jugde user's manipulation. When (first color = black && totalProcedure = even || first color = white && totalprocedure = odd) We can go.
        private void Board_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("X: " + MousePosition.X + "  Y: " + MousePosition.Y);
            //if (totalProcedure != 4 && totalProcedure >= 0)
            //{
                if (manColor == Black)
                {
                    for (int i = 0; i < 15; i++)
                        for (int j = 0; j < 15; j++)
                            if (MousePosition.X >= 34 * i + 510 - 20 && MousePosition.X <= 34 * i + 510 + 20)
                                if (MousePosition.Y >= 34 * j + 160 - 20 && MousePosition.Y <= 34 * j + 160 + 20)
                                    if (totalProcedure % 2 == 0 || totalProcedure == 1)
                                    {
                                        if (totalProcedure == 1)
                                        {
                                            chessBoard[i, j] = White;
                                            totalProcedure++;
                                        }
                                        else
                                        {
                                            chessBoard[i, j] = Black;
                                            totalProcedure = totalProcedure + 1;
                                        }

                                    }
                    copyChessToEverystep(totalProcedure);

                }
                else if (manColor == White)
                {
                    for (int i = 0; i < 15; i++)
                        for (int j = 0; j < 15; j++)
                            if (MousePosition.X >= 34 * i + 510 - 20 && MousePosition.X <= 34 * i + 510 + 20)
                                if (MousePosition.Y >= 34 * j + 160 - 20 && MousePosition.Y <= 34 * j + 160 + 20)
                                    if (totalProcedure % 2 == 1) //&& totalProcedure >= 3)
                                    {
                                        chessBoard[i, j] = White;
                                        totalProcedure++;
                                    }
                    copyChessToEverystep(totalProcedure);
                }
            //}
            /*
            else if(totalProcedure == 4 || totalProcedure < 0)//这时候黑棋应下一开始规定的棋子数，白棋选择其一留下。黑棋所下应在天元附近10×10方格内，且N打不同形。
            {
                if(manColor == Black)//White Chess Pieces is charged for changing totalprocedures!
                {
                    int firstx=0,firsty=0,secondx=0,secondy=0,count=1;
                    double distance1=0, distance2=0, distance3=0, distance4=0;
                    for (int i = 2; i < 13; i++)//First circulate, get existed black point. In order to calculate distance between old and new point.
                        for (int j = 2; j < 13; j++)
                        {
                            if(chessBoard[i,j] == Black && count == 1)
                            {
                                firstx = i;//Get first black Point.
                                firsty = j;
                                count++;//count = 2.
                            }
                            if(chessBoard[i,j] == Black && count == 2)
                            {
                                secondx = i;//Get second black Point.
                                secondy = j;
                                count++;//count = 3.
                            }
                        }
                    //If WU SHOU 2 DA, count = 2.
                    for (int i = 0; i < 15; i++)//Second circulate, draw points if they meet the demand.
                        for (int j = 0; j < 15; j++)
                            if (MousePosition.X >= 34 * i + 510 - 20 && MousePosition.X <= 34 * i + 510 + 20)
                                if (MousePosition.Y >= 34 * j + 160 - 20 && MousePosition.Y <= 34 * j + 160 + 20)
                                { 
                                    if(countnidaye == fifthpiecenumber)//When draw the first point, we need to calculate the distance between this point and old point.
                                    {
                                        chessBoard[i, j] = Black;
                                        distance1 = Math.Sqrt(Math.Pow(i - firstx, 2) + Math.Pow(j - firsty, 2));
                                        distance2 = Math.Sqrt(Math.Pow(i - secondx, 2) + Math.Pow(j - secondy, 2));
                                        blackx41 = i;
                                        blacky41 = j;
                                        countnidaye--;
                                        totalProcedure *= -1;
                                        //MessageBox.Show("" + totalProcedure+" "+count);
                                        drawChessPieces();
                                    }
                                    else if(countnidaye > 0 && countnidaye != fifthpiecenumber)
                                    {
                                        distance3 = Math.Sqrt(Math.Pow(i - firstx, 2) + Math.Pow(j - firsty, 2));
                                        distance4 = Math.Sqrt(Math.Pow(i - secondx, 2) + Math.Pow(j - secondy, 2));
                                        //MessageBox.Show("tester");
                                        if (distance3 == distance1 || distance3 == distance2 || distance4 == distance1 || distance4 == distance2)
                                        {
                                            MessageBox.Show("落子位置请勿出现同形棋!");
                                        }
                                        
                                        else
                                        {
                                            chessBoard[i, j] = Black;//Change the state.
                                            totalProcedure *= -1;
                                            //MessageBox.Show("" + totalProcedure);
                                            if(countnidaye == fifthpiecenumber - 1)
                                            {
                                                blackx42 = i;
                                                blacky42 = j;
                                                //MessageBox.Show("zheliblackx41: " + blackx41 + "blacky41: " + blacky41 + "blackx42: " + blackx42 + "blacky42: " + blacky42);
                                            }
                                            else if (countnidaye == fifthpiecenumber - 2)
                                            {
                                                blackx43 = i;
                                                blacky43 = j;
                                            }
                                            else if (countnidaye == fifthpiecenumber - 3)
                                            {
                                                blackx44 = i;
                                                blacky44 = j;
                                            }
                                            countnidaye--;
                                            //MessageBox.Show("blackx41: " + blackx41 + "blacky41: " + blacky41 + "blackx42: " + blackx42 + "blacky42: " + blacky42);
                                            drawChessPieces();
                                        }
                                    }
                                }
                }
                else if(manColor == White)
                {
                    //Firstly find third step's Black Chess Piece Point.
                    int twox = 0, twoy = 0, threex = 0,threey = 0,fourx = 0, foury = 0, fivex=0,fivey=0,sixx=0,sixy=0;
                    for (int i = 0; i < 15; i++)
                         for (int j = 0; j < 15; j++)
                         {
                             if(everystep[3,i,j] == Black)
                                 if (i == j && i == 7) { }
                                 else
                                 {
                                     twox = i;
                                     twoy = j;
                                 }
                         }
                    if(fifthpiecenumber == 2)
                    {
                        int count1 = 2;//Find two optional points.
                        for (int i = 0; i < 15; i++)
                         for (int j = 0; j < 15; j++)
                         {
                             if (chessBoard[i, j] == Black)
                             {
                                 if (i == 7 && j == 7) { }
                                 else if (i == twox && j == twoy) { }
                                 else if(count1 == 2)
                                 {
                                     threex = i;
                                     threey = j;
                                     count1--;
                                 }
                                 else if(count1 == 1)
                                 {
                                     fourx = i;
                                     foury = j;
                                 }
                             }
                         }
                        //Choose a Black Piece to stay and delete others.
                        if (MousePosition.X >= 34 * threex + 510 - 20 && MousePosition.X <= 34 * threex + 510 + 20 && MousePosition.Y >= 34 * threey + 160 - 20 && MousePosition.Y <= 34 * threey + 160 + 20)
                            {
                                chessBoard[fourx, foury] = Empty;
                            }
                        else if (MousePosition.X >= 34 * fourx + 510 - 20 && MousePosition.X <= 34 * fourx + 510 + 20 &&MousePosition.Y >= 34 * foury + 160 - 20 && MousePosition.Y <= 34 * foury + 160 + 20)
                            {
                                chessBoard[threex, threey] = Empty;
                            }
                        //Change the steps.
                        drawChessPieces();
                        totalProcedure++;
                        copyChessToEverystep(totalProcedure);
                    }
                    else if(fifthpiecenumber == 4)
                    {
                        int count2 = 4;//Find four optional points.
                        for (int i = 0; i < 15; i++)
                            for (int j = 0; j < 15; j++)
                            {
                                if (chessBoard[i, j] == Black)
                                {
                                    if (i == 7 && j == 7) { }
                                    else if (i == twox && j == twoy) { }
                                    else if (count2 == 4)
                                    {
                                        threex = i;
                                        threey = j;
                                        count2--;
                                    }
                                    else if (count2 == 3)
                                    {
                                        fourx = i;
                                        foury = j;
                                        count2--;
                                    }
                                    else if (count2 == 2)
                                    {
                                        fivex = i;
                                        fivey = j;
                                        count2--;
                                    }
                                    else if (count2 == 1)
                                    {
                                        sixx = i;
                                        sixy = j;
                                    }
                                }
                            }
                        //Choose a Black Piece to stay and delete others.
                        if (MousePosition.X >= 34 * threex + 510 - 20 && MousePosition.X <= 34 * threex + 510 + 20 && MousePosition.Y >= 34 * threey + 160 - 20 && MousePosition.Y <= 34 * threey + 160 + 20)
                            {
                                chessBoard[fourx, foury] = Empty;
                                chessBoard[fivex, fivey] = Empty;
                                chessBoard[sixx, sixy] = Empty;
                            }
                        else if (MousePosition.X >= 34 * fourx + 510 - 20 && MousePosition.X <= 34 * fourx + 510 + 20 && MousePosition.Y >= 34 * foury + 160 - 20 && MousePosition.Y <= 34 * foury + 160 + 20)
                            {
                                chessBoard[threex, threey] = Empty;
                                chessBoard[fivex, fivey] = Empty;
                                chessBoard[sixx, sixy] = Empty;
                            }
                        else if (MousePosition.X >= 34 * fivex + 510 - 20 && MousePosition.X <= 34 * fivex + 510 + 20 && MousePosition.Y >= 34 * fivey + 160 - 20 && MousePosition.Y <= 34 * fivey + 160 + 20)
                            {
                                chessBoard[threex, threey] = Empty;
                                chessBoard[fourx, foury] = Empty;
                                chessBoard[sixx, sixy] = Empty;
                            }
                        else if (MousePosition.X >= 34 * sixx + 510 - 20 && MousePosition.X <= 34 * sixx + 510 + 20 && MousePosition.Y >= 34 * sixy + 160 - 20 && MousePosition.Y <= 34 * sixy + 160 + 20)
                            {
                                chessBoard[threex, threey] = Empty;
                                chessBoard[fourx, foury] = Empty;
                                chessBoard[fivex, fivey] = Empty;
                            }
                        //Change the steps.
                        drawChessPieces();
                        totalProcedure++;
                        copyChessToEverystep(totalProcedure);
                    }
                }

            }
             */
            drawChessPieces();
            if (totalProcedure == 225)
            {
                MessageBox.Show("您已和棋!");
            }
            machineGo();
        }
        //Through calculation, Computer Move to Next Step.
        private void machineGo()
        {
            
            //If the steps <= 2, then only calculate one more step.
            if(manColor == White)
            {
                int WFx=6, WFy=6, BSx=6, BSy=7;
                /*if(totalProcedure == 1)
                {
                    #region position of white1
                    if (kaiju < 13 && kaiju >= 0)
                        WFx = 7;
                    else
                        WFx = 8;
                    #endregion
                    chessBoard[WFx, WFy] = White;
                    totalProcedure++;
                    copyChessToEverystep(totalProcedure);
                    //drawChessPieces();
                    if (totalProcedure == 2)
                    {
                        #region position of black3
                        if(kaiju == 0)
                        {
                            BSx = 9;
                            BSy = 6;
                        }
                        else if(kaiju  == 1)
                        {
                            BSx = 7;
                            BSy = 9;
                        }
                        else if(kaiju  == 2)
                        {
                            BSx = 8;
                            BSy = 7;
                        }
                        else if(kaiju  == 3)
                        {
                            BSx = 9;
                            BSy = 5;
                        }
                         else if(kaiju  == 4)
                        {
                            BSx = 7;
                            BSy = 8;
                        }
                         else if(kaiju  == 5)
                        {
                            BSx = 9;
                            BSy = 8;
                        }
                         else if(kaiju  == 6)
                        {
                            BSx = 8;
                            BSy = 6;
                        }
                         else if(kaiju  == 7)
                        {
                            BSx = 7;
                            BSy = 5;
                        }
                         else if(kaiju  == 8)
                        {
                            BSx = 8;
                            BSy = 8;
                        }
                         else if(kaiju  == 9)
                        {
                            BSx = 8;
                            BSy = 9;
                        }
                         else if(kaiju  == 10)
                        {
                            BSx = 9;
                            BSy = 7;
                        }
                         else if(kaiju  == 11)
                        {
                            BSx = 8;
                            BSy = 5;
                        }
                         else if(kaiju  == 12)
                        {
                            BSx = 9;
                            BSy = 9;
                        }
                         else if(kaiju  == 13)
                        {
                            BSx = 6;
                            BSy = 8;
                        }
                         else if(kaiju  == 14)
                        {
                            BSx = 6;
                            BSy = 9;
                        }
                         else if(kaiju  == 15)
                        {
                            BSx = 9;
                            BSy = 7;
                        }
                         else if(kaiju  == 16)
                        {
                            BSx = 8;
                            BSy = 9;
                        }
                         else if(kaiju  == 17)
                        {
                            BSx = 7;
                            BSy = 9;
                        }
                         else if(kaiju  == 18)
                        {
                            BSx = 9;
                            BSy = 6;
                        }
                         else if(kaiju  == 19)
                        {
                            BSx = 9;
                            BSy = 5;
                        }
                         else if(kaiju  == 20)
                        {
                            BSx = 8;
                            BSy = 8;
                        }
                        else if(kaiju  == 21)
                        {
                            BSx = 8;
                            BSy = 7;
                        }
                        else if(kaiju  == 22)
                        {
                            BSx = 9;
                            BSy = 8;
                        }
                        else if(kaiju  == 23)
                        {
                            BSx = 7;
                            BSy = 8;
                        }
                        else if(kaiju  == 24)
                        {
                            BSx = 9;
                            BSy = 9;
                        }
                        else if(kaiju  == 25)
                        {
                            BSx = 5;
                            BSy = 9;
                        }
                        #endregion
                        chessBoard[BSx, BSy] = Black;
                        totalProcedure++;
                        copyChessToEverystep(totalProcedure);
                        drawChessPieces();
                    }
                }*/
                //if (totalProcedure >= 4 &&)
                    if(totalProcedure % 2 == 0)//偶数的时候走
                {//未写 若新棋子在原棋形外5格外 不考虑的情况
                    /*if (totalProcedure == 4)//走一层
                    {
                        findEgde();
                        //MessageBox.Show("board" + newupEdge + newdownEdge + newleftEdge + newrightEdge);
                        for (int z = 0; z < fifthpiecenumber; z++)//走不能走的点？？？
                        {
                            CreateSerchingTreeBlack();
                            copyFromTmptoChessBoard();
                            drawChessPieces();
                            findEgde();
                        }
                    }
                    if (totalProcedure > 4)//走两层
                    {*/
                        findEgde();
                        CreateSerchingTreeBlack();
                        copyFromTmptoChessBoard();
                        drawChessPieces();
                        totalProcedure++;
                        copyChessToEverystep(totalProcedure);
                   // }
                }
            }
            else if(manColor == Black)
            {
                /*if(totalProcedure == 3)//走一层
                {
                    findEgde();
                    CreateSerchingTreeWhite();
                    copyFromTmptoChessBoard();
                    drawChessPieces();
                    totalProcedure++;
                    //MessageBox.Show("" + totalProcedure);
                    copyChessToEverystep(totalProcedure);
                }
                else if(totalProcedure == 4)//留一子
                {
                    //MessageBox.Show("0000000");
                    if(fifthpiecenumber == 2)
                    {
                        int a = 0, b = 0;
                        chessBoard[blackx41, blacky41] = Empty;
                        findEgde();
                        a = estimateScore();
                        chessBoard[blackx41, blacky41] = Black;
                        chessBoard[blackx42, blacky42] = Empty;
                        b = estimateScore();
                        
                        if(a<b)
                        {
                            chessBoard[blackx41, blacky41] = Empty;
                            chessBoard[blackx42, blacky42] = Black;
                            drawChessPieces();
                            totalProcedure++;
                            copyChessToEverystep(totalProcedure);
                        }
                        else
                        {
                            //MessageBox.Show("a: " + a + "b: " + b);
                            //MessageBox.Show("blackx41: " + blackx41 + "blacky41: " + blacky41 + "blackx42: " + blackx42 + "blacky42: "+blacky42);
                            chessBoard[blackx41, blacky41] = Black;
                            chessBoard[blackx42, blacky42] = Empty;
                            drawChessPieces();
                            totalProcedure++;
                            copyChessToEverystep(totalProcedure);
                        }
                    }
                    else
                    {
                        int a = 0, b = 0, c = 0, d = 0;
                        chessBoard[blackx41, blacky41] = Black;
                        chessBoard[blackx42, blacky42] = Empty;
                        chessBoard[blackx43, blacky43] = Empty;
                        chessBoard[blackx44, blacky44] = Empty;
                        findEgde();
                        a = estimateScore();
                        chessBoard[blackx41, blacky41] = Empty;
                        chessBoard[blackx42, blacky42] = Black;
                        chessBoard[blackx43, blacky43] = Empty;
                        chessBoard[blackx44, blacky44] = Empty;
                        findEgde();
                        b = estimateScore();
                        chessBoard[blackx41, blacky41] = Empty;
                        chessBoard[blackx42, blacky42] = Empty;
                        chessBoard[blackx43, blacky43] = Black;
                        chessBoard[blackx44, blacky44] = Empty;
                        findEgde();
                        c = estimateScore();
                        chessBoard[blackx41, blacky41] = Empty;
                        chessBoard[blackx42, blacky42] = Empty;
                        chessBoard[blackx43, blacky43] = Empty;
                        chessBoard[blackx44, blacky44] = Black;
                        findEgde();
                        d = estimateScore();
                        if (a < b && a < c && a < d)
                        {
                            chessBoard[blackx41, blacky41] = Black;
                            chessBoard[blackx42, blacky42] = Empty;
                            chessBoard[blackx43, blacky43] = Empty;
                            chessBoard[blackx44, blacky44] = Empty;
                        }
                        else if (b < a && b < c && b < d)
                        {
                            chessBoard[blackx41, blacky41] = Empty;
                            chessBoard[blackx42, blacky42] = Black;
                            chessBoard[blackx43, blacky43] = Empty;
                            chessBoard[blackx44, blacky44] = Empty;
                        }
                        else if (c < a && c < b && c < d)
                        {
                            chessBoard[blackx41, blacky41] = Empty;
                            chessBoard[blackx42, blacky42] = Empty;
                            chessBoard[blackx43, blacky43] = Black;
                            chessBoard[blackx44, blacky44] = Empty;
                        }
                        else
                        {
                            chessBoard[blackx41, blacky41] = Empty;
                            chessBoard[blackx42, blacky42] = Empty;
                            chessBoard[blackx43, blacky43] = Empty;
                            chessBoard[blackx44, blacky44] = Black;
                        }
                        drawChessPieces();
                        totalProcedure++;
                        copyChessToEverystep(totalProcedure);
                    }
                }
                else 
                 */
                //if(totalProcedure >= 5 && 
                if(totalProcedure % 2 == 1)//奇数的时候走，走两层
                {
                    findEgde();
                    CreateSerchingTreeWhite();
                    copyFromTmptoChessBoard();
                    drawChessPieces();
                    totalProcedure++;
                    copyChessToEverystep(totalProcedure);
                }
            }
        }
        //Find egdes of chessboard and assign tempchessboard.
        #endregion
        #region action
        //RadioButton Action1.
        private void radioButton2子_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2子.Checked)
            {
                fifthpiecenumber = 2;
                countnidaye = fifthpiecenumber;
            }
        }
        private void radioButton4子_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4子.Checked)
            {
                fifthpiecenumber = 4;
                countnidaye = fifthpiecenumber;
            }
        }
        //RadioButton Action2.
        private void radioButton黑子_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton黑子.Checked)
                manColor = Black;
        }
        private void radioButton白子_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton白子.Checked)
                manColor = White;
        }
        //Button Action.
        private void button三手交换_Click(object sender, EventArgs e)
        {
            if (totalProcedure == 3 && manColor == White)
            {
                manColor = Black;
                machineGo();//machine作为白棋继续走，此时totalProcedure = 3.
            }
        }
        private void button悔棋_Click(object sender, EventArgs e)
        {
            if (totalProcedure >= 1)
            {
                copyEverystepToChess(totalProcedure - 1);
                //MessageBox.Show("chessBoard:" + chessBoard[6, 6] + chessBoard[6, 7] + chessBoard[6, 8] + chessBoard[7, 6] + chessBoard[7, 7] + chessBoard[7, 8]);
                drawChessPieces();
                totalProcedure--;
            }
        }
        //Copy array.
        public void copyChessToEverystep(int step)
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    everystep[step, i, j] = chessBoard[i, j];
        }
        public void copyEverystepToChess(int step)
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    chessBoard[i, j] = everystep[step, i, j];
        }

        #endregion
        
        private void findEgde()
        {
            upEdge = 14;
            downEdge = 0;
            leftEdge = 14;
            rightEdge = 0;
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                {
                    tempchessBoard[i, j] = chessBoard[i, j];
                    if (chessBoard[i, j] != Empty)//up=left=14,down=right=0.
                    {
                        if (j < upEdge)
                        {
                            upEdge = j;
                        }
                        if (i < leftEdge)
                        {
                            leftEdge = i;
                        }
                        if (j > downEdge)
                        {
                            downEdge = j;
                        }
                        if (i > rightEdge)
                        {
                            rightEdge = i;
                        }
                    }
                }
            
            if (upEdge >= 2)
                newupEdge = upEdge - 2;
            else if (upEdge == 1)
                newupEdge = upEdge - 1;
            if (downEdge <= 12)
                newdownEdge = downEdge + 2;
            else if (downEdge == 13)
                newdownEdge = downEdge + 1;
            if (leftEdge >= 2)
                newleftEdge = leftEdge - 2;
            else if (leftEdge == 1)
                newleftEdge = leftEdge - 1;
            if (rightEdge <= 12)
                newrightEdge = rightEdge + 2;
            else if (rightEdge == 13)
                newrightEdge = rightEdge + 1;
            
            //if (upEdge >= 1)
              //  newupEdge = upEdge - 1;
            //if (downEdge <= 13)
              //  newdownEdge = downEdge + 1;
            //if (leftEdge >= 1)
              //  newleftEdge = leftEdge - 1;
            //if (rightEdge <= 13)
              //  newrightEdge = rightEdge + 1;
            
        }
        //Find egdes of tempchessboard.
        private void findTmpEgde()
        {
            upEdge = 14;
            downEdge = 0;
            leftEdge = 14;
            rightEdge = 0;
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                {
                    if (tempchessBoard[i, j] != Empty)//up=left=14,down=right=0.
                    {
                        if (j < upEdge)
                        {
                            upEdge = j;
                        }
                        if (i < leftEdge)
                        {
                            leftEdge = i;
                        }
                        if (j > downEdge)
                        {
                            downEdge = j;
                        }
                        if (i > rightEdge)
                        {
                            rightEdge = i;
                        }
                    }
                }
            if (upEdge >= 2)
                newupEdge = upEdge - 2;
            else if (upEdge == 1)
                newupEdge = upEdge - 1;
            if (downEdge <= 12)
                newdownEdge = downEdge + 2;
            else if (downEdge == 13)
                newdownEdge = downEdge + 1;
            if (leftEdge >= 2)
                newleftEdge = leftEdge - 2;
            else if (leftEdge == 1)
                newleftEdge = leftEdge - 1;
            if (rightEdge <= 12)
                newrightEdge = rightEdge + 2;
            else if (rightEdge == 13)
                newrightEdge = rightEdge + 1;
            /*if (upEdge >= 1)
                newupEdge = upEdge - 1;
            if (downEdge <= 13)
                newdownEdge = downEdge + 1;
            if (leftEdge >= 1)
                newleftEdge = leftEdge - 1;
            if (rightEdge <= 13)
                newrightEdge = rightEdge + 1;
             * */
        }       

        //Calculate the score of temperate chessboard.
        public int estimateScore()
        {
            int estimate = 0;
            ConsoleApplication2.Program pr = new ConsoleApplication2.Program();
            for (int m = 0; m < 15; m++)
                for (int n = 0; n < 15; n++)
                    pr.tempchessBoard[m, n] = tempchessBoard[m, n];
            estimate = pr.GuZhi(newupEdge,newdownEdge,newleftEdge,newrightEdge);
            return estimate;
        }

        public int estimateScoreWhite()
        {
            int estimate = 0;
            ConsoleApplication2.Program pr = new ConsoleApplication2.Program();
            for (int m = 0; m < 15; m++)
                for (int n = 0; n < 15; n++)
                    pr.tempchessBoard[m, n] = tempchessBoard[m, n];
            estimate = pr.GuZhiWhite(newupEdge, newdownEdge, newleftEdge, newrightEdge);
            return estimate;
        }
        //temp棋盘中的内容、传递来的父亲节点，分数和深度插入到树的末尾。
        public void addToEndofTree(int index, int nparent,int nscore,int ndepth)
        {
            tree[index].nodeboard = new int[15,15];
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    tree[index].nodeboard[i, j] = tempchessBoard[i, j];
            tree[index].parentnode = nparent;
            //tree[index].nodescore = nscore;
            //tree[index].nodedepth = ndepth;
        }
        public void updateScore(int index, int nscore)
        {
            tree[index].nodescore = nscore;
        }
        public void copyFromTreetoTmp(int index)
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    tempchessBoard[i, j] = tree[index].nodeboard[i, j];
        }
        public void copyFromTmptoChessBoard()
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    chessBoard[i, j] = tempchessBoard[i, j];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "残月局")
                kaiju = 0;
            else if (comboBox1.Text == "瑞星局")
                kaiju = 1;
            else if (comboBox1.Text == "雨月局")
                kaiju = 2;
            else if (comboBox1.Text == "疏星局")
                kaiju = 3;
            else if (comboBox1.Text == "松月局")
                kaiju = 4;
            else if (comboBox1.Text == "新月局")
                kaiju = 5;
            else if (comboBox1.Text == "花月局")
                kaiju = 6;
            else if (comboBox1.Text == "寒星局")
                kaiju = 7;
            else if (comboBox1.Text == "丘月局")
                kaiju = 8;
            else if (comboBox1.Text == "山月局")
                kaiju = 9;
            else if (comboBox1.Text == "金星局")
                kaiju = 10;
            else if (comboBox1.Text == "溪月局")
                kaiju = 11;
            else if (comboBox1.Text == "游星局")
                kaiju = 12;
            else if (comboBox1.Text == "斜月局")
                kaiju = 13;
            else if (comboBox1.Text == "名月局")
                kaiju = 14;
            else if (comboBox1.Text == "恒星局")
                kaiju = 15;
            else if (comboBox1.Text == "岚月局")
                kaiju = 16;
            else if (comboBox1.Text == "明星局")
                kaiju = 17;
            else if (comboBox1.Text == "峡月局")
                kaiju = 18;
            else if (comboBox1.Text == "长星局")
                kaiju = 19;
            else if (comboBox1.Text == "浦月局")
                kaiju = 20;
            else if (comboBox1.Text == "云月局")
                kaiju = 21;
            else if (comboBox1.Text == "水月局")
                kaiju = 22;
            else if (comboBox1.Text == "银月局")
                kaiju = 23;
            else if (comboBox1.Text == "流星局")
                kaiju = 24;
            else if (comboBox1.Text == "彗星局")
                kaiju = 25;
        }
        /*public int getNumbersOfThirdDepth(int parent)
        {
            int countnumber=0;
            for (int i = 0; i < lengthOFTree; i++)
                if (tree[i].nodedepth == 3 && tree[i].parentnode == parent)
                    countnumber++;
            return countnumber;
        }*/


        private void CreateSerchingTreeBlack()//Try to pass from center to edge! Tree structure. Need Depth. Alpha-Beta Cut.
        {
            //往上下左右最多距离中心7的偏差值
            int leftmin1 = (newrightEdge - newleftEdge) / 2;
            int rightmin1 = newrightEdge - newleftEdge - leftmin1;
            int upmin1 = (newdownEdge - newupEdge) / 2;
            int downmin1 = newdownEdge - newupEdge - upmin1;
            //MessageBox.Show("board " + newleftEdge + " " + newrightEdge + " " + newupEdge + " " + newdownEdge);
            int up1 = 1, down1 = 1, left1 = 1, right1 = 1;
            int up2 = 1, down2 = 1, left2 = 1, right2 = 1;
            int up3 = 1, down3 = 1, left3 = 1, right3 = 1;
            int up4 = 1, down4 = 1, left4 = 1, right4 = 1;
            score = 0;

            int index = 0;//往树里插入的时候的节点序号
            int indexWEWANT = 1;//我们要的那个第一层节点存在树中的序号

            int changescore = 100000, changescore1, changescore2 = 0, changescore3 = 100000;//参考分数值
            int tag1 = 0, tag2 = 0, tag3 = 0;//跳出两层循环的标志
            int count = 0, count1 = 0, count2 = 0, count3 = 0, number = 0;//计算一个节点下面加了多少个节点 count是根下的，count1是第一层下面的，以此类推
            addToEndofTree(index, -1, 0, 0);//index=0
            index++;//插入后加1  //index=1
            #region 1st floor
            do
            {
                if (left1 > leftmin1)
                    left1 = leftmin1;
                if (right1 > rightmin1)
                    right1 = rightmin1;
                if (up1 > upmin1)
                    up1 = upmin1;
                if (down1 > downmin1)
                    down1 = downmin1;
                for (int i = 7 - left1; i <= 7 + right1; i += left1 + right1)
                {
                    for (int j = 7 - up1; j <= 7 + down1; j++)
                    {
                        copyFromTreetoTmp(0);//每次都是从根节点开始生成第一层
                        if (tempchessBoard[i, j] == Empty)
                        {
                            tempchessBoard[i, j] = Black;
                            //MessageBox.Show("" + judgeJINSHOU(i, j));
                            addToEndofTree(index, 0, 0, 1);//index=1
                            index++;//index=2
                            count++;
                            //MessageBox.Show("o" + count);
                            count1 = 0;//清零
                            number = 0;
                            //MessageBox.Show("1" + count1);
                            findTmpEgde();
                            int leftmin2 = (newrightEdge - newleftEdge) / 2;
                            int rightmin2 = newrightEdge - newleftEdge - leftmin2;
                            int upmin2 = (newdownEdge - newupEdge) / 2;
                            int downmin2 = newdownEdge - newupEdge - upmin2;
                            //MessageBox.Show("board" + leftmin2 + rightmin2 + upmin2 + downmin2);
                            //生成第二层
                            #region 2nd floor
                            do
                            {
                                //MessageBox.Show("board" + leftmin2 + rightmin2 + upmin2 + downmin2);
                                if (left2 > leftmin2)
                                    left2 = leftmin2;
                                if (right2 > rightmin2)
                                    right2 = rightmin2;
                                if (up2 > upmin2)
                                    up2 = upmin2;
                                if (down2 > downmin2)
                                    down2 = downmin2;
                                for (int m = 7 - left2; m <= 7 + right2; m += left2 + right2)
                                {
                                    if (tag3 == 1)
                                    {
                                        tag3 = 0;
                                        break;
                                    }
                                    for (int n = 7 - up2; n <= 7 + down2; n++)
                                    {
                                        copyFromTreetoTmp(index - 1 - count1);//每次都是从父节点生成子节点
                                        if (tempchessBoard[m, n] == Empty)
                                        {
                                            tempchessBoard[m, n] = White;
                                            addToEndofTree(index, index - 1 - count1, 0, 2);//index=2
                                            index++;//index=3
                                            //count++;
                                            count1++;
                                            number++;
                                            count2 = 0;//清零
                                            findTmpEgde();
                                            int leftmin3 = (newrightEdge - newleftEdge) / 2;
                                            int rightmin3 = newrightEdge - newleftEdge - leftmin3;
                                            int upmin3 = (newdownEdge - newupEdge) / 2;
                                            int downmin3 = newdownEdge - newupEdge - upmin3;
                                            //生成第三层
                                            #region 3rd floor
                                            do
                                            {

                                                if (left3 > leftmin3)
                                                    left3 = leftmin3;
                                                if (right3 > rightmin3)
                                                    right3 = rightmin3;
                                                if (up3 > upmin3)
                                                    up3 = upmin3;
                                                if (down3 > downmin3)
                                                    down3 = downmin3;
                                                for (int a = 7 - left3; a <= 7 + right3; a += left3 + right3)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b++)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = Black;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {

                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                //MessageBox.Show("tester");
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                for (int a = 7 - left3 + 1; a <= 7 + right3 - 1; a++)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b += up3 + down3)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = Black;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                left3 += 1;
                                                right3 += 1;
                                                up3 += 1;
                                                down3 += 1;
                                            } while (left3 <= leftmin3 || right3 <= rightmin3 || up3 <= upmin3 || down3 <= downmin3);
                                            #endregion
                                            //updateScore(index - 1 - count2, changescore2);//更新第二层节点(其父节点)的分数值
                                            #region alpha jianzhi
                                            if (count > 1)//生成多于一个第二层节点时要进行剪枝
                                            {//第二层新值比根节点参考值要小，就不算这波第二层了
                                                if (changescore2 < score)
                                                {
                                                    tag3 = 1;
                                                    break;
                                                }
                                            }
                                            #endregion
                                            if (number == 1)//生成第一个第二层节点时，记录参考分数，小于它再更改
                                            {
                                                //MessageBox.Show("tester");
                                                changescore3 = changescore2;
                                            }//changescore3 是第一层节点最终的分数值
                                            else
                                            {
                                                if (changescore2 < changescore3)//新的值小于参考值
                                                {
                                                    changescore3 = changescore2;//新的值赋给参考值
                                                }//参考值是结果
                                            }


                                        }//if (tempchessBoard[m, n] == Empty)
                                    }//for n
                                }//for m
                                for (int m = 7 - left2 + 1; m <= 7 + right2 - 1; m++)
                                {
                                    if (tag3 == 1)
                                    {
                                        tag3 = 0;
                                        break;
                                    }
                                    for (int n = 7 - up2; n <= 7 + down2; n += up2 + down2)
                                    {
                                        copyFromTreetoTmp(index - 1 - count1);//每次都是从父节点生成子节点
                                        if (tempchessBoard[m, n] == Empty)
                                        {
                                            tempchessBoard[m, n] = White;
                                            addToEndofTree(index, index - 1 - count1, 0, 2);//index=2
                                            index++;//index=3
                                            //count++;
                                            count1++;
                                            number++;
                                            count2 = 0;//清零
                                            findTmpEgde();
                                            int leftmin3 = (newrightEdge - newleftEdge) / 2;
                                            int rightmin3 = newrightEdge - newleftEdge - leftmin3;
                                            int upmin3 = (newdownEdge - newupEdge) / 2;
                                            int downmin3 = newdownEdge - newupEdge - upmin3;
                                            //生成第三层
                                            #region 3rd floor
                                            do
                                            {
                                                if (left3 > leftmin3)
                                                    left3 = leftmin3;
                                                if (right3 > rightmin3)
                                                    right3 = rightmin3;
                                                if (up3 > upmin3)
                                                    up3 = upmin3;
                                                if (down3 > downmin3)
                                                    down3 = downmin3;
                                                for (int a = 7 - left3; a <= 7 + right3; a += left3 + right3)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b++)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = Black;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                for (int a = 7 - left3 + 1; a <= 7 + right3 - 1; a++)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b += up3 + down3)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = Black;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                left3 += 1;
                                                right3 += 1;
                                                up3 += 1;
                                                down3 += 1;
                                            } while (left3 <= leftmin3 || right3 <= rightmin3 || up3 <= upmin3 || down3 <= downmin3);
                                            #endregion
                                            //updateScore(index - 1 - count2, changescore2);//更新第二层节点(其父节点)的分数值
                                            #region alpha jianzhi
                                            if (count > 1)//生成多于一个第二层节点时要进行剪枝
                                            {//第二层新值比根节点参考值要小，就不算这波第二层了
                                                if (changescore2 < score)
                                                {
                                                    tag3 = 1;
                                                    break;
                                                }
                                            }
                                            #endregion
                                            if (number == 1)//生成第一个第二层节点时，记录参考分数，小于它再更改
                                            {
                                                changescore3 = changescore2;
                                            }//changescore3 是第一层节点最终的分数值
                                            else
                                            {
                                                if (changescore2 < changescore3)//新的值小于参考值
                                                {
                                                    changescore3 = changescore2;//新的值赋给参考值
                                                }//参考值是结果
                                            }


                                        }//if (tempchessBoard[m, n] == Empty)
                                    }//for n
                                }//for m
                                left2 += 1;
                                right2 += 1;
                                up2 += 1;
                                down2 += 1;
                            } while (left2 <= leftmin2 || right2 <= rightmin2 || up2 <= upmin2 || down2 <= downmin2);
                            #endregion
                            //updateScore(index - 1 - count1, changescore3);//更新第一层节点(其父节点)的分数值
                            if (count == 1)//记录参考值 大于它再更改，更改同时要更新节点的index
                            {
                                //MessageBox.Show("tester");
                                score = changescore3;
                            }
                            else
                            {
                                if (changescore3 > score)
                                {
                                    score = changescore3;
                                    indexWEWANT = index - 1 - count1; //找到这个第一层节点的index值
                                }//score是最后的分数
                            }
                        }//if (tempchessBoard[i, j] == Empty)
                    }//for j
                }//for i
                for (int i = 7 - left1 + 1; i <= 7 + right1 - 1; i++)
                {
                    for (int j = 7 - up1; j <= 7 + down1; j += up1 + down1)
                    {
                        copyFromTreetoTmp(0);//每次都是从根节点开始生成第一层
                        if (tempchessBoard[i, j] == Empty)
                        {
                            //MessageBox.Show("testooo");
                            tempchessBoard[i, j] = Black;


                            addToEndofTree(index, 0, 0, 1);//index=1
                            index++;//index=2
                            count++;
                            count1 = 0;//清零
                            number = 0;
                            //MessageBox.Show("1" + count1);
                            findTmpEgde();
                            int leftmin2 = (newrightEdge - newleftEdge) / 2;
                            int rightmin2 = newrightEdge - newleftEdge - leftmin2;
                            int upmin2 = (newdownEdge - newupEdge) / 2;
                            int downmin2 = newdownEdge - newupEdge - upmin2;
                            //生成第二层
                            #region 2nd floor
                            do
                            {
                                if (left2 > leftmin2)
                                    left2 = leftmin2;
                                if (right2 > rightmin2)
                                    right2 = rightmin2;
                                if (up2 > upmin2)
                                    up2 = upmin2;
                                if (down2 > downmin2)
                                    down2 = downmin2;
                                for (int m = 7 - left2; m <= 7 + right2; m += left2 + right2)
                                {
                                    if (tag3 == 1)
                                    {
                                        tag3 = 0;
                                        break;
                                    }
                                    for (int n = 7 - up2; n <= 7 + down2; n++)
                                    {
                                        copyFromTreetoTmp(index - 1 - count1);//每次都是从父节点生成子节点
                                        if (tempchessBoard[m, n] == Empty)
                                        {
                                            tempchessBoard[m, n] = White;
                                            addToEndofTree(index, index - 1 - count1, 0, 2);//index=2
                                            index++;//index=3
                                            //count++;
                                            count1++;
                                            number++;
                                            count2 = 0;//清零
                                            findTmpEgde();
                                            int leftmin3 = (newrightEdge - newleftEdge) / 2;
                                            int rightmin3 = newrightEdge - newleftEdge - leftmin3;
                                            int upmin3 = (newdownEdge - newupEdge) / 2;
                                            int downmin3 = newdownEdge - newupEdge - upmin3;
                                            //生成第三层
                                            #region 3rd floor
                                            do
                                            {
                                                if (left3 > leftmin3)
                                                    left3 = leftmin3;
                                                if (right3 > rightmin3)
                                                    right3 = rightmin3;
                                                if (up3 > upmin3)
                                                    up3 = upmin3;
                                                if (down3 > downmin3)
                                                    down3 = downmin3;
                                                for (int a = 7 - left3; a <= 7 + right3; a += left3 + right3)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b++)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = Black;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                for (int a = 7 - left3 + 1; a <= 7 + right3 - 1; a++)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b += up3 + down3)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = Black;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                left3 += 1;
                                                right3 += 1;
                                                up3 += 1;
                                                down3 += 1;
                                            } while (left3 <= leftmin3 || right3 <= rightmin3 || up3 <= upmin3 || down3 <= downmin3);
                                            #endregion
                                            //updateScore(index - 1 - count2, changescore2);//更新第二层节点(其父节点)的分数值
                                            #region alpha jianzhi
                                            if (count > 1)//生成多于一个第二层节点时要进行剪枝
                                            {//第二层新值比根节点参考值要小，就不算这波第二层了
                                                if (changescore2 < score)
                                                {
                                                    tag3 = 1;
                                                    break;
                                                }
                                            }
                                            #endregion
                                            if (number == 1)//生成第一个第二层节点时，记录参考分数，小于它再更改
                                            {
                                                //MessageBox.Show("tester");
                                                changescore3 = changescore2;
                                            }//changescore3 是第一层节点最终的分数值
                                            else
                                            {
                                                if (changescore2 < changescore3)//新的值小于参考值
                                                {
                                                    changescore3 = changescore2;//新的值赋给参考值
                                                }//参考值是结果
                                            }


                                        }//if (tempchessBoard[m, n] == Empty)
                                    }//for n
                                }//for m
                                for (int m = 7 - left2 + 1; m <= 7 + right2 - 1; m++)
                                {
                                    if (tag3 == 1)
                                    {
                                        tag3 = 0;
                                        break;
                                    }
                                    for (int n = 7 - up2; n <= 7 + down2; n += up2 + down2)
                                    {
                                        copyFromTreetoTmp(index - 1 - count1);//每次都是从父节点生成子节点
                                        if (tempchessBoard[m, n] == Empty)
                                        {
                                            tempchessBoard[m, n] = White;
                                            addToEndofTree(index, index - 1 - count1, 0, 2);//index=2
                                            index++;//index=3
                                            //count++;
                                            count1++;
                                            number++;
                                            count2 = 0;//清零
                                            findTmpEgde();
                                            int leftmin3 = (newrightEdge - newleftEdge) / 2;
                                            int rightmin3 = newrightEdge - newleftEdge - leftmin3;
                                            int upmin3 = (newdownEdge - newupEdge) / 2;
                                            int downmin3 = newdownEdge - newupEdge - upmin3;
                                            //生成第三层
                                            #region 3rd floor
                                            do
                                            {
                                                if (left3 > leftmin3)
                                                    left3 = leftmin3;
                                                if (right3 > rightmin3)
                                                    right3 = rightmin3;
                                                if (up3 > upmin3)
                                                    up3 = upmin3;
                                                if (down3 > downmin3)
                                                    down3 = downmin3;
                                                for (int a = 7 - left3; a <= 7 + right3; a += left3 + right3)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b++)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = Black;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                for (int a = 7 - left3 + 1; a <= 7 + right3 - 1; a++)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b += up3 + down3)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = Black;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = White;
                                                                            count3++;
                                                                            changescore1 = estimateScore();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                left3 += 1;
                                                right3 += 1;
                                                up3 += 1;
                                                down3 += 1;
                                            } while (left3 <= leftmin3 || right3 <= rightmin3 || up3 <= upmin3 || down3 <= downmin3);
                                            #endregion
                                            //updateScore(index - 1 - count2, changescore2);//更新第二层节点(其父节点)的分数值
                                            #region alpha jianzhi
                                            if (count > 1)//生成多于一个第二层节点时要进行剪枝
                                            {//第二层新值比根节点参考值要小，就不算这波第二层了
                                                if (changescore2 < score)
                                                {
                                                    tag3 = 1;
                                                    break;
                                                }
                                            }
                                            #endregion
                                            if (number == 1)//生成第一个第二层节点时，记录参考分数，小于它再更改
                                            {
                                                changescore3 = changescore2;
                                            }//changescore3 是第一层节点最终的分数值
                                            else
                                            {
                                                if (changescore2 < changescore3)//新的值小于参考值
                                                {
                                                    changescore3 = changescore2;//新的值赋给参考值
                                                }//参考值是结果
                                            }


                                        }//if (tempchessBoard[m, n] == Empty)
                                    }//for n
                                }//for m
                                left2 += 1;
                                right2 += 1;
                                up2 += 1;
                                down2 += 1;
                            } while (left2 <= leftmin2 || right2 <= rightmin2 || up2 <= upmin2 || down2 <= downmin2);
                            #endregion
                            //updateScore(index - 1 - count1, changescore3);//更新第一层节点(其父节点)的分数值
                            if (count == 1)//记录参考值 大于它再更改，更改同时要更新节点的index
                            {
                                //MessageBox.Show("tester");
                                score = changescore3;
                            }
                            else
                            {
                                //MessageBox.Show("test1");
                                if (changescore3 > score)
                                {
                                    score = changescore3;
                                    indexWEWANT = index - 1 - count1; //找到这个第一层节点的index值
                                }//score是最后的分数
                            }
                        }//if (tempchessBoard[i, j] == Empty)
                    }//for j
                }//for i
                left1 += 1;
                right1 += 1;
                up1 += 1;
                down1 += 1;
            } while (left1 <= leftmin1 || right1 <= rightmin1 || up1 <= upmin1 || down1 <= downmin1);
            #endregion
            //updateScore(0, score);
            //MessageBox.Show("test"+index+" "+indexWEWANT + " " +score);
            copyFromTreetoTmp(indexWEWANT);//至此为止，tmp中存放的是最后要走的那步棋，要将它放到chessBoard中，放入everystep中，画图就好了
        }


        private void CreateSerchingTreeWhite()//Try to pass from center to edge! Tree structure. Need Depth. Alpha-Beta Cut.
        {
            //往上下左右最多距离中心7的偏差值
            int leftmin1 = (newrightEdge - newleftEdge) / 2;
            int rightmin1 = newrightEdge - newleftEdge - leftmin1;
            int upmin1 = (newdownEdge - newupEdge) / 2;
            int downmin1 = newdownEdge - newupEdge - upmin1;
            //MessageBox.Show("board " + newleftEdge + " " + newrightEdge + " " + newupEdge + " " + newdownEdge);
            int up1 = 1, down1 = 1, left1 = 1, right1 = 1;
            int up2 = 1, down2 = 1, left2 = 1, right2 = 1;
            int up3 = 1, down3 = 1, left3 = 1, right3 = 1;
            int up4 = 1, down4 = 1, left4 = 1, right4 = 1;
            score = 0;

            int index = 0;//往树里插入的时候的节点序号
            int indexWEWANT = 1;//我们要的那个第一层节点存在树中的序号

            int changescore = 100000, changescore1, changescore2 = 0, changescore3 = 100000;//参考分数值
            int tag1 = 0, tag2 = 0, tag3 = 0;//跳出两层循环的标志
            int count = 0, count1 = 0, count2 = 0, count3 = 0, number = 0;//计算一个节点下面加了多少个节点 count是根下的，count1是第一层下面的，以此类推
            addToEndofTree(index, -1, 0, 0);//index=0
            index++;//插入后加1  //index=1
            #region 1st floor
            do
            {
                if (left1 > leftmin1)
                    left1 = leftmin1;
                if (right1 > rightmin1)
                    right1 = rightmin1;
                if (up1 > upmin1)
                    up1 = upmin1;
                if (down1 > downmin1)
                    down1 = downmin1;
                for (int i = 7 - left1; i <= 7 + right1; i += left1 + right1)
                {
                    for (int j = 7 - up1; j <= 7 + down1; j++)
                    {
                        copyFromTreetoTmp(0);//每次都是从根节点开始生成第一层
                        if (tempchessBoard[i, j] == Empty)
                        {
                            tempchessBoard[i, j] = White;
                            //MessageBox.Show("" + judgeJINSHOU(i, j));


                            addToEndofTree(index, 0, 0, 1);//index=1
                            index++;//index=2
                            count++;
                            //MessageBox.Show("o" + count);
                            count1 = 0;//清零
                            number = 0;
                            //MessageBox.Show("1" + count1);
                            findTmpEgde();
                            int leftmin2 = (newrightEdge - newleftEdge) / 2;
                            int rightmin2 = newrightEdge - newleftEdge - leftmin2;
                            int upmin2 = (newdownEdge - newupEdge) / 2;
                            int downmin2 = newdownEdge - newupEdge - upmin2;
                            //MessageBox.Show("board" + leftmin2 + rightmin2 + upmin2 + downmin2);
                            //生成第二层
                            #region 2nd floor
                            do
                            {
                                //MessageBox.Show("board" + leftmin2 + rightmin2 + upmin2 + downmin2);
                                if (left2 > leftmin2)
                                    left2 = leftmin2;
                                if (right2 > rightmin2)
                                    right2 = rightmin2;
                                if (up2 > upmin2)
                                    up2 = upmin2;
                                if (down2 > downmin2)
                                    down2 = downmin2;
                                for (int m = 7 - left2; m <= 7 + right2; m += left2 + right2)
                                {
                                    if (tag3 == 1)
                                    {
                                        tag3 = 0;
                                        break;
                                    }
                                    for (int n = 7 - up2; n <= 7 + down2; n++)
                                    {
                                        copyFromTreetoTmp(index - 1 - count1);//每次都是从父节点生成子节点
                                        if (tempchessBoard[m, n] == Empty)
                                        {
                                            tempchessBoard[m, n] = Black;
                                            addToEndofTree(index, index - 1 - count1, 0, 2);//index=2
                                            index++;//index=3
                                            //count++;
                                            count1++;
                                            number++;
                                            count2 = 0;//清零
                                            findTmpEgde();
                                            int leftmin3 = (newrightEdge - newleftEdge) / 2;
                                            int rightmin3 = newrightEdge - newleftEdge - leftmin3;
                                            int upmin3 = (newdownEdge - newupEdge) / 2;
                                            int downmin3 = newdownEdge - newupEdge - upmin3;
                                            //生成第三层
                                            #region 3rd floor
                                            do
                                            {

                                                if (left3 > leftmin3)
                                                    left3 = leftmin3;
                                                if (right3 > rightmin3)
                                                    right3 = rightmin3;
                                                if (up3 > upmin3)
                                                    up3 = upmin3;
                                                if (down3 > downmin3)
                                                    down3 = downmin3;
                                                for (int a = 7 - left3; a <= 7 + right3; a += left3 + right3)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b++)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = White;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {

                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                //MessageBox.Show("tester");
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                for (int a = 7 - left3 + 1; a <= 7 + right3 - 1; a++)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b += up3 + down3)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = White;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                left3 += 1;
                                                right3 += 1;
                                                up3 += 1;
                                                down3 += 1;
                                            } while (left3 <= leftmin3 || right3 <= rightmin3 || up3 <= upmin3 || down3 <= downmin3);
                                            #endregion
                                            //updateScore(index - 1 - count2, changescore2);//更新第二层节点(其父节点)的分数值
                                            #region alpha jianzhi
                                            if (count > 1)//生成多于一个第二层节点时要进行剪枝
                                            {//第二层新值比根节点参考值要小，就不算这波第二层了
                                                if (changescore2 < score)
                                                {
                                                    tag3 = 1;
                                                    break;
                                                }
                                            }
                                            #endregion
                                            if (number == 1)//生成第一个第二层节点时，记录参考分数，小于它再更改
                                            {
                                                //MessageBox.Show("tester");
                                                changescore3 = changescore2;
                                            }//changescore3 是第一层节点最终的分数值
                                            else
                                            {
                                                if (changescore2 < changescore3)//新的值小于参考值
                                                {
                                                    changescore3 = changescore2;//新的值赋给参考值
                                                }//参考值是结果
                                            }


                                        }//if (tempchessBoard[m, n] == Empty)
                                    }//for n
                                }//for m
                                for (int m = 7 - left2 + 1; m <= 7 + right2 - 1; m++)
                                {
                                    if (tag3 == 1)
                                    {
                                        tag3 = 0;
                                        break;
                                    }
                                    for (int n = 7 - up2; n <= 7 + down2; n += up2 + down2)
                                    {
                                        copyFromTreetoTmp(index - 1 - count1);//每次都是从父节点生成子节点
                                        if (tempchessBoard[m, n] == Empty)
                                        {
                                            tempchessBoard[m, n] = Black;
                                            addToEndofTree(index, index - 1 - count1, 0, 2);//index=2
                                            index++;//index=3
                                            //count++;
                                            count1++;
                                            number++;
                                            count2 = 0;//清零
                                            findTmpEgde();
                                            int leftmin3 = (newrightEdge - newleftEdge) / 2;
                                            int rightmin3 = newrightEdge - newleftEdge - leftmin3;
                                            int upmin3 = (newdownEdge - newupEdge) / 2;
                                            int downmin3 = newdownEdge - newupEdge - upmin3;
                                            //生成第三层
                                            #region 3rd floor
                                            do
                                            {
                                                if (left3 > leftmin3)
                                                    left3 = leftmin3;
                                                if (right3 > rightmin3)
                                                    right3 = rightmin3;
                                                if (up3 > upmin3)
                                                    up3 = upmin3;
                                                if (down3 > downmin3)
                                                    down3 = downmin3;
                                                for (int a = 7 - left3; a <= 7 + right3; a += left3 + right3)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b++)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = White;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                for (int a = 7 - left3 + 1; a <= 7 + right3 - 1; a++)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b += up3 + down3)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = White;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                left3 += 1;
                                                right3 += 1;
                                                up3 += 1;
                                                down3 += 1;
                                            } while (left3 <= leftmin3 || right3 <= rightmin3 || up3 <= upmin3 || down3 <= downmin3);
                                            #endregion
                                            //updateScore(index - 1 - count2, changescore2);//更新第二层节点(其父节点)的分数值
                                            #region alpha jianzhi
                                            if (count > 1)//生成多于一个第二层节点时要进行剪枝
                                            {//第二层新值比根节点参考值要小，就不算这波第二层了
                                                if (changescore2 < score)
                                                {
                                                    tag3 = 1;
                                                    break;
                                                }
                                            }
                                            #endregion
                                            if (number == 1)//生成第一个第二层节点时，记录参考分数，小于它再更改
                                            {
                                                changescore3 = changescore2;
                                            }//changescore3 是第一层节点最终的分数值
                                            else
                                            {
                                                if (changescore2 < changescore3)//新的值小于参考值
                                                {
                                                    changescore3 = changescore2;//新的值赋给参考值
                                                }//参考值是结果
                                            }


                                        }//if (tempchessBoard[m, n] == Empty)
                                    }//for n
                                }//for m
                                left2 += 1;
                                right2 += 1;
                                up2 += 1;
                                down2 += 1;
                            } while (left2 <= leftmin2 || right2 <= rightmin2 || up2 <= upmin2 || down2 <= downmin2);
                            #endregion
                            //updateScore(index - 1 - count1, changescore3);//更新第一层节点(其父节点)的分数值
                            if (count == 1)//记录参考值 大于它再更改，更改同时要更新节点的index
                            {
                                //MessageBox.Show("tester");
                                score = changescore3;
                            }
                            else
                            {
                                if (changescore3 > score)
                                {
                                    score = changescore3;
                                    indexWEWANT = index - 1 - count1; //找到这个第一层节点的index值
                                }//score是最后的分数
                            }
                        }//if (tempchessBoard[i, j] == Empty)
                    }//for j
                }//for i
                for (int i = 7 - left1 + 1; i <= 7 + right1 - 1; i++)
                {
                    for (int j = 7 - up1; j <= 7 + down1; j += up1 + down1)
                    {
                        copyFromTreetoTmp(0);//每次都是从根节点开始生成第一层
                        if (tempchessBoard[i, j] == Empty)
                        {
                            //MessageBox.Show("testooo");
                            tempchessBoard[i, j] = White;


                            addToEndofTree(index, 0, 0, 1);//index=1
                            index++;//index=2
                            count++;
                            count1 = 0;//清零
                            number = 0;
                            //MessageBox.Show("1" + count1);
                            findTmpEgde();
                            int leftmin2 = (newrightEdge - newleftEdge) / 2;
                            int rightmin2 = newrightEdge - newleftEdge - leftmin2;
                            int upmin2 = (newdownEdge - newupEdge) / 2;
                            int downmin2 = newdownEdge - newupEdge - upmin2;
                            //生成第二层
                            #region 2nd floor
                            do
                            {
                                if (left2 > leftmin2)
                                    left2 = leftmin2;
                                if (right2 > rightmin2)
                                    right2 = rightmin2;
                                if (up2 > upmin2)
                                    up2 = upmin2;
                                if (down2 > downmin2)
                                    down2 = downmin2;
                                for (int m = 7 - left2; m <= 7 + right2; m += left2 + right2)
                                {
                                    if (tag3 == 1)
                                    {
                                        tag3 = 0;
                                        break;
                                    }
                                    for (int n = 7 - up2; n <= 7 + down2; n++)
                                    {
                                        copyFromTreetoTmp(index - 1 - count1);//每次都是从父节点生成子节点
                                        if (tempchessBoard[m, n] == Empty)
                                        {
                                            tempchessBoard[m, n] = Black;
                                            addToEndofTree(index, index - 1 - count1, 0, 2);//index=2
                                            index++;//index=3
                                            //count++;
                                            count1++;
                                            number++;
                                            count2 = 0;//清零
                                            findTmpEgde();
                                            int leftmin3 = (newrightEdge - newleftEdge) / 2;
                                            int rightmin3 = newrightEdge - newleftEdge - leftmin3;
                                            int upmin3 = (newdownEdge - newupEdge) / 2;
                                            int downmin3 = newdownEdge - newupEdge - upmin3;
                                            //生成第三层
                                            #region 3rd floor
                                            do
                                            {
                                                if (left3 > leftmin3)
                                                    left3 = leftmin3;
                                                if (right3 > rightmin3)
                                                    right3 = rightmin3;
                                                if (up3 > upmin3)
                                                    up3 = upmin3;
                                                if (down3 > downmin3)
                                                    down3 = downmin3;
                                                for (int a = 7 - left3; a <= 7 + right3; a += left3 + right3)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b++)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = White;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                for (int a = 7 - left3 + 1; a <= 7 + right3 - 1; a++)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b += up3 + down3)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = White;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                left3 += 1;
                                                right3 += 1;
                                                up3 += 1;
                                                down3 += 1;
                                            } while (left3 <= leftmin3 || right3 <= rightmin3 || up3 <= upmin3 || down3 <= downmin3);
                                            #endregion
                                            //updateScore(index - 1 - count2, changescore2);//更新第二层节点(其父节点)的分数值
                                            #region alpha jianzhi
                                            if (count > 1)//生成多于一个第二层节点时要进行剪枝
                                            {//第二层新值比根节点参考值要小，就不算这波第二层了
                                                if (changescore2 < score)
                                                {
                                                    tag3 = 1;
                                                    break;
                                                }
                                            }
                                            #endregion
                                            if (number == 1)//生成第一个第二层节点时，记录参考分数，小于它再更改
                                            {
                                                //MessageBox.Show("tester");
                                                changescore3 = changescore2;
                                            }//changescore3 是第一层节点最终的分数值
                                            else
                                            {
                                                if (changescore2 < changescore3)//新的值小于参考值
                                                {
                                                    changescore3 = changescore2;//新的值赋给参考值
                                                }//参考值是结果
                                            }


                                        }//if (tempchessBoard[m, n] == Empty)
                                    }//for n
                                }//for m
                                for (int m = 7 - left2 + 1; m <= 7 + right2 - 1; m++)
                                {
                                    if (tag3 == 1)
                                    {
                                        tag3 = 0;
                                        break;
                                    }
                                    for (int n = 7 - up2; n <= 7 + down2; n += up2 + down2)
                                    {
                                        copyFromTreetoTmp(index - 1 - count1);//每次都是从父节点生成子节点
                                        if (tempchessBoard[m, n] == Empty)
                                        {
                                            tempchessBoard[m, n] = Black;
                                            addToEndofTree(index, index - 1 - count1, 0, 2);//index=2
                                            index++;//index=3
                                            //count++;
                                            count1++;
                                            number++;
                                            count2 = 0;//清零
                                            findTmpEgde();
                                            int leftmin3 = (newrightEdge - newleftEdge) / 2;
                                            int rightmin3 = newrightEdge - newleftEdge - leftmin3;
                                            int upmin3 = (newdownEdge - newupEdge) / 2;
                                            int downmin3 = newdownEdge - newupEdge - upmin3;
                                            //生成第三层
                                            #region 3rd floor
                                            do
                                            {
                                                if (left3 > leftmin3)
                                                    left3 = leftmin3;
                                                if (right3 > rightmin3)
                                                    right3 = rightmin3;
                                                if (up3 > upmin3)
                                                    up3 = upmin3;
                                                if (down3 > downmin3)
                                                    down3 = downmin3;
                                                for (int a = 7 - left3; a <= 7 + right3; a += left3 + right3)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b++)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = White;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                for (int a = 7 - left3 + 1; a <= 7 + right3 - 1; a++)
                                                {
                                                    if (tag2 == 1)
                                                    {
                                                        tag2 = 0;
                                                        break;
                                                    }
                                                    for (int b = 7 - up3; b <= 7 + down3; b += up3 + down3)
                                                    {
                                                        copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                                        if (tempchessBoard[a, b] == Empty)
                                                        {
                                                            tempchessBoard[a, b] = White;


                                                            addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                            index++;//index=4
                                                            //count++;
                                                            count1++;
                                                            count2++;
                                                            count3 = 0;//清零
                                                            findTmpEgde();
                                                            int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                            int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                            int upmin4 = (newdownEdge - newupEdge) / 2;
                                                            int downmin4 = newdownEdge - newupEdge - upmin4;
                                                            //Get a score of third depth.
                                                            #region 4th floor
                                                            do
                                                            {
                                                                if (left4 > leftmin4)
                                                                    left4 = leftmin4;
                                                                if (right4 > rightmin4)
                                                                    right4 = rightmin4;
                                                                if (up4 > upmin4)
                                                                    up4 = upmin4;
                                                                if (down4 > downmin4)
                                                                    down4 = downmin4;
                                                                for (int c = 7 - left4; c <= 7 + right4; c += left4 + right4)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d++)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                for (int c = 7 - left4 + 1; c <= 7 + right4 - 1; c++)
                                                                {
                                                                    if (tag1 == 1)
                                                                    {
                                                                        tag1 = 0;
                                                                        break;
                                                                    }
                                                                    for (int d = 7 - up4; d <= 7 + down4; d += up4 + down4)
                                                                    {
                                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                                        if (tempchessBoard[c, d] == Empty)
                                                                        {
                                                                            tempchessBoard[c, d] = Black;
                                                                            count3++;
                                                                            changescore1 = estimateScoreWhite();
                                                                            #region alpha jianzhi
                                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                                            {
                                                                                if (changescore1 < changescore2)
                                                                                {
                                                                                    tag1 = 1;//=1时跳出两层循环
                                                                                    break;
                                                                                }//changescore1是最底层节点分数值
                                                                            }
                                                                            #endregion
                                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                                            {
                                                                                changescore = changescore1;
                                                                            }
                                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                                            {
                                                                                changescore = changescore1;
                                                                            }//最后的结果：changescore是第三层节点的分数值
                                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                                    }//for d
                                                                }//for c
                                                                left4 += 1;
                                                                right4 += 1;
                                                                up4 += 1;
                                                                down4 += 1;
                                                            } while (left4 <= leftmin4 || right4 <= rightmin4 || up4 <= upmin4 || down4 <= downmin4);
                                                            #endregion
                                                            //updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                            #region beta jianzhi
                                                            if (number > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                            {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                                if (changescore > changescore3)
                                                                {
                                                                    tag2 = 1;
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                            {
                                                                changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                                //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                            }
                                                            else
                                                            {
                                                                if (changescore > changescore2)//新值大于旧值 更新
                                                                {
                                                                    changescore2 = changescore;
                                                                }//第二层节点选最大的值，第二层节点的值是changescore2
                                                            }

                                                        }//if (tempchessBoard[a, b] == Empty)
                                                    }//for b
                                                }//for a
                                                left3 += 1;
                                                right3 += 1;
                                                up3 += 1;
                                                down3 += 1;
                                            } while (left3 <= leftmin3 || right3 <= rightmin3 || up3 <= upmin3 || down3 <= downmin3);
                                            #endregion
                                            //updateScore(index - 1 - count2, changescore2);//更新第二层节点(其父节点)的分数值
                                            #region alpha jianzhi
                                            if (count > 1)//生成多于一个第二层节点时要进行剪枝
                                            {//第二层新值比根节点参考值要小，就不算这波第二层了
                                                if (changescore2 < score)
                                                {
                                                    tag3 = 1;
                                                    break;
                                                }
                                            }
                                            #endregion
                                            if (number == 1)//生成第一个第二层节点时，记录参考分数，小于它再更改
                                            {
                                                changescore3 = changescore2;
                                            }//changescore3 是第一层节点最终的分数值
                                            else
                                            {
                                                if (changescore2 < changescore3)//新的值小于参考值
                                                {
                                                    changescore3 = changescore2;//新的值赋给参考值
                                                }//参考值是结果
                                            }


                                        }//if (tempchessBoard[m, n] == Empty)
                                    }//for n
                                }//for m
                                left2 += 1;
                                right2 += 1;
                                up2 += 1;
                                down2 += 1;
                            } while (left2 <= leftmin2 || right2 <= rightmin2 || up2 <= upmin2 || down2 <= downmin2);
                            #endregion
                            //updateScore(index - 1 - count1, changescore3);//更新第一层节点(其父节点)的分数值
                            if (count == 1)//记录参考值 大于它再更改，更改同时要更新节点的index
                            {
                                //MessageBox.Show("tester");
                                score = changescore3;
                            }
                            else
                            {
                                //MessageBox.Show("test1");
                                if (changescore3 > score)
                                {
                                    score = changescore3;
                                    indexWEWANT = index - 1 - count1; //找到这个第一层节点的index值
                                }//score是最后的分数
                            }
                        }//if (tempchessBoard[i, j] == Empty)
                    }//for j
                }//for i
                left1 += 1;
                right1 += 1;
                up1 += 1;
                down1 += 1;
            } while (left1 <= leftmin1 || right1 <= rightmin1 || up1 <= upmin1 || down1 <= downmin1);
            #endregion
            //updateScore(0, score);
            //MessageBox.Show("test" + index + " " + indexWEWANT + " " + score);
            copyFromTreetoTmp(indexWEWANT);//至此为止，tmp中存放的是最后要走的那步棋，要将它放到chessBoard中，放入everystep中，画图就好了
        }


        #region createserchengtree
        
        private void CreateSerchingTree()//Try to pass from center to edge! Tree structure. Need Depth. Alpha-Beta Cut.
        {
            //往上下左右最多距离中心7的偏差值
            int leftmin1 = (newrightEdge - newleftEdge) / 2;
            int rightmin1 = newrightEdge - newleftEdge - leftmin1;
            int upmin1 = (newdownEdge - newupEdge) / 2;
            int downmin1 = newdownEdge - newupEdge - upmin1;
            //MessageBox.Show("board" + leftmin1 + rightmin1 + upmin1 + downmin1);
            int up1 = 1, down1 = 1, left1 = 1, right1 = 1;
            int up2 = 1, down2 = 1, left2 = 1, right2 = 1;
            int up3 = 1, down3 = 1, left3 = 1, right3 = 1;
            int up4 = 1, down4 = 1, left4 = 1, right4 = 1;
            int index = 0;//往树里插入的时候的节点序号
            int indexWEWANT = 0;//我们要的那个第一层节点存在树中的序号
            int changescore = 100000, changescore1, changescore2 = 0, changescore3 = 100000;//参考分数值
            int tag1 = 0, tag2 = 0, tag3 = 0;//跳出两层循环的标志
            int count = 0, count1 = 0, count2 = 0, count3 = 0;//计算一个节点下面加了多少个节点 count是根下的，count1是第一层下面的，以此类推
            addToEndofTree(index, -1, 0, 0);//index=0
            index++;//插入后加1  //index=1
            #region 1st floor
            for (int i = 7 - leftmin1; i <= 7 + rightmin1; i++)
            {
                for (int j = 7 - upmin1; j <= 7 + downmin1; j++)
                {
                    copyFromTreetoTmp(0);//每次都是从根节点开始生成第一层
                    if (tempchessBoard[i, j] == Empty)
                    {
                        tempchessBoard[i, j] = Black;
                        addToEndofTree(index, 0, 0, 1);//index=1
                        index++;//index=2
                        count++;
                        count1 = 0;//清零
                        //MessageBox.Show("1" + count1);
                        findTmpEgde();
                        int leftmin2 = (newrightEdge - newleftEdge) / 2;
                        int rightmin2 = newrightEdge - newleftEdge - leftmin2;
                        int upmin2 = (newdownEdge - newupEdge) / 2;
                        int downmin2 = newdownEdge - newupEdge - upmin2;
                        //生成第二层
                        #region 2nd floor
                        for (int m = 7 - leftmin2; m <= 7 + rightmin2; m++)
                        {
                            if (tag3 == 1)
                            {
                                tag3 = 0;
                                break;
                            }
                            for (int n = 7 - upmin2; n <= 7 + downmin2; n++)
                            {
                                copyFromTreetoTmp(index - 1 - count1);//每次都是从父节点生成子节点
                                if (tempchessBoard[m, n] == Empty)
                                {
                                    tempchessBoard[m, n] = White;
                                    addToEndofTree(index, index - 1 - count1, 0, 2);//index=2
                                    index++;//index=3
                                    //count++;
                                    count1++;
                                    count2 = 0;//清零
                                    findTmpEgde();
                                    int leftmin3 = (newrightEdge - newleftEdge) / 2;
                                    int rightmin3 = newrightEdge - newleftEdge - leftmin3;
                                    int upmin3 = (newdownEdge - newupEdge) / 2;
                                    int downmin3 = newdownEdge - newupEdge - upmin3;
                                    //生成第三层
                                    #region 3rd floor
                                    for (int a = 7 - leftmin3; a <= 7 + rightmin3; a++)
                                    {
                                        if (tag2 == 1)
                                        {
                                            tag2 = 0;
                                            break;
                                        }
                                        for (int b = 7 - upmin3; b <= 7 + downmin3; b++)
                                        {
                                            copyFromTreetoTmp(index - 1 - count2);//每次都是从父节点开始生成子节点
                                            if (tempchessBoard[a, b] == Empty)
                                            {
                                                tempchessBoard[a, b] = Black;
                                                addToEndofTree(index, index - 1 - count2, 0, 3);//index=3   fujiedianzhi!
                                                index++;//index=4
                                                //count++;
                                                count1++;
                                                count2++;
                                                count3 = 0;//清零
                                                findTmpEgde();
                                                int leftmin4 = (newrightEdge - newleftEdge) / 2;
                                                int rightmin4 = newrightEdge - newleftEdge - leftmin4;
                                                int upmin4 = (newdownEdge - newupEdge) / 2;
                                                int downmin4 = newdownEdge - newupEdge - upmin4;
                                                //Get a score of third depth.
                                                #region 4th floor
                                                //do
                                                //{
                                                for (int c = 7 - leftmin4; c <= 7 + rightmin4; c++)
                                                {
                                                    if (tag1 == 1)
                                                    {
                                                        tag1 = 0;
                                                        break;
                                                    }
                                                    for (int d = 7 - upmin4; d <= 7 + downmin4; d++)
                                                    {
                                                        copyFromTreetoTmp(index - 1);//index-1=3 第四层不存入tree中，index不会增加，每次的父节点就是index-1
                                                        if (tempchessBoard[c, d] == Empty)
                                                        {
                                                            tempchessBoard[c, d] = White;
                                                            count3++;
                                                            changescore1 = estimateScore();
                                                            #region alpha jianzhi
                                                            if (count2 > 1)//alpha jianzhi! 生成的第四层新值比第二层参考值都小，就不要算这一波第四层了
                                                            {
                                                                if (changescore1 < changescore2)
                                                                {
                                                                    tag1 = 1;//=1时跳出两层循环
                                                                    break;
                                                                }//changescore1是最底层节点分数值
                                                            }
                                                            #endregion
                                                            if (count3 == 1)//生成第一个第四层节点时记分，分数作为参考值，小于它再更改
                                                            {
                                                                changescore = changescore1;
                                                            }
                                                            if (changescore1 < changescore)//第一个时两者相等不会判断，之后若新分数更小则更新
                                                            {
                                                                changescore = changescore1;
                                                            }//最后的结果：changescore是第三层节点的分数值
                                                        }//if (tempchessBoard[c, d] == Empty) end
                                                    }//for d
                                                }//for c
                                                //} while (left4 <= leftmin4 && right4 <= rightmin4 && up4 <= upmin4 && down4 <= downmin4);
                                                #endregion
                                                updateScore(index - 1, changescore); //index-1=3   每次更新第三层节点(父节点)的分数值（score）
                                                #region beta jianzhi
                                                if (count1 > 1)//不是第一个第三层节点是要考虑剪枝的问题
                                                {//第二次生成及以后的第三层的新值比第一层参考值还大，就不要计算这波第三层了
                                                    if (changescore > changescore3)
                                                    {
                                                        tag2 = 1;
                                                        break;
                                                    }
                                                }
                                                #endregion
                                                if (count2 == 1)//生成了第一个第三层节点时，记参考分数，大于它再更改
                                                {
                                                    changescore2 = changescore;//changescore2是第一个算出的第三层节点分数值,也是最终的第二节点分数值
                                                    //updateScore(index - 1 - count2, changescore2);//更新父节点分数值 index和count2同时加过1，父节点仍然这么算
                                                }
                                                else
                                                {
                                                    if (changescore > changescore2)//新值大于旧值 更新
                                                    {
                                                        changescore2 = changescore;
                                                    }//第二层节点选最大的值，第二层节点的值是changescore2
                                                }

                                            }//if (tempchessBoard[a, b] == Empty)
                                        }//for b
                                    }//for a
                                    #endregion
                                    updateScore(index - 1 - count2, changescore2);//更新第二层节点(其父节点)的分数值
                                    #region alpha jianzhi
                                    if (count > 1)//生成多于一个第二层节点时要进行剪枝
                                    {//第二层新值比根节点参考值要小，就不算这波第二层了
                                        if (changescore2 < score)
                                        {
                                            tag3 = 1;
                                            break;
                                        }
                                    }
                                    #endregion
                                    if (count1 == 1)//生成第一个第二层节点时，记录参考分数，小于它再更改
                                    {
                                        changescore3 = changescore2;
                                    }//changescore3 是第一层节点最终的分数值
                                    else
                                    {
                                        if (changescore2 < changescore3)//新的值小于参考值
                                        {
                                            changescore3 = changescore2;//新的值赋给参考值
                                        }//参考值是结果
                                    }


                                }//if (tempchessBoard[m, n] == Empty)
                            }//for n
                        }//for m
                        #endregion
                        updateScore(index - 1 - count1, changescore3);//更新第一层节点(其父节点)的分数值
                        if (count == 1)//记录参考值 大于它再更改，更改同时要更新节点的index
                        {
                            score = changescore3;
                        }
                        else
                        {
                            if (changescore3 > score)
                            {
                                score = changescore3;
                                indexWEWANT = index - 1 - count1; //找到这个第一层节点的index值
                            }//score是最后的分数
                        }
                    }//if (tempchessBoard[i, j] == Empty)
                }//for j
            }//for i
            #endregion
            updateScore(0, score);
            copyFromTreetoTmp(indexWEWANT);//至此为止，tmp中存放的是最后要走的那步棋，要将它放到chessBoard中，放入everystep中，画图就好了
        }
    



        #endregion

        private int judgeJINSHOU(int i,int j)//判断禁手
        {
            int result = 0;
            Test.Program test = new Test.Program();
            for (int a = 0; a < 15; a++)
                for (int b = 0; b < 15; b++)
                    test.tempchessBoard[a, b] = tempchessBoard[a, b];
            result = test.ban(i, j);
            return result;
        }
    }
}

namespace Test
{
    class Program
    {
        public const int Black = 1;
        public const int White = 0;
        public const int Empty = 2;
        public int up;
        public int down;
        public int left;
        public int right;
        public int leftUp;
        public int leftDown;
        public int rightUp;
        public int rightDown;
        public int[,] tempchessBoard = new int[15, 15];

        public int ban(int i, int j)
        {
            up = down = left = right = leftUp = leftDown = rightUp = rightDown = 0;
            int oneSix = notBanJudge(i, j, 1);
            if (oneSix == 1)
            {
                int twoSix = notBanJudge(i, j, 2);
                if (twoSix == 1)
                {
                    int threeSix = notBanJudgeFour(i, j, 3);
                    {
                        if (threeSix == 1)
                        {
                            return (1);
                        }
                    }
                }
            }
            int specialBan = banSpeFour(i, j);
            if (specialBan == 1)
            {
                return (1);
            }
            int anospecialBan = anoBanSpeFour(i, j);
            if (anospecialBan == 1)
            {
                return (1);
            }
            int oneSameThr = banJudgeSame(i, j, 1);
            if (oneSameThr == 1)
            {
                int twoSameThr = banJudgeSame(i, j, 2);
                if (twoSameThr == 1)
                {
                    return (1);
                }
            }
            int oneThr = banJudge(i, j, 1);
            if (oneThr == 1)
            {
                return (1);
            }
            else
            {
                int oneFour = notBanJudge(i, j, 1);
                if (oneFour == 1)
                {
                    int twoFour = notBanJudgeFour(i, j, 2);
                    if (twoFour == 1)
                    {
                        return (1);
                    }
                }
            }
            return (0);
        }

        public int banJudgeSame(int i, int j, int k)
        {
            int m = k - 1;
            if (up == m)
            {
                if (((i - k) > 0) && tempchessBoard[(i - k), j] == Black)
                {
                    up++;
                }
                else
                {
                    up = 0;
                }
            }

            if (down == m)
            {
                if (((i + k) < 14) && tempchessBoard[(i + k), j] == Black)
                {
                    down++;
                }
                else
                {
                    down = 0;
                }
            }

            if (left == m)
            {
                if (((j - k) > 0) && tempchessBoard[i, (j - k)] == Black)
                {
                    left++;
                }
                else
                {
                    left = 0;
                }
            }

            if (right == m)
            {
                if (((j + k) < 14) && tempchessBoard[i, (j + k)] == Black)
                {
                    right++;
                }
                else
                {
                    right = 0;
                }
            }

            if (leftUp == m)
            {
                if (((i - k) > 0) && ((j - k) > 0) && tempchessBoard[(i - k), (j - k)] == Black)
                {
                    leftUp++;
                }
                else
                {
                    leftUp = 0;
                }
            }

            if (leftDown == m)
            {
                if (((i + k) < 14) && ((j - k) > 0) && tempchessBoard[(i + k), (j - k)] == Black)
                {
                    leftDown++;
                }
                else
                {
                    leftDown = 0;
                }
            }

            if (rightUp == m)
            {
                if (((i - k) > 0) && ((j + k) < 14) && tempchessBoard[(i - k), (j + k)] == Black)
                {
                    rightUp++;
                }
                else
                {
                    rightUp = 0;
                }
            }

            if (rightDown == m)
            {
                if (((i + k) < 14) && ((j + k) < 14) && tempchessBoard[(i + k), (j + k)] == Black)
                {
                    rightDown++;
                }
                else
                {
                    rightDown = 0;
                }
            }

            if ((up + down + left + right + leftDown + leftUp + rightDown + rightUp) >= 2 * k)
            {
                return (1);
            }
            return (0);


        }

        public int notBanJudgeFour(int i, int j, int k)
        {
            int m = k - 1;
            if (up == m)
            {
                if ((i - k) >= 0 && tempchessBoard[i - k, j] == Black)
                {
                    up++;
                }
            }

            if (down == m)
            {
                if ((i + k <= 14) && tempchessBoard[i + k, j] == Black)
                {
                    down++;
                }
            }

            if (left == m)
            {
                if ((j - k >= 0) && tempchessBoard[i, j - k] == Black)
                {
                    left++;
                }
            }

            if (right == m)
            {
                if ((j + k <= 14) && tempchessBoard[i, j + k] == Black)
                {
                    right++;
                }
            }

            if (leftUp == m)
            {
                if ((i - k >= 14) && (j - k >= 0) && tempchessBoard[i - k, j - k] == Black)
                {
                    leftUp++;
                }
            }

            if (leftDown == m)
            {
                if ((i + k <= 14) && (j - k >= 0) && tempchessBoard[i + k, j - k] == Black)
                {
                    leftDown++;
                }
            }

            if (rightUp == m)
            {
                if ((i - k >= 0) && (j + k <= 14) && tempchessBoard[i - k, j + k] == Black)
                {
                    rightUp++;
                }
            }

            if (rightDown == m)
            {
                if ((i + k <= 14) && (j + k <= 14) && tempchessBoard[i + k, j + k] == Black)
                {
                    rightDown++;
                }
            }

            if (((up + down) == (2 * k - 1)) || ((left + right) == (2 * k - 1)) || ((leftUp + rightDown) == (2 * k - 1)) || ((rightUp + leftDown) == (2 * k - 1)))
            {
                return (1);
            }
            return (0);
        }

        public int banJudge(int i, int j, int k)
        {
            int m = k - 1;
            if (up == m)
            {
                if ((i - k > 0) && tempchessBoard[i - k, j] == Black)
                {
                    up++;
                }
            }

            if (down == m)
            {
                if ((i + k < 14) && tempchessBoard[i + k, j] == Black)
                {
                    down++;
                }
            }

            if (left == m)
            {
                if ((j - k > 0) && tempchessBoard[i, j - k] == Black)
                {
                    left++;
                }
            }

            if (right == m)
            {
                if ((j + k < 14) && tempchessBoard[i, j + k] == Black)
                {
                    right++;
                }
            }

            if (leftUp == m)
            {
                if ((i - k > 0) && (j - k > 0) && tempchessBoard[i - k, j - k] == Black)
                {
                    leftUp++;
                }
            }

            if (leftDown == m)
            {
                if ((i + k < 14) && (j - k > 0) && tempchessBoard[i + k, j - k] == Black)
                {
                    leftDown++;
                }
            }

            if (rightUp == m)
            {
                if ((i - k > 0) && (j + k < 14) && tempchessBoard[i - k, j + k] == Black)
                {
                    rightUp++;
                }
            }

            if (rightDown == m)
            {
                if ((i + k < 14) && (j + k < 14) && tempchessBoard[i + k, j + k] == Black)
                {
                    rightDown++;
                }
            }

            if (((up + down) == 2 * k) || ((left + right) == 2 * k) || ((leftUp + rightDown) == 2 * k) || ((rightUp + leftDown) == 2 * k))
            {
                return (1);
            }
            return (0);
        }

        public int notBanJudge(int i, int j, int k)
        {
            int m = k - 1;
            if (up == m)
            {
                if ((i - k >= 14) && tempchessBoard[i - k, j] == Black)
                {
                    up++;
                }
            }

            if (down == m)
            {
                if ((i + k <= 14) && tempchessBoard[i + k, j] == Black)
                {
                    down++;
                }
            }

            if (left == m)
            {
                if ((j - k >= 0) && tempchessBoard[i, j - k] == Black)
                {
                    left++;
                }
            }

            if (right == m)
            {
                if ((j + k <= 14) && tempchessBoard[i, j + k] == Black)
                {
                    right++;
                }
            }

            if (leftUp == m)
            {
                if ((i - k >= 14) && (j - k >= 0) && tempchessBoard[i - k, j - k] == Black)
                {
                    leftUp++;
                }
            }

            if (leftDown == m)
            {
                if ((i + k <= 14) && (j - k >= 0) && tempchessBoard[i + k, j - k] == Black)
                {
                    leftDown++;
                }
            }

            if (rightUp == m)
            {
                if ((i - k >= 14) && (j + k <= 14) && tempchessBoard[i - k, j + k] == Black)
                {
                    rightUp++;
                }
            }

            if (rightDown == m)
            {
                if ((i + k <= 14) && (j + k <= 14) && tempchessBoard[i + k, j + k] == Black)
                {
                    rightDown++;
                }
            }

            if (((up + down) == (2 * k)) || ((left + right) == (2 * k)) || ((leftUp + rightDown) == (2 * k)) || ((rightUp + leftDown) == (2 * k)))
            {
                return (1);
            }
            return (0);

        }

        public int banSpeFour(int i, int j)
        {
            if (((j - 1) >= 0) && tempchessBoard[i, j - 1] == Black)
            {
                if (((j - 2) >= 0) && ((j + 1) <= 14) && tempchessBoard[i, j - 2] == Empty && tempchessBoard[i, j + 1] == Empty)
                {
                    if ((j - 3) >= 0 && ((j - 4) >= 0) && ((j + 2) <= 14) && ((j + 3) <= 14) && tempchessBoard[i, j - 3] == Black && tempchessBoard[i, j - 4] == Black && tempchessBoard[i, j + 2] == Black && tempchessBoard[i, j + 3] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((j + 1) <= 14) && tempchessBoard[i, j + 1] == Black)
            {
                if (((j + 2) <= 14) && ((j - 1) >= 0) && tempchessBoard[i, j + 2] == Empty && tempchessBoard[i, j - 1] == Empty)
                {
                    if (((j + 3) <= 14) && ((j + 4) <= 14) && ((j - 2) >= 0) && ((j - 3) >= 0) && tempchessBoard[i, j + 3] == Black && tempchessBoard[i, j + 4] == Black && tempchessBoard[i, j - 2] == Black && tempchessBoard[i, j - 3] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((i + 1) <= 14) && tempchessBoard[i + 1, j] == Black)
            {
                if (((i + 2) <= 14) && ((i - 1) >= 0) && tempchessBoard[i + 2, j] == Empty && tempchessBoard[i - 1, j] == Empty)
                {
                    if (((i + 3) <= 14) && ((i + 4) <= 14) && ((i - 2) >= 0) && ((i - 3) >= 0) && tempchessBoard[i + 3, j] == Black && tempchessBoard[i + 4, j] == Black && tempchessBoard[i - 2, j] == Black && tempchessBoard[i - 3, j] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((i - 1) >= 0) && tempchessBoard[i - 1, j] == Black)
            {
                if (((i - 2) >= 0) && ((i + 1) <= 14) && tempchessBoard[i - 2, j] == Empty && tempchessBoard[i + 1, j] == Empty)
                {
                    if (((i - 3) >= 0) && ((i - 4) >= 0) && ((i + 2) <= 14) && ((i + 3) <= 14) && tempchessBoard[i - 3, j] == Black && tempchessBoard[i - 4, j] == Black && tempchessBoard[i + 2, j] == Black && tempchessBoard[i + 3, j] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((i - 1) >= 0) && ((j - 1) >= 0) && tempchessBoard[i - 1, j - 1] == Black)
            {
                if (((i - 2) >= 0) && ((i + 1) <= 14) && ((j - 2) >= 0) && ((j + 1) <= 14) && tempchessBoard[i - 2, j - 2] == Empty && tempchessBoard[i + 1, j + 1] == Empty)
                {
                    if (((i - 3) >= 0) && ((i - 4) >= 0) && ((i + 2) <= 14) && ((i + 3) <= 14) && ((j - 3) >= 0) && ((j - 4) >= 0) && ((j + 2) <= 14) && ((j + 3) <= 14) && tempchessBoard[i - 3, j - 3] == Black && tempchessBoard[i - 4, j - 4] == Black && tempchessBoard[i + 2, j + 2] == Black && tempchessBoard[i + 3, j + 3] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((i + 1) <= 14) && ((j + 1) <= 14) && tempchessBoard[i + 1, j + 1] == Black)
            {
                if (((i + 2) <= 14) && ((i - 1) >= 0) && ((j + 2) <= 14) && ((j - 1) >= 0) && tempchessBoard[i + 2, j + 2] == Empty && tempchessBoard[i - 1, j - 1] == Empty)
                {
                    if (((i + 3) <= 14) && ((i + 4) <= 14) && ((i - 2) >= 0) && ((i - 3) >= 0) && ((j + 3) <= 14) && ((j + 4) <= 14) && ((j - 2) >= 0) && ((j - 3) >= 0) && tempchessBoard[i + 3, j + 3] == Black && tempchessBoard[i + 4, j + 4] == Black && tempchessBoard[i - 2, j - 2] == Black && tempchessBoard[i - 3, j - 3] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((i - 1) >= 0) && ((j + 1) <= 14) && tempchessBoard[i - 1, j + 1] == Black)
            {
                if (((i + 1) <= 14) && ((i - 2) >= 0) && ((j + 2) <= 14) && ((j - 1) >= 0) && tempchessBoard[i - 2, j + 2] == Empty && tempchessBoard[i + 1, j - 1] == Empty)
                {
                    if (((i + 2) <= 14) && ((i + 3) <= 14) && ((i - 3) >= 0) && ((i - 4) >= 0) && ((j + 3) <= 14) && ((j + 4) <= 14) && ((j - 2) >= 0) && ((j - 3) >= 0) && tempchessBoard[i - 3, j + 3] == Black && tempchessBoard[i - 4, j + 4] == Black && tempchessBoard[i + 2, j - 2] == Black && tempchessBoard[i + 3, j - 3] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((i + 1) <= 14) && ((j - 1) >= 0) && tempchessBoard[i + 1, j - 1] == Black)
            {
                if (((j - 2) >= 0) && ((j + 1) <= 14) && ((i - 1) >= 0) && ((i + 2) <= 14) && tempchessBoard[i - 1, j + 1] == Empty && tempchessBoard[i + 2, j - 2] == Empty)
                {
                    if (((i - 3) >= 0) && ((i - 4) >= 0) && ((i + 2) <= 14) && ((i + 3) <= 14) && ((j - 3) >= 0) && ((j - 4) >= 0) && ((j + 2) <= 14) && ((j + 3) <= 14) && tempchessBoard[i - 2, j + 2] == Black && tempchessBoard[i - 3, j + 3] == Black && tempchessBoard[i + 3, j - 3] == Black && tempchessBoard[i + 4, j - 4] == Black)
                    {
                        return (1);
                    }
                }
            }
            return (0);
        }

        public int anoBanSpeFour(int i, int j)
        {
            if (((i - 1) >= 0) && ((i + 1) <= 14) && tempchessBoard[i - 1, j] == Black && tempchessBoard[i + 1, j] == Black)
            {
                if (((i - 2) >= 0) && ((i + 2) <= 14) && tempchessBoard[i - 2, j] == Empty && tempchessBoard[i + 2, j] == Empty)
                {
                    if (((i - 3) >= 0) && ((i + 3) <= 14) && tempchessBoard[i - 3, j] == Black && tempchessBoard[i + 3, j] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((j - 1) >= 0) && ((j + 1) <= 14) && tempchessBoard[i, j - 1] == Black && tempchessBoard[i, j + 1] == Black)
            {
                if (((j - 2) >= 0) && ((j + 2) <= 14) && tempchessBoard[i, j - 2] == Empty && tempchessBoard[i, j + 2] == Empty)
                {
                    if (((j - 3) >= 0) && ((j + 3) <= 14) && tempchessBoard[i, j - 3] == Black && tempchessBoard[i, j + 3] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((i - 1) >= 0) && ((i + 1) <= 14) && ((j - 1) >= 0) && ((j + 1) <= 14) && tempchessBoard[i - 1, j - 1] == Black && tempchessBoard[i + 1, j + 1] == Black)
            {
                if (((i - 2) >= 0) && ((i + 2) <= 14) && ((j - 2) >= 0) && ((j + 2) <= 14) && tempchessBoard[i - 2, j - 2] == Empty && tempchessBoard[i + 2, j + 2] == Empty)
                {
                    if (((i - 3) >= 0) && ((i + 3) <= 14) && ((j - 3) >= 0) && ((j + 3) <= 14) && tempchessBoard[i - 3, j - 3] == Black && tempchessBoard[i + 3, j + 3] == Black)
                    {
                        return (1);
                    }
                }
            }
            if (((i - 1) >= 0) && ((i + 1) <= 14) && ((j - 1) >= 0) && ((j + 1) <= 14) && tempchessBoard[i - 1, j + 1] == Black && tempchessBoard[i + 1, j - 1] == Black)
            {
                if (((i - 2) >= 0) && ((i + 2) <= 14) && ((j - 2) >= 0) && ((j + 2) <= 14) && tempchessBoard[i - 2, j + 2] == Empty && tempchessBoard[i + 2, j - 2] == Empty)
                {
                    if (((i - 3) >= 0) && ((i + 3) <= 14) && ((j - 3) >= 0) && ((j + 3) <= 14) && tempchessBoard[i - 3, j + 3] == Black && tempchessBoard[i + 3, j - 3] == Black)
                    {
                        return (1);
                    }
                }
            }
            return (0);
        }





    }
}


namespace ConsoleApplication2
{
    class Program
    {
        
        public const int HUO1 = 20;
        public const int HUO2 = 400;
        public const int HUO3 = 6000;
        public const int HUO4 = 20000;
        public const int SI1 = 4;
        public const int SI2 = 90;
        public const int SI3 = 800;
        public const int SI4 = 10000;
        public const int WULIAN = 50000;
        /*
        public const int HUO1 = 20;
        public const int HUO2 = 4000;
        public const int HUO3 = 80000;
        public const int HUO4 = 100000;
        public const int SI1 = 20;
        public const int SI2 = 500;
        public const int SI3 = 5000;
        public const int SI4 = 100000;
        public const int WULIAN = 50000;
        */


        
        
        public const int White = 0;
        public const int Black = 1;
        public const int Empty = 2;
        public int[,] tempchessBoard = new int[15, 15];
        public int[] BlackHuoZi = new int[4] { 0, 0, 0, 0 };//HuoZi[0]表示活二数目，HuoZi[1]表示活三数目，HuoZi[2]表示活四数目
        public int[] BlackSiZi = new int[4] { 0, 0, 0, 0 };
        public int BlackWuLian;
        public int[] WhiteHuoZi = new int[4] { 0, 0, 0, 0 };//HuoZi[0]表示活二数目，HuoZi[1]表示活三数目，HuoZi[2]表示活四数目
        public int[] WhiteSiZi = new int[4] { 0, 0, 0, 0 };
        public int WhiteWuLian;
        //BlackSiZi[0]表示死一，BlackSiZi[1]表示死二，BlackSiZi[2]表示死三，BlackSiZi[3]表示死四
        //judging the number of HUO ZI situation
        public void BlackHuoZiPanDing(int u, int d, int l, int r)
        {
            //数组BlackHuoZi初始化归零
            BlackHuoZi[0] = BlackHuoZi[1] = BlackHuoZi[2] = BlackHuoZi[3] = 0;
            BlackSiZi[0] = BlackSiZi[1] = BlackSiZi[2] = BlackSiZi[3] = 0;
            BlackWuLian = 0;
            //int u, d, l, r;
            int i = 0, j = 0;
            int countWuLian = 0;
            int countRows1 = 0, countRows2 = 0, countRows3 = 0, countRows4 = 0;
            int countRS1 = 0, countRS2 = 0, countRS3 = 0, countRS4 = 0;
            for (i = u; i <= d; i++)
            {
                for (j = l+1; j <= r; j++)
                {
                    if (tempchessBoard[i, j - 1] == Empty)
                    {
                        if (tempchessBoard[i, j] == Black)
                        {
                            if ((j + 1 <= r) && tempchessBoard[i, j + 1] == Black)
                            {
                                if ((j + 2 <= r) && tempchessBoard[i, j + 2] == Black)
                                {
                                    if ((j + 3 <= r) && tempchessBoard[i, j + 3] == Black)
                                    {//活四&死四&五连
                                        if ((j + 4 <= r) && tempchessBoard[i, j + 4] == Empty)
                                            countRows4++;
                                        if ((j + 4 <= r) &&　tempchessBoard[i, j + 4] == White)
                                            countRS4++;
                                        if ((j + 4 <= r) && tempchessBoard[i, j + 4] == Black)
                                            countWuLian++;
                                        if (j + 4 == 15)
                                            countRS4++;
                                    }
                                    else//活三&死三
                                    {
                                        if ((j + 3 <= r) && tempchessBoard[i, j + 3] == Empty)
                                            countRows3++;
                                        if ((j + 3 <= r) && tempchessBoard[i, j + 3] == White)
                                            countRS3++;
                                        if (j + 3 == 15)
                                            countRS3++;
                                    }
                                }
                                else//活二&死二
                                {
                                    if ((j + 2 <= r) && tempchessBoard[i, j + 2] == Empty)
                                        countRows2++;
                                    if ((j + 2 <= r) && tempchessBoard[i, j + 2] == White)
                                        countRS2++;
                                    if (j + 2 == 15)
                                        countRS2++;
                                }
                            }
                            else//活一&死一
                            {
                                if ((j + 1 <= r) && tempchessBoard[i, j + 1] == Empty)
                                    countRows1++;
                                if ((j + 1 <= r) && tempchessBoard[i, j + 1] == White)
                                    countRS1++;
                                if (j == 14)
                                    countRS1++;
                            }
                        }
                    }
                    if (tempchessBoard[i, j - 1] == White)
                    {
                        if (tempchessBoard[i, j] == Black)
                        {
                            if ((j + 1 <= r) && tempchessBoard[i, j + 1] == Black)
                            {
                                if ((j + 2 <= r) && tempchessBoard[i, j + 2] == Black)
                                {
                                    if ((j + 3 <= r) && tempchessBoard[i, j + 3] == Black)
                                    {
                                        if ((j + 4 <= r) && tempchessBoard[i, j + 4] == Empty)
                                            countRS4++;
                                    }
                                    else{
                                        if ((j + 3 <= r) && tempchessBoard[i, j + 3] == Empty)
                                            countRS3++;
                                    }
                                }
                                else{
                                    if ((j + 2 <= r) && tempchessBoard[i, j + 2] == Empty)
                                        countRS2++;
                                }
                            }
                            else{
                                if ((j + 1 <= r) && tempchessBoard[i, j + 1] == Empty)
                                    countRS1++;
                            }
                        }
                    }
                }
            }
            BlackHuoZi[1] += countRows2; BlackSiZi[1] += countRS2;
            BlackHuoZi[2] += countRows3; BlackSiZi[2] += countRS3;
            BlackHuoZi[3] += countRows4; BlackSiZi[3] += countRS4;
            BlackHuoZi[0] += countRows1; BlackSiZi[0] += countRS1;
            BlackWuLian += countWuLian;
            //Console.WriteLine("一：Rows:");
            //Console.Write("Rows2:{0}        ", countRows2);
            //Console.Write("Rows3:{0}        ", countRows3);
            //Console.WriteLine("Rows4:{0}", countRows4);
            //Console.Write("Si2:{0}    ", BlackSiZi[1]); Console.Write("Si3:{0}    ", BlackSiZi[2]); Console.WriteLine("Si4:{0}    ", BlackSiZi[3]);
            i = j = 0;
            countRS1 = countRS2 = countRS3 = countRS4 = 0;
            countWuLian = 0;
            int countCloumns1 = 0, countCloumns2 = 0, countCloumns3 = 0, countCloumns4 = 0;
            for (j = l; j <= r; j++)
            {
                for (i = u+1; i <= d; i++)
                {
                    if (tempchessBoard[i - 1, j] == Empty)
                    {
                        if (tempchessBoard[i, j] == Black)
                        {
                            if ((i + 1 <= d) && tempchessBoard[i + 1, j] == Black)
                            {
                                if ((i + 2 <= d) && tempchessBoard[i + 2, j] == Black)
                                {
                                    if ((i + 3 <= d) && tempchessBoard[i + 3, j] == Black)
                                    {//活四&死四&五连
                                        if ((i + 4 <= d) && tempchessBoard[i + 4, j] == Empty)
                                            countCloumns4++;
                                        if ((i + 4 <= d) && tempchessBoard[i + 4, j] == White)
                                            countRS4++;
                                        if ((i + 4 <= d) && tempchessBoard[i + 4, j] == Black)
                                            countWuLian++;
                                        if ((i + 4 == 15))
                                            countRS4++;
                                    }
                                    else//活三&死三
                                    {
                                        if ((i + 3 <= d) && tempchessBoard[i + 3, j] == Empty)
                                            countCloumns3++;
                                        if ( (i + 3 <= d) && tempchessBoard[i + 3, j] == White)
                                            countRS3++;
                                        if (i + 3 == 15)
                                            countRS3++;
                                    }
                                }
                                else//活二&死二
                                {
                                    if ((i + 2 <= d) && tempchessBoard[i + 2, j] == Empty)
                                        countCloumns2++;
                                    if ((i + 2 <= d) && tempchessBoard[i + 2, j] == White)
                                        countRS2++;
                                    if (i + 2 == 15)
                                        countRS2++;
                                }
                            }
                            else//活一&死一
                            {
                                if ((i + 1 <= d) && tempchessBoard[i + 1, j] == Empty)
                                    countCloumns1++;
                                if ((i + 1 <= d) && tempchessBoard[i + 1, j] == White)
                                    countRS1++;
                                if (i + 1 == 15)
                                    countRS1++;
                            }
                        }               
                    }
                    if (tempchessBoard[i - 1, j] == White)
                    {
                        if (tempchessBoard[i, j] == Black)
                        {
                            if ((i + 1 <= d) && tempchessBoard[i + 1, j] == Black)
                            {
                                if ((i + 2 <= d) && tempchessBoard[i + 2, j] == Black)
                                {
                                    if ((i + 3 <= d) && tempchessBoard[i + 3, j] == Black)
                                    {
                                        if ((i + 4 <= d) && tempchessBoard[i + 4, j] == Empty)
                                            countRS4++;
                                    }
                                    else{
                                        if ((i + 3 <= d) && tempchessBoard[i + 3, j] == Empty)
                                            countRS3++;
                                    }
                                }
                                else{
                                    if ((i + 2 <= d) && tempchessBoard[i + 2, j] == Empty)
                                        countRS2++;
                                }
                            }
                            else{
                                if ((i + 1 <= d) && tempchessBoard[i + 1, j] == Empty)
                                    countRS1++;
                            }
                        }   
                    }
                }
            }
            BlackHuoZi[1] += countCloumns2; BlackSiZi[1] += countRS2;
            BlackHuoZi[2] += countCloumns3; BlackSiZi[2] += countRS3;
            BlackHuoZi[3] += countCloumns4; BlackSiZi[3] += countRS4;
            BlackHuoZi[0] += countCloumns1; BlackSiZi[0] += countRS1;
            BlackWuLian += countWuLian;
            //Console.WriteLine("二：Cloumns:");
            //Console.Write("Cloumns2:{0}     ", countCloumns2);
            //Console.Write("Cloumns3:{0}     ", countCloumns3);
            //Console.WriteLine("Cloumns4:{0}", countCloumns4);
            //Console.Write("Si2:{0}    ", BlackSiZi[1]); Console.Write("Si3:{0}    ", BlackSiZi[2]); Console.WriteLine("Si4:{0}    ", BlackSiZi[3]);
            //判定斜向活子情况
            //从右上到左下->左斜
            i = j = 0;
            countRS1 = countRS2 = countRS3 = countRS4 = 0;
            countWuLian = 0;
            int countLeftXie1, countLeftXie2, countLeftXie3, countLeftXie4;
            countLeftXie1 = countLeftXie2 = countLeftXie3 = countLeftXie4 = 0;
            for (i = u+1; i <= d-1; i++)
            {
                for (j = l+1; j <= r-1; j++)
                {
                    if (tempchessBoard[i - 1, j + 1] == Empty)
                    {
                        if (tempchessBoard[i, j] == Black)
                        {
                            if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == Black)
                            {
                                if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == Black)
                                {
                                    if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == Black)
                                    {//活四&死四&五连
                                        if ((i + 4 <= d) && (j - 4 >= l) && tempchessBoard[i + 4, j - 4] == Empty)
                                            countLeftXie4++;
                                        if ((i + 4 <= d) && (j - 4 >= l) && tempchessBoard[i + 4, j - 4] == White)
                                            countRS4++;
                                        if ((i + 4 <= d) && (j - 4 >= l) && tempchessBoard[i + 4, j - 4] == Black)
                                            countWuLian++;
                                        if ((i + 4 == 15) || (j - 4 == -1))
                                            countRS4++;
                                    }
                                    else//活三&死三
                                    {
                                        if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == Empty)
                                            countLeftXie3++;
                                        if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == White)
                                            countRS3++;
                                        if ((i + 3 == 15) || (j - 3 == -1))
                                            countRS3++;
                                    }
                                }
                                else//活二&死二
                                {
                                    if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == Empty)
                                        countLeftXie2++;
                                    if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == White)
                                        countRS2++;
                                    if ((i + 2 == 15) || (j - 2 == -1))
                                        countRS2++;
                                }
                            }
                            else//活一&死一
                            {
                                if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == Empty)
                                    countLeftXie1++;
                                if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == White)
                                    countRS1++;
                                if ((i + 1 == 15) || (j - 1 == -1))
                                    countRS1++; 
                            }
                        }                
                    }
                    if (tempchessBoard[i - 1, j + 1] == White)
                    {
                        if (tempchessBoard[i, j] == Black)
                        {
                            if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == Black)
                            {
                                if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == Black)
                                {
                                    if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == Black)
                                    {
                                        if ((i + 4 <= d) && (j - 4 >= l) && tempchessBoard[i + 4, j - 4] == Empty)
                                            countRS4++;
                                    }
                                    else{
                                        if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == Empty)
                                            countRS3++;
                                    }
                                }
                                else{
                                    if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == Empty)
                                        countRS2++;
                                }
                            }
                            else{
                                if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == Empty)
                                    countRS1++;
                            }
                        }
                    }
                }
            }
            BlackHuoZi[1] += countLeftXie2; BlackSiZi[1] += countRS2;
            BlackHuoZi[2] += countLeftXie3; BlackSiZi[2] += countRS3;
            BlackHuoZi[3] += countLeftXie4; BlackSiZi[3] += countRS4;
            BlackHuoZi[0] += countLeftXie1; BlackSiZi[0] += countRS1;
            BlackWuLian += countWuLian;
            //Console.WriteLine("三：Left:");
            //Console.Write("LeftXie2:{0}     ", countLeftXie2);
            //Console.Write("LeftXie3:{0}     ", countLeftXie3);
            //Console.WriteLine("LeftXie4:{0}", countLeftXie4);
            //Console.Write("Si2:{0}    ", BlackSiZi[1]); Console.Write("Si3:{0}    ", BlackSiZi[2]); Console.WriteLine("Si4:{0}    ", BlackSiZi[3]);
            //从左上到右下->右斜
            i = j = 0;
            countRS1 = countRS2 = countRS3 = countRS4 = 0;
            countWuLian = 0;
            int countRightXie1, countRightXie2, countRightXie3, countRightXie4;
            countRightXie1 = countRightXie2 = countRightXie3 = countRightXie4 = 0;
            for (i = u+1; i <= d-1; i++)
            {
                for (j = l+1; j <= r-1; j++)
                {
                    if (tempchessBoard[i - 1, j - 1] == Empty)
                    {
                        if (tempchessBoard[i, j] == Black)
                        {
                            if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == Black)
                            {
                                if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == Black)
                                {
                                    if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == Black)
                                    {//活四&死四&五连
                                        if ((i + 4 <= d) && (j + 4 <= r) && tempchessBoard[i + 4, j + 4] == Empty)
                                            countRightXie4++;
                                        if ((i + 4 <= d) && (j + 4 <= r) && tempchessBoard[i + 4, j + 4] == White)
                                            countRS4++;
                                        if ((i + 4 <= d) && (j + 4 <= r) && tempchessBoard[i + 4, j + 4] == Black)
                                            countWuLian++;
                                        if ((i + 4 == 15) || (j + 4 == 15))
                                            countRS4++;
                                    }
                                    else//活三&死三
                                    {
                                        if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == Empty)
                                            countRightXie3++;
                                        if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == White)
                                            countRS3++;
                                        if ((i + 3 == 15) || (j + 3 == 15))
                                            countRS3++;
                                    }
                                }
                                else//活二&死二
                                {
                                    if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == Empty)
                                        countRightXie2++;
                                    if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == White)
                                        countRS2++;
                                    if ((i + 2 == 15) || (j + 2 == 15))
                                        countRS2++;
                                }
                            }
                            else//活一&死一
                            {
                                if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == Empty)
                                    countRightXie1++;
                                if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == White)
                                    countRS1++;
                                if ((i + 1 == 15) || (j + 1 == 15))
                                    countRS1++;
                            }
                        }                
                    }
                    if (tempchessBoard[i - 1, j - 1] == White)
                    {
                        if (tempchessBoard[i, j] == Black)
                        {
                            if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == Black)
                            {
                                if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == Black)
                                {
                                    if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == Black)
                                    {
                                        if ((i + 4 <= d) && (j + 4 <= r) && tempchessBoard[i + 4, j + 4] == Empty)
                                            countRS4++;
                                    }
                                    else
                                    {
                                        if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == Empty)
                                            countRS3++;
                                    }
                                }
                                else
                                {
                                    if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == Empty)
                                        countRS2++;
                                }
                            }
                            else
                            {
                                if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == Empty)
                                    countRS1++;
                            }
                        }                
                    }
                }
            }
            BlackHuoZi[1] += countRightXie2; BlackSiZi[1] += countRS2;
            BlackHuoZi[2] += countRightXie3; BlackSiZi[2] += countRS3;
            BlackHuoZi[3] += countRightXie4; BlackSiZi[3] += countRS4;
            BlackHuoZi[0] += countRightXie1; BlackSiZi[0] += countRS1;
            BlackWuLian += countWuLian;
            //Console.WriteLine("四：Right:");
            //Console.Write("RightXie2:{0}    ", countRightXie2);
            //Console.Write("RightXie3:{0}    ", countRightXie3);
            //Console.WriteLine("RightXie4:{0}", countRightXie4);
            //Console.Write("Si2:{0}    ", BlackSiZi[1]); Console.Write("Si3:{0}    ", BlackSiZi[2]); Console.WriteLine("Si4:{0}    ", BlackSiZi[3]);
            //------------------------------------------------------------
            i = j = 0;
            countRS1 = countRS2 = countRS3 = countRS4 = 0;
            countWuLian = 0;
            for(i = 0; i <= 14; i++)
            {
                //Rows
                j = 0;
                if (tempchessBoard[i,j] == Black)
                {
                    if (tempchessBoard[i, j + 1] == Black)
                    {
                        if (tempchessBoard[i, j + 2] == Black)
                        {
                            if (tempchessBoard[i, j + 3] == Black)
                            {
                                if(tempchessBoard[i, j + 4] == Empty)
                                    countRS4++;
                            }
                            else
                            {
                                if (tempchessBoard[i, j + 3] == Empty)
                                    countRS3++;
                            }
                        }
                        else
                        {
                            if (tempchessBoard[i, j + 2] == Empty)
                                countRS2++;
                        }
                    }
                    else
                    {
                        if (tempchessBoard[i, j + 1] == Empty)
                            countRS1++;
                    }
                }
                j = 0;
                if (tempchessBoard[i,j] == Black)
                {
                    if ((i + 1 <= 14) && (j + 1 <= 14) && tempchessBoard[i + 1, j + 1] == Black)
                    {
                        if ((i + 2 <= 14) && (j + 2 <= 14) && tempchessBoard[i + 2, j + 2] == Black)
                        {
                            if ((i + 3 <= 14) && (j + 3 <= 14) && tempchessBoard[i + 3, j + 3] == Black)
                            {
                                if ((i + 4 <= 14) && (j + 4 <= 14) && tempchessBoard[i + 4, j + 4] == Empty)
                                    countRS4++;
                            }
                            else
                            {
                                if ((i + 3 <= 14) && (j + 3 <= 14) && tempchessBoard[i + 3, j + 3] == Empty)
                                    countRS3++;
                            }
                        }
                        else
                        {
                            if ((i + 2 <= 14) && (j + 2 <= 14) && tempchessBoard[i + 2, j + 2] == Empty)
                                countRS2++;
                        }
                    }
                    else
                    {
                        if ((i + 1 <= 14) && (j + 1 <= 14) && tempchessBoard[i + 1, j + 1] == Empty)
                            countRS1++;
                    }
                }     
            }
            i = j = 0;
            for (j = 0; j <= 14; j++)
            {
                //Cloumns
                i = 0;
                if (tempchessBoard[i, j] == Black)
                {
                    if (tempchessBoard[i + 1, j] == Black)
                    {
                        if (tempchessBoard[i + 2, j] == Black)
                        {
                            if (tempchessBoard[i + 3, j] == Black)
                            {
                                if (tempchessBoard[i + 4, j] == Empty)
                                    countRS4++;
                            }
                            else
                            {
                                if (tempchessBoard[i + 3, j] == Empty)
                                    countRS3++;
                            }
                        }
                        else
                        {
                            if (tempchessBoard[i + 2, j] == Empty)
                                countRS2++;
                        }
                    }
                    else
                    {
                        if (tempchessBoard[i + 1, j] == Empty)
                            countRS1++;
                    }
                }
                //LeftXie
                i = 0;
                if (tempchessBoard[i, j] == Black)
                {
                    if ((i - 1 >= 0) && (j - 1 >= 0) && tempchessBoard[i - 1, j - 1] == Black)
                    {
                        if ((i - 2 >= 0) && (j - 2 >= 0) && tempchessBoard[i - 2, j - 2] == Black)
                        {
                            if ((i - 3 >= 0) && (j - 3 >= 0) && tempchessBoard[i - 3, j - 3] == Black)
                            {
                                if ((i - 4 >= 0) && (j - 4 >= 0) && tempchessBoard[i - 4, j - 4] == Empty)
                                    countRS4++;
                            }
                            else
                            {
                                if ((i - 3 >= 0) && (j - 3 >= 0) && tempchessBoard[i - 3, j - 3] == Empty)
                                    countRS3++;
                            }
                        }
                        else
                        {
                            if ((i - 2 >= 0) && (j - 2 >= 0) && tempchessBoard[i - 2, j - 2] == Empty)
                                countRS2++;
                        }
                    }
                    else
                    {
                        if ((i - 1 >= 0) && (j - 1 >= 0) && tempchessBoard[i - 1, j - 1] == Empty)
                            countRS1++;
                    }
                }
            }
            BlackSiZi[0] += countRS1;
            BlackSiZi[1] += countRS2;
            BlackSiZi[2] += countRS3;
            BlackSiZi[3] += countRS4;
            for (i = 0; i <= 14; i++)
            {
                j = 0;
                if (tempchessBoard[i, j] == Black)
                {
                    if (tempchessBoard[i, j + 1] == Black)
                    {
                        if (tempchessBoard[i, j + 2] == Black)
                        {
                            if (tempchessBoard[i, j + 3] == Black)
                            {
                                if (tempchessBoard[i, j + 4] == Black)
                                    countWuLian++;
                            }
                        }
                    }
                }
                j = 0;
                if (tempchessBoard[i, j] == Black)
                {
                    if ((i + 1 <= 14) && tempchessBoard[i + 1, j + 1] == Black)
                    {
                        if ((i + 2 <= 14) && tempchessBoard[i + 2, j + 2] == Black)
                        {
                            if ((i + 3 <= 14) && tempchessBoard[i + 3, j + 3] == Black)
                            {
                                if ((i + 4 <= 14) && tempchessBoard[i + 4, j + 4] == Black)
                                    countWuLian++;
                            }
                        }
                    }
                }
                j = 14;
                if (tempchessBoard[i, j] == Black)
                {
                    if ((i - 1 >= 0) && tempchessBoard[i - 1, j - 1] == Black)
                    {
                        if ((i - 2 >= 0) && tempchessBoard[i - 2, j - 2] == Black)
                        {
                            if ((i - 3 >= 0) && tempchessBoard[i - 3, j - 3] == Black)
                            {
                                if ((i - 4 >= 0) && tempchessBoard[i - 4, j - 4] == Black)
                                    countWuLian++;
                            }
                        }
                    }
                }
            }
            for (j = 0; j <= 14; j++)
            {
                i = 0;
                if (tempchessBoard[i, j] == Black)
                {
                    if (tempchessBoard[i + 1, j] == Black)
                    {
                        if (tempchessBoard[i + 2, j] == Black)
                        {
                            if (tempchessBoard[i + 3, j] == Black)
                            {
                                if (tempchessBoard[i + 4, j] == Black)
                                    countWuLian++;
                            }
                        }
                    }
                }
            }
            BlackWuLian += countWuLian;
        }
        public int GuZhi(int u , int d, int l, int r)
        {
            BlackHuoZiPanDing(u, d, l, r);
            WhiteHuoZiPanDing(u, d, l, r);
            int sum = 0;
            int sumHuo = 0;
            int sumSi = 0;
            int whitesumHuo = 0;
            int whitesumSi = 0;
            sumHuo = HUO1 * BlackHuoZi[0] + HUO2 * BlackHuoZi[1] + HUO3 * BlackHuoZi[2] + HUO4 * BlackHuoZi[3];
            sumSi = SI1 * BlackSiZi[0] + SI2 * BlackSiZi[1] + SI3 * BlackSiZi[2] + SI4 * BlackSiZi[3];
            whitesumHuo = HUO1 * WhiteHuoZi[0] + HUO2 * WhiteHuoZi[1] + HUO3 * WhiteHuoZi[2] + HUO4 * WhiteHuoZi[3];
            whitesumSi = SI1 * WhiteSiZi[0] + SI2 * WhiteSiZi[1] + SI3 * WhiteSiZi[2] + SI4 * WhiteSiZi[3];
            sum = sumSi + sumHuo + WULIAN * BlackWuLian - (whitesumHuo*3 + whitesumSi + WULIAN * WhiteWuLian*3);
            return sum;
        }
        public int GuZhiWhite(int u, int d, int l, int r)
        {
            BlackHuoZiPanDing(u, d, l, r);
            WhiteHuoZiPanDing(u, d, l, r);
            int sum = 0;
            int sumHuo = 0;
            int sumSi = 0;
            int whitesumHuo = 0;
            int whitesumSi = 0;
            sumHuo = HUO1 * BlackHuoZi[0] + HUO2 * BlackHuoZi[1] + HUO3 * BlackHuoZi[2] + HUO4 * BlackHuoZi[3];
            sumSi = SI1 * BlackSiZi[0] + SI2 * BlackSiZi[1] + SI3 * BlackSiZi[2] + SI4 * BlackSiZi[3];
            whitesumHuo = HUO1 * WhiteHuoZi[0] + HUO2 * WhiteHuoZi[1] + HUO3 * WhiteHuoZi[2] + HUO4 * WhiteHuoZi[3];
            whitesumSi = SI1 * WhiteSiZi[0] + SI2 * WhiteSiZi[1] + SI3 * WhiteSiZi[2] + SI4 * WhiteSiZi[3];
            sum = whitesumHuo + whitesumSi + WULIAN * WhiteWuLian - (sumSi + sumHuo*3 + WULIAN * BlackWuLian*3);
            return sum;
        }
        public void WhiteHuoZiPanDing(int u, int d, int l, int r)
        {
            //数组BlackHuoZi初始化归零
            WhiteHuoZi[0] = WhiteHuoZi[1] = WhiteHuoZi[2] = WhiteHuoZi[3] = 0;
            WhiteSiZi[0] = WhiteSiZi[1] = WhiteSiZi[2] = WhiteSiZi[3] = 0;
            WhiteWuLian = 0;
            //int u, d, l, r;
            int i = 0, j = 0;
            int countWuLian = 0;
            int countRows1 = 0, countRows2 = 0, countRows3 = 0, countRows4 = 0;
            int countRS1 = 0, countRS2 = 0, countRS3 = 0, countRS4 = 0;
            for (i = u; i <= d; i++)
            {
                for (j = l + 1; j <= r; j++)
                {
                    if (tempchessBoard[i, j - 1] == Empty)
                    {
                        if (tempchessBoard[i, j] == White)
                        {
                            if ((j + 1 <= r) && tempchessBoard[i, j + 1] == White)
                            {
                                if ((j + 2 <= r) && tempchessBoard[i, j + 2] == White)
                                {
                                    if ((j + 3 <= r) && tempchessBoard[i, j + 3] == White)
                                    {//活四&死四&五连
                                        if ((j + 4 <= r) && tempchessBoard[i, j + 4] == Empty)
                                            countRows4++;
                                        if ((j + 4 <= r) && tempchessBoard[i, j + 4] == Black)
                                            countRS4++;
                                        if ((j + 4 <= r) && tempchessBoard[i, j + 4] == White)
                                            countWuLian++;
                                        if (j + 4 == 15)
                                            countRS4++;
                                    }
                                    else//活三&死三
                                    {
                                        if ((j + 3 <= r) && tempchessBoard[i, j + 3] == Empty)
                                            countRows3++;
                                        if ((j + 3 <= r) && tempchessBoard[i, j + 3] == Black)
                                            countRS3++;
                                        if (j + 3 == 15)
                                            countRS3++;
                                    }
                                }
                                else//活二&死二
                                {
                                    if ((j + 2 <= r) && tempchessBoard[i, j + 2] == Empty)
                                        countRows2++;
                                    if ((j + 2 <= r) && tempchessBoard[i, j + 2] == Black)
                                        countRS2++;
                                    if (j + 2 == 15)
                                        countRS2++;
                                }
                            }
                            else//活一&死一
                            {
                                if ((j + 1 <= r) && tempchessBoard[i, j + 1] == Empty)
                                    countRows1++;
                                if ((j + 1 <= r) && tempchessBoard[i, j + 1] == Black)
                                    ////////////////////////////////////////////////////////
                                    countRS1++;
                                if (j == 14)
                                    countRS1++;
                            }
                        }
                    }
                    if (tempchessBoard[i, j - 1] == Black)
                    {
                        if (tempchessBoard[i, j] == White)
                        {
                            if ((j + 1 <= r) && tempchessBoard[i, j + 1] == White)
                            {
                                if ((j + 2 <= r) && tempchessBoard[i, j + 2] == White)
                                {
                                    if ((j + 3 <= r) && tempchessBoard[i, j + 3] == White)
                                    {
                                        if ((j + 4 <= r) && tempchessBoard[i, j + 4] == Empty)
                                            countRS4++;
                                    }
                                    else
                                    {
                                        if ((j + 3 <= r) && tempchessBoard[i, j + 3] == Empty)
                                            countRS3++;
                                    }
                                }
                                else
                                {
                                    if ((j + 2 <= r) && tempchessBoard[i, j + 2] == Empty)
                                        countRS2++;
                                }
                            }
                            else
                            {
                                if ((j + 1 <= r) && tempchessBoard[i, j + 1] == Empty)
                                    countRS1++;
                            }
                        }
                    }
                }
            }
            WhiteHuoZi[1] += countRows2; WhiteSiZi[1] += countRS2;
            WhiteHuoZi[2] += countRows3; WhiteSiZi[2] += countRS3;
            WhiteHuoZi[3] += countRows4; WhiteSiZi[3] += countRS4;
            WhiteHuoZi[0] += countRows1; WhiteSiZi[0] += countRS1;
            WhiteWuLian += countWuLian;
            //Console.WriteLine("一：Rows:");
            //Console.Write("Rows2:{0}        ", countRows2);
            //Console.Write("Rows3:{0}        ", countRows3);
            //Console.WriteLine("Rows4:{0}", countRows4);
            //Console.Write("Si2:{0}    ", WhiteSiZi[1]); Console.Write("Si3:{0}    ", WhiteSiZi[2]); Console.WriteLine("Si4:{0}    ", WhiteSiZi[3]);
            i = j = 0;
            countRS1 = countRS2 = countRS3 = countRS4 = 0;
            countWuLian = 0;
            int countCloumns1 = 0, countCloumns2 = 0, countCloumns3 = 0, countCloumns4 = 0;
            for (j = l; j <= r; j++)
            {
                for (i = u + 1; i <= d; i++)
                {
                    if (tempchessBoard[i - 1, j] == Empty)
                    {
                        if (tempchessBoard[i, j] == White)
                        {
                            if ((i + 1 <= d) && tempchessBoard[i + 1, j] == White)
                            {
                                if ((i + 2 <= d) && tempchessBoard[i + 2, j] == White)
                                {
                                    if ((i + 3 <= d) && tempchessBoard[i + 3, j] == White)
                                    {//活四&死四&五连
                                        if ((i + 4 <= d) && tempchessBoard[i + 4, j] == Empty)
                                            countCloumns4++;
                                        if ((i + 4 <= d) && tempchessBoard[i + 4, j] == Black)
                                            countRS4++;
                                        if ((i + 4 <= d) && tempchessBoard[i + 4, j] == White)
                                            countWuLian++;
                                        if ((i + 4 == 15))
                                            countRS4++;
                                    }
                                    else//活三&死三
                                    {
                                        if ((i + 3 <= d) && tempchessBoard[i + 3, j] == Empty)
                                            countCloumns3++;
                                        if ((i + 3 <= d) && tempchessBoard[i + 3, j] == Black)
                                            countRS3++;
                                        if (i + 3 == 15)
                                            countRS3++;
                                    }
                                }
                                else//活二&死二
                                {
                                    if ((i + 2 <= d) && tempchessBoard[i + 2, j] == Empty)
                                        countCloumns2++;
                                    if ((i + 2 <= d) && tempchessBoard[i + 2, j] == Black)
                                        countRS2++;
                                    if (i + 2 == 15)
                                        countRS2++;
                                }
                            }
                            else//活一&死一
                            {
                                if ((i + 1 <= d) && tempchessBoard[i + 1, j] == Empty)
                                    countCloumns1++;
                                if ((i + 1 <= d) && tempchessBoard[i + 1, j] == Black)
                                    countRS1++;
                                if (i + 1 == 15)
                                    countRS1++;
                            }
                        }
                    }
                    if (tempchessBoard[i - 1, j] == Black)
                    {
                        if (tempchessBoard[i, j] == White)
                        {
                            if ((i + 1 <= d) && tempchessBoard[i + 1, j] == White)
                            {
                                if ((i + 2 <= d) && tempchessBoard[i + 2, j] == White)
                                {
                                    if ((i + 3 <= d) && tempchessBoard[i + 3, j] == White)
                                    {
                                        if ((i + 4 <= d) && tempchessBoard[i + 4, j] == Empty)
                                            countRS4++;
                                    }
                                    else
                                    {
                                        if ((i + 3 <= d) && tempchessBoard[i + 3, j] == Empty)
                                            countRS3++;
                                    }
                                }
                                else
                                {
                                    if ((i + 2 <= d) && tempchessBoard[i + 2, j] == Empty)
                                        countRS2++;
                                }
                            }
                            else
                            {
                                if ((i + 1 <= d) && tempchessBoard[i + 1, j] == Empty)
                                    countRS1++;
                            }
                        }
                    }
                }
            }
            WhiteHuoZi[1] += countCloumns2; WhiteSiZi[1] += countRS2;
            WhiteHuoZi[2] += countCloumns3; WhiteSiZi[2] += countRS3;
            WhiteHuoZi[3] += countCloumns4; WhiteSiZi[3] += countRS4;
            WhiteHuoZi[0] += countCloumns1; WhiteSiZi[0] += countRS1;
            WhiteWuLian += countWuLian;
            //Console.WriteLine("二：Cloumns:");
            //Console.Write("Cloumns2:{0}     ", countCloumns2);
            //Console.Write("Cloumns3:{0}     ", countCloumns3);
            //Console.WriteLine("Cloumns4:{0}", countCloumns4);
            //Console.Write("Si2:{0}    ", WhiteSiZi[1]); Console.Write("Si3:{0}    ", WhiteSiZi[2]); Console.WriteLine("Si4:{0}    ", WhiteSiZi[3]);
            //判定斜向活子情况
            //从右上到左下->左斜
            i = j = 0;
            countRS1 = countRS2 = countRS3 = countRS4 = 0;
            countWuLian = 0;
            int countLeftXie1, countLeftXie2, countLeftXie3, countLeftXie4;
            countLeftXie1 = countLeftXie2 = countLeftXie3 = countLeftXie4 = 0;
            for (i = u + 1; i <= d - 1; i++)
            {
                for (j = l + 1; j <= r - 1; j++)
                {
                    if (tempchessBoard[i - 1, j + 1] == Empty)
                    {
                        if (tempchessBoard[i, j] == White)
                        {
                            if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == White)
                            {
                                if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == White)
                                {
                                    if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == White)
                                    {//活四&死四&五连
                                        if ((i + 4 <= d) && (j - 4 >= l) && tempchessBoard[i + 4, j - 4] == Empty)
                                            countLeftXie4++;
                                        if ((i + 4 <= d) && (j - 4 >= l) && tempchessBoard[i + 4, j - 4] == Black)
                                            countRS4++;
                                        if ((i + 4 <= d) && (j - 4 >= l) && tempchessBoard[i + 4, j - 4] == White)
                                            countWuLian++;
                                        if ((i + 4 == 15) || (j - 4 == -1))
                                            countRS4++;
                                    }
                                    else//活三&死三
                                    {
                                        if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == Empty)
                                            countLeftXie3++;
                                        if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == Black)
                                            countRS3++;
                                        if ((i + 3 == 15) || (j - 3 == -1))
                                            countRS3++;
                                    }
                                }
                                else//活二&死二
                                {
                                    if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == Empty)
                                        countLeftXie2++;
                                    if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == Black)
                                        countRS2++;
                                    if ((i + 2 == 15) || (j - 2 == -1))
                                        countRS2++;
                                }
                            }
                            else//活一&死一
                            {
                                if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == Empty)
                                    countLeftXie1++;
                                if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == Black)
                                    countRS1++;
                                if ((i + 1 == 15) || (j - 1 == -1))
                                    countRS1++;
                            }
                        }
                    }
                    if (tempchessBoard[i - 1, j + 1] == Black)
                    {
                        if (tempchessBoard[i, j] == White)
                        {
                            if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == White)
                            {
                                if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == White)
                                {
                                    if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == White)
                                    {
                                        if ((i + 4 <= d) && (j - 4 >= l) && tempchessBoard[i + 4, j - 4] == Empty)
                                            countRS4++;
                                    }
                                    else
                                    {
                                        if ((i + 3 <= d) && (j - 3 >= l) && tempchessBoard[i + 3, j - 3] == Empty)
                                            countRS3++;
                                    }
                                }
                                else
                                {
                                    if ((i + 2 <= d) && (j - 2 >= l) && tempchessBoard[i + 2, j - 2] == Empty)
                                        countRS2++;
                                }
                            }
                            else
                            {
                                if ((i + 1 <= d) && (j - 1 >= l) && tempchessBoard[i + 1, j - 1] == Empty)
                                    countRS1++;
                            }
                        }
                    }
                }
            }
            WhiteHuoZi[1] += countLeftXie2; WhiteSiZi[1] += countRS2;
            WhiteHuoZi[2] += countLeftXie3; WhiteSiZi[2] += countRS3;
            WhiteHuoZi[3] += countLeftXie4; WhiteSiZi[3] += countRS4;
            WhiteHuoZi[0] += countLeftXie1; WhiteSiZi[0] += countRS1;
            WhiteWuLian += countWuLian;
            //Console.WriteLine("三：Left:");
            //Console.Write("LeftXie2:{0}     ", countLeftXie2);
            //Console.Write("LeftXie3:{0}     ", countLeftXie3);
            //Console.WriteLine("LeftXie4:{0}", countLeftXie4);
            //Console.Write("Si2:{0}    ", WhiteSiZi[1]); Console.Write("Si3:{0}    ", WhiteSiZi[2]); Console.WriteLine("Si4:{0}    ", WhiteSiZi[3]);
            //从左上到右下->右斜
            i = j = 0;
            countRS1 = countRS2 = countRS3 = countRS4 = 0;
            countWuLian = 0;
            int countRightXie1, countRightXie2, countRightXie3, countRightXie4;
            countRightXie1 = countRightXie2 = countRightXie3 = countRightXie4 = 0;
            for (i = u + 1; i <= d - 1; i++)
            {
                for (j = l + 1; j <= r - 1; j++)
                {
                    if (tempchessBoard[i - 1, j - 1] == Empty)
                    {
                        if (tempchessBoard[i, j] == White)
                        {
                            if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == White)
                            {
                                if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == White)
                                {
                                    if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == White)
                                    {//活四&死四&五连
                                        if ((i + 4 <= d) && (j + 4 <= r) && tempchessBoard[i + 4, j + 4] == Empty)
                                            countRightXie4++;
                                        if ((i + 4 <= d) && (j + 4 <= r) && tempchessBoard[i + 4, j + 4] == Black)
                                            countRS4++;
                                        if ((i + 4 <= d) && (j + 4 <= r) && tempchessBoard[i + 4, j + 4] == White)
                                            countWuLian++;
                                        if ((i + 4 == 15) || (j + 4 == 15))
                                            countRS4++;
                                    }
                                    else//活三&死三
                                    {
                                        if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == Empty)
                                            countRightXie3++;
                                        if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == Black)
                                            countRS3++;
                                        if ((i + 3 == 15) || (j + 3 == 15))
                                            countRS3++;
                                    }
                                }
                                else//活二&死二
                                {
                                    if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == Empty)
                                        countRightXie2++;
                                    if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == Black)
                                        countRS2++;
                                    if ((i + 2 == 15) || (j + 2 == 15))
                                        countRS2++;
                                }
                            }
                            else//活一&死一
                            {
                                if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == Empty)
                                    countRightXie1++;
                                if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == Black)
                                    countRS1++;
                                if ((i + 1 == 15) || (j + 1 == 15))
                                    countRS1++;
                            }
                        }
                    }
                    if (tempchessBoard[i - 1, j - 1] == Black)
                    {
                        if (tempchessBoard[i, j] == White)
                        {
                            if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == White)
                            {
                                if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == White)
                                {
                                    if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == White)
                                    {
                                        if ((i + 4 <= d) && (j + 4 <= r) && tempchessBoard[i + 4, j + 4] == Empty)
                                            countRS4++;
                                    }
                                    else
                                    {
                                        if ((i + 3 <= d) && (j + 3 <= r) && tempchessBoard[i + 3, j + 3] == Empty)
                                            countRS3++;
                                    }
                                }
                                else
                                {
                                    if ((i + 2 <= d) && (j + 2 <= r) && tempchessBoard[i + 2, j + 2] == Empty)
                                        countRS2++;
                                }
                            }
                            else
                            {
                                if ((i + 1 <= d) && (j + 1 <= r) && tempchessBoard[i + 1, j + 1] == Empty)
                                    countRS1++;
                            }
                        }
                    }
                }
            }
            WhiteHuoZi[1] += countRightXie2; WhiteSiZi[1] += countRS2;
            WhiteHuoZi[2] += countRightXie3; WhiteSiZi[2] += countRS3;
            WhiteHuoZi[3] += countRightXie4; WhiteSiZi[3] += countRS4;
            WhiteHuoZi[0] += countRightXie1; WhiteSiZi[0] += countRS1;
            WhiteWuLian += countWuLian;
            //Console.WriteLine("四：Right:");
            //Console.Write("RightXie2:{0}    ", countRightXie2);
            //Console.Write("RightXie3:{0}    ", countRightXie3);
            //Console.WriteLine("RightXie4:{0}", countRightXie4);
            //Console.Write("Si2:{0}    ", WhiteSiZi[1]); Console.Write("Si3:{0}    ", WhiteSiZi[2]); Console.WriteLine("Si4:{0}    ", WhiteSiZi[3]);
            //------------------------------------------------------------
            i = j = 0;
            countRS1 = countRS2 = countRS3 = countRS4 = 0;
            countWuLian = 0;
            for (i = 0; i <= 14; i++)
            {
                //Rows
                j = 0;
                if (tempchessBoard[i, j] == White)
                {
                    if (tempchessBoard[i, j + 1] == White)
                    {
                        if (tempchessBoard[i, j + 2] == White)
                        {
                            if (tempchessBoard[i, j + 3] == White)
                            {
                                if (tempchessBoard[i, j + 4] == Empty)
                                    countRS4++;
                            }
                            else
                            {
                                if (tempchessBoard[i, j + 3] == Empty)
                                    countRS3++;
                            }
                        }
                        else
                        {
                            if (tempchessBoard[i, j + 2] == Empty)
                                countRS2++;
                        }
                    }
                    else
                    {
                        if (tempchessBoard[i, j + 1] == Empty)
                            countRS1++;
                    }
                }
                j = 0;
                if (tempchessBoard[i, j] == White)
                {
                    if ((i + 1 <= 14) && (j + 1 <= 14) && tempchessBoard[i + 1, j + 1] == White)
                    {
                        if ((i + 2 <= 14) && (j + 2 <= 14) && tempchessBoard[i + 2, j + 2] == White)
                        {
                            if ((i + 3 <= 14) && (j + 3 <= 14) && tempchessBoard[i + 3, j + 3] == White)
                            {
                                if ((i + 4 <= 14) && (j + 4 <= 14) && tempchessBoard[i + 4, j + 4] == Empty)
                                    countRS4++;
                            }
                            else
                            {
                                if ((i + 3 <= 14) && (j + 3 <= 14) && tempchessBoard[i + 3, j + 3] == Empty)
                                    countRS3++;
                            }
                        }
                        else
                        {
                            if ((i + 2 <= 14) && (j + 2 <= 14) && tempchessBoard[i + 2, j + 2] == Empty)
                                countRS2++;
                        }
                    }
                    else
                    {
                        if ((i + 1 <= 14) && (j + 1 <= 14) && tempchessBoard[i + 1, j + 1] == Empty)
                            countRS1++;
                    }
                }
            }
            i = j = 0;
            for (j = 0; j <= 14; j++)
            {
                //Cloumns
                i = 0;
                if (tempchessBoard[i, j] == White)
                {
                    if (tempchessBoard[i + 1, j] == White)
                    {
                        if (tempchessBoard[i + 2, j] == White)
                        {
                            if (tempchessBoard[i + 3, j] == White)
                            {
                                if (tempchessBoard[i + 4, j] == Empty)
                                    countRS4++;
                            }
                            else
                            {
                                if (tempchessBoard[i + 3, j] == Empty)
                                    countRS3++;
                            }
                        }
                        else
                        {
                            if (tempchessBoard[i + 2, j] == Empty)
                                countRS2++;
                        }
                    }
                    else
                    {
                        if (tempchessBoard[i + 1, j] == Empty)
                            countRS1++;
                    }
                }
                //LeftXie
                i = 0;
                if (tempchessBoard[i, j] == White)
                {
                    if ((i - 1 >= 0) && (j - 1 >= 0) && tempchessBoard[i - 1, j - 1] == White)
                    {
                        if ((i - 2 >= 0) && (j - 2 >= 0) && tempchessBoard[i - 2, j - 2] == White)
                        {
                            if ((i - 3 >= 0) && (j - 3 >= 0) && tempchessBoard[i - 3, j - 3] == White)
                            {
                                if ((i - 4 >= 0) && (j - 4 >= 0) && tempchessBoard[i - 4, j - 4] == Empty)
                                    countRS4++;
                            }
                            else
                            {
                                if ((i - 3 >= 0) && (j - 3 >= 0) && tempchessBoard[i - 3, j - 3] == Empty)
                                    countRS3++;
                            }
                        }
                        else
                        {
                            if ((i - 2 >= 0) && (j - 2 >= 0) && tempchessBoard[i - 2, j - 2] == Empty)
                                countRS2++;
                        }
                    }
                    else
                    {
                        if ((i - 1 >= 0) && (j - 1 >= 0) && tempchessBoard[i - 1, j - 1] == Empty)
                            countRS1++;
                    }
                }
            }
            WhiteSiZi[0] += countRS1;
            WhiteSiZi[1] += countRS2;
            WhiteSiZi[2] += countRS3;
            WhiteSiZi[3] += countRS4;
            for (i = 0; i <= 14; i++)
            {
                j = 0;
                if (tempchessBoard[i, j] == White)
                {
                    if (tempchessBoard[i, j + 1] == White)
                    {
                        if (tempchessBoard[i, j + 2] == White)
                        {
                            if (tempchessBoard[i, j + 3] == White)
                            {
                                if (tempchessBoard[i, j + 4] == White)
                                    countWuLian++;
                            }
                        }
                    }
                }
                j = 0;
                if (tempchessBoard[i, j] == White)
                {
                    if ((i + 1 <= 14) && tempchessBoard[i + 1, j + 1] == White)
                    {
                        if ((i + 2 <= 14) && tempchessBoard[i + 2, j + 2] == White)
                        {
                            if ((i + 3 <= 14) && tempchessBoard[i + 3, j + 3] == White)
                            {
                                if ((i + 4 <= 14) && tempchessBoard[i + 4, j + 4] == White)
                                    countWuLian++;
                            }
                        }
                    }
                }
                j = 14;
                if (tempchessBoard[i, j] == White)
                {
                    if ((i - 1 >= 0) && tempchessBoard[i - 1, j - 1] == White)
                    {
                        if ((i - 2 >= 0) && tempchessBoard[i - 2, j - 2] == White)
                        {
                            if ((i - 3 >= 0) && tempchessBoard[i - 3, j - 3] == White)
                            {
                                if ((i - 4 >= 0) && tempchessBoard[i - 4, j - 4] == White)
                                    countWuLian++;
                            }
                        }
                    }
                }
            }
            for (j = 0; j <= 14; j++)
            {
                i = 0;
                if (tempchessBoard[i, j] == White)
                {
                    if (tempchessBoard[i + 1, j] == White)
                    {
                        if (tempchessBoard[i + 2, j] == White)
                        {
                            if (tempchessBoard[i + 3, j] == White)
                            {
                                if (tempchessBoard[i + 4, j] == White)
                                    countWuLian++;
                            }
                        }
                    }
                }
            }
            WhiteWuLian += countWuLian;
            
            /*countRows4 = 0;
            
for(i = u; i <= d; i++)
{
	for(j = l; j<= r; j++)
	{
		if(j != 0)
		{
			if(tempchessBoard[i,j - 1] == Empty || tempchessBoard[i, j - 1] == Black)
			{
				if(tempchessBoard[i, j] == White)
				{
					if((j+1 <= 14) && tempchessBoard[i, j + 1] == White)
					{
						if((j + 2 <= 14) && tempchessBoard[i,j + 2] == Empty)
						{
							if((j + 3 <=14)&&tempchessBoard[i, j + 3] == White)
							{
								if((j + 4 <= 14)&&tempchessBoard[i, j + 4] == White)
									countRows4++;
							}
						}
						if((j + 2 <= 14) && tempchessBoard[i,j + 2] == White)
						{
							if((j + 3 <= 14) && tempchessBoard[i,j + 3] == Empty)
							{
								if((j + 4 <= 14) && tempchessBoard[i,j + 4] == White)
									countRows4++;
							}	
						}
					}
					if((j+1 <= 14) && tempchessBoard[i, j + 1] == Empty)
					{
						if((j+2 <= 14) && tempchessBoard[i, j + 2] == White)
						{
							if((j+3 <= 14) && tempchessBoard[i, j + 3] == White)
							{
								if((j+3 <= 14) && tempchessBoard[i, j + 4] == White)
									countRows4++;
							}
						}
					}
				}
			}
		}
		if(j == 0)
		{
			if(tempchessBoard[i,j] == White)
			{
				if(tempchessBoard[i,j + 1] == White)
				{
					if(tempchessBoard[i, j + 2] == Empty)
					{
						if(tempchessBoard[i, j + 3] == White)
						{
                            if (tempchessBoard[i, j + 4] == White)
                                countRows4++;
                        }
					}
				}
				if(tempchessBoard[i, j +1] == Empty)
				{
					if(tempchessBoard[i, j + 2] == White)
					{
						if(tempchessBoard[i, j + 3] == White)
						{
							if(tempchessBoard[i, j + 4] == White)
								countRows4++;
						}
					}
				}
				if(tempchessBoard[i, j +1] == White)
				{
					if(tempchessBoard[i, j + 2] == White)
					{
						if(tempchessBoard[i, j + 3] == Empty)
						{
							if(tempchessBoard[i, j + 4] == White)
								countRows4++;
						}
					}
				}
			}
		}
	}
}
BlackHuoZi[3] += countRows4;
            */
        }
    }
}

