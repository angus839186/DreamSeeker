//註解
/*多行註解*/
//TODO: 註解
//#Tag 標籤
//-> DONE 流程故意在此處結束

//*選項
//+保留選項
//**選項中子選項

//{}條件選項 {not a}{a}有無經過此事件
//{}條件選項 進階條件用法

//{a and/&& b}同時兩個條件通過
//{a or/|| b}其中一個條件通過
//{ Knots > 3}執行過3次之後才條件成立
//{條件: 成立的對話 | 不成立的對話}。
//{TURNS()}目前經過的回合，選過幾個選項
//{TURNS_SINCE(-> knots名字)}指定特定的 Knots 開始，選過幾個選項
//{CHOICE_COUNT}確認選項的數量

//{a...|b|c|d|...}間隔出不同的對話，每次造訪都讀取下一個欄位的文字
//{a...&|b|c|d|...}&當到最後一格時，會再循環回第一個項目
//{a...!|b|c|d|...}只循環一次，顯示到最後一個內容後，再次執行就會剩下空白
//{a...~|b|c|d|...}隨機從中間取一個內容拿出來

//[]選擇後隱藏
//===節點=== 段落
//->轉移銜接 轉到段落
//<>膠水 前後文連接
//= 縫線 呼叫宣告
//INCLUDE 引用其他ink檔案

//VAR 變數
//{變數}
//~變數賦值(變數公式化)

//if
/*{
    x>0:
    ~y = x
    x<0:
    ~y = x 
    else:
    ~y = x}
*/

//switch
/*
{ x:
- 0: 	zero
- 1: 	one
- 2: 	two
- else: lots
}
*/

//LIST 狀態清單 列表 變數
//LIST statesOfGrace = ambiguous, saintly, fallen
//{LIST_COUNT(statesOfGrac)}

//主角:向卉
//使者

~ SEED_RANDOM(255)
->story00

===story00===
A
"不好意思 請問你知道這裡是哪裡嗎?"
B
嗯？這裡是人死後到達的地方喔 
A
你一臉驚恐 
什麼我已經死掉了嗎 我連我發生什麼事都不知道⋯
B
使者看到你被嚇到露出調皮的微笑
B
抱歉抱歉跟你開玩笑的 來到這裡不是死掉 但算是瀕死
A
你露出鬆了一口氣的表情 又好像想到什麼臉色又逐漸驚恐
"瀕死？！那只比死掉好一點點而已耶！！"
B
使者看了你的反應用溫柔且平和的語氣安撫道
"只要你知道自己是誰，帶著極度想活下去的意志，就可以回到原本的地方了喔~"
A
你用手捂著臉努力用腦想著跟自己相關的事但腦袋始終一片空白
"怎麼會這樣 我對自己的任何事完全沒半點印象 "
B
使者揮了下手臂身旁出現了一扇門
"沒事的，這代表你的課題需要透過這種方式來完成，我可以幫助你"
A
你像找到了希望般看著使者
"真的嗎?！！ "
B
使者面帶著微笑摸了你的頭 
"當然畢竟這也是我的工作，那關於你自身記憶相關的事"
使者指向了剛剛叫出來的門
"你可以試著進去裡面找看看喔～"
    ->END




















