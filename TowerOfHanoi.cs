// ハノイの塔
// paiza e-learning
// 2020 - 1 - 24

using System;
using System.Collections.Generic;

public class Hello{
    
    static List<List<int>> piles = new List<List<int>>();
    
    static int counter = 0;
    
    public static void Initialize(int n, int location) {
        for(var i = 0; i < 3; i++) {
            piles.Add(new List<int>());
        }
        
        for(var i = n ; i > 0; i--) {
            piles[location].Add(i);
        }
    }
    
    static void Print() {
        Console.WriteLine("--- (" + counter + ") ----");
        for(var i = 0; i < 3; i++) {
            Console.Write(i + ": ");
            foreach(var v in piles[i]) {
                Console.Write(v + " ");
            }
            Console.WriteLine("");
        }
    }
    
    static void MoveTo(int from, int to) {
        var lastIndex = piles[from].Count - 1;
        var disk = piles[from][lastIndex];
        piles[from].RemoveAt(lastIndex); // Remove Last
        
        piles[to].Add(disk);
        counter++;
        Print();
    }
    
    static void RecMoveDisks(int num, int from, int to, int other) {
        if( num == 0) {
            return;
        }
        
        RecMoveDisks(num - 1, from, other, to);
        MoveTo(from, to);
        RecMoveDisks(num - 1, other, to, from);
    }
    
    public static void Main(){
        var n = 6;
        if(n < 1) {
            Console.WriteLine("nを1以上にしてください");
        }
        
        Initialize(n, 0);
        Print();
        
        RecMoveDisks(n, 0, 2, 1);
    }
}
