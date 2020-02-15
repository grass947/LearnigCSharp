// テーブルのデータ入力を空白で揃えた見やすいフォーマットにして表示する
// 2020-2-15
// paiza rank B skill check


//#define DEBUG

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public class Print {
    
    public static int GetIndexOfMaxLength(int columnIndex, string[] title,  string[,] data) {
        var maxLength = title[columnIndex].Length;
        for(var i=0; i < data.GetLength(1); i++) {
            var d = data[columnIndex, i].Length;
            if(d > maxLength) {
                maxLength = d;
            }
        }
        return maxLength;
    }
    
    // sb is reference type.
    public static void MakeLine(StringBuilder sb, int[] strLengthArray, string[] str, int strLength) {
        sb.Append("|");
        for(var i=0; i < strLength; i++) {
            sb.AppendFormat($" {{0, -{strLengthArray[i]}}} |", str[i]);
        }
        Console.WriteLine(sb.ToString());
        sb.Length = 0;
    }
    
    public static void Main() {
        var numColumns = int.Parse(Console.ReadLine());
        var title = Console.ReadLine().Trim().Split(' ');
        
        var dataCount = int.Parse(Console.ReadLine());
        var data = new string[numColumns, dataCount];
        
        for(var i=0; i < dataCount; i++) {
            var input = Console.ReadLine().Trim().Split(' ');
            for(var j=0; j < numColumns; j++) {
                data[j, i] = input[j];
            }
        }
        
        var strLengthArray = new int[numColumns];
        for(var i=0; i < numColumns; i++) {
            strLengthArray[i] = GetIndexOfMaxLength(i, title, data);
        }
        
        #if DEBUG
        foreach(var a in strLengthArray) {
            Console.Write($"{a.ToString()} ");
        }
        Console.WriteLine();
        
        Console.WriteLine(strLengthArray.Sum());
        #endif
        
        // static method int[].Sum() is in System.Linq
        var sbLength = strLengthArray.Sum() + (data.GetLength(0) * 3) + 10; //(余分に)
        var sb = new StringBuilder(sbLength);
        
        // title
        sb.Append("|");
        for(var i=0; i < numColumns; i++) {
            // 空白埋めは PadRight() か PadLeft()
            sb.Append($" {title[i].PadRight(strLengthArray[i])} |");
        }
        Console.WriteLine(sb.ToString());
        sb.Length = 0;
        
        // dash
        sb.Append("|");
        for(var i=0; i < numColumns; i++) {
            for(var j=0; j < strLengthArray[i] + 2; j++) {
                sb.Append("-");
            }
            sb.Append("|");
        }
        Console.WriteLine(sb.ToString());
        sb.Length = 0;
        
        // list
        for(var i=0; i < dataCount; i++) {
            sb.Append("|");
            for(var j=0; j < numColumns; j++) {
                sb.Append($" {data[j, i].PadRight(strLengthArray[j])} |");
                // 以下はややこしいので削除
                //sb.AppendFormat($" {{0, -{strLengthArray[j]}}} |", data[j , i]);
            }
            Console.WriteLine(sb.ToString());
            sb.Length = 0;
        }
        
    }
}
