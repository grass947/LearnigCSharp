// 2020-3-4
// paiza skill check rank B
// チェスボードのような一辺Nマスの正方形上に、座標(H W)で置かれた擬似クイーンの移動できるマスの個数を求める
// 擬似クイーンは斜めは直線状に移動でき、縦横は一マスだけ移動できる
// 入力は N H W K
// Kはクイーンを動かせる回数
// 予想通りだが３回で全てのマスを埋めることができる
// 白と黒のチェック状のボードを考えた時、初期位置を黒色とする
// ２回目の動きで黒は全て埋まる。同様に3回目の動きで白色も全て埋まる。

#define DEBUG

using System;
using System.Collections.Generic;

public class MockEightQueen {
    
    static bool[,] field;
    static int result = 0;
    static List<MockQueen> queens = new List<MockQueen>();
    
    static bool IsInField(MockQueen _mq) {
        return !(0 > _mq.X || _mq.X >= field.GetLength(1) || 0 > _mq.Y || _mq.Y >= field.GetLength(0));
    }
    
    static void CheckCell(MockQueen mq) {
        if(!field[mq.Y, mq.X]) {
            field[mq.Y, mq.X] = true;
            result ++;
            var next = new MockQueen(mq.X, mq.Y);
            queens.Add(next);
        }
    }
    
    static void SearchDiagonallyOneWay(MockQueen _mq, Action<MockQueen> move) {
        var q = _mq.Copy();
        while(IsInField(q)) {
            // 始めに評価しないと Index out of range exception
            CheckCell(q);
            move(q);
        }
    }
    
    // 斜めに探索
    static void SearchDiagonally(MockQueen _mq) {
        // delegateの練習
        SearchDiagonallyOneWay(_mq, delegate(MockQueen mq) {
           mq.Move(-1,-1); 
        });
        
        SearchDiagonallyOneWay(_mq, (MockQueen mq) => {
            mq.Move(-1, 1);
        });
        
        SearchDiagonallyOneWay(_mq, (MockQueen mq) => mq.Move( 1,-1));
        
        SearchDiagonallyOneWay(_mq, mq => mq.Move(1,1));
    }
    
    //上下に探索
    static void  SearchAround(MockQueen _mq) {
        MockQueen q;
        for(var i=0; i < 4; i++) {
            q = _mq.Copy();
            switch(i) {
                case 0:
                    q.Move(-1, 0);
                    break;
                case 1:
                    q.Move(1, 0);
                    break;
                case 2:
                    q.Move(0, -1);
                    break;
                case 3:
                    q.Move(0, 1);
                    break;
            }
            if(IsInField(q)) {
                CheckCell(q);
            }
        }
    }
    
    static void PrintField() {
        for(var i=0; i < field.GetLength(0); i++) {
            for(var j=0; j < field.GetLength(1); j++) {
                var s = field[i, j] ? "●" : "○";
                Console.Write(s);
                
                if(j != field.GetLength(1) - 1) {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    
    public static void Main() {
        var input = Console.ReadLine().Trim().Split(' ');
        var length = int.Parse(input[0]);
        var y = int.Parse(input[1]) - 1;
        var x = int.Parse(input[2]) - 1;
        var movementCount = int.Parse(input[3]);
        
        field = new bool[length, length];

        var first = new MockQueen(x, y);
        queens.Add(first);
        
        field[first.Y, first.X] = true;
        result = 1;
        
        List<MockQueen> _queens;
        for(var i = 0; i < movementCount; i++) {
            // List をコピー生成
            _queens = new List<MockQueen>(queens);
            queens.Clear();
            
            foreach(var q in _queens) {
                SearchDiagonally(q);
                SearchAround(q);
            }
            
            #if DEBUG
            PrintField();
            #endif
        }
        
        Console.WriteLine(result);
    }
}

public class MockQueen {
    public int X {get; set;}
    public int Y {get; set;}
    
    public MockQueen(int x, int y) {
        this.X = x;
        this.Y = y;
    }
    
    public void Move(int mx, int my) {
        this.X += mx;
        this.Y += my;
    }
    
    public MockQueen Copy() {
        return new MockQueen(this.X, this.Y);
    }
}
