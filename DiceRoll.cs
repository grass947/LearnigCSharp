// 2020-3-5
// paiza skill check rank B
// time up(3h)
/*
アルゴリズム
縦と横で配列を二つ作る
     2
  --------      
 |   2
1| 3 6 4 1
 |   5
 |   1

初期状態からこの図のようになる
サイコロが右に動いた場合、横の配列をひとつ左にずらす
サイコロの底面の値 X は必ず縦横どちらの配列でも array[1]に存在する
動かしたあとは縦横の配列の最後を 7 - Xにする
  
アイディアとしてはスロットなどのリールから
*/

using System;
using System.Text;
using System.Collections.Generic;

public class DicePainting {
    public static void Main() {
        var input = Console.ReadLine().Trim().Split(' ');
        var movingCount = int.Parse(input[0]);
        var height = int.Parse(input[1]);
        var width = int.Parse(input[2]);
        
        var input2 = Console.ReadLine().Trim().Split(' ');
        var sy = int.Parse(input2[0]) - 1;
        var sx = int.Parse(input2[1]) - 1;
        
        var movingList = Console.ReadLine().Trim();
        
        var field = new int[height, width];
        
        var dice = new Dice(sx, sy);
        field[sy, sx] = 6;
        
        foreach(var direction in movingList) {
            dice.Roll(direction);
            field[dice.Y, dice.X] = dice.GetValue();
        }
        
        for(var y = 0; y < height; y++) {
            for(var x = 0; x < width; x++) {
                Console.Write(field[y, x]);
                
                if(x != width - 1) {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}

public class Dice {
    public int X {get; set;}
    public int Y {get; set;}
    
    private int[] horizontalRoll = new int[]{ 3, 6, 4, 1};
    private int[] verticalRoll = new int[]{2, 6, 5, 1};

    public Dice(int x, int y) {
        this.X = x;
        this.Y = y;
    }
    
    public int GetValue() {
        return horizontalRoll[1];
    }
    
    private void MoveArray(bool isHorizontal, bool isMovingRightOrDown) {
        int[] roll1;
        int[] roll2;
        
        if (isHorizontal) {
            roll1 = horizontalRoll;
            roll2 = verticalRoll;
            
        } else {
            roll1 = verticalRoll;
            roll2 = horizontalRoll;
        }
        
        if(isMovingRightOrDown) {
            for(var i = 0; i < 3; i++) {
                roll1[i] = roll1[i + 1];
            }
            roll1[3] = 7 - roll1[1];
            
        } else {
            for(var i = 3; i > 0; i--) {
                roll1[i] = roll1[i - 1];
            }
            roll1[0] = 7 - roll1[2];
        }
        
        roll2[1] = roll1[1];
        roll2[3] = roll1[3];
    }
    
    public void Roll(char direction) {
        switch(direction) {
            case 'U':
                Y -= 1;
                MoveArray(false, false);
                break;
            case 'D':
                Y += 1;
                MoveArray(false, true);
                break;
            case 'L':
                X -= 1;
                MoveArray(true, false);
                break;
            case 'R':
                X += 1;
                MoveArray(true, true);
                break;
        }
    }
    
    private void DebugPrint() {
        Console.Write("H-Roll ");
        foreach(var h in horizontalRoll) {
            Console.Write($"{h} ");
        }
        Console.WriteLine();
        
        Console.Write("V-Roll ");
        foreach(var v in verticalRoll) {
            Console.Write($"{v} " );
        }
        Console.WriteLine();
    }

}
