using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;
using System.Drawing;
using System.Threading;
using System.IO;
 
namespace lab1
{
    class Program
    {
        //***********************************************************************************
        //Program: lab1.cs
        //Description: Drawing Shapes
        //Date: Jan. 18/2017
        //Author: Wonsang Yoon
        //Course: CMPE1600
        //Class: CMPE1600
        //***********************************************************************************


        enum Shape { Circle,Squre, Star} // types of shape
        struct info // make struct for shape information
        {
            public Shape _shape;
            public double XLocation;
            public double YLocatoin;
            public Color _shapeColor;
        }
        static void Main(string[] args)
        {
            //intializing
            Color objColor = new Color();
            List<info> structureslist = new List<info>();
            CDrawer canvas = new CDrawer();
            Point location = new Point(200, 200);
            info infoN;
            string filename;
            Shape shape = Shape.Circle;
            int i=0;

            Options(ref canvas);// Open Menu

            while (true) // go into while loop unless it fails
            {
                if (canvas.GetLastMouseLeftClick(out location)) //catch mouse click point
                {
                    //change shape information depends on click location
                    if (location.X > 20 && location.X < 70 && location.Y > 40 && location.Y < 90)
                        objColor = Color.Red;

                    else if (location.X > 80 && location.X < 170 && location.Y > 20 && location.Y < 90)
                        objColor = Color.Green;

                    else if (location.X > 180 && location.X < 270 && location.Y > 40 && location.Y < 90)
                        objColor = Color.Blue;

                    else if (location.X > 280 && location.X < 350 && location.Y > 40 && location.Y < 90)
                        shape = Shape.Circle;

                    else if (location.X > 360 && location.X < 450 && location.Y > 40 && location.Y < 90)
                        shape = Shape.Squre;

                    else if (location.X > 460 && location.X < 510 && location.Y > 40 && location.Y < 90)
                        shape = Shape.Star;

                    else if (location.X > 520 && location.X < 590 && location.Y > 40 && location.Y < 90) //Save option
                    {
                        Console.Write("Enter your filename: "); //choosing saving file name
                        filename = Console.ReadLine();

                        StreamWriter swOutputFile = new StreamWriter(filename);
                        //save shape information each line
                        foreach (info item in structureslist)
                        {
                            swOutputFile.WriteLine("{0},{1},{2},{3}", item._shape, item._shapeColor, item.XLocation, item.YLocatoin);
                        }
                        swOutputFile.Close(); //save
                    }
                       

                    else if (location.X > 600 && location.X < 670 && location.Y > 40 && location.Y < 90) //Load Option
                    {
                        Console.Write("Enter your filename: "); // choose loading file name
                        filename = Console.ReadLine();

                        //initializing for load variables
                        StreamReader swInputFile;
                        List<string> LoadList = new List<string>();
                        string text;
                        try // catch error while opening file
                        {
                            swInputFile = new StreamReader(filename);
                            while ((text = swInputFile.ReadLine()) != null)
                            {
                                string[] text2;
                                text2 = text.Split(',');
                                info infoO;
                                infoO._shapeColor = Color.Red;
                                infoO._shape = Shape.Circle;
                                infoO.XLocation = 0;
                                infoO.YLocatoin = 0;
                                switch (text2[0]) //check shape type
                                {
                                    case "Cirlce": { infoO._shape = Shape.Circle; break; }
                                    case "Square": { infoO._shape = Shape.Squre; break; }
                                    case "Star": { infoO._shape = Shape.Star; break; }
                                }
                                switch (text2[1])//check shape color
                                {
                                    case "Color [Red]": { infoO._shapeColor = Color.Red; break; }
                                    case "Color [Green]": { infoO._shapeColor = Color.Green; break; }
                                    case "Color [Blue]": { infoO._shapeColor = Color.Blue; break; }
                                }
                                infoO.XLocation = Convert.ToInt64(text2[2]); //check shape X location
                                infoO.YLocatoin = Convert.ToInt64(text2[3]); //check shape Y location
                                structureslist.Add(infoO); //add on to list to save information

                                foreach (info item in structureslist) //Call information on to GDI drawer from list
                                {

                                    switch (item._shape)
                                    {
                                        case Shape.Circle:
                                            {
                                                if (item._shapeColor == Color.Red) canvas.AddCenteredEllipse((int)item.XLocation, (int)item.YLocatoin, 20, 20, Color.Red);
                                                else if (item._shapeColor == Color.Green) canvas.AddCenteredEllipse((int)item.XLocation, (int)item.YLocatoin, 20, 20, Color.Green);
                                                else if (item._shapeColor == Color.Blue) canvas.AddCenteredEllipse((int)item.XLocation, (int)item.YLocatoin, 20, 20, Color.Blue);
                                                break;
                                            }
                                        case Shape.Squre:
                                            {
                                                if (item._shapeColor == Color.Red) canvas.AddCenteredRectangle((int)item.XLocation, (int)item.YLocatoin, 20, 20, Color.Red);
                                                else if (item._shapeColor == Color.Green) canvas.AddCenteredRectangle((int)item.XLocation, (int)item.YLocatoin, 20, 20, Color.Green);
                                                else if (item._shapeColor == Color.Blue) canvas.AddCenteredRectangle((int)item.XLocation, (int)item.YLocatoin, 20, 20, Color.Blue);
                                                break;
                                            }
                                        case Shape.Star:
                                            {
                                                if (item._shapeColor == Color.Red) canvas.AddText("S", 20, (int)item.XLocation, (int)item.YLocatoin, 20, 20, Color.Red);
                                                else if (item._shapeColor == Color.Green) canvas.AddText("S", 20, (int)item.XLocation, (int)item.YLocatoin, 20, 20, Color.Green);
                                                else if (item._shapeColor == Color.Blue) canvas.AddText("S", 20, (int)item.XLocation, (int)item.YLocatoin, 20, 20, Color.Blue);
                                                break;
                                            }
                                    }

                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error, the error was {0}", e);
                        }

                    }

                    else if (location.X > 680 && location.X < 750 && location.Y > 40 && location.Y < 90)
                    {
                        canvas.Clear(); // clear GDI drawer
                        Options(ref canvas); //Recall menu
                    }
                    else //If mouse click location is out of menu area draw the shapes
                    {
                        switch (shape)//Goes into diffrent options depends on the shape
                        {
                            case Shape.Circle: 
                                {
                                    canvas.AddCenteredEllipse(location, 20, 20, objColor);
                                    infoN._shape = shape;
                                    infoN._shapeColor = objColor;
                                    infoN.XLocation = location.X;
                                    infoN.YLocatoin = location.Y;
                                    structureslist.Add(infoN);

                                    break;
                                }
                            case Shape.Squre:
                                {
                                    canvas.AddCenteredRectangle(location, 20, 20, objColor);
                                    infoN._shape = shape;
                                    infoN._shapeColor = objColor;
                                    infoN.XLocation = location.X;
                                    infoN.YLocatoin = location.Y;
                                    structureslist.Add(infoN);
                                    break;
                                }
                            case Shape.Star:
                                {
                                    canvas.AddText("S", 20, location.X, location.Y, 20, 30, objColor);
                                    infoN._shape = shape;
                                    infoN._shapeColor = objColor;
                                    infoN.XLocation = location.X;
                                    infoN.YLocatoin = location.Y;
                                    structureslist.Add(infoN);
                                    break;
                                }
                        }
                    }
                }
            }
        }
        //********************************************************************************************
        //Method: static void Options (ref CDrawer canvas)
        //Purpose: Draw Menu for the Options
        //Parameters:Cdrawer
        //*********************************************************************************************
        static void Options(ref CDrawer canvas) //menu
        {
            canvas.AddText("Red",    16, 20,  50, 60,  40, Color.Red);
            canvas.AddText("Green",  16, 80,  50, 100, 40, Color.Green);
            canvas.AddText("Blue",   16, 180, 50, 60,  40, Color.Blue);
            canvas.AddText("Circle", 16, 280, 50, 100, 40, Color.Yellow);
            canvas.AddText("Square", 16, 360, 50, 100, 40, Color.Yellow);
            canvas.AddText("Star",   16, 460, 50, 60,  40, Color.Yellow);
            canvas.AddText("Save",   16, 520, 50, 60,  40, Color.Yellow);
            canvas.AddText("Load",   16, 600, 50, 60,  40, Color.Yellow);
            canvas.AddText("Clear",  16, 680, 50, 60,  40, Color.Yellow);
        }
    }
}
