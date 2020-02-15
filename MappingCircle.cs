// 2020-2-15
// paiza skill check rank B

//一マスの一辺が長さ１である方眼紙を考えます。
//適当な交点を中心とした半径rの円を描き、内部を黒で塗りつぶします。
//一部でも黒が塗られているマスの数を出力しなさい。
//
// 入力は r が与えられます。
// (1 ≦ r ≦ 1000) (r は0.00001刻み)

//#define DEBUG

using System;
using System.Collections.Generic;

public class CircleMap {
    // 円の４分の１を1*1の四角で賽の目上にして、一つ一つの最も遠い点から中心までの距離を測る
    // それが半径以内であれば、カウントアップする
    // 結果は４倍したものになる
    public static void Main() {
        var r = double.Parse(Console.ReadLine());
        var mapLength = (int) r + 1;
        
        #if DEBUG
        Console.WriteLine(r);
        Console.WriteLine(mapLength);
        #endif
        
        var result = 0;
        
        for(var i=0; i < mapLength; i++) {
            for(var j=0; j < mapLength; j++) {
                var c = Math.Sqrt(i * i + j * j);
                if(c < r) {
                    result ++;
                }
                
                #if DEBUG
                Console.Write($"{c} ");
                #endif
            }
            #if DEBUG
            Console.WriteLine();
            #endif
        }
        
        result *= 4;
        Console.WriteLine(result);
    }
}
