// Robin Hood (Rank B)
// 2020 - 2- 9
// https://paiza.jp/challenges/199/show

//#define DEBUG
//#define ThiefTest

using System;
using System.Collections.Generic;

public enum Direction : int {
    North = 0, East, South, West
}

public struct Point {
    public int X { get; set; }
    public int Y { get; set; }
    
    public Point(int x, int y) {
        this.X = x;
        this.Y = y;
    }
}

public class Thief {
    public int X { get; set; }
    public int Y { get; set; }
    private Direction Dir { get; set; }
    
    private Point[] pointsForTurningRight = new[] {
        new Point(1, 0), new Point(0, 1), new Point(-1, 0), new Point(0, -1)
    };
    private Point[] pointsForTurningLeft = new[] {
        new Point(-1, 0), new Point(0, -1), new Point(1, 0), new Point(0, 1)
    };
    
    public Thief(int x, int y) {
        this.X = x;
        this.Y = y;
        this.Dir = Direction.North;
    }
    
    public void TurnRight() {
        var dir = (int)Dir;
        this.X += pointsForTurningRight[dir].X;
        this.Y += pointsForTurningRight[dir].Y;
        
        this.Dir = (Direction)(((int)this.Dir + 1 ) % Enum.GetValues(typeof(Direction)).Length);
    }
    
    public void TurnLeft() {
        var dir = (int)Dir;
        this.X += pointsForTurningLeft[dir].X;
        this.Y += pointsForTurningLeft[dir].Y;
        
        var enumLength = Enum.GetValues(typeof(Direction)).Length;
        this.Dir = (Direction)(((int)this.Dir + enumLength - 1) % enumLength);
    }
    
    public void PrintStatus() {
        Console.WriteLine(this.X.ToString() + ", " + this.Y.ToString() + ", " + this.Dir.ToString());
        Console.WriteLine();
    }
}

public static class MainRoutine {
    public static void PrintTown(char[,] town) {
        var height = town.GetLength(1);
        var width = town.GetLength(0);
        
        for(var i=0; i < height; i++) {
            for(var j=0; j < width; j++) {
                Console.Write(town[j,i]);
            }
            Console.WriteLine();
        }
        
        #if DEBUG
        Console.WriteLine();
        #endif
    }
    
    public static void UpdateThiefPosition(Thief thief, char[,] town, bool isOld) {
        if (isOld) {
            town[thief.X, thief.Y] = 'O';
        } else {
            town[thief.X, thief.Y] = 'N';
        }
    }
    
    public static bool IsInTown(Thief thief, char[,] town) {
        var height = town.GetLength(1);
        var width = town.GetLength(0);
        return (0 < thief.X && thief.X < width && 0 < thief.Y && thief.Y < height);
    }
    
    
    public static void Main() {
        var initInput = Console.ReadLine().Trim().Split(' ');
        var height = int.Parse(initInput[0]);
        var width = int.Parse(initInput[1]);
        
        var initInput2 = Console.ReadLine().Trim().Split(' ');
        var xxx = int.Parse(initInput2[0]) - 1;
        var yyy = int.Parse(initInput2[1]) - 1;
        var thief = new Thief(xxx, yyy);
        
        var town = new char[width, height];
        for(var i=0; i < height; i++) {
            var input = Console.ReadLine().Trim();
            for(var j=0; j < width; j++) {
                town[j, i] = input[j];
            }
        }
        
        #if DEBUG
        for(var i=0; i< height; i++) {
            for(var j=0; j < width; j++) {
                if(town[j, i] == '*') {
                    town[j, i] = 'F';
                } else {
                    town[j, i] = 'O';
                }
            }
        }
        #endif
        
        #if ThiefTest
        for(var i=0; i < height; i++) {
            for(var j=0; j < width; j++) {
                town[j, i]  = 'O';
            }
        }
        #endif
        
        #if DEBUG
        var tempChar = town[thief.X, thief.Y];
        town[thief.X, thief.Y] = 'S'; 
        PrintTown(town);
        town[thief.X, thief.Y] = tempChar;
        thief.PrintStatus();
        #endif
        
        while(IsInTown(thief, town)) {
            
            #if DEBUG
            if (town[thief.X, thief.Y] == 'O') {
                town[thief.X, thief.Y] = 'F';
                thief.TurnRight();
            } else {
                town[thief.X, thief.Y] = 'O';
                thief.TurnLeft();
            }
            PrintTown(town);
            thief.PrintStatus();
            
            #else
            if (town[thief.X, thief.Y] == '.') {
                town[thief.X, thief.Y] = '*';
                thief.TurnRight();
            } else {
                town[thief.X, thief.Y] = '.';
                thief.TurnLeft();
            }
            
            #endif
        }
        
        #if DEBUG
        #else
        PrintTown(town);
        #endif
    }
}
