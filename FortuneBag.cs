// 2020-2-29
// paiza skill check rank B
// no clear...
// 閾値以上のアイテムが入っている福袋（一つのみ）のパターンを出力せよ
// ただし、いずれかのアイテムを一つ取り除いた場合、閾値以下となること

#define DEBUG

using System;
using System.Collections.Generic;

public class FortuneBag {
    public static void Main() {
        var threshold = int.Parse(Console.ReadLine().Trim());
        var n = int.Parse(Console.ReadLine().Trim());
        var result = 0;
        
        var items = new List<int>();
        for(var i=0; i < n; i++) {
            var input = int.Parse(Console.ReadLine().Trim());
            if(input >= threshold){
                result ++;
            } else {
                items.Add(input);
            }
        }
        
        items.Sort();
        
        #if DEBUG
        Console.WriteLine(result);
        
        foreach(var item in items) {
            Console.WriteLine(item);
        }
        #endif
        
        if(items.Count > 0) {
            for(var i=items.Count - 1; i > 0; i--) {
                var value = items[i];
                items.RemoveAt(i);
                
                for(var j = items.Count - 1; j >=0; j--) {
                    var item = items[j];
                    if(value + item >= threshold) {
                        #if DEBUG
                        Console.WriteLine($"{value} + {item} >= {threshold} : {result}");
                        #endif
                        result ++;
                    } else {
                        value += item;
                    }
                }
            }
        }
        Console.WriteLine(result);
    }
}
