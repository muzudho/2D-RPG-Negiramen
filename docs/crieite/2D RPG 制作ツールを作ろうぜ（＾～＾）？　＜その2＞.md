# 前の記事

📖 前回の記事： [2D RPG 制作ツールを作ろうぜ（＾～＾）？ ＜その1＞](https://crieit.net/posts/2D-RPG)  

# 📅 （2023-07-17 mon 海の日）ボタンのマウスホバーはだいたいでけた

![202307__maui__17-0228--localization.gif](https://crieit.now.sh/upload_images/efd4a650ef0521a3bacaddd5fd70387464b42a30339d0.gif)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　MAUI は　タップ仕様なんで　マウスに対応してない。これでも　ちょっとは　マシにした方」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　今日は　ここまでだな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　せっかく　２つ目の記事に来たのに……」  

（Zzz　Zzz　Zzz　Zzz）

## ズームしろだぜ

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　起きた」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　枠線が太くて　タイルがよく見えない。タイルをでかく表示してくれだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ズームか。  
係数を　ちゃんと正しく書けば　いけるはず……」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👇　行列演算するんじゃないか？」  

📖 [SkiaSharp + Zoom](https://social.msdn.microsoft.com/Forums/en-US/b2e908f0-f5ae-4478-9740-e985cbc4dee3/skiasharp-zoom?forum=xamarinlibraries)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　キャンバスがやってくれるのか、画像がやってくれるのか分からん」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　キャンバスの　横幅と　縦幅を変えるだけで　ズームしてくれるんじゃないの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あっ！　そういうことか！」  

（カタ　カタ　カタ　カタ）  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　グリッドは　画像として実装しているので、  ズームされると　ばかでかい画像になるんだが」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　グリッドは　画像に対して　かけるのではなく、  
画面に　かければいいんじゃないの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そうか！」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　画面のスクロールと連携とれるかな？」  

### グリッドの実装を考えようぜ？

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　グリッドは　元画像のサイズとは関係なく　画像サイズも指定することにしたい」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　グリッドの線には太さがあって　元の画像から　1px　はみ出るけど、  
1024 x 1024 pixels の画像にかけるグリッドが　1026 x 1026 pixels なの、気持ち悪くない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ゲームの実行時に合わせて最適化すると、開発環境は　割り食うが、  
ゲームの実行環境より、開発環境の方が　大幅にマシン性能が良いということを期待して、  
開発環境は　割り食うことにしようぜ？」  

![202307__maui__17-1729--GridPhase-o2o0.png](https://crieit.now.sh/upload_images/2e947fadaab544f62e2f39a7455cf67064b4fc0633d55.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　まず、 `グリッド全体`　と呼んでいたものを、 `グリッド位相`　に改名する」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　上側のメニュー、 `画像` の１行じゃなくて、  
`元画像` と `ビュー` の２段にしないと　のちのち　混乱するんじゃない？」  

![202307__maui__17-1754--WorkingImageSize-o2o0.png](https://crieit.now.sh/upload_images/b8a46ea927e6ca3052dc476a3521176864b501ff45668.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　利用者としては　不要な情報だが、  
これがないと　開発中の　わたしが混乱するから　付けておこう」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　Width と Height を変えて画像がデカくなるの　元画像を引き延ばすだけなら　そうかもしれないが、  
いろいろ　加工したいので　ズームは自力実装することにするぜ」  

![202307__maui__17-1838--Zoom.png](https://crieit.now.sh/upload_images/966874468b9c81e2d14d792a25f0476f64b50c6508aa2.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　数字を　ちょこちょこ変更できる入力欄に　すぐ反応するように　ズーム機能を実装するの　大変だな。  
要らん処理しないようにすることが」  

# 📅 （2023-07-18 tue） 健康診断のため休み

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　寝不足で　電車移動と　炎天下の徒歩で　きつい」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　せっかくの年休なのに　お父んが　ヘロヘロだ。  
寝てるぐらいなら　風呂掃除でもしてくれだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ユニットバスの床だけ　洗い流した。  
全部きっちりやろうとすると　つらいので　これで終わり」  

## グリッドの画像サイズ

![202307__maui__18-1404--Grid.png](https://crieit.now.sh/upload_images/87e0efed897c66ca6ef088c39bfec3a464b61da98f6d5.png)  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　👆　グリッドがなんか切れてない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　グリッドも画像だからな。ズームをしたときに  
グリッドの画像サイズも変えてないので　切れてるんだぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　分かってるんだったら　直せだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　画面上に　グリッドの画像サイズを出そうかな。わたしがデバッグするために」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　グリッドは　なんかトリック・コード書いていて　やりづらいな……」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　トリック無しで　グリッドを再描画できないの？  
`Invalidate()` メソッドとか使えば　再描画できないの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　なんらかのプロパティが変わってないと　再描画しないようだぜ」  

![202307__maui__18-1457--GridError.png](https://crieit.now.sh/upload_images/07ec27f8a0471b56e05c8c1517c54af564b629e70377e.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ズームに実数を入れると、ずれてしまうな。  
計算式の端数の丸め方が　揃っていないのだろう」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　for文で　横幅ずつ　横に移動しているところが　int 型なの、これが怪しいな　double 型へ変更しよう」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　SkiaSharp のメソッドが float 型を受け取るようだから、 double 型ではなく、 float 型にしよ」  

![202307__maui__18-1853--TileCursorError.png](https://crieit.now.sh/upload_images/06057acdcd1b8dd52af86be05d02bf4864b661680e152.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　グリッドは　ズームに対応したぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　カーソルが　ずれてるぜ。直せだぜ」  

![202307__maui__18-2356--FloatFormat.png](https://crieit.now.sh/upload_images/cd425b45e5b73cae01452c77d5e40b1964b6a83d71f2c.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　テキストボックスの　小数点の桁数を何桁表示するかのフォーマットって　設定できないかだぜ？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👇　そんなものは無いかもしれないな」  

📖 [Microsoft　＞　入力](https://learn.microsoft.com/ja-jp/dotnet/maui/user-interface/controls/entry)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　じゃあ　表示用に整形したプロパティを別途用意するかだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　書式指定子　わかんね」  

📖 [Microsoft　＞　Standard numeric format strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings?redirectedfrom=MSDN)  

![202307__maui__19-0024--FloatFormat.png](https://crieit.now.sh/upload_images/b789869df82cb32e6493d3e4baf1676564b6aebaab947.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　"F1" にすれば　小数点以下１桁　になるみたいだぜ」  

![202307__maui__19-0033--WIP-Zoom-o2o0.png](https://crieit.now.sh/upload_images/52d71dc2809ad70e6b2c3bc26ea673c664b6b11c38546.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　カーソルや、カラー・マップを　ズームに対応させるの　まだまだ　かかりそう。  
今日はもう寝る」  

# 📅 （2023-07-19 wed） 野球のオールスター観てた

休み  

# 📅 （2023-07-20 thu） 野球のオールスター観てた

休み  

# 📅 （2023-07-21 fri） カラーマップもズームに対応しろだぜ

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　作業用ＢＧＭなんか　ないかな？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　👇　植松伸夫の　バトル１　を永遠に聞き続けなさい」  

📖　[バトル1](https://www.youtube.com/watch?v=VQ0nLB4WIas)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ＲＰＧだ！」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　原曲じゃないか……　アレンジがいいのに……」  

![202307_maui_21-2230--recordList-o2o0.png](https://crieit.now.sh/upload_images/34c68df5f651b70d538fe8efd1e41d0c64ba88ee28a08.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　この `List<TileRecord> RecordList` というのが　タイル１個分のデータだが、  
ズーム後の表示サイズのようなものは　記録しないぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そこで　例えば　`List<TileViewModelRecord> RecordViewModelList`　のように  
ズーム後の表示サイズも記録するように　拡張するのが　基本的な考え方だぜ」  

![202307_maui_21-2237--tileRecord-o2o0.png](https://crieit.now.sh/upload_images/6e2f151a2e3a7502c69ca13d3b9857d464ba8a56e4cdd.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`TileRecord`　ってこういうモデルだが、  これとは別に　`TileRecordViewModel`　というビューモデルを作ることにしよう」  

![202307_maui_21-2259--tileRecordViewModel-o2o0.png](https://crieit.now.sh/upload_images/1fc5be594ce5afc9d3fa9e5e6195731c64ba909ca4b76.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　主な違いは、位置とサイズを、元データとズーム後の２つに分けて持つことだぜ」  

![202307_maui_21-2351--tileSettingViewModel-o2o0.png](https://crieit.now.sh/upload_images/f8f264f5aba1e3749426f59f20f0ac5764ba9c143efec.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　タイル設定ファイルも　ビューモデル　バージョンを作っておこうぜ」  

![202307_maui_22-0148--zoomAsFloat-o2o0.png](https://crieit.now.sh/upload_images/3b491eff4e3325bb08480f489c0b091964bab7338717e.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　そして　ズーム欄の数値を変更したタイミングで、  
カラー・マップの　色矩形の位置とサイズを　全部更新すればいいわけだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ズームの数値を変えたときに　起こることが  
タイルセット画像の伸縮と、グリッドの伸縮と、  
切抜きカーソルや、画面上のオブジェクトの位置とサイズの変更と、  
やることが多いのよねえ」  

![202307_maui_22-0211--zoomedColorMap.png](https://crieit.now.sh/upload_images/4ff6bd3001e78df0f9c4f1059470e53a64babc4235457.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　カラーマップを拡大したぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　お父ん、タイルセット画像を暗くしていたのが　効いてないぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あれま　ほんとだぜ。なんでだろな　調べるか」  

![202307_maui_22-0226--zoomedColorMap.png](https://crieit.now.sh/upload_images/17106d5b465437dccef297cadea249f064babfff7f857.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ズームした時も　明度を下げるようにしたぜ」  

## 入力もズームの縮尺に合わせろだぜ

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　お父ん、マウスカーソルが指している位置と、切抜きカーソルが出てくる位置がずれているぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　入力は　ズームかかってないからな。そこも実装しないといけないか～」  

＜書きかけ＞