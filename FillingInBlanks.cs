// 穴埋め問題
// 2020 - 1 - 26
// paiza skill check problem rank B
//
// 縦・横・斜めの一列合計が全て同じ正方形マスの入力が与えられる。
// ただし、そのうち２つは空欄(0)になっている。
// その二つの空欄を補完したマスを出力する。

using System;

public struct Point {
    public int X {get; set;}
    public int Y {get; set;}
    
    public Point(int x, int y) {
        this.X = x;
        this.Y = y;
    }
}

public class Hello{
    
    static int CompareAndSum(int[,] arrays, int x, int y) {
        var sum1 = 0;
        var sum2 = 0;
        
        for(var i=0;i<arrays.GetLength(0);i++){
            sum1 += arrays[x, i];
            sum2 += arrays[i, y];
        }
        
        return Math.Max(sum1, sum2);
    }
    
    public static void Main(){
        var n = int.Parse(Console.ReadLine());
        
        var arrays = new int[n, n];
        
        var first = new Point(-1, -1);
        var second = new Point(-1, -1);
        
        var input = new string[n];
        for (var i = 0; i < n; i++) {
            input = Console.ReadLine().Trim().Split(' ');
            for(var j = 0; j < n; j++) {
                arrays[i,j] = int.Parse(input[j]);
                if(arrays[i,j] == 0) {
                    if(first.X == -1) {
                        first.X = i;
                        first.Y = j;
                    } else {
                        second.X = i;
                        second.Y = j;
                    }
                }
            }
        }
        
        /*
        Console.WriteLine("first (" + first.X + ", " + first.Y + ")");
        Console.WriteLine("second (" + second.X + ", " + second.Y + ")");
        */
        
        //3行の合計を取得して最大値を一行の合計とする
        var n1 = 0;
        var n2 = 0;
        var n3 = 0;
        for(var i=0;i < n; i++) {
            n1 += arrays[0,i];
            n2 += arrays[1,i];
            n3 += arrays[2,i];
        }
        
        var sum = Math.Max(n1, Math.Max(n2, n3));
        
        /*
        Console.WriteLine("sum " + sum);
        */
        
        //first と second の縦横を計算して大きい方をsumから引く
        arrays[first.X, first.Y] = sum - CompareAndSum(arrays, first.X, first.Y);
        arrays[second.X, second.Y] = sum - CompareAndSum(arrays, second.X, second.Y);
        
        //output
        for(var i=0;i<n;i++) {
            for(var j=0;j<n;j++) {
                Console.Write(arrays[i, j]);
                if(j != n - 1) {
                    Console.Write(" ");
                } 
            }
            Console.WriteLine("");
        }
        
    }
}
