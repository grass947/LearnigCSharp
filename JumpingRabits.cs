//#define DEBUG

using System;
using System.Collections.Generic;

public class JumpingRabits{
    
    public static int Jump(int current, bool[] bushes) {
        bushes[current] = false;
        var newPosition = 0;
        var length = bushes.Length;
        for(var i=1; i <= length; i++) {
            newPosition = (current + i) % length;
            if(!bushes[newPosition]) {
                bushes[newPosition] = true;
                #if DEBUG
                Console.Write($"({current} to {newPosition}) ");
                #endif
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
        var bushes = new bool[numBushes];
        var rabits = new int[numRabits];
        
        for(var i=0; i < numRabits; i++) {
            rabits[i] = int.Parse(Console.ReadLine().Trim()) - 1;
            bushes[rabits[i]] = true;
        }
        
        #if DEBUG
        for(var i=0; i < numBushes; i++) {
            if(bushes[i]) {
                Console.WriteLine(i + 1);
            }
        }
        #endif
        
        for(var i=0; i < jumpTimes; i++) {
            #if DEBUG
            Console.Write("Rabit Jump! ");
            #endif
            
            for(var j=0; j < numRabits; j++) {
                rabits[j] = Jump(rabits[j], bushes);
            }
            
            #if DEBUG
            Console.WriteLine();
            #endif
        }
        
        for(var i=0; i < numRabits; i++) {
            Console.WriteLine(rabits[i] + 1);
        }
    }
}
