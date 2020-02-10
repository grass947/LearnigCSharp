// うさぎジャンプ
// 2020-2-10
// paiza skill check rank B
// https://paiza.jp/challenges/96/show

//#define DEBUG

using System;
using System.Collections.Generic;

public class JumpingRabits{
    
    public static int GetJumpedPosition(int current, bool[] bushes) {
        var newPosition = 0;
        var length = bushes.Length;
        for(var i=1; i <= length; i++) {
            newPosition = (current + length - i) % length;
            if(!bushes[newPosition]) {
                break;
            }
        }
        return newPosition;
    }
    
    public static void Main(){
        var initInput = Console.ReadLine().Trim().Split(' ');
        var numBushes = int.Parse(initInput[0]);
        var numRabits = int.Parse(initInput[1]);
        var jumpTimes = int.Parse(initInput[2]);
        
        var rabits = new int[numRabits];
        var bushes = new bool[numBushes];
        
        for(var i=0; i < numRabits; i++) {
            rabits[i] = int.Parse(Console.ReadLine().Trim()) - 1;
            bushes[rabits[i]] = true;
        }
        
        #if DEBUG
        for(var i=0; i < numRabits; i++) {
            Console.WriteLine(rabits[i] + 1);
        }
        Console.Write("Rabit Jump! ");
        #endif
        
        var count = 0;
        while(count < jumpTimes) {
            for(var i=0; i < numRabits; i++) {
                bushes[rabits[i]] = false;
                var p = GetJumpedPosition(rabits[i], bushes);
                #if DEBUG
                Console.Write($"({rabits[i]} to {p}) ");
                
                #endif
                
                bushes[p] = true;
                rabits[i] = p;
                count ++;
                if(count >= jumpTimes) {
                    break;
                }
            }
        }
        #if DEBUG
        Console.WriteLine();
        #endif
        
        for(var i=0; i < numRabits; i++) {
            Console.WriteLine(rabits[i] + 1);
        }
    }
}
