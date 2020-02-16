// paiza skill check rank B
// 2020-2-16
// Dictionaryと降順の考え方
// https://paiza.jp/career/challenges/361/retry

/*
  各商品について以下のルールでスコア T_i (1 ≦ i ≦ N) を計算し、スコアの高い順に表示
  M 種類の指標別に、N 個の商品を大きい順に並び替えます。並び替えた後、i 番目の商品の順位が k 番の時、 T_i に N - k が加算されます。
  同じ指標が複数存在する場合、同順位を付け、以降は、同順位の数だけ順位が繰り下がります。

  1 行目には、表示する商品の数を表す整数 N、商品の指標の数を表す整数 M がこの順に半角スペース区切りで与えられます。
  続く N 行目のうち i 行目 (1 ≦ i ≦ N) には、各 j (1 ≦ j ≦ M) について、i 番目の商品の j 番目の指標の値を表す S_{i, j} が、この順に半角スペース区切りで与えられます。
  入力は合計で N + 1 行となり、入力値最終行の末尾に改行が 1 つ入ります。
  
  各商品 (1 ≦ i ≦ N) についてスコア T_i を計算し、スコアの高い順に出力
  k 行目 (1 ≦ k ≦ N) には、スコアが k 番目となる商品の番号を出力してください。
  スコアが等しくなる商品が存在する場合は、番号が小さいほうを先に出力してください。
*/

//#define DEBUG

using System;
using System.Linq;
using System.Collections.Generic;

public class RatedItem {
    public static void Main() {
        var input = Console.ReadLine().Trim().Split(' ');
        var numItem = int.Parse(input[0]);
        var numIndex = int.Parse(input[1]);
        
        var itemList = new int[numItem][];
        for(var i=0; i < itemList.Length; i++) {
            itemList[i] = new int[numIndex];
        }
        
        for(var i=0; i < numItem; i++) {
            var input2 = Console.ReadLine().Trim().Split(' ');
            for(var j=0; j < numIndex; j++) {
                //入力符号反転で昇順ソートが順位になる
                itemList[i][j] = int.Parse(input2[j]) * -1;
            }
        }
        
        var result = new int[numItem];
        
        //　各指標ごとの昇順リスト
        var dic = new SortedDictionary<int, List<int>>();
        
        for(var i=0; i < numIndex; i++) {
            // dicに各数値を格納 (同順を考慮して List<int> に詰め込む)
            for(var j=0; j < numItem; j++) {
                var key = itemList[j][i];
                if(!dic.ContainsKey(key)) {
                    dic[key] = new List<int>();
                }
                dic[key].Add(j);
            }
            
            #if DEBUG
            // grep dic
            foreach(var d in dic) {
                Console.Write($"{d.Key} ->");
                foreach(var v in d.Value) {
                    Console.Write($"{v} ");
                }
                Console.WriteLine();
            }
            #endif
            
            // 各指標ごとのリスト作成
            var scoreList = new int[numItem]; // for debug
            var rank = 1;
            foreach(var d in dic) {
                var score = numItem - rank;
                foreach(var v in d.Value) {
                    scoreList[v] = score;
                }
                rank += d.Value.Count;
            }
            for(var j=0; j< numItem; j++) {
                result[j] += scoreList[j]; 
            }
            
            #if DEBUG
            for(var j=0; j < numItem; j++)  {
                Console.Write($"{ scoreList[j] } ");
            }
            Console.WriteLine();
            Console.WriteLine();
            #endif
            
            dic.Clear();
        }
        
        // 結果用に集計
        for(var i = 0; i < numItem; i++) {
            var key = result[i] * -1;
            if(!dic.ContainsKey(key)) {
                dic[key] = new List<int>();
            }
            dic[key].Add(i + 1);
        }
        
        // 結果表示
        foreach(var d in dic) {
            foreach(var v in d.Value) {
                Console.WriteLine(v);
            }
        }
    }
}
