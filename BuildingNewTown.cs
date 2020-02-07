//　街の建設(RANK A)
// Paiza hackathon
// https://paiza.jp/botchi/challenges/botchi_a_1001
// 2020 - 1 - 29

using System;
using System.Collections.Generic;

public class Building {
    public int Num {get; set;}
    public int Width {get; set;}
    public int Height {get; set;}
    public int DoorX {get; set;}
    public int DoorY {get; set;}
    
    public Building(int num, int width, int height, int doorX, int doorY) {
        this.Num = num;
        this.Width = width;
        this.Height = height;
        this.DoorX = doorX;
        this.DoorY = doorY;
    }
    
    // 一周り大きい建物を返す
    public Building ToBoarden2() {
        return new Building(this.Num, this.Width + 2, this.Height + 2, this.DoorX, this.DoorY);
    }
}

public class NewTown {
    public int[,] Field {get; set;}
    
    public NewTown(int width, int height) {
        this.Field = new int[width, height];
    }
    
    bool IsInField(Building b, int x, int y) {
        return (0 <= x && x + b.Width <= Field.GetLength(0) && 0 <= y && y + b.Height <= Field.GetLength(1));
    }
    
    bool IsIntersect(Building b, int x, int y) {
        for(var i=x; i < x + b.Width; i++) {
            for(var j=y; j < y + b.Height; j++) {
                if(Field[i,j] != 0) {
                    return true;
                }
            }
        }
        return false;
    }
    
    public bool CanPut(Building b, int x, int y) {
        return (IsInField(b, x, y) && !IsIntersect(b, x, y));
    }
    
    public void Put(Building b, int x, int y) {
        for(var i=x; i < x + b.Width; i++) {
            for(var j=y; j < y + b.Height; j++) {
                Field[i,j] = b.Num;
            }
        }
    }
    
    public void PrintTown() {
        var width = Field.GetLength(0);
        for(var i=0; i < Field.GetLength(1); i++) {
            for(var j=0; j < width; j++) {
                Console.Write(Field[j,i]);
                if(j != width - 1) {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}

public class MainClass {
    public static List<int> GetInput() {
        var input = new List<string>(Console.ReadLine().Trim().Split(' '));
        return input.ConvertAll(i => int.Parse(i));
    }
    
    public static void Main(){
        // input H W N h w r c
        var input = GetInput();
        var height = input[0];
        var width = input[1];
        var n = input[2];

        var newTown = new NewTown(width, height);
        
        var buildingList = new List<Building>();
        for(var i=0; i < n ;i++) {
            input = GetInput();
            buildingList.Add(new Building(i + 1, input[1], input[0], input[3], input[2]));
        }
        
        var buildCount = 0;
        
        // 周りを一つ離して置く
        foreach(var b in buildingList) {
            var isPut = false; //ループ抜け用bool
            for(var x=0; x < width; x++) {
                if(isPut) continue;
                
                for(var y=0; y < height; y++) {
                    if(isPut) continue;
                    
                    if(newTown.CanPut(b.ToBoarden2(), x, y)) {
                        newTown.Put(b, x + 1, y + 1);
                        buildCount++;
                        isPut = true;
                    }
                }
            }
        }
        
        //　一つも置いて無かったら、一つ置く
        if(buildCount == 0) {
            foreach(var b in buildingList) {
                if(newTown.CanPut(b, 0, 0)) {
                    newTown.Put(b, 0, 0);
                }
            }
        }
        
        newTown.PrintTown();
    }
}
