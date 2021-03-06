﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFinderCodeChallenge
{
    /// <summary>
    ///  This partial class contains all the "Go" or "Start" from Menu functions.
    ///  Refctoring the project to make it easier to read.
    /// </summary>
    partial class PathFinder
    {
        // everything below here requires thousands of tries to find the lowest move value.
        // using random, 1 or 2 search directions
        // implement three search directions instead of two.
        // this will be a mix of the direction only and 


        // Attempt to reduce the number of path trys by checking for direction towards the destination.\
        // finds a mov value of 55-60 in 5000 trys
        // this takes less than 1 second to run the search loop.
        public void GoRandomPathsWithPointedDirectionAndTerrainAnalysis(GridPos dest)
        {
            DrawMap();
            shortestTrack.Clear();
            TempTrack.Clear();
            List<int> MoveCount = new List<int>();

            Random rand = new Random(Convert.ToInt32(DateTime.UtcNow.Millisecond)); //reseed with each try.

            Int32 xDirection = Convert.ToInt32(rand.Next(-1, 2));
            Int32 yDirection = Convert.ToInt32(rand.Next(-1, 2));

            for (int i = 0; i < 5000; i++)
            {
                TempTrack.Clear();

                int MoveValues = 0;
                mo.gridPos.x = Convert.ToInt32(myMainForm.textBoxStartXHelper.Text); //reset
                mo.gridPos.y = Convert.ToInt32(myMainForm.textBoxStartYHelper.Text); //reset

                // use random number generator and run 6000 times
                while (true)
                {
                    ////////// Determine direction towards destination ////////////////                    
                    if (finalDest.x - mo.gridPos.x == 0)
                        xDirection = 0;
                    else if (finalDest.x - mo.gridPos.x > 0) // move pos dir
                        xDirection = GetRandomDir(rand, true);
                    else                      // else move new dir
                        xDirection = GetRandomDir(rand, false);


                    if (finalDest.y - mo.gridPos.y == 0)
                        yDirection = 0;
                    else if (finalDest.y - mo.gridPos.y > 0) // move pos dir
                        yDirection = GetRandomDir(rand, true);
                    else                     // else move neg dir
                        yDirection = GetRandomDir(rand, false);

                    //xDirection = Convert.ToInt32(rand.Next(-1, 2));
                    //yDirection = Convert.ToInt32(rand.Next(-1, 2));

                    // stay away from the edges.
                    if (mo.gridPos.x + xDirection > 19 || mo.gridPos.x + xDirection < 1)
                        continue;
                    if (mo.gridPos.y + yDirection > 19 || mo.gridPos.y + yDirection < 1)
                        continue;

                    bool foundHome = false;
                    // are we one step away from destination?
                    if ((mo.gridPos.x + xDirection) == finalDest.x
                         && (mo.gridPos.y + yDirection) == finalDest.y)
                    { // we are one step away, so use these coordinates.
                        mo.gridPos.x += xDirection;
                        mo.gridPos.y += yDirection;
                        foundHome = true;
                        break;
                    }
                    else
                    {

                        //////////////// The meat of the search is here /////////////////////////////////////////
                        // check to see if new x and y gets us closer or further away from the destination.
                        Int32 DeltaCurrX = Math.Abs((mo.gridPos.x - dest.x));
                        Int32 DeltaCurrY = Math.Abs((mo.gridPos.y - dest.y));
                        Int32 DeltaProposedX = Math.Abs((mo.gridPos.x + xDirection) - dest.x);
                        Int32 DeltaProposedY = Math.Abs((mo.gridPos.y + yDirection) - dest.y);
                        if ((DeltaCurrX != 0 && DeltaCurrY != 0) && (DeltaProposedX > DeltaCurrX || DeltaProposedY > DeltaCurrY))
                        { // don't move away from the destination
                            continue; // try for another set of random directions.
                        }

                        /////////////////////////////////////////////////////////////////////////
                        // Pick 2nd direction towards the goal 
                        /////////////////////////////////////////////////////////////////////////
                        Int32 xDirection2 = 0;
                        Int32 yDirection2 = 0;
                        while (true)
                        {

                            ////////// Determine direction towards destination ////////////////                    
                            if (finalDest.x - mo.gridPos.x == 0)
                                xDirection2 = 0;
                            else if (finalDest.x - mo.gridPos.x > 0) // move pos dir
                                xDirection2 = GetRandomDir(rand, true);
                            else                      // else move new dir
                                xDirection2 = GetRandomDir(rand, false);


                            if (finalDest.y - mo.gridPos.y == 0)
                                yDirection2 = 0;
                            else if (finalDest.y - mo.gridPos.y > 0) // move pos dir
                                yDirection2 = GetRandomDir(rand, true);
                            else                     // else move neg dir
                                yDirection2 = GetRandomDir(rand, false);


                            // stay away from the edges.
                            if (mo.gridPos.x + xDirection2 > 19 || mo.gridPos.x + xDirection2 < 1)
                                continue;
                            if (mo.gridPos.y + yDirection2 > 19 || mo.gridPos.y + yDirection2 < 1)
                                continue;

                            if ((mo.gridPos.x + xDirection2) == finalDest.x
                                && (mo.gridPos.y + yDirection2) == finalDest.y)
                            { // we are one step away
                                mo.gridPos.x += xDirection2;
                                mo.gridPos.y += yDirection2;
                                foundHome = true;
                                break;
                            }
                            else
                            {

                                // check to see if new x and y gets us closer or further away from the destination.
                                Int32 DeltaCurrX2 = Math.Abs((mo.gridPos.x - dest.x));
                                Int32 DeltaCurrY2 = Math.Abs((mo.gridPos.y - dest.y));
                                Int32 DeltaProposedX2 = Math.Abs((mo.gridPos.x + xDirection2) - dest.x);
                                Int32 DeltaProposedY2 = Math.Abs((mo.gridPos.y + yDirection2) - dest.y);
                                // Weird bug in here if mo moves beyond the destination.
                                if ((DeltaCurrX2 != 0 && DeltaCurrY2 != 0) && (DeltaProposedX2 > DeltaCurrX2 || DeltaProposedY2 > DeltaCurrY2))
                                { // don't move away from the destination
                                    continue; // try for another set of random directions.
                                }

                                // don't chose the same x and y as previous x and y
                                // otherwise get a new xDirection2 and yDirection2
                                if (xDirection != xDirection2 || yDirection != yDirection2)
                                {
                                    break; // Don't compare two "next steps" that are the same x and y.
                                }
                            }
                        }

                        if (foundHome == false)
                        {
                            // now compare the two mapvalues
                            if (Math.Abs(map.mapValue[mo.gridPos.y + yDirection, mo.gridPos.x + xDirection])
                                < Math.Abs(map.mapValue[mo.gridPos.y + yDirection2, mo.gridPos.x + xDirection2])
                                && xDirection != 0 && yDirection != 0)
                            { // use xDirection yDirection
                                mo.gridPos.x += xDirection;
                                mo.gridPos.y += yDirection;
                            }
                            else
                            { // use xDirection2 yDirection2
                                mo.gridPos.x += xDirection2;
                                mo.gridPos.y += yDirection2;
                            }
                        }
                    }
                    //////////////// The meat of the search is above /////////////////////////////////////////
                    //myMainForm.listBox1Helper.Items.Add("Mo moved to: " + map.mapTer[mo.gridPos.x, mo.gridPos.y] + " x: " + mo.gridPos.x.ToString() + ", y: " + mo.gridPos.y.ToString());

                    MoveValues += map.mapValue[mo.gridPos.y, mo.gridPos.x];

                    String x = mo.gridPos.x.ToString(); // make immutable
                    String y = mo.gridPos.y.ToString(); // make immutable

                    GridPos gp1 = new GridPos();
                    gp1.x = Convert.ToInt32(x);
                    gp1.y = Convert.ToInt32(y);

                    TempTrack.Add(gp1);


                    // move there
                    // add up terrain values 
                    // lowest terrain values wins.
                    if (mo.gridPos.x == dest.x && mo.gridPos.y == dest.y)
                        break;
                }
                MoveCount.Add(MoveValues);
                // scan existing Movecounts... if new is lowest, Move TempTrack to Lowest Track
                foreach (int mov in MoveCount)
                {
                    if (MoveValues >= mov) // newer lowest
                    {
                        shortestTrack.Clear();
                        foreach (GridPos gp in TempTrack)
                            shortestTrack.Add(gp);

                        break;
                    }
                }
                finalPath.Clear();
                TempTrack.Clear();
            }



            int lowestVal = MoveCount.FirstOrDefault();
            foreach (int val in MoveCount)
            {
                if (val > lowestVal)
                    lowestVal = val;
            }
            myMainForm.listBox1Helper.Items.Add("Lowest move val: " + lowestVal.ToString());

            // Draw the path taken
            List<Point> points = new List<Point>();
            foreach (GridPos gp in shortestTrack)
            {
                Point pt = new Point();
                pt.X = gp.x;
                pt.Y = gp.y;
                points.Add(pt);
                DrawAGridPos(points);
                myMainForm.listBox1Helper.Items.Add(map.mapTer[gp.x, gp.y]);
                Thread.Sleep(100);
            }
        }

        // Attempt to reduce the number of path trys by checking for direction towards the destination.\
        // finds a mov value of 40-50 in 50,000 trys
        // this takes less than 1 second to run the search loop.
        public void GoRandomPathsWithBestDirectionAndTerrainAnalysis(GridPos dest)
        {
            DrawMap();
            shortestTrack.Clear();
            TempTrack.Clear();
            List<int> MoveCount = new List<int>();

            Random rand = new Random(Convert.ToInt32(DateTime.UtcNow.Millisecond)); //reseed with each try.

            Int32 xDirection = Convert.ToInt32(rand.Next(-1, 2));
            Int32 yDirection = Convert.ToInt32(rand.Next(-1, 2));

            for (int i = 0; i < 50000; i++)
            {
                TempTrack.Clear();

                int MoveValues = 0;
                mo.gridPos.x = Convert.ToInt32(myMainForm.textBoxStartXHelper.Text); //reset
                mo.gridPos.y = Convert.ToInt32(myMainForm.textBoxStartYHelper.Text); //reset

                // use random number generator and run 6000 times
                while (true)
                {
                    // pick a random direction for x and y
                    xDirection = GetRandomDir(rand);
                    yDirection = GetRandomDir(rand);
                    //xDirection = Convert.ToInt32(rand.Next(-1, 2));
                    //yDirection = Convert.ToInt32(rand.Next(-1, 2));

                    // stay away from the edges.
                    if (mo.gridPos.x + xDirection > 19 || mo.gridPos.x + xDirection < 1)
                        continue;
                    if (mo.gridPos.y + yDirection > 19 || mo.gridPos.y + yDirection < 1)
                        continue;

                    bool foundHome = false;
                    // are we one step away from destination?
                    if ((mo.gridPos.x + xDirection) == finalDest.x
                         && (mo.gridPos.y + yDirection) == finalDest.y)
                    { // we are one step away, so use these coordinates.
                        mo.gridPos.x += xDirection;
                        mo.gridPos.y += yDirection;
                        foundHome = true;
                        break;
                    }
                    else
                    {

                        //////////////// The meat of the search is here /////////////////////////////////////////
                        // check to see if new x and y gets us closer or further away from the destination.
                        Int32 DeltaCurrX = Math.Abs((mo.gridPos.x - dest.x));
                        Int32 DeltaCurrY = Math.Abs((mo.gridPos.y - dest.y));
                        Int32 DeltaProposedX = Math.Abs((mo.gridPos.x + xDirection) - dest.x);
                        Int32 DeltaProposedY = Math.Abs((mo.gridPos.y + yDirection) - dest.y);
                        if ((DeltaCurrX != 0 && DeltaCurrY != 0) && (DeltaProposedX > DeltaCurrX || DeltaProposedY > DeltaCurrY))
                        { // don't move away from the destination
                            continue; // try for another set of random directions.
                        }

                        /////////////////////////////////////////////////////////////////////////
                        // Pick 2nd direction towards the goal 
                        /////////////////////////////////////////////////////////////////////////
                        Int32 xDirection2 = 0;
                        Int32 yDirection2 = 0;
                        while (true)
                        {
                            xDirection2 = GetRandomDir(rand);
                            yDirection2 = GetRandomDir(rand);

                            // stay away from the edges.
                            if (mo.gridPos.x + xDirection2 > 19 || mo.gridPos.x + xDirection2 < 1)
                                continue;
                            if (mo.gridPos.y + yDirection2 > 19 || mo.gridPos.y + yDirection2 < 1)
                                continue;

                            if ((mo.gridPos.x + xDirection2) == finalDest.x
                                && (mo.gridPos.y + yDirection2) == finalDest.y)
                            { // we are one step away
                                mo.gridPos.x += xDirection2;
                                mo.gridPos.y += yDirection2;
                                foundHome = true;
                                break;
                            }
                            else
                            {

                                // check to see if new x and y gets us closer or further away from the destination.
                                Int32 DeltaCurrX2 = Math.Abs((mo.gridPos.x - dest.x));
                                Int32 DeltaCurrY2 = Math.Abs((mo.gridPos.y - dest.y));
                                Int32 DeltaProposedX2 = Math.Abs((mo.gridPos.x + xDirection2) - dest.x);
                                Int32 DeltaProposedY2 = Math.Abs((mo.gridPos.y + yDirection2) - dest.y);
                                // Weird bug in here if mo moves beyond the destination.
                                if ((DeltaCurrX2 != 0 && DeltaCurrY2 != 0) && (DeltaProposedX2 > DeltaCurrX2 || DeltaProposedY2 > DeltaCurrY2))
                                { // don't move away from the destination
                                    continue; // try for another set of random directions.
                                }

                                // don't chose the same x and y as previous x and y
                                // otherwise get a new xDirection2 and yDirection2
                                if (xDirection != xDirection2 || yDirection != yDirection2)
                                {
                                    break; // Don't compare two "next steps" that are the same x and y.
                                }
                            }
                        }

                        if (foundHome == false)
                        {
                            // now compare the two mapvalues
                            if (Math.Abs(map.mapValue[mo.gridPos.y + yDirection, mo.gridPos.x + xDirection])
                                < Math.Abs(map.mapValue[mo.gridPos.y + yDirection2, mo.gridPos.x + xDirection2]))
                            { // use xDirection yDirection
                                mo.gridPos.x += xDirection;
                                mo.gridPos.y += yDirection;
                            }
                            else
                            { // use xDirection2 yDirection2
                                mo.gridPos.x += xDirection2;
                                mo.gridPos.y += yDirection2;
                            }
                        }
                    }
                    //////////////// The meat of the search is above /////////////////////////////////////////
                    //myMainForm.listBox1Helper.Items.Add("Mo moved to: " + map.mapTer[mo.gridPos.x, mo.gridPos.y] + " x: " + mo.gridPos.x.ToString() + ", y: " + mo.gridPos.y.ToString());

                    MoveValues += map.mapValue[mo.gridPos.y, mo.gridPos.x];

                    String x = mo.gridPos.x.ToString(); // make immutable
                    String y = mo.gridPos.y.ToString(); // make immutable

                    GridPos gp1 = new GridPos();
                    gp1.x = Convert.ToInt32(x);
                    gp1.y = Convert.ToInt32(y);

                    TempTrack.Add(gp1);


                    // move there
                    // add up terrain values 
                    // lowest terrain values wins.
                    if (mo.gridPos.x == dest.x && mo.gridPos.y == dest.y)
                        break;
                }
                MoveCount.Add(MoveValues);
                // scan existing Movecounts... if new is lowest, Move TempTrack to Lowest Track
                foreach (int mov in MoveCount)
                {
                    if (MoveValues >= mov) // newer lowest
                    {
                        shortestTrack.Clear();
                        foreach (GridPos gp in TempTrack)
                            shortestTrack.Add(gp);

                        break;
                    }
                }
                finalPath.Clear();
                TempTrack.Clear();
            }



            int lowestVal = MoveCount.FirstOrDefault();
            foreach (int val in MoveCount)
            {
                if (val > lowestVal)
                    lowestVal = val;
            }
            myMainForm.listBox1Helper.Items.Add("Lowest move val: " + lowestVal.ToString());

            // Draw the path taken
            List<Point> points = new List<Point>();
            foreach (GridPos gp in shortestTrack)
            {
                Point pt = new Point();
                pt.X = gp.x;
                pt.Y = gp.y;
                points.Add(pt);
                DrawAGridPos(points);
                myMainForm.listBox1Helper.Items.Add(map.mapTer[gp.x, gp.y]);
                Thread.Sleep(100);
            }
        }
        // Attempt to reduce the number of path trys by checking for direction towards the destination.\
        // finds a mov value of 40-50 in 50,000 trys
        // this takes less than 1 second to run the search loop.
        public void GoRandomPathsWithBestDirection(GridPos dest)
        {

            //_pictureBox1.Image = new Bitmap(_pictureBox1.Width, _pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(myMainForm.PictureBox1Helper.Image))
            {
                // draw black background
                //g.Clear(Color.White);
                Rectangle rect = new Rectangle(0, 0, myMainForm.PictureBox1Helper.Width, myMainForm.PictureBox1Helper.Height);
                g.DrawRectangle(Pens.Blue, rect);
            }

            shortestTrack.Clear();
            TempTrack.Clear();
            List<int> MoveCount = new List<int>();

            Random rand = new Random(Convert.ToInt32(DateTime.UtcNow.Millisecond)); //reseed with each try.

            Int32 xDirection = Convert.ToInt32(rand.Next(-1, 2));
            Int32 yDirection = Convert.ToInt32(rand.Next(-1, 2));

            for (int i = 0; i < 50000; i++)
            {
                TempTrack.Clear();

                int MoveValues = 0;
                mo.gridPos.x = Convert.ToInt32(myMainForm.textBoxStartXHelper.Text);
                mo.gridPos.y = Convert.ToInt32(myMainForm.textBoxStartYHelper.Text);

                // use random number generator and run 6000 times
                while (true)
                {
                    // pick a random direction for x and y

                    xDirection = Convert.ToInt32(rand.Next(-1, 2));
                    yDirection = Convert.ToInt32(rand.Next(-1, 2));

                    // stay away from the edges.
                    if (mo.gridPos.x + xDirection > 19 || mo.gridPos.x + xDirection < 1)
                        continue;
                    if (mo.gridPos.y + yDirection > 19 || mo.gridPos.y + yDirection < 1)
                        continue;


                    //////////////// The meat of the search is here /////////////////////////////////////////
                    // check to see if new x and y gets us closer or further away from the destination.
                    Int32 DeltaCurrX = Math.Abs((mo.gridPos.x - dest.x));
                    Int32 DeltaCurrY = Math.Abs((mo.gridPos.y - dest.y));
                    Int32 DeltaX = Math.Abs((mo.gridPos.x + xDirection) - dest.x);
                    Int32 DeltaY = Math.Abs((mo.gridPos.y + yDirection) - dest.y);
                    if (DeltaX > DeltaCurrX || DeltaY > DeltaCurrY)
                    { // don't move away from the destination
                        continue; // try for another set of random directions.
                    }
                    //////////////// The meat of the search is above /////////////////////////////////////////



                    mo.gridPos.x += xDirection;
                    mo.gridPos.y += yDirection;
                    MoveValues += map.mapValue[mo.gridPos.y, mo.gridPos.x];

                    String x = mo.gridPos.x.ToString(); // make immutable
                    String y = mo.gridPos.y.ToString(); // make immutable

                    GridPos gp1 = new GridPos();
                    gp1.x = Convert.ToInt32(x);
                    gp1.y = Convert.ToInt32(y);

                    TempTrack.Add(gp1);


                    // move there
                    // add up terrain values 
                    // lowest terrain values wins.
                    if (mo.gridPos.x == dest.x && mo.gridPos.y == dest.y)
                        break;
                }
                MoveCount.Add(MoveValues);
                // scan existing Movecounts... if new is lowest, Move TempTrack to Lowest Track
                foreach (int mov in MoveCount)
                {
                    if (MoveValues >= mov) // newer lowest
                    {
                        shortestTrack.Clear();
                        foreach (GridPos gp in TempTrack)
                            shortestTrack.Add(gp);
                        break;
                    }
                }
                TempTrack.Clear();
            }
            int lowestVal = MoveCount.FirstOrDefault();
            foreach (int val in MoveCount)
            {
                if (val > lowestVal)
                    lowestVal = val;
            }
            myMainForm.listBox1Helper.Items.Add("Lowest move val: " + lowestVal.ToString());

            // Draw the path taken
            List<Point> points = new List<Point>();
            foreach (GridPos gp in shortestTrack)
            {
                Point pt = new Point();
                pt.X = gp.x;
                pt.Y = gp.y;
                points.Add(pt);
                DrawAGridPos(points);
                Thread.Sleep(1);
            }
        }

        // Going pure random paths requires running the loop 500,000 to 1,000,000 times or more before
        // finding something even close to the lowest value for movement cost-by-terrain
        // this takes 5 minutes or more.
        public void GoRandomPaths(GridPos dest)
        {

            myMainForm.PictureBox1Helper.Image = new Bitmap(myMainForm.PictureBox1Helper.Width, myMainForm.PictureBox1Helper.Height);
            using (Graphics g = Graphics.FromImage(myMainForm.PictureBox1Helper.Image))
            {
                // draw black background
                g.Clear(Color.White);
                Rectangle rect = new Rectangle(0, 0, myMainForm.PictureBox1Helper.Width, myMainForm.PictureBox1Helper.Height);
                g.DrawRectangle(Pens.Blue, rect);
            }

            shortestTrack.Clear();
            TempTrack.Clear();
            List<int> MoveCount = new List<int>();

            Random rand = new Random(Convert.ToInt32(DateTime.UtcNow.Millisecond)); //reseed with each try.

            Int32 xDirection = Convert.ToInt32(rand.Next(-1, 2));
            Int32 yDirection = Convert.ToInt32(rand.Next(-1, 2));

            for (int i = 0; i < 100000; i++)
            {
                TempTrack.Clear();

                int MoveValues = 0;
                mo.gridPos.x = Convert.ToInt32(myMainForm.textBoxStartXHelper.Text); //reset
                mo.gridPos.y = Convert.ToInt32(myMainForm.textBoxStartYHelper.Text); //reset

                // use random number generator and run 6000 times
                while (true)
                {
                    // pick a random direction for x and y

                    xDirection = Convert.ToInt32(rand.Next(-1, 2));
                    yDirection = Convert.ToInt32(rand.Next(-1, 2));

                    // stay away from the edges.
                    if (mo.gridPos.x + xDirection > 19 || mo.gridPos.x + xDirection < 1)
                        continue;
                    if (mo.gridPos.y + yDirection > 19 || mo.gridPos.y + yDirection < 1)
                        continue;

                    mo.gridPos.x += xDirection;
                    mo.gridPos.y += yDirection;
                    MoveValues += map.mapValue[mo.gridPos.y, mo.gridPos.x];

                    String x = mo.gridPos.x.ToString(); // make immutable
                    String y = mo.gridPos.y.ToString(); // make immutable

                    GridPos gp1 = new GridPos();
                    gp1.x = Convert.ToInt32(x);
                    gp1.y = Convert.ToInt32(y);

                    TempTrack.Add(gp1);


                    // move there
                    // add up terrain values 
                    // lowest terrain values wins.
                    if (mo.gridPos.x == dest.x && mo.gridPos.y == dest.y)
                        break;
                }
                MoveCount.Add(MoveValues);
                // scan existing Movecounts... if new is lowest, Move TempTrack to Lowest Track
                foreach (int mov in MoveCount)
                {
                    if (MoveValues >= mov) // newer lowest
                    {
                        shortestTrack.Clear();
                        foreach (GridPos gp in TempTrack)
                            shortestTrack.Add(gp);
                        break;
                    }
                }
                TempTrack.Clear();
            }
            int lowestVal = MoveCount.FirstOrDefault();
            foreach (int val in MoveCount)
            {
                if (val > lowestVal)
                    lowestVal = val;
            }
            myMainForm.listBox1Helper.Items.Add("Lowest move val: " + lowestVal.ToString());

            // Draw the path taken
            List<Point> points = new List<Point>();
            foreach (GridPos gp in shortestTrack)
            {
                Point pt = new Point();
                pt.X = gp.x;
                pt.Y = gp.y;
                points.Add(pt);
                DrawAGridPos(points);
                Thread.Sleep(1);
            }
        }

        public void GoDirected(GridPos dest)
        {
            int CycleCount = 0;

            int MoveCount = 0;

            mo.moveRate = 10; //reset to default - refactor this later

            List<GridPos> FinalTrack = new List<GridPos>();
            GridPos PreviousStepGridPos = new GridPos();
            while (dest.x != mo.gridPos.x || dest.y != mo.gridPos.y)
            {
                myMainForm.Refresh();
                if (AtDest(dest, mo) == true)
                {
                    break;
                }
                // Must check to see if Mo is one step away from the destination.
                // If it is one step away, then just move there.
                // Looking at terrain can cause Mo to do circles around the destination, looking for the best way to get up the hill or mountain or swamp.
                if (Math.Abs(mo.gridPos.x - dest.x) <= 1 && Math.Abs(mo.gridPos.y - dest.y) <= 1)
                {
                    mo.gridPos.x = dest.x;
                    mo.gridPos.y = dest.y;
                    FinalTrack.Add(dest);

                    myMainForm.listBox1Helper.Items.Add("Mo moved to: " + map.mapTer[mo.gridPos.y, mo.gridPos.x] + " x:" + mo.gridPos.x.ToString() + ", y:" + mo.gridPos.y.ToString());
                    myMainForm.listBox1Helper.Refresh();
                    Point pt1 = new Point();
                    pt1.X = mo.gridPos.x;
                    pt1.Y = mo.gridPos.y;
                    DrawGridPos(pt1);


                    break;
                }
                else
                {


                    CycleCount++;
                    myMainForm.listBox1Helper.Items.Add("CycleCount: " + CycleCount.ToString());

                    compassDir DirScore = FindCompassDir(dest, mo);

                    int dirValLeft = -100;// Set defaultvalues to -100, incase we have to ignore one of these due to boundaries
                    int dirValCenter = -100;
                    int dirValRight = -100;
                    int dirValHardL = -100;// Set defaultvalues to -100, incase we have to ignore one of these due to boundaries
                    int dirValHardR = -100;// Set defaultvalues to -100, incase we have to ignore one of these due to boundaries

                    GridPos nextStepGridPos = new GridPos();
                    GridPos nextStepGridPosL = new GridPos(); // Left
                    GridPos nextStepGridPosC = new GridPos(); // Center
                    GridPos nextStepGridPosR = new GridPos(); // Right

                    GridPos nextStepGridPosHardL = new GridPos(); // Hard Left
                    GridPos nextStepGridPosHardR = new GridPos(); // Hard Right


                    GridPos InitGrid = new GridPos();
                    InitGrid.x = 0;
                    InitGrid.y = 0;

                    nextStepGridPos = InitGrid;


                    nextStepGridPosL = mo.gridPos; // Set default next pos to current location, incase we have to ignore one of these due to boundaries
                    nextStepGridPosC = mo.gridPos;
                    nextStepGridPosR = mo.gridPos;

                    nextStepGridPosHardL = mo.gridPos; // Hard Left
                    nextStepGridPosHardR = mo.gridPos; // Hard Right


                    GridPos gPL = new GridPos();
                    GridPos gPC = new GridPos();
                    GridPos gPR = new GridPos();
                    GridPos gPHardL = new GridPos(); // Hard Left
                    GridPos gPHardR = new GridPos(); // Hard Right

                    switch (DirScore)
                    {
                        case compassDir.N:


                            if (mo.gridPos.x - 1 >= 0)
                                gPL.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPL.y = mo.gridPos.y - 1;

                            dirValLeft = map.mapValue[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            gPC.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPC.y = mo.gridPos.y - 1;

                            dirValCenter = map.mapValue[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x + 1 < 19)
                                gPR.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPR.y = mo.gridPos.y - 1;

                            dirValRight = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardL.x = mo.gridPos.x - 1;
                            gPHardL.y = mo.gridPos.y;

                            dirValHardL = map.mapValue[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;


                            if (mo.gridPos.x + 1 < 19)
                                gPHardR.x = mo.gridPos.x + 1;
                            gPHardR.y = mo.gridPos.y;

                            dirValHardR = map.mapValue[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;

                        case compassDir.NE:

                            gPL.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPL.y = mo.gridPos.y - 1;

                            dirValLeft = map.mapValue[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x + 1 < 19)
                                gPC.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPC.y = mo.gridPos.y - 1;

                            dirValCenter = map.mapValue[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x + 1 < 19)
                                gPR.x = mo.gridPos.x + 1;
                            gPR.y = mo.gridPos.y;

                            dirValRight = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardL.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardL.y = mo.gridPos.y - 1;

                            dirValHardL = map.mapValue[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x + 1 < 19)
                                gPHardR.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardR.y = mo.gridPos.y + 1;

                            dirValHardR = map.mapValue[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.E:

                            if (mo.gridPos.x + 1 < 19)
                                gPL.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPL.y = mo.gridPos.y - 1;

                            dirValLeft = map.mapValue[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x + 1 < 19)
                                gPC.x = mo.gridPos.x + 1;
                            gPC.y = mo.gridPos.y;

                            dirValCenter = map.mapValue[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x + 1 < 19)
                                gPR.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPR.y = mo.gridPos.y + 1;

                            dirValRight = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;


                            // Hard left and hard right follows:

                            gPHardL.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardL.y = mo.gridPos.y - 1;

                            dirValHardL = map.mapValue[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            gPHardR.x = mo.gridPos.x;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardR.y = mo.gridPos.y + 1;

                            dirValHardR = map.mapValue[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.SE:

                            if (mo.gridPos.x + 1 < 19)
                                gPL.x = mo.gridPos.x + 1;
                            gPL.y = mo.gridPos.y;

                            dirValLeft = map.mapValue[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x + 1 < 19)
                                gPC.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPC.y = mo.gridPos.y + 1;

                            dirValCenter = map.mapValue[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            gPR.x = mo.gridPos.x;
                            if (mo.gridPos.y + 1 < 19)
                                gPR.y = mo.gridPos.y + 1;

                            dirValRight = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x + 1 < 19)
                                gPHardL.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardL.y = mo.gridPos.y - 1;

                            dirValHardL = map.mapValue[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardR.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardR.y = mo.gridPos.y + 1;

                            dirValHardR = map.mapValue[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.S:
                            if (mo.gridPos.x + 1 < 19)
                                gPL.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPL.y = mo.gridPos.y + 1;

                            dirValLeft = map.mapValue[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            gPC.x = mo.gridPos.x;
                            if (mo.gridPos.y + 1 < 19)
                                gPC.y = mo.gridPos.y + 1;

                            dirValCenter = map.mapValue[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x - 1 >= 0)
                                gPR.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPR.y = mo.gridPos.y + 1;

                            dirValRight = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x + 1 < 19)
                                gPHardL.x = mo.gridPos.x + 1;
                            gPHardL.y = mo.gridPos.y;

                            dirValHardL = map.mapValue[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardR.x = mo.gridPos.x - 1;
                            gPHardR.y = mo.gridPos.y;

                            dirValHardR = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.SW:
                            if (mo.gridPos.x - 1 >= 0)
                                gPL.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPL.y = mo.gridPos.y + 1;

                            dirValLeft = map.mapValue[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPC.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPC.y = mo.gridPos.y + 1;

                            dirValCenter = map.mapValue[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x - 1 >= 0)
                                gPR.x = mo.gridPos.x - 1;
                            gPR.y = mo.gridPos.y;

                            dirValRight = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x + 1 < 19)
                                gPHardL.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardL.y = mo.gridPos.y + 1;

                            dirValHardL = map.mapValue[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardR.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardR.y = mo.gridPos.y - 1;

                            dirValHardR = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.W:
                            if (mo.gridPos.x - 1 >= 0)
                            {
                                gPL.x = mo.gridPos.x - 1;
                                if (mo.gridPos.y + 1 < 19)
                                {
                                    gPL.y = mo.gridPos.y + 1;

                                    dirValLeft = map.mapValue[gPL.y, gPL.x];
                                    nextStepGridPosL = gPL;
                                }
                            }
                            if (mo.gridPos.x - 1 >= 0)
                                gPC.x = mo.gridPos.x - 1;
                            gPC.y = mo.gridPos.y;

                            dirValCenter = map.mapValue[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x - 1 >= 0)
                                gPR.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPR.y = mo.gridPos.y - 1;

                            dirValRight = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            gPHardL.x = mo.gridPos.x;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardL.y = mo.gridPos.y + 1;

                            dirValHardL = map.mapValue[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            gPHardR.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardR.y = mo.gridPos.y - 1;

                            dirValHardR = map.mapValue[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.NW:
                            if (mo.gridPos.x - 1 >= 0)
                                gPL.x = mo.gridPos.x - 1;
                            gPL.y = mo.gridPos.y;

                            dirValLeft = map.mapValue[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPC.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPC.y = mo.gridPos.y - 1;

                            dirValCenter = map.mapValue[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            gPR.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPR.y = mo.gridPos.y - 1;

                            dirValRight = map.mapValue[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardL.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardL.y = mo.gridPos.y + 1;

                            dirValHardL = map.mapValue[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x + 1 < 19)
                                gPHardR.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardR.y = mo.gridPos.y - 1;

                            dirValHardR = map.mapValue[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                    }

                    // Add a movement penalty for moving left and right - this should make mo prefer going straight.
                    //dirValLeft += Convert.ToInt16(Average/3);
                    //dirValRight += Convert.ToInt16(Average/3);
                    // Add a movement penalty for moving hard left and hard right - this should make mo prefer going straight. 
                    //dirValHardL += Convert.ToInt16(Average*.5);
                    //dirValHardR += Convert.ToInt16(Average*.5);

                    bool cantMove = false;

                    if (dirValCenter >= dirValLeft && dirValCenter >= dirValRight
                     && dirValCenter >= dirValHardL && dirValCenter >= dirValHardR
                     && (PreviousStepGridPos.x != nextStepGridPosC.x
                     || PreviousStepGridPos.y != nextStepGridPosC.y)
                     )
                    {
                        MoveCount += dirValCenter;
                        //if (mo.moveRate >= dirValCenter)
                        nextStepGridPos = nextStepGridPosC;
                        mo.moveRate = mo.moveRate - dirValCenter;
                        // Go Center
                    }
                    else if (dirValLeft >= dirValRight // is left better than right? // check  left and  right after checking center
                          && dirValLeft >= dirValHardL && dirValLeft >= dirValHardR
                          && (PreviousStepGridPos.x != nextStepGridPosL.x
                          || PreviousStepGridPos.y != nextStepGridPosL.y)
                          )
                    {
                        MoveCount += dirValLeft;
                        nextStepGridPos = nextStepGridPosL;
                        mo.moveRate = mo.moveRate - dirValLeft;
                        // Go Left
                    }
                    else if (dirValRight >= dirValHardL // is Right better than LL
                          && dirValRight >= dirValHardR
                          && (PreviousStepGridPos.x != nextStepGridPosR.y
                          || PreviousStepGridPos.y != nextStepGridPosR.y)
                          )
                    {
                        MoveCount += dirValRight;
                        nextStepGridPos = nextStepGridPosR;
                        mo.moveRate = mo.moveRate - dirValRight;
                        // Go Right
                    }
                    else if (dirValHardL >= dirValHardR // is LL better than RR
                        && dirValHardL >= dirValLeft && dirValHardL >= dirValRight && dirValHardL >= dirValCenter
                        && (PreviousStepGridPos.x != nextStepGridPosHardL.x
                        || PreviousStepGridPos.y != nextStepGridPosHardL.y)
                        )
                    {
                        MoveCount += dirValHardL;
                        nextStepGridPos = nextStepGridPosHardL;
                        mo.moveRate = mo.moveRate - dirValHardL;
                        // Go Hard Left
                    }
                    else if (dirValHardR >= dirValCenter // is RR better than Center?
                        && dirValHardR >= dirValLeft && dirValHardR >= dirValRight && dirValHardR >= dirValHardL
                        && (PreviousStepGridPos.x != nextStepGridPosHardR.x
                        || PreviousStepGridPos.y != nextStepGridPosHardR.y)
                        )
                    {
                        MoveCount += dirValHardR;
                        nextStepGridPos = nextStepGridPosHardR;
                        mo.moveRate = mo.moveRate - dirValHardR;
                        // Go Hard Right
                    }
                    else // go center
                    {
                        MoveCount += dirValCenter;
                        //if (mo.moveRate >= dirValCenter)
                        nextStepGridPos = nextStepGridPosC;
                        mo.moveRate = mo.moveRate - dirValCenter;
                        // Go Center
                    }
                    PreviousStepGridPos = mo.gridPos; // Store the current value before moving, then next time through, don't allow new step to equal prev step. No step-backs. Forward only

                    if (cantMove == true)
                    {
                        // not enough movement remaining. Break, increment the cycle count, continue with next cycle.
                        //mo.moveRate = 10; //reset to default - refactor this later
                        break;
                    }

                    mo.gridPos = nextStepGridPos;
                    FinalTrack.Add(nextStepGridPos);

                    myMainForm.listBox1Helper.Items.Add("Mo moved to: " + map.mapTer[mo.gridPos.y, mo.gridPos.x] + " x:" + mo.gridPos.x.ToString() + ", y:" + mo.gridPos.y.ToString());
                    myMainForm.listBox1Helper.Refresh();
                    Point pt1 = new Point();
                    pt1.X = mo.gridPos.x;
                    pt1.Y = mo.gridPos.y;
                    DrawGridPos(pt1);
                }
            }


            myMainForm.listBox1Helper.Items.Add("Movement Rate: " + MoveCount.ToString());

            List<Point> points = new List<Point>();
            // Draw start and end poitsn
            Point pt = new Point();
            pt.X = Convert.ToInt32(myMainForm.textBoxStartXHelper.Text);
            pt.Y = Convert.ToInt32(myMainForm.textBoxStartYHelper.Text);
            points.Add(pt);

            foreach (GridPos gp in FinalTrack)
            {
                pt = new Point();
                pt.X = gp.x;
                pt.Y = gp.y;
                points.Add(pt);
                myMainForm.listBox1Helper.Items.Add(map.mapTer[gp.y, gp.x]);
            }
            // Draw dest
            pt = new Point();
            pt.X = dest.x;
            pt.Y = dest.y;
            points.Add(pt);
            DrawAGridPos(points);
        }


        public void GoWarGames(GridPos finalDest, bool walledOff)
        {
            // spin up two process
            // Run one step of one,
            // run one step of second
            // return if one wins to end, run forever otherwise.
            moOnePreviousStepGridPos = new GridPos();
            moOnePreviousStepGridPos.x = moOnePreviousStepGridPos.y = 0;
            moTwoPreviousStepGridPos = new GridPos();
            moTwoPreviousStepGridPos.x = moTwoPreviousStepGridPos.y = 0;
            bool Won = false;
            bool Won2 = false;

            mo.gridPos.x = Convert.ToInt16(myMainForm.textBoxStartXHelper.Text);
            mo.gridPos.y = Convert.ToInt16(myMainForm.textBoxStartYHelper.Text);
            mo2.gridPos.x = Convert.ToInt16(myMainForm.textBox2StartXHelper.Text);
            mo2.gridPos.y = Convert.ToInt16(myMainForm.textBox2StartYHelper.Text);

            GridPos mo1Dest = new GridPos();
            GridPos mo2Dest = new GridPos();

            while (!Won && !Won2)
            {
                Thread.Sleep(250);
                // Set each mo dest to current mo.gridpos of opponent.
                mo1Dest.x = mo2.gridPos.x;
                mo1Dest.y = mo2.gridPos.y;
                DrawMapGridPos(false);
                Won = GoOne(mo1Dest, walledOff);
                if (Won == true)
                {
                    MessageBox.Show("Mo TWO Wins!");
                    break;
                }
                mo2Dest.x = mo.gridPos.x;
                mo2Dest.y = mo.gridPos.y;
                Won2 = GoTwo(mo2Dest, walledOff);
                if (Won2 == true)
                {
                    MessageBox.Show("Mo ONE Wins!");
                    break;
                }
            }
        }

        public bool GoTwo(GridPos dest, bool walledOff)
        {
            int CycleCount = 0;

            int MoveCount = 0;

            mo2.moveRate = 10; //reset to default - refactor this later

            List<GridPos> FinalTrack = new List<GridPos>();
            //while (dest.x != mo2..gridPos.x || dest.y != mo2.gridPos.y)
            {
                myMainForm.Refresh();
                if (AtDest(dest, mo2) == true)
                {
                    return true; // Mo one already WON! not mo 2!
                }
                // Must check to see if mo2.is one step away from the destination.
                // If it is one step away, then just mo2.e there.
                // Looking at terrain can cause mo2.to do circles around the destination, looking for the best way to get up the hill or mo2.ntain or swamp.
                if (Math.Abs(mo2.gridPos.x - dest.x) <= 1 && Math.Abs(mo2.gridPos.y - dest.y) <= 1)
                {
                    mo2.gridPos.x = dest.x;
                    mo2.gridPos.y = dest.y;
                    FinalTrack.Add(dest);

                    myMainForm.listBox1Helper.Items.Add("Mo 2 moved to: " + map2.mapTer[mo2.gridPos.y, mo2.gridPos.x] + " x:" + mo2.gridPos.x.ToString() + ", y:" + mo2.gridPos.y.ToString());
                    myMainForm.listBox1Helper.Refresh();
                    Point pt1 = new Point();
                    pt1.X = mo2.gridPos.x;
                    pt1.Y = mo2.gridPos.y;
                    DrawGridPosMap2(pt1, false);
                    return false;
                    //break;
                }
                else
                {


                    CycleCount++;
                    myMainForm.listBox1Helper.Items.Add("CycleCount: " + CycleCount.ToString());

                    compassDir DirScore = FindCompassDir(dest, mo2);

                    int dirValLeft = -100;// Set defaultvalues to -100, incase we have to ignore one of these due to boundaries
                    int dirValCenter = -100;
                    int dirValRight = -100;
                    int dirValHardL = -100;// Set defaultvalues to -100, incase we have to ignore one of these due to boundaries
                    int dirValHardR = -100;// Set defaultvalues to -100, incase we have to ignore one of these due to boundaries

                    GridPos nextStepGridPos = new GridPos();
                    GridPos nextStepGridPosL = new GridPos(); // Left
                    GridPos nextStepGridPosC = new GridPos(); // Center
                    GridPos nextStepGridPosR = new GridPos(); // Right

                    GridPos nextStepGridPosHardL = new GridPos(); // Hard Left
                    GridPos nextStepGridPosHardR = new GridPos(); // Hard Right


                    GridPos InitGrid = new GridPos();
                    InitGrid.x = 0;
                    InitGrid.y = 0;

                    nextStepGridPos = InitGrid;


                    nextStepGridPosL = mo2.gridPos; // Set default next pos to current location, incase we have to ignore one of these due to boundaries
                    nextStepGridPosC = mo2.gridPos;
                    nextStepGridPosR = mo2.gridPos;

                    nextStepGridPosHardL = mo2.gridPos; // Hard Left
                    nextStepGridPosHardR = mo2.gridPos; // Hard Right


                    GridPos gPL = new GridPos();
                    GridPos gPC = new GridPos();
                    GridPos gPR = new GridPos();
                    GridPos gPHardL = new GridPos(); // Hard Left
                    GridPos gPHardR = new GridPos(); // Hard Right

                    switch (DirScore)
                    {
                        case compassDir.N:


                            if (mo2.gridPos.x - 1 >= 0)
                                gPL.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPL.y = mo2.gridPos.y - 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            gPC.x = mo2.gridPos.x;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPC.y = mo2.gridPos.y - 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo2.gridPos.x + 1 < 19)
                                gPR.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPR.y = mo2.gridPos.y - 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo2.gridPos.x - 1 >= 0)
                                gPHardL.x = mo2.gridPos.x - 1;
                            gPHardL.y = mo2.gridPos.y;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;


                            if (mo2.gridPos.x + 1 < 19)
                                gPHardR.x = mo2.gridPos.x + 1;
                            gPHardR.y = mo2.gridPos.y;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;

                        case compassDir.NE:

                            gPL.x = mo2.gridPos.x;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPL.y = mo2.gridPos.y - 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo2.gridPos.x + 1 < 19)
                                gPC.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPC.y = mo2.gridPos.y - 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo2.gridPos.x + 1 < 19)
                                gPR.x = mo2.gridPos.x + 1;
                            gPR.y = mo2.gridPos.y;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo2.gridPos.x - 1 >= 0)
                                gPHardL.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPHardL.y = mo2.gridPos.y - 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo2.gridPos.x + 1 < 19)
                                gPHardR.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPHardR.y = mo2.gridPos.y + 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.E:

                            if (mo2.gridPos.x + 1 < 19)
                                gPL.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPL.y = mo2.gridPos.y - 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo2.gridPos.x + 1 < 19)
                                gPC.x = mo2.gridPos.x + 1;
                            gPC.y = mo2.gridPos.y;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo2.gridPos.x + 1 < 19)
                                gPR.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPR.y = mo2.gridPos.y + 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;


                            // Hard left and hard right follows:

                            gPHardL.x = mo2.gridPos.x;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPHardL.y = mo2.gridPos.y - 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            gPHardR.x = mo2.gridPos.x;
                            if (mo2.gridPos.y + 1 < 19)
                                gPHardR.y = mo2.gridPos.y + 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.SE:

                            if (mo2.gridPos.x + 1 < 19)
                                gPL.x = mo2.gridPos.x + 1;
                            gPL.y = mo2.gridPos.y;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo2.gridPos.x + 1 < 19)
                                gPC.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPC.y = mo2.gridPos.y + 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            gPR.x = mo2.gridPos.x;
                            if (mo2.gridPos.y + 1 < 19)
                                gPR.y = mo2.gridPos.y + 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo2.gridPos.x + 1 < 19)
                                gPHardL.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPHardL.y = mo2.gridPos.y - 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo2.gridPos.x - 1 >= 0)
                                gPHardR.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPHardR.y = mo2.gridPos.y + 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.S:
                            if (mo2.gridPos.x + 1 < 19)
                                gPL.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPL.y = mo2.gridPos.y + 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            gPC.x = mo2.gridPos.x;
                            if (mo2.gridPos.y + 1 < 19)
                                gPC.y = mo2.gridPos.y + 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo2.gridPos.x - 1 >= 0)
                                gPR.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPR.y = mo2.gridPos.y + 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo2.gridPos.x + 1 < 19)
                                gPHardL.x = mo2.gridPos.x + 1;
                            gPHardL.y = mo2.gridPos.y;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo2.gridPos.x - 1 >= 0)
                                gPHardR.x = mo2.gridPos.x - 1;
                            gPHardR.y = mo2.gridPos.y;

                            dirValHardR = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.SW:
                            if (mo2.gridPos.x - 1 >= 0)
                                gPL.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPL.y = mo2.gridPos.y + 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo2.gridPos.x - 1 >= 0)
                                gPC.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPC.y = mo2.gridPos.y + 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo2.gridPos.x - 1 >= 0)
                                gPR.x = mo2.gridPos.x - 1;
                            gPR.y = mo2.gridPos.y;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo2.gridPos.x + 1 < 19)
                                gPHardL.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPHardL.y = mo2.gridPos.y + 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo2.gridPos.x - 1 >= 0)
                                gPHardR.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPHardR.y = mo2.gridPos.y - 1;

                            dirValHardR = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.W:
                            if (mo2.gridPos.x - 1 >= 0)
                            {
                                gPL.x = mo2.gridPos.x - 1;
                                if (mo2.gridPos.y + 1 < 19)
                                {
                                    gPL.y = mo2.gridPos.y + 1;

                                    dirValLeft = map2.map2Value[gPL.y, gPL.x];
                                    nextStepGridPosL = gPL;
                                }
                            }
                            if (mo2.gridPos.x - 1 >= 0)
                                gPC.x = mo2.gridPos.x - 1;
                            gPC.y = mo2.gridPos.y;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo2.gridPos.x - 1 >= 0)
                                gPR.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPR.y = mo2.gridPos.y - 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            gPHardL.x = mo2.gridPos.x;
                            if (mo2.gridPos.y + 1 < 19)
                                gPHardL.y = mo2.gridPos.y + 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            gPHardR.x = mo2.gridPos.x;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPHardR.y = mo2.gridPos.y - 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.NW:
                            if (mo2.gridPos.x - 1 >= 0)
                                gPL.x = mo2.gridPos.x - 1;
                            gPL.y = mo2.gridPos.y;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo2.gridPos.x - 1 >= 0)
                                gPC.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPC.y = mo2.gridPos.y - 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            gPR.x = mo2.gridPos.x;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPR.y = mo2.gridPos.y - 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo2.gridPos.x - 1 >= 0)
                                gPHardL.x = mo2.gridPos.x - 1;
                            if (mo2.gridPos.y + 1 < 19)
                                gPHardL.y = mo2.gridPos.y + 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo2.gridPos.x + 1 < 19)
                                gPHardR.x = mo2.gridPos.x + 1;
                            if (mo2.gridPos.y - 1 >= 0)
                                gPHardR.y = mo2.gridPos.y - 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                    }

                    // Add a movement penalty for moving left and right - this should make mo prefer going straight.
                    //dirValLeft += Convert.ToInt16(Average/3);
                    //dirValRight += Convert.ToInt16(Average/3);
                    // Add a movement penalty for moving hard left and hard right - this should make mo prefer going straight. 
                    //dirValHardL += Convert.ToInt16(Average*.5);
                    //dirValHardR += Convert.ToInt16(Average*.5);

                    bool cantMove = false;
                    String MoveDir = "";

                    if (walledOff)
                    {
                        if (dirValCenter == tv.Road)
                            MoveDir = "Center";
                        else if (dirValLeft == tv.Road)
                            MoveDir = "Left";
                        else if (dirValRight == tv.Road)
                            MoveDir = "Right";
                        else if (dirValHardL == tv.Road)
                            MoveDir = "HardL";
                        else if (dirValHardR == tv.Road)
                            MoveDir = "HardR";
                    }
                    else
                    {
                        if (dirValCenter == tv.Road
                            && dirValCenter >= dirValHardR
                            && dirValCenter >= dirValHardL)
                            MoveDir = "Center";
                        else if (dirValLeft == tv.Road
                            && dirValLeft >= dirValHardR
                            && dirValLeft >= dirValHardL)
                            MoveDir = "Left";
                        else if (dirValRight == tv.Road
                            && dirValRight >= dirValHardL)
                            MoveDir = "Right";
                        else if (dirValHardL == tv.Road)
                            MoveDir = "HardL";
                        else
                            MoveDir = "HardR";
                    }


                    if (MoveDir == "Center"
                     && (moTwoPreviousStepGridPos.x != nextStepGridPosC.x
                     || moTwoPreviousStepGridPos.y != nextStepGridPosC.y)
                     )
                    {
                        MoveCount += dirValCenter;
                        //if (mo.moveRate >= dirValCenter)
                        nextStepGridPos = nextStepGridPosC;
                        mo.moveRate = mo.moveRate - dirValCenter;
                        // Go Center
                    }
                    else if (MoveDir == "Left"
                          && (moTwoPreviousStepGridPos.x != nextStepGridPosL.x
                          || moTwoPreviousStepGridPos.y != nextStepGridPosL.y)
                          )
                    {
                        MoveCount += dirValLeft;
                        nextStepGridPos = nextStepGridPosL;
                        mo.moveRate = mo.moveRate - dirValLeft;
                        // Go Left
                    }
                    else if (MoveDir == "Right" // is Right better than LL
                          && (moTwoPreviousStepGridPos.x != nextStepGridPosR.y
                          || moTwoPreviousStepGridPos.y != nextStepGridPosR.y)
                          )
                    {
                        MoveCount += dirValRight;
                        nextStepGridPos = nextStepGridPosR;
                        mo.moveRate = mo.moveRate - dirValRight;
                        // Go Right
                    }
                    else if (MoveDir == "HardL"  // is LL better than RR
                        && (moTwoPreviousStepGridPos.x != nextStepGridPosHardL.x
                        || moTwoPreviousStepGridPos.y != nextStepGridPosHardL.y)
                        )
                    {
                        MoveCount += dirValHardL;
                        nextStepGridPos = nextStepGridPosHardL;
                        mo.moveRate = mo.moveRate - dirValHardL;
                        // Go Hard Left
                    }
                    else if (MoveDir == "HardR" // is RR better than Center?
                        && (moTwoPreviousStepGridPos.x != nextStepGridPosHardR.x
                        || moTwoPreviousStepGridPos.y != nextStepGridPosHardR.y)
                        )
                    {
                        MoveCount += dirValHardR;
                        nextStepGridPos = nextStepGridPosHardR;
                        mo.moveRate = mo.moveRate - dirValHardR;
                        // Go Hard Right
                    }
                    else if (walledOff == false)// go center
                    {
                        MoveCount += dirValCenter;
                        //if (mo.moveRate >= dirValCenter)
                        nextStepGridPos = nextStepGridPosC;
                        mo.moveRate = mo.moveRate - dirValCenter;
                        // Go Center
                    }
                    else
                    {
                        nextStepGridPos = mo2.gridPos; // don't move!
                    }

                    moTwoPreviousStepGridPos = mo2.gridPos; // Store the current value before moving, then next time through, don't allow new step to equal prev step. No step-backs. Forward only

                    if (cantMove == true)
                    {
                        // not enough movement remaining. Break, increment the cycle count, continue with next cycle.
                        //mo.moveRate = 10; //reset to default - refactor this later
                        return false;
                        //break;
                    }

                    mo2.gridPos = nextStepGridPos;
                    FinalTrack.Add(nextStepGridPos);

                    myMainForm.listBox1Helper.Items.Add("Mo 2 moved to: " + map.mapTer[mo2.gridPos.y, mo2.gridPos.x] + " x:" + mo2.gridPos.x.ToString() + ", y:" + mo2.gridPos.y.ToString());
                    myMainForm.listBox1Helper.Refresh();
                    Point pt1 = new Point();
                    pt1.X = mo2.gridPos.x;
                    pt1.Y = mo2.gridPos.y;
                    DrawGridPosMap2(pt1, false);
                }
            }


            myMainForm.listBox1Helper.Items.Add("Movement Rate: " + MoveCount.ToString());

            List<Point> points = new List<Point>();
            //// Draw start and end poitsn
            Point pt = new Point();
            //pt.X = Convert.ToInt32(textBoxStartX.Text);
            //pt.Y = Convert.ToInt32(textBoxStartY.Text);
            //points.Add(pt);

            foreach (GridPos gp in FinalTrack)
            {
                pt = new Point();
                pt.X = gp.x;
                pt.Y = gp.y;
                points.Add(pt);
                myMainForm.listBox1Helper.Items.Add(map2.map2Ter[gp.y, gp.x]);
                Thread.Sleep(100);
            }
            // Draw dest
            //pt = new Point();
            //pt.X = dest.x;
            //pt.Y = dest.y;
            //points.Add(pt);
            //DrawAGridPosMap(points);

            return false;
        }

        public bool GoOne(GridPos dest, bool walledOff)
        {
            int CycleCount = 0;

            int MoveCount = 0;

            mo.moveRate = 10; //reset to default - refactor this later

            List<GridPos> FinalTrack = new List<GridPos>();
            //while (dest.x != mo.gridPos.x || dest.y != mo.gridPos.y)
            {
                myMainForm.Refresh();
                if (AtDest(dest, mo) == true)
                {
                    return true;
                }
                // Must check to see if Mo is one step away from the destination.
                // If it is one step away, then just move there.
                // Looking at terrain can cause Mo to do circles around the destination, looking for the best way to get up the hill or mountain or swamp.
                if (Math.Abs(mo.gridPos.x - dest.x) <= 1 && Math.Abs(mo.gridPos.y - dest.y) <= 1)
                {
                    mo.gridPos.x = dest.x;
                    mo.gridPos.y = dest.y;
                    FinalTrack.Add(dest);

                    myMainForm.listBox1Helper.Items.Add("Mo 1 moved to: " + map2.mapTer[mo.gridPos.y, mo.gridPos.x] + " x:" + mo.gridPos.x.ToString() + ", y:" + mo.gridPos.y.ToString());
                    myMainForm.listBox1Helper.Refresh();
                    Point pt1 = new Point();
                    pt1.X = mo.gridPos.x;
                    pt1.Y = mo.gridPos.y;
                    DrawGridPosMap2(pt1, true);
                    return false;
                    //break;
                }
                else
                {


                    CycleCount++;
                    myMainForm.listBox1Helper.Items.Add("CycleCount: " + CycleCount.ToString());

                    compassDir DirScore = FindCompassDir(dest, mo);

                    int dirValLeft = -100;// Set defaultvalues to -100, incase we have to ignore one of these due to boundaries
                    int dirValCenter = -100;
                    int dirValRight = -100;
                    int dirValHardL = -100;// Set defaultvalues to -100, incase we have to ignore one of these due to boundaries
                    int dirValHardR = -100;// Set defaultvalues to -100, incase we have to ignore one of these due to boundaries

                    GridPos nextStepGridPos = new GridPos();
                    GridPos nextStepGridPosL = new GridPos(); // Left
                    GridPos nextStepGridPosC = new GridPos(); // Center
                    GridPos nextStepGridPosR = new GridPos(); // Right
                    GridPos nextStepGridPosHardL = new GridPos(); // Hard Left
                    GridPos nextStepGridPosHardR = new GridPos(); // Hard Right


                    GridPos InitGrid = new GridPos();
                    InitGrid.x = 0;
                    InitGrid.y = 0;

                    nextStepGridPos = InitGrid;


                    nextStepGridPosL = mo.gridPos; // Set default next pos to current location, incase we have to ignore one of these due to boundaries
                    nextStepGridPosC = mo.gridPos;
                    nextStepGridPosR = mo.gridPos;

                    nextStepGridPosHardL = mo.gridPos; // Hard Left
                    nextStepGridPosHardR = mo.gridPos; // Hard Right


                    GridPos gPL = new GridPos();
                    GridPos gPC = new GridPos();
                    GridPos gPR = new GridPos();
                    GridPos gPHardL = new GridPos(); // Hard Left
                    GridPos gPHardR = new GridPos(); // Hard Right

                    switch (DirScore)
                    {
                        case compassDir.N:


                            if (mo.gridPos.x - 1 >= 0)
                                gPL.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPL.y = mo.gridPos.y - 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            gPC.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPC.y = mo.gridPos.y - 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x + 1 < 19)
                                gPR.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPR.y = mo.gridPos.y - 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardL.x = mo.gridPos.x - 1;
                            gPHardL.y = mo.gridPos.y;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;


                            if (mo.gridPos.x + 1 < 19)
                                gPHardR.x = mo.gridPos.x + 1;
                            gPHardR.y = mo.gridPos.y;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;

                        case compassDir.NE:

                            gPL.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPL.y = mo.gridPos.y - 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x + 1 < 19)
                                gPC.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPC.y = mo.gridPos.y - 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x + 1 < 19)
                                gPR.x = mo.gridPos.x + 1;
                            gPR.y = mo.gridPos.y;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardL.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardL.y = mo.gridPos.y - 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x + 1 < 19)
                                gPHardR.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardR.y = mo.gridPos.y + 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.E:

                            if (mo.gridPos.x + 1 < 19)
                                gPL.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPL.y = mo.gridPos.y - 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x + 1 < 19)
                                gPC.x = mo.gridPos.x + 1;
                            gPC.y = mo.gridPos.y;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x + 1 < 19)
                                gPR.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPR.y = mo.gridPos.y + 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;


                            // Hard left and hard right follows:

                            gPHardL.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardL.y = mo.gridPos.y - 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            gPHardR.x = mo.gridPos.x;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardR.y = mo.gridPos.y + 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.SE:

                            if (mo.gridPos.x + 1 < 19)
                                gPL.x = mo.gridPos.x + 1;
                            gPL.y = mo.gridPos.y;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x + 1 < 19)
                                gPC.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPC.y = mo.gridPos.y + 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            gPR.x = mo.gridPos.x;
                            if (mo.gridPos.y + 1 < 19)
                                gPR.y = mo.gridPos.y + 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x + 1 < 19)
                                gPHardL.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardL.y = mo.gridPos.y - 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardR.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardR.y = mo.gridPos.y + 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.S:
                            if (mo.gridPos.x + 1 < 19)
                                gPL.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPL.y = mo.gridPos.y + 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            gPC.x = mo.gridPos.x;
                            if (mo.gridPos.y + 1 < 19)
                                gPC.y = mo.gridPos.y + 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x - 1 >= 0)
                                gPR.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPR.y = mo.gridPos.y + 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x + 1 < 19)
                                gPHardL.x = mo.gridPos.x + 1;
                            gPHardL.y = mo.gridPos.y;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardR.x = mo.gridPos.x - 1;
                            gPHardR.y = mo.gridPos.y;

                            dirValHardR = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.SW:
                            if (mo.gridPos.x - 1 >= 0)
                                gPL.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPL.y = mo.gridPos.y + 1;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPC.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPC.y = mo.gridPos.y + 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x - 1 >= 0)
                                gPR.x = mo.gridPos.x - 1;
                            gPR.y = mo.gridPos.y;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x + 1 < 19)
                                gPHardL.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardL.y = mo.gridPos.y + 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardR.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardR.y = mo.gridPos.y - 1;

                            dirValHardR = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.W:
                            if (mo.gridPos.x - 1 >= 0)
                            {
                                gPL.x = mo.gridPos.x - 1;
                                if (mo.gridPos.y + 1 < 19)
                                {
                                    gPL.y = mo.gridPos.y + 1;

                                    dirValLeft = map2.map2Value[gPL.y, gPL.x];
                                    nextStepGridPosL = gPL;
                                }
                            }
                            if (mo.gridPos.x - 1 >= 0)
                                gPC.x = mo.gridPos.x - 1;
                            gPC.y = mo.gridPos.y;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            if (mo.gridPos.x - 1 >= 0)
                                gPR.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPR.y = mo.gridPos.y - 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            gPHardL.x = mo.gridPos.x;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardL.y = mo.gridPos.y + 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            gPHardR.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardR.y = mo.gridPos.y - 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                        case compassDir.NW:
                            if (mo.gridPos.x - 1 >= 0)
                                gPL.x = mo.gridPos.x - 1;
                            gPL.y = mo.gridPos.y;

                            dirValLeft = map2.map2Value[gPL.y, gPL.x];
                            nextStepGridPosL = gPL;

                            if (mo.gridPos.x - 1 >= 0)
                                gPC.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPC.y = mo.gridPos.y - 1;

                            dirValCenter = map2.map2Value[gPC.y, gPC.x];
                            nextStepGridPosC = gPC;

                            gPR.x = mo.gridPos.x;
                            if (mo.gridPos.y - 1 >= 0)
                                gPR.y = mo.gridPos.y - 1;

                            dirValRight = map2.map2Value[gPR.y, gPR.x];
                            nextStepGridPosR = gPR;

                            // Hard left and hard right follows:

                            if (mo.gridPos.x - 1 >= 0)
                                gPHardL.x = mo.gridPos.x - 1;
                            if (mo.gridPos.y + 1 < 19)
                                gPHardL.y = mo.gridPos.y + 1;

                            dirValHardL = map2.map2Value[gPHardL.y, gPHardL.x];
                            nextStepGridPosHardL = gPHardL;

                            if (mo.gridPos.x + 1 < 19)
                                gPHardR.x = mo.gridPos.x + 1;
                            if (mo.gridPos.y - 1 >= 0)
                                gPHardR.y = mo.gridPos.y - 1;

                            dirValHardR = map2.map2Value[gPHardR.y, gPHardR.x];
                            nextStepGridPosHardR = gPHardR;

                            break;
                    }

                    // Add a movement penalty for moving left and right - this should make mo prefer going straight.
                    //dirValLeft += Convert.ToInt16(Average/3);
                    //dirValRight += Convert.ToInt16(Average/3);
                    // Add a movement penalty for moving hard left and hard right - this should make mo prefer going straight. 
                    //dirValHardL += Convert.ToInt16(Average*.5);
                    //dirValHardR += Convert.ToInt16(Average*.5);

                    bool cantMove = false;

                    String MoveDir = "";

                    if (walledOff)
                    {
                        if (dirValCenter == tv.Road)
                            MoveDir = "Center";
                        else if (dirValLeft == tv.Road)
                            MoveDir = "Left";
                        else if (dirValRight == tv.Road)
                            MoveDir = "Right";
                        else if (dirValHardL == tv.Road)
                            MoveDir = "HardL";
                        else if (dirValHardR == tv.Road)
                            MoveDir = "HardR";
                    }
                    else
                    {
                        if (dirValCenter == tv.Road
                        && dirValCenter >= dirValHardR
                        && dirValCenter >= dirValHardL)
                            MoveDir = "Center";
                        else if (dirValLeft == tv.Road
                            && dirValLeft >= dirValHardR
                            && dirValLeft >= dirValHardL)
                            MoveDir = "Left";
                        else if (dirValRight == tv.Road
                            && dirValRight >= dirValHardL)
                            MoveDir = "Right";
                        else if (dirValHardL == tv.Road)
                            MoveDir = "HardL";
                        else
                            MoveDir = "HardR";
                    }

                    if (MoveDir == "Center"
                     && (moOnePreviousStepGridPos.x != nextStepGridPosC.x
                     || moOnePreviousStepGridPos.y != nextStepGridPosC.y)
                     )
                    {
                        MoveCount += dirValCenter;
                        //if (mo.moveRate >= dirValCenter)
                        nextStepGridPos = nextStepGridPosC;
                        mo.moveRate = mo.moveRate - dirValCenter;
                        // Go Center
                    }
                    else if (MoveDir == "Left"
                          && (moOnePreviousStepGridPos.x != nextStepGridPosL.x
                          || moOnePreviousStepGridPos.y != nextStepGridPosL.y)
                          )
                    {
                        MoveCount += dirValLeft;
                        nextStepGridPos = nextStepGridPosL;
                        mo.moveRate = mo.moveRate - dirValLeft;
                        // Go Left
                    }
                    else if (MoveDir == "Right" // is Right better than LL
                          && (moOnePreviousStepGridPos.x != nextStepGridPosR.y
                          || moOnePreviousStepGridPos.y != nextStepGridPosR.y)
                          )
                    {
                        MoveCount += dirValRight;
                        nextStepGridPos = nextStepGridPosR;
                        mo.moveRate = mo.moveRate - dirValRight;
                        // Go Right
                    }
                    else if (MoveDir == "HardL"  // is LL better than RR
                        && (moOnePreviousStepGridPos.x != nextStepGridPosHardL.x
                        || moOnePreviousStepGridPos.y != nextStepGridPosHardL.y)
                        )
                    {
                        MoveCount += dirValHardL;
                        nextStepGridPos = nextStepGridPosHardL;
                        mo.moveRate = mo.moveRate - dirValHardL;
                        // Go Hard Left
                    }
                    else if (MoveDir == "HardR" // is RR better than Center?
                        && (moOnePreviousStepGridPos.x != nextStepGridPosHardR.x
                        || moOnePreviousStepGridPos.y != nextStepGridPosHardR.y)
                        )
                    {
                        MoveCount += dirValHardR;
                        nextStepGridPos = nextStepGridPosHardR;
                        mo.moveRate = mo.moveRate - dirValHardR;
                        // Go Hard Right
                    }
                    else if (walledOff == false)// go center
                    {
                        MoveCount += dirValCenter;
                        //if (mo.moveRate >= dirValCenter)
                        nextStepGridPos = nextStepGridPosC;
                        mo.moveRate = mo.moveRate - dirValCenter;
                        // Go Center
                    }
                    else
                    {
                        nextStepGridPos = mo.gridPos; // don't move!
                    }
                    moOnePreviousStepGridPos = mo.gridPos; // Store the current value before moving, then next time through, don't allow new step to equal prev step. No step-backs. Forward only

                    if (cantMove == true)
                    {
                        // not enough movement remaining. Break, increment the cycle count, continue with next cycle.
                        //mo.moveRate = 10; //reset to default - refactor this later
                        return false;
                        //break;
                    }

                    mo.gridPos = nextStepGridPos;
                    FinalTrack.Add(nextStepGridPos);

                    myMainForm.listBox1Helper.Items.Add("Mo 1 moved to: " + map.mapTer[mo.gridPos.y, mo.gridPos.x] + " x:" + mo.gridPos.x.ToString() + ", y:" + mo.gridPos.y.ToString());
                    myMainForm.listBox1Helper.Refresh();
                    Point pt1 = new Point();
                    pt1.X = mo.gridPos.x;
                    pt1.Y = mo.gridPos.y;
                    DrawGridPosMap2(pt1, true);
                }
            }


            myMainForm.listBox1Helper.Items.Add("Movement Rate: " + MoveCount.ToString());

            List<Point> points = new List<Point>();
            //// Draw start and end poitsn
            Point pt = new Point();
            //pt.X = Convert.ToInt32(textBoxStartX.Text);
            //pt.Y = Convert.ToInt32(textBoxStartY.Text);
            //points.Add(pt);

            foreach (GridPos gp in FinalTrack)
            {
                pt = new Point();
                pt.X = gp.x;
                pt.Y = gp.y;
                points.Add(pt);
                myMainForm.listBox1Helper.Items.Add(map2.map2Ter[gp.y, gp.x]);
                Thread.Sleep(100);
            }
            // Draw dest
            //pt = new Point();
            //pt.X = dest.x;
            //pt.Y = dest.y;
            //points.Add(pt);
            //DrawAGridPosMap(points);

            return false;
        }

        public void GoWarGames2(GridPos finalDest, bool walledOff)
        {
            // spin up two process
            // Run one step of one,
            // run one step of second
            // return if one wins to end, run forever otherwise.
            moOnePreviousStepGridPos = new GridPos();
            moOnePreviousStepGridPos.x = moOnePreviousStepGridPos.y = 0;
            moTwoPreviousStepGridPos = new GridPos();
            moTwoPreviousStepGridPos.x = moTwoPreviousStepGridPos.y = 0;
            bool Won = false;
            bool Won2 = false;

            mo.gridPos.x = Convert.ToInt16(myMainForm.textBoxStartXHelper.Text);
            mo.gridPos.y = Convert.ToInt16(myMainForm.textBoxStartYHelper.Text);
            mo2.gridPos.x = Convert.ToInt16(myMainForm.textBox2StartXHelper.Text);
            mo2.gridPos.y = Convert.ToInt16(myMainForm.textBox2StartYHelper.Text);

            GridPos mo1Dest = new GridPos();
            GridPos mo2Dest = new GridPos();

            while (!Won && !Won2)
            {
                Thread.Sleep(250);
                // Set each mo dest to current mo.gridpos of opponent.
                mo1Dest.x = mo2.gridPos.x;
                mo1Dest.y = mo2.gridPos.y;
                DrawMapGridPos(false);
                Won = GoOne(mo1Dest, walledOff);
                // Check line of sight, shoot and win.
                Won = CheckLOS(mo, mo2);

                if (Won == true)
                {
                    MessageBox.Show("Mo ONE Wins!");
                    break;
                }
                mo2Dest.x = mo.gridPos.x;
                mo2Dest.y = mo.gridPos.y;
                Won2 = GoTwo(mo2Dest, walledOff);
                Won2 = CheckLOS(mo, mo2);
                if (Won2 == true)
                {
                    MessageBox.Show("Mo TWO Wins!");
                    break;
                }
            }
        }

    }
}
