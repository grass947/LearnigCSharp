// 2020-2-29
// paiza skill check rank B
// 126min score 0
// 指定回数(repeatCount)、ルールで模様を拡大配置
// ルール
// '#'ならそのままパターンを配置
// '.'なら'.'を配置
//
// つまづいたポイント
// resultLengthを累乗として計算していた
// Array.Copyにつまづく
// 拡大後の配列の位置計算が間違っていた　→　ひとつずつ行うことでプログラミングしやすくなる


//#define DEBUG

using System;
using System.Collections.Generic;

public class GraphicPattern {
    public static int GetMaxLength(int length, int count) {
        var result = length;
        for(var i=0; i < count; i++) {
            result *= result;
        }
        return result;
    }
    
    public static void Main() {
        var repeatCount = int.Parse(Console.ReadLine().Trim());
        var length = int.Parse(Console.ReadLine().Trim());
        
        var resultLength = GetMaxLength(length, repeatCount);
        var resultPattern = new char[resultLength][];
        for(var i=0; i < resultLength; i++) {
            resultPattern[i] = new char[resultLength];
        }
        
        var tempPattern = new char[resultLength][];
        for(var i=0; i < resultLength; i++) {
            tempPattern[i] = new char[resultLength];
        }
        
        for(var i=0; i < length; i++) {
            var input = Console.ReadLine().Trim();
            for(var j=0; j < length; j++) {
                resultPattern[i][j] = input[j];
            }
        }
        
        #if DEBUG
        Console.WriteLine($"resultLength: {resultLength}");
        for(var i=0; i< length; i++) {
            for(var j=0; j < length; j++) {
                Console.Write(resultPattern[i][j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine("Debug End");
        Console.WriteLine();
        #endif
        
        for(var i=0; i < repeatCount; i++) {
            // Array Copy
            for(var j=0; j < resultLength; j++) {
                Array.Copy(resultPattern[j], tempPattern[j], resultLength);
            }
            
            for(var j=0; j < length; j++) {
                for(var k=0; k < length; k++) {
                    var c = tempPattern[j][k];
                    for(var j2=0; j2 < length; j2++) {
                        for(var k2=0; k2 < length; k2++) {
                            if(c == '#'){
                                resultPattern[j2 + (j * length)][k2 + (k * length)] = tempPattern[j2][k2];
                            } else {
                                resultPattern[j2 + (j * length)][k2 + (k * length)] = '.';
                            }
                        }
                    }
                }
            }
            length *= length;
            
            #if DEBUG
            for(var p=0; p < length; p++) {
                for(var j=0; j < length; j++) {
                    Console.Write(resultPattern[p][j]);
                }
                Console.WriteLine();
            }
            #endif
        }
        
        for(var i=0; i < resultLength; i++) {
            for(var j=0; j < resultLength; j++) {
                Console.Write(resultPattern[i][j]);
            }
            Console.WriteLine();
        }
    }
}
