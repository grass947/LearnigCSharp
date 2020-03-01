// 2020-3-1
// paiza skill check rank B
//  N × N ピクセルに0 ~ 255の整数値が入っているグレースケール画像を
// K分の１（KはNの約数）で圧縮する
// N × Nを　K×K で区切り、範囲の平均値にし、新しい圧縮後の画像を作る

//#define INPUT_DEBUG
//#define DEBUG

using System;
using System.Collections.Generic;

public class ImageCompress {
    public static void Main() {
        var input = Console.ReadLine().Trim().Split(' ');
        var n = int.Parse(input[0]);
        var k = int.Parse(input[1]);
        
        var originalImage = new int[n, n];
        for(var i=0; i < n; i++) {
            var imageInput = Console.ReadLine().Trim().Split(' ');
            for(var j=0; j < n; j++) {
                originalImage[i, j] = int.Parse(imageInput[j]);
            }
        }
        
        #if INPUT_DEBUG
        for(var i=0; i < n; i++) {
            for(var j=0; j < n; j++) {
                Console.Write($"{originalImage[i, j]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        #endif
        
        var n2 = n / k;
        var compressedImage = new int[n2, n2];
        
        for(var i=0; i < n2; i++) {
            for(var j=0; j < n2; j++) {
                
                var debugCounter = 0;
                var sum = 0;
                for(var i2=0; i2 < k; i2++) {
                    for(var j2=0; j2 < k; j2++) {
                        sum += originalImage[i2 + (i * k), j2 + (j * k)];
                        debugCounter ++;
                    }
                }
                
                #if DEBUG
                Console.WriteLine($"{sum} / {k * k} = {sum / (k * k)} : {debugCounter}");
                #endif
                
                compressedImage[i, j] = (int)(sum / (k * k));
            }
        }
        
        for(var i=0; i < n2; i++) {
            for(var j=0; j < n2; j++) {
                Console.Write(compressedImage[i, j]);
                if(j != (n2 - 1)) {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        
    }
}
