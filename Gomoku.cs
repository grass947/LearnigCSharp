// 五目並べ
// 2020 - 1 -22
// https://paiza.jp/works/mondai/prob60/tic_tac_toe_9?language_uid=c-sharp
/*
5行5列の五目並べの盤面が与えられます。
盤面の各マスには、"O"か"X"か"."が書かれています。
"O"と"X"は、それぞれプレイヤーの記号を表します。
同じ記号が縦か横か斜めに連続で5つ並んでいれば、その記号のプレイヤーが勝者となります。

勝者の記号を1行で表示してください。
勝者がいない場合は、引き分けとして、"D"を表示してください。
*/

using System;
using System.Collections.Generic;

public class Hello{
    const int N = 5;
    
    public static void Main(){
        
        var array = new char[N][]; // jag array
        for(var i=0; i< N; i++) {
            array[i] = Console.ReadLine().ToCharArray();
        }
        
        var result = "";
        
        for (var p = 0; p < 2; p++){
        for(var i = 0; i < N; i++) {
            var o = 0;
            var x = 0;
            for(var j = 0; j < N; j++) {
                var c = (p == 0) ? array[j][i] : array[i][j];
                if (c == 'O') {
                    o++;
                 } else if (c == 'X') {
                    x++;
                }
            }
            result = Judge(o, x);
            if(IsGetResult(result)) { break; }
        }
        if(IsGetResult(result)) { break; }
        }
        
        
        if (!IsGetResult(result)){
            var toRightDouwnCounter = 0;
            var o1 = 0;
            var x1 = 0;
            
            var toLeftDownCounter = N - 1;
            var o2 = 0;
            var x2 = 0;
            
            var c = ' ';
            
            for(var i=0; i<N; i++) {
                // 左上から右下へ探索
                c = array[i][toRightDouwnCounter];
                if (c == 'O') {
                    o1++;
                } else if (c == 'X') {
                    x1++;
                }
                toRightDouwnCounter++;
                
                // 右上から左下へ探索
                c = array[i][toLeftDownCounter];
                if(c == 'O') {
                    o2++;
                } else if (c == 'X') {
                    x2++;
                }
                toLeftDownCounter--;
            }
            result = Judge(o1, x1);
            if (!IsGetResult(result)) {
                result = Judge(o2, x2);
            }
        }
        
        Console.WriteLine(result);
    }
    
    static string Judge(int numO, int numX) {
        var result = "";
        if(numO == N) {
            result = "O";
        } else if (numX == N) {
            result = "X";
        } else {
            result = "D";
        }
        return result;
    }
    
    static bool IsGetResult(string result) {
        return (result == "O" || result == "X");
    }
}
