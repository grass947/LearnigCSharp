// 魔法の呪文(RANK S)
// paiza online packathon
// https://paiza.jp/logic_summoner/challenges/logics_skill_4005
// 2020 - 1 - 28

//#define DEBUG
//#define TEST

using System;
using System.Collections.Generic;

public struct Point{
    public int X {get; set;}
    public int Y {get; set;}
    
    public Point(int x, int y) {
        this.X = x;
        this.Y = y;
    }
    
    public string Show() {
        var str = this.X + " " + this.Y;
        return str;
    }
    
    public override bool Equals(object obj) {
        if ((object)obj == null || !(obj is Point)) {
            return false;
        } else {
            return (X == ((Point)obj).X && Y == ((Point)obj).Y);
        }
    }
    
    public override int GetHashCode() {
        return X * Y;
    }
    
    public static bool operator == (Point p1, Point p2) {
        return (p1.X == p2.X && p1.Y == p2.Y);
    }
    
    public static bool operator != (Point p1, Point p2) {
        return !(p1 == p2);
    }
    
    public static Point operator + (Point p1, Point p2) {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }
}

public class MagicSpell{
    public static readonly Point EndPoint = new Point(-1, -1);
    public static readonly char WS = ' '; // White Spaceの略 (見にくいのでドットに変更)
    
    static Point[] directions = new Point[4] {
            new Point(0, -1), new Point(0, 1), new Point(-1, 0), new Point(1, 0)
        };
    
    static void PrintField(char[,] field) {
        for(var i=0;i < field.GetLength(1); i++) {
            for(var j=0; j < field.GetLength(0); j++) {
                Console.Write(field[j, i]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    
    static void PrintGroupList(List<List<Point>> groupList) {
        for(var i=0;i < groupList.Count;i++) {
            Console.Write(i + ": ");
            foreach(var point in groupList[i]) {
                Console.Write("(" + point.Show() + ") ");
            }
            Console.WriteLine();
        }
    }
    
    static void PrintPointList(List<Point> list) {
        foreach(var point in list) {
            Console.Write(point.Show());
        }
        Console.WriteLine();
    }
    
    // 参照型の二次元配列のフィールドをコピー
    static char[,] CopyField(char[,] field) {
        var newField = new char[field.GetLength(0), field.GetLength(1)];
        Array.Copy(field, newField, field.Length);
        return newField;
    }
    
    // 探索者がフィールドの中にあるかどうか
    static bool IsInField(Point searcher, char[,] field) {
        return (0 <= searcher.X && searcher.X < field.GetLength(0)
        && 0 <= searcher.Y && searcher.Y < field.GetLength(1));
    }
    
    // 探索者の初期位置を取得(取得できなければ EndPointを返す)
    static Point GetStart(char[,] field) {
        for(var i=0; i < field.GetLength(1); i++) {
            for(var j=0; j < field.GetLength(0); j++) {
                if(field[j, i] != WS) {
                    return new Point(j, i);
                }
            }
        }
        return EndPoint;
    }
    
    // 探索してグループごとにリストを作成(色の情報は持たない)
    static List<List<Point>> GroupSameChars(char[,] _field) {
        var result = new List<List<Point>>();
        
        var field = CopyField(_field);
        
        var color = ' ';
        var searcher = new Point(0,0);
        var next = new Point(0,0);
        
        while((searcher = GetStart(field)) != EndPoint) {
            // Listは参照型なので newで新しくメモリを確保
            var passList = new List<Point>();
            var searchList = new List<Point>();
            
            color = field[searcher.X, searcher.Y];
            // 探索者の初期値をsearch listに追加
            searchList.Add(new Point(searcher.X, searcher.Y));
            
            // searchListがなくなるまで
            while(searchList.Count > 0) {
                //４方向に動けるか調べる
                for(var i=0;i<4;i++) {
                    // 現在の座標に、それぞれ上下方向に動いた時の座標を追加
                    next = searcher + directions[i];
                    if(!IsInField(next, field)) {
                        continue;
                    }
                    if(field[next.X, next.Y] == color) {
                        // nextが同じ色であればsearchListに追加
                        searchList.Add(new Point(next.X, next.Y));
                    }
                }
                
                //通った座標をpassListに追加
                passList.Add(new Point(searcher.X + 1, searcher.Y + 1));
                //探索したものはダッシュに置き換える
                field[searcher.X, searcher.Y] = WS;
                
                //次の値をseachListからpop
                searcher = searchList[searchList.Count - 1];                
                searchList.RemoveAt(searchList.Count - 1); // pop
            }
            
            //PrintPointList(passList);
            result.Add(passList);
        }
        
        return result;
    }
    
    // リストの個数（塊の大きさ）が最大の物のインデックスを配列化
    static int[] GetMaxCountIndexList(List<List<Point>> groupList) {
        var maxCountList = new List<int>(groupList.Count);
        var maxValue = 0;
        for(var i=0; i < groupList.Count; i++) {
            var count = groupList[i].Count;
            if (maxValue < count) {
                maxValue = count;
                maxCountList.Clear(); //リスト更新
                maxCountList.Add(i);
            } else if (maxValue == count) {
                maxCountList.Add(i);
            }
        }
        return maxCountList.ToArray();
    }
    
    //　リストの座標で空白に置き換える
    static char[,] DeleteBlockByList(char[,] _field, List<Point> list) {
        var field = CopyField(_field);
        foreach(var point in list) {
            field[point.X - 1, point.Y - 1] = WS;
        }
        return field;
    }
    
    // ブロックを落下させて空白を置き換える
    static char[,] ReplaceField(char[,] _field) {
        var width = _field.GetLength(0);
        var height = _field.GetLength(1);
        var field = CopyField(_field);
        
        for(var i=0; i < width; i++) {
            for(var j = height - 1; j > 0; j--) {
                // 空白かつ一つ上が範囲外でなければ
                if (field[i,j] == WS && j - 1 >= 0) {
                    // 上方向に探す(見つからなければ WSのまま)
                    for(var k = j - 1; k >= 0 ; k--) {
                        if(field[i, k] != WS) {
                            //見つかったらWSと値を交換
                            field[i, j] = field[i, k];
                            field[i,k] = WS;
                            break;
                        }
                    }
                }
            }
        }
        return field;
    }
    
    public static void Main(){
        //入力
        #if !TEST
        var input = Console.ReadLine().Trim().Split(' ');
        var width = int.Parse(input[0]);
        var height = int.Parse(input[1]);
        var magicLimit = int.Parse(input[2]);
        
        var field = new char[width, height];
        for(var i=0;i < height; i++) {
            var input2 = Console.ReadLine().Trim();
            for(var j=0;j < width; j++) {
                field[j, i] = input2[j];
            }
        }
        
        #else
        var random = new Random();
        var width = random.Next(10, 30);
        var height = random.Next(10, 30);
        var magicLimit = random.Next(3, 10);
        
        var testRGB = new[] { 'A','C','Z' };
        var field = new char[width, height];
        for(var i=0;i < height; i++) {
            for(var j=0;j < width; j++) {
                field[j, i] = testRGB[random.Next(0,3)];
            }
        }
        Console.WriteLine("W: " + width + " H: " + height + " N: " + magicLimit);
        
        #endif
        
        var magicList = new List<Point>(magicLimit);
        
        #if DEBUG
        PrintField(field);
        #endif
        
        for(var i=0;i < magicLimit; i++) {
            // 探索してグループごとにリストを作成(色の情報は持たない)
            var groupList = GroupSameChars(field);
            
            //PrintGroupList(groupList);
            
            // リストを作成できない場合はbreak
            if(groupList.Count == 0) {
                break;
            }
            
            // リストの個数（塊の大きさ）が最大の物のインデックスを配列化
            var maxCountList = GetMaxCountIndexList(groupList);
            
            // 塊の大きさが同じ物が複数あった場合、下から消していく
            // 落ちた後の連鎖を考慮して
            // (どっちがいいのだろうか...)
            // (本来はそれごとに分岐をするのが良いのかも知れない)
            
            // maxCountListの最後を取得し、それを元に消去する塊を選択
            var removeList = groupList[maxCountList[maxCountList.Length - 1]];
            
            // 先頭座標をListに格納
            magicList.Add(removeList[0]);
            
            #if DEBUG
            // 消した文字を表示
            var p = removeList[0];
            Console.WriteLine("----- " + field[p.X - 1, p.Y - 1] + " : " + removeList.Count + " -----");
            #endif
            
            // フィールドの塊一つを空白で置き換え
            field = DeleteBlockByList(field, removeList);
            
            #if DEBUG
            PrintField(field);
            #endif
            
            // ブロックを落下させ空白を置き換え
            field = ReplaceField(field);
            
            #if DEBUG
            //PrintField(field);
            #endif
        }
        
        //出力
        Console.WriteLine(magicList.Count); //回数
        foreach(var m in magicList) {
            Console.WriteLine(m.Show());
        }
    }
}
