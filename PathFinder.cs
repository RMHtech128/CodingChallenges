using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PathFinderCodeChallenge
{
    partial class PathFinder
    {
        TerrainValues tv = new TerrainValues();

        public Map map = new Map();
        public Map map2 = new Map();
        public Mo mo = new Mo();
        public Mo mo2 = new Mo();
        public GridPos finalDest = new GridPos();
        public List<String> finalPath = new List<String>();
        const Int32 cdestx = 3;
        const Int32 cdesty = 3;
        const Int32 cstartx = 17;
        const Int32 cstarty = 17;
        List<GridPos> shortestTrack = new List<GridPos>();
        List<GridPos> TempTrack = new List<GridPos>();
        enum compassDir { N = 0, NE, E, SE, S, SW, W, NW };
        GridPos moOnePreviousStepGridPos;
        GridPos moTwoPreviousStepGridPos;


        public Form1 myMainForm;
        public PathFinder(Form1 mainForm)
        {
            myMainForm = mainForm;
        }
        
        #region PathFinderMethods


        public void DrawMapGridPos(bool map1or2)
        {
            string[,] mapTer = new string[20, 20];
            if (map1or2 == true) // use map1Terr
            {
                for (int i = 0; i < 20; i++)
                    for (int j = 0; j < 20; j++)
                    {
                        mapTer[i, j] = map.mapTer[i, j];
                    }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                    for (int j = 0; j < 20; j++)
                    {
                        mapTer[i, j] = map.map2Ter[i, j];
                    }
            }
            
            if (myMainForm.PictureBox1Helper.Image == null)
            {
                myMainForm.PictureBox1Helper.Image = new Bitmap(myMainForm.PictureBox1Helper.Width, myMainForm.PictureBox1Helper.Height);
            }
            if (myMainForm.PictureBox2Helper.Image == null)
            {
                myMainForm.PictureBox2Helper.Image = new Bitmap(myMainForm.PictureBox2Helper.Width, myMainForm.PictureBox2Helper.Height);
            }
            using (Graphics g = (map1or2 == true) ? Graphics.FromImage(myMainForm.PictureBox1Helper.Image) : Graphics.FromImage(myMainForm.PictureBox2Helper.Image))     //using (Graphics g = Graphics.FromImage(_pictureBox1.Image))
            { // draw colors on map for terrain types.
                g.Clear(Color.White);
                for (int x = 0; x < 20; x++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        Rectangle rect = new Rectangle(y * 10, x * 10, 10, 10);
                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                        if (mapTer[x, y].Contains("F"))
                        {
                            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
                        }
                        if (mapTer[x, y].Contains("R"))
                        {
                            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Brown);
                        }
                        if (mapTer[x, y].Contains("W"))
                        {
                            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen);
                        }
                        if (mapTer[x, y].Contains("S"))
                        {
                            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Aqua);
                        }
                        if (mapTer[x, y].Contains("H"))
                        {
                            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                        }
                        if (mapTer[x, y].Contains("M"))
                        {
                            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
                        }
                        g.FillRectangle(myBrush, rect);
                    }
                }
            }
            if (map1or2 == true) // use map1Terr
            {
                myMainForm.PictureBox1Helper.Invalidate(); // both were picturebox1
                myMainForm.PictureBox1Helper.Update();
            }
            else
            {
                myMainForm.PictureBox2Helper.Invalidate();
                myMainForm.PictureBox2Helper.Update();
            }

        }

        public void DrawAGridPos(List<Point> gridPts)
        {
            if (myMainForm.PictureBox1Helper.Image == null)
            {
                myMainForm.PictureBox1Helper.Image = new Bitmap(myMainForm.PictureBox1Helper.Width, myMainForm.PictureBox1Helper.Height);
            }
            using (Graphics g = Graphics.FromImage(myMainForm.PictureBox1Helper.Image))
            {
                // draw black background
                //g.Clear(Color.Black);
                foreach (Point pt in gridPts)
                {
                    Rectangle rect = new Rectangle(pt.X * 10, pt.Y * 10, 10, 10);
                    //g.DrawRectangle(Pens.Blue, rect);

                    Image img = Image.FromFile("hiker.png", true);
                    g.DrawImage(img, rect);
                }
            }
            myMainForm.PictureBox1Helper.Invalidate();
            myMainForm.PictureBox1Helper.Update();
        }

        public void DrawGridPos(Point gridPt)
        {
            if (myMainForm.PictureBox1Helper.Image == null)
            {
                myMainForm.PictureBox1Helper.Image = new Bitmap(myMainForm.PictureBox1Helper.Width, myMainForm.PictureBox1Helper.Height);
            }
            using (Graphics g = Graphics.FromImage(myMainForm.PictureBox1Helper.Image))
            {
                Rectangle rect = new Rectangle(gridPt.X * 10, gridPt.Y * 10, 10, 10);
                //g.DrawRectangle(Pens.DarkViolet, rect);

                Image img = Image.FromFile("hiker.png", true);
                g.DrawImage(img, rect);

            }
            myMainForm.PictureBox1Helper.Invalidate();
            myMainForm.PictureBox1Helper.Update();
        }

        public void DrawAGridPosMap2(List<Point> gridPts, bool MoOne)
        {
            if (myMainForm.PictureBox2Helper.Image == null)
            {
                myMainForm.PictureBox2Helper.Image = new Bitmap(myMainForm.PictureBox2Helper.Width, myMainForm.PictureBox2Helper.Height);
            }
            using (Graphics g = Graphics.FromImage(myMainForm.PictureBox1Helper.Image))
            {
                // draw black background
                //g.Clear(Color.Black);
                foreach (Point pt in gridPts)
                {
                    Rectangle rect = new Rectangle(pt.X * 10, pt.Y * 10, 10, 10);
                    g.DrawRectangle(Pens.Blue, rect);
                    if (MoOne)
                        g.FillRectangle(Brushes.DarkBlue, rect);
                    else
                        g.FillRectangle(Brushes.Black, rect);
                }
            }
            myMainForm.PictureBox2Helper.Invalidate();
            myMainForm.PictureBox2Helper.Update();
        }

        public void DrawGridPosMap2(Point gridPt, bool MoOne)
        {
            if (myMainForm.PictureBox2Helper.Image == null)
            {
                myMainForm.PictureBox2Helper.Image = new Bitmap(myMainForm.PictureBox2Helper.Width, myMainForm.PictureBox2Helper.Height);
            }
            using (Graphics g = Graphics.FromImage(myMainForm.PictureBox2Helper.Image))
            {
                Rectangle rect = new Rectangle(gridPt.X * 10, gridPt.Y * 10, 10, 10);
                g.DrawRectangle(Pens.DarkViolet, rect);
                if (MoOne)
                {
                    //g.FillRectangle(Brushes.DarkBlue, rect);
                    Image img = Image.FromFile("hiker.png", true);
                    g.DrawImage(img, rect);
                }
                else
                {
                    //g.FillRectangle(Brushes.Black, rect);
                    Image img = Image.FromFile("hiker2.png", true);
                    g.DrawImage(img, rect);
                }
            }
            myMainForm.PictureBox2Helper.Invalidate();
            myMainForm.PictureBox2Helper.Update();
        }
        
        public void DrawMap()
        {
            //_pictureBox1.Image = new Bitmap(_pictureBox1.Width, _pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(myMainForm.PictureBox1Helper.Image))
            {
                // draw black background
                //g.Clear(Color.White);
                Rectangle rect = new Rectangle(0, 0, myMainForm.PictureBox1Helper.Width, myMainForm.PictureBox1Helper.Height);
                g.DrawRectangle(Pens.Blue, rect);
            }
        }

        public void ConvertMap(bool map1)
        {

            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                { //finish intializing the map values.
                    map.mapValue[x, y] = map.mapTer[x, y] == "R" ? tv.Road
                                      : (map.mapTer[x, y] == "F" ? tv.Field
                                      : (map.mapTer[x, y] == "H" ? tv.Hill
                                      : (map.mapTer[x, y] == "W" ? tv.Woods
                                      : (map.mapTer[x, y] == "S" ? tv.Swamp
                                      : (map.mapTer[x, y] == "M" ? tv.Mountain : -100)))));
                    //myMainForm.listBox1Helper.Items.Add(map.mapValue[x, y].ToString());
                    //this.Refresh();
                }
            }
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                { //finish intializing the map values.
                    map2.map2Value[x, y] = map2.map2Ter[x, y] == "R" ? tv.Road
                                      : (map2.map2Ter[x, y] == "F" ? tv.Field
                                      : (map2.map2Ter[x, y] == "H" ? tv.Hill
                                      : (map2.map2Ter[x, y] == "W" ? tv.Woods
                                      : (map2.map2Ter[x, y] == "S" ? tv.Swamp
                                      : (map2.map2Ter[x, y] == "M" ? tv.Mountain : -100)))));
                    //myMainForm.listBox1Helper.Items.Add(map.mapValue[x, y].ToString());
                    //this.Refresh();
                }
            }

        }

        public Int32 GetRandomDir(Random rand, bool posOrNeg)
        {
            if (posOrNeg == true) // pick zero or one
                return Convert.ToInt32(rand.Next(0, 2));
            else                 // else pick zero or neg one
                return Convert.ToInt32(rand.Next(-1, 1));
        }

        public Int32 GetRandomDir(Random rand)
        {
            return Convert.ToInt32(rand.Next(-1, 2));
        }

        private compassDir FindCompassDir(GridPos dest, Mo mo)
        {
            // calculate the angle and set limits within 30 degrees for each direction.
            Int64 deltaX = dest.x - mo.gridPos.x;
            Int64 deltaY = dest.y - mo.gridPos.y;

            Int64 angleInDegrees = Convert.ToInt64(Math.Atan2(deltaY, deltaX) * 180 / Math.PI);

            // now I have the angle in degrees... what direction is the triangle facing?

            if (angleInDegrees > 0)
            {
                if (angleInDegrees >= 0 && angleInDegrees <= 15)
                    return compassDir.E;
                else if (angleInDegrees >= 15 && angleInDegrees <= 60)
                    return compassDir.SE;
                else if (angleInDegrees >= 60 && angleInDegrees <= 105)
                    return compassDir.S;
                else if (angleInDegrees >= 105 && angleInDegrees <= 150)
                    return compassDir.SW;
                else if (angleInDegrees >= 150 && angleInDegrees <= 180)
                    return compassDir.W;
            }
            else
            {
                if (angleInDegrees <= 0 && angleInDegrees >= -15)
                    return compassDir.E;
                else if (angleInDegrees <= -15 && angleInDegrees >= -60)
                    return compassDir.NE;
                else if (angleInDegrees <= -60 && angleInDegrees >= -105)
                    return compassDir.N;
                else if (angleInDegrees <= -105 && angleInDegrees >= -150)
                    return compassDir.NW;
                else if (angleInDegrees <= -150 && angleInDegrees >= -180)
                    return compassDir.W;
            }
            return compassDir.N;
        }

        private bool AtDest(GridPos dest, Mo mo)
        {
            if (mo.gridPos.x == dest.x && mo.gridPos.y == dest.y)
            {
                return true;
            }
            return false;
        }

        // RMH Notes: 
        // Ref: Maze routing algorithms... Lee algorithm, BF search, wave expansion, 
        // Lee only allows N-S and E-W movement. Not Diagonal movement.
        // A star path finder - fills the girds around the mo with a value 1, the next grids beyond that circle get a 2, next grids beyond that circle get 3, etc until entire grid is filled with values.

        public bool CheckLOS(Mo moShooter, Mo moTarget)
        {
            // check every grid along the line between moShooter and moTarget - this includes corners?

            // round angle to nearest 1 degree, Subtract or add to x and y and round tonearest whole value. 
            // I must determine every possible x and y grid combination that falls within the stated angle.
            // here's some geometry for you!!!

            // look at adjacent side first. Increment each "adjacent side" by one grid position and determine if "opposite side" changed.
            // keep a list of all the x,y grids and check the map to see if it is not a tv.Road (now called a tunnel that can be fired down.)
            // known values: Adjacent and Angle. Calculate Opposite.
            // sin < = O/H
            // cos < = A/H
            // Tan < = O/A
            // Tan knownAngle = Opp / knownAdjacent
            // Opp = (Tan KnownAngle) * knownAdjacent
            double Opp = 0;

            Int64 delX = (moShooter.gridPos.x - moTarget.gridPos.x);
            Int64 delY = (moShooter.gridPos.y - moTarget.gridPos.y);
            //int xPosNeg = (delX < 0) ? 1 : -1;
            //int yPosNeg = (delY < 0) ? 1 : -1;

            //delY = Math.Abs(delX);
            //delX = Math.Abs(delY);

            Int64 angleInDegrees = Convert.ToInt64(Math.Atan2(delY, delX) * 180 / Math.PI);

            double lastx = moShooter.gridPos.x;
            double lasty = moShooter.gridPos.y;

            for (double Adj = 1; Adj < delX; Adj++)
            { // we have Adjencent (x) and Angle.
                //Calculate opposite (y)
                // Tagent angle = Opp / Adjacent
                // solve for Opposite
                // Adjacent X Tan Angle = Opposite

                Opp = Adj * Math.Tan(angleInDegrees);
                // now check grid position for x,y (which is Adj,Opp) and see if it is a tv.Road
                // if any are not a tv.Road then return false,
                // else continue to end and return true.
                // Add opp and adjacent back into the mo and check the grid corrdinates on the map
                double xCheckGrid = Math.Abs(moShooter.gridPos.x - Adj);
                double yCheckGrid = Math.Abs(moShooter.gridPos.y - Opp);

                // Make certain I look at all the grids and don't skip any between shooter and target.
                if (xCheckGrid > lastx + 1)
                {
                    xCheckGrid = lastx + 1;
                    lastx = xCheckGrid;
                }
                else if (xCheckGrid < lastx - 1)
                {
                    xCheckGrid = lastx - 1;
                    lastx = xCheckGrid;
                }
                if (yCheckGrid > lasty + 1)
                {
                    yCheckGrid = lasty + 1;
                    lasty = yCheckGrid;
                }
                else if (yCheckGrid < lasty - 1)
                {
                    yCheckGrid = lasty - 1;
                    lasty = yCheckGrid;
                }


                if (map2.map2Value[Convert.ToInt16(yCheckGrid), Convert.ToInt16(xCheckGrid)] != tv.Road)
                    return false;
            }
            return true;
        }

        #endregion

    }
}
