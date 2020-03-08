using System;
using System.Linq;
using System.Collections.Generic;

public class Progression {
    public static void Main() {
        var input = Console.ReadLine().Trim().Split(' ');
        var p1 = double.Parse(input[0]);
        var p2 = double.Parse(input[1]);
        var p3 = double.Parse(input[2]);
        var resultIndex = int.Parse(input[3]) - 1;
        
        // p3 (max == 7) Math.Pow(7, 12) < 81716954175 < Math.Pow(7, 13)
        var list = new List<long>(30 * 3);
        
        long _p1 = 1L;
        long _p2 = 1L;
        long _p3 = 1L;
        long sum;
        for(var i = 0; i < 24; i++) {
            for(var j = 0; j < 24; j++) {
                for(var k = 0; k < 24; k++) {
                    try {
                    _p1 = (long)checked(Math.Pow(p1, k));
                    sum = checked(_p1 * _p2 * _p3);
                    
                    } catch (OverflowException e) {
                        continue;
                    }
                    list.Add(sum);
                    //Console.WriteLine($"{sum} = {_p3} * {_p2} * {_p1} : {i}-{j}-{k}");
                }
                try {
                _p2 = (long)checked(Math.Pow(p2, j));
                } catch (OverflowException e) {
                    continue;
                }
            }
            
            try {
                _p3 = (long)checked(Math.Pow(p3, i));
            } catch (OverflowException e) {
                continue;
            }
            
        }
        list.Sort();
        var lst = list.Distinct().ToList();
        
        // Debug
        for(var i=0;i < lst.Count; i++) {
            Console.WriteLine($"{i} {lst[i].ToString()}");
        }
        
        if(resultIndex < lst.Count)
        Console.WriteLine(lst[resultIndex]);
    }
}
