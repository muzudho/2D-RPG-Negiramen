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

![202307_maui_22-0316--zoomedCursor.png](https://crieit.now.sh/upload_images/edd41c943c871459fcad62d652f3a91864bacbbb5067e.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　入力も　ズームを考慮して　行えるようにしたぜ」  

![202307_maui_22-0319--zoomedCursorError-o2o0.png](https://crieit.now.sh/upload_images/d54b4cd78beefcc9932d3af2e6ffacde64bacc5ccdda7.png)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👆　ズームを変えても、切抜きカーソルが　置いてけぼりをくらってるぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ズームの数字が変わったら、切抜きカーソルの矩形の位置とサイズも　再設定しないといけないな」  

![202307_maui_22-0422--zoomedCursor.png](https://crieit.now.sh/upload_images/feaa3e7eaae0d99307ec42bc3f549ebc64bb5eec6c105.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　カーソルも　ズームに置いてけぼりされないようにしたぜ」  

## インターセクションが無いようにしましょう

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　矩形が　重なりまくって　分けわかんないんだけど」  

![202307_maui_22-1435--noIntersection.png](https://crieit.now.sh/upload_images/a159f5fd02bfda991b205c20d8ad0d1864bb6adaaae52.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　つまり　こういう状態になることを　強制する方法か」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　既存の登録タイルと重なっているときは　タイルを追加できないようにすればいいのでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　**既存のタイルと重なっている** と、ユーザーにどうやって　気づかせるんだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　とりあえず　追加ボタンが押せなければいいんじゃないの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　じゃあ　切抜きカーソルの位置を決めるために　マウスを動かしている間、  
インターセクション（Intersection；重なり合った領域）があるかどうか　判定すればいいんだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　交差の判定方法を調べて」  

📖 [矩形同士の交差](https://blog.y-yuki.net/entry/2019/08/29/200000)  

![202307_maui_22-1658--rectangleIntersection-o2o0.png](https://crieit.now.sh/upload_images/a3152154833608af519c5ef818ca8c3564bb8c9fa649f.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　判定方法を実装」  

![202307_maui_22-1703--hasIntersection-o2o0.png](https://crieit.now.sh/upload_images/59217521a2a8ae91b7647b4981d515c664bb8dace9a34.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　登録されているすべてのタイルと　比較すればいいわけだが、  
リストではなく、 `IEnumerator` 型を使うのが　ひと工夫だぜ」  

![202307_maui_22-1707--viewModelIntersection-o2o0.png](https://crieit.now.sh/upload_images/5e14b7ed4bb687f1dc0f56241962695b64bb8e7da0f56.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　このように　ストリームで渡せるようにしておくと、  
ビューモデルも　同じアルゴリズムを使い回せるな」  

![202307_maui_22-1711--intersecting-o2o0.png](https://crieit.now.sh/upload_images/95710ce502d90b65ef5a301d307fdd0464bb8f8bcf445.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これで交差中か　判定できるようになったぜ」  

![202307_maui_22-1734--congruence-o2o0.png](https://crieit.now.sh/upload_images/68623cb89e2a03827a63039a96badc2664bb953320915.png)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👆　お父ん、コングルエンス（congruence ；合同）と　インターセクション（Intersection；交差）は　区別してくれだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　考え出すと　お互いに部分的な交差、完全に覆っている交差、完全に覆われている交差、いろいろあるな。  
インターセクションから　コングルエンスを除外するか、それとも　インターセクションでありコングルエンスでもあることがあるか？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　インターセクションを見つけることと、コングルエンスを見つけることは　時間が異なるんじゃない？  
同じアルゴリズムで一緒にやろうとしたら　実行時間が最悪のケースにならない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　じゃあメソッドを分けるか」  

![202307_maui_22-2040--congruence.png](https://crieit.now.sh/upload_images/4a20a5979804632d63ed340ba481635b64bbc0b60f5c4.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　合同のときは　上書きできるように直したが、  
今度は削除ボタンが効かなくなった。調べるぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　論理削除されているものを　保存から除外してたから　保存されなかったんだ。  
論理削除されているものも　保存するように直そう」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　それでも直らん」  

## ファイルの内容をチェックするプログラムを入れておくべきでは？

![202307_maui_22-2104--congruence-o2o0.png](https://crieit.now.sh/upload_images/8c5e29f5f051945b43cdf0adaf2850e264bbc6432831a.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　データの中に　合同の矩形が２つある！  
片方を論理削除しても、もう片方が残ってる！」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ファイルの内容をチェックするプログラムを入れておくべきでは？」  

![202307_maui_22-2132--validation-o2o0.png](https://crieit.now.sh/upload_images/f7a931f50ba82418dd5e90fde947376f64bbccafa154f.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　とりあえず　デバッグモードで確認するようにした」  

![202307_maui_22-2140--Id-o2o0.png](https://crieit.now.sh/upload_images/0c12b600870ddfa9af7cc0f9920d613c64bbcea793fcb.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　あれっ！　Ｉｄが重複しまくってる！」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　Ｉｄの採番部を見直せだぜ」  

![202307_maui_22-2300--fix-id.png](https://crieit.now.sh/upload_images/bb19a84999e705ac2b9b4893578254be64bbe14c3a5f9.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　Ｉｄの採番部を直したぜ」  

## Disable のボタンのスタイル直せないの？

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　不活性のボタンは　文字が黒い青いボタンになってるけど、  
グレーのボタンにできないの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　スタイル　いつの間にか　おかしくなった　調べるか」  

![202307_maui_22-2306--buttonStyle-o2o0.png](https://crieit.now.sh/upload_images/6efe601d2a7b39091da867aea930c60c64bbe27ff3705.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ここに指定している通りに　表示してくれたらいいのに……」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　その上で　`BackgroundColor`　を指定しているのが　ダメなんだろうかだぜ？」  

![202307_maui_22-2311--buttonStyleNormal-o2o0.png](https://crieit.now.sh/upload_images/0a6b079c3db7194b98e4a04b5ae2bae464bbe3c1d7b79.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　Normal も設定したら　直った」  

## グリッドの色を白にしない？

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　グリッドの色が赤なの主張が強いから　白にしない？」  

![202307_maui_22-2333--gridIsWhite.png](https://crieit.now.sh/upload_images/ddc115bc1f417e803c4b799a18f7fb9964bbe8ebf36f8.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　カラーマップの色と　衝突してしまうぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　タイルセット画像と同じだけ　明度を落とした白にすんのよ」  

![202307_maui_22-2337--gridIsGray.png](https://crieit.now.sh/upload_images/5098b48fdf11afcb3844d47efb9cff9a64bbe9b5f156a.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　落ち着いた　目に優しい色になったぜ」  

## カラーマップの枠の色の青紫の明度を上げれないか？

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　青紫の明度が低いなあ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　SkiaSharp には　ＲＧＢじゃなくて　色相と明度で指定する方法　無いの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　ＨＳＶか。有るかも知れないな。調べてみるか。有った」

📖 [SKColor.FromHsv(Single, Single, Single, Byte) Method](https://learn.microsoft.com/en-us/dotnet/api/skiasharp.skcolor.fromhsv?view=skiasharp-2.88)

![202307_maui_23-0013--colorMap.png](https://crieit.now.sh/upload_images/89cd37bb76a75a348d294047336be8e664bbf281ae56a.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　目に優しくないな……」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ビビッドか、ストロングなの　やめなさいよ」  

![202307_maui_23-0019--light.png](https://crieit.now.sh/upload_images/9bf400ab3303e9a523382bc5d566117c64bbf39a39f72.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　じゃあ　ライトで」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ここらへんで　決めましょう」  

## 作業中の表示が邪魔じゃないか？

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ここまで画面ができあがってくると、画面上に出ている **作業中** の表示は　もう要らないんじゃないか？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　コメントアウトの切替えで復活できるようには　しておきたいな」  

![202307_maui_23-0100--no-debug.png](https://crieit.now.sh/upload_images/81fe2be5d48f4598f715fd5a5a82f82464bbfd6d49e0f.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　デバッグ用のものを　非表示にしたぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ひとまず　この画面は完成としましょう」  

## タイルセット画像選択画面を作れだぜ

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　その画面に入る前に　本当は　タイルセット画像を選ぶ画面が要るだろ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　フォルダーや　ファイルの概念が分からない　ツクラー種族や、スマホ種族を　どうしてくれよう」  

![202307_maui_23-0111--tileset-folder.png](https://crieit.now.sh/upload_images/af5068fc4bed227a009c4bc5439b14f964bbffc72fd6f.png)  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　👆　このフォルダーに置け、と決めて置いたら　いいんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　タイルセット画像というものが、ファイル名ぐらいでしか管理されてないことに  
前時代的なものを感じるぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　Author で分けたくは　ある」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　全ての `.png` 画像に、 `.toml` 設定ファイルを添えるかな？」  

![202307_maui_23-0140--image-data.png](https://crieit.now.sh/upload_images/fcf9d06697988421aa7a4ff69e2e07d664bc0684ea8c3.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これぐらいじゃ　全然足りないだろうけど」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　画像フォルダーに　`.toml`　入ってたら　邪魔じゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ファイルの説明のファイルだから、ファイルの隣に有ってほしいんだぜ。  
同じファイル・パスですぐ見つかる感じで」  

## ファイル名のリストを表示してみろだぜ

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　試しにファイル名のリストを表示してみろだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　といっても　テーブル・ビューを使えばいいのか、  
コレクション・ビューを使えばいいのか、　リスト・ビューを使えばいいのか、  
さっぱり分からないが……」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👇　サンプルがあるそうだぜ」  

📖 [.NET MAUIのCollectionViewを試してみる](https://devlog.grapecity.co.jp/dotnet-maui-collectionview/)  
📖 [DotNet　＞　maui-samples](https://github.com/dotnet/maui-samples)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　これを見てみるかだぜ」  

📖 [https://github.com/dotnet/maui-samples/tree/main/7.0/UserInterface/ControlGallery](https://github.com/dotnet/maui-samples/tree/main/7.0/UserInterface/ControlGallery)  

![202307_maui_23-0512--collectionView.png](https://crieit.now.sh/upload_images/8c42b74a9b8f66de44b6570f78090dbe64bc384c682e4.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これが　コレクション・ビュー　らしいんだが、何がいいんだろなあ？」  

![202307_maui_23-0512--listView.png](https://crieit.now.sh/upload_images/114a992b8c83925456287e6aee71a95264bc39d43dd10.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これが　リスト・ビュー　らしいんだが、何がいいんだろなあ？」  

![202307_maui_23-0531--refreshView.png](https://crieit.now.sh/upload_images/40db734690019a1bb2d134e0e1a0819664bc3cd9a8018.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これが　リフレッシュ・ビュー　らしいんだが、何がいいんだろなあ？」  

![202307_maui_23-0533--swipeView.png](https://crieit.now.sh/upload_images/e71c084469cd1979fc3191621b2bdfa664bc3d48317a6.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これが　スワイプ・ビュー　らしいんだが、何がいいんだろなあ？」  

![202307_maui_23-0535--tableViewForAForm.png](https://crieit.now.sh/upload_images/f30779f654104b9a220649bafc743d6164bc3daec83b9.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これが　フォームが乗っているテーブル・ビュー　らしいんだが、何がいいんだろなあ？」  

![202307_maui_23-0536--tableViewForAMenu.png](https://crieit.now.sh/upload_images/20ffebfb1aac79919642fc0f4261194b64bc3dfc47cd3.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これが　メニューになっているテーブル・ビュー　らしいんだが、クリックすると画面遷移するぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　どれが　いいんだろな？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　グーグルのイメージ検索結果みたいに　画像を並べるのがいいんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そういうのが　コレクション・ビュー　なのかな」  

![202307_maui_23-1433--collectionViewXaml-o2o0.png](https://crieit.now.sh/upload_images/d959a741125693d1fb42de48a2d8b40164bcbc0b9a7b4.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　じゃあ　コレクション・ビューの　ＸＡＭＬ　を見てみるか」  

```xml
ItemsSource="{Binding Monkeys}"
```

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　👆　`Monkeys`　って何なの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ＭＶＶＭ　使ったサンプルなのかと思ったら　ＭＶＶＭ　使ってねー」  

![202307_maui_23-1445--monkeyList-o2o0.png](https://crieit.now.sh/upload_images/2830c39a4e3c8ceda7ae00d7a0650ee464bcbea90728b.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ただの　Monkey　型のリストだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　Monkey　型は、　Name,　Location,　Details,　ImageURL　のプロパティを持っているぜ」  

## 作業用ＢＧＭをかけようぜ？

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　作業用ＢＧＭを掛けようぜ？   
ＹｏｕＴｕｂｅ　に何かないかな？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　それは　作業用ＢＧＭじゃなくて　違法アップロード動画なのよ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　この音楽データは　買ったことがあると思うぜ」  

📺 [Rockman Arrange Album - iwao](https://www.youtube.com/watch?v=G5XHR6v6JAc)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　また　ＲＯＣＫＭＡＮ　だ。　お父んは　音楽の聞き分けは　ＲＯＣＫＭＡＮ　かそうでないか　ぐらいしか　耳が分からないんだ」  

## コレクション・ビューを調べようぜ？

![202307_maui_23-1516--monkeyClass-o2o0.png](https://crieit.now.sh/upload_images/fc170123741f8e1ac58b70396cfaf44764bcc5f17a4af.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　Monkey　クラスは　ただのモデルだぜ」  

```xml
                <DataTemplate>
                    <Grid Padding="10">
                        <Grid.RowDefinitions>
                            <RowDefinition Height="35" />
                            <RowDefinition Height="35" />
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="70" />
                            <ColumnDefinition Width="80" />
                        </Grid.ColumnDefinitions>
                        <Image Grid.RowSpan="2" 
                               Source="{Binding ImageUrl}" 
                               Aspect="AspectFill"
                               HeightRequest="60" 
                               WidthRequest="60" />
                        <Label Grid.Column="1" 
                               Text="{Binding Name}" 
                               FontAttributes="Bold"
                               LineBreakMode="TailTruncation" />
                        <Label Grid.Row="1"
                               Grid.Column="1" 
                               Text="{Binding Location}"
                               LineBreakMode="TailTruncation"
                               FontAttributes="Italic" 
                               VerticalOptions="End" />
                    </Grid>
                </DataTemplate>
```

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　👆　じゃあ　`<DataTemplate>`　というのは　ただのグリッド・レイアウトだから  
それ以外のところに　コレクション・ビュー　独自のものがあるはずよ」  

```xml
ItemsLayout="VerticalGrid, 2"
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ここだけしか　独自のものが無いが……」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👇　`ItemsLayout` は　ここにまとまってあるぜ」  

📖 [CollectionView レイアウトの指定](https://learn.microsoft.com/ja-jp/dotnet/maui/user-interface/controls/collectionview/layout)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　コレクション・ビューの使い方は分かった。使ってみよう」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　じゃあ　Monkey　クラスに当たるものを作ろう。  
`TilesetRecord`　みたいな名前でいいかな」  

![202307_maui_23-1543--tilesetRecord-o2o0.png](https://crieit.now.sh/upload_images/1b4acb4ad591bdbad486338933c6d82e64bccc2e39449.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　とりあえず　深く考えず　`TilesetRecord`　を作った」  

![202307_maui_23-1610--tilesetRecords-o2o0.png](https://crieit.now.sh/upload_images/26b0d46478a7367fa6aa495331b76a7764bcd2aea8040.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　リストも　仮の内容で　作っておこうぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　`internal` プロパティは　ＸＡＭＬから見えないのか。 `public` プロパティに変えよ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　モデルのプロパティに　`public`　を強要するのは　おかしい。  
本来は　ビューモデル　にするべきでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ＭＶＶＭのサンプルじゃないしなあ。  
ＭＶＶＵでやるなら　ビューモデルかな」  

![202307_maui_23-1626--collectionView.png](https://crieit.now.sh/upload_images/18d489ed85ca337dfe585e69002b152d64bcd65891225.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　こういう見た目になった。　サムネイル画像が無いと　寂しい」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　サムネイル画像を生成するプログラムを　先に書いたらいいんじゃない？」  

## テンポラリー・フォルダーをどこにするかだぜ？

![202307_maui_23-1632--temporaryFolder.png](https://crieit.now.sh/upload_images/e1989d577b4321b3ade48c0772aa8d1c64bcd7ac8e6c5.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ユニティ・プロジェクトに　一時ファイルを入れたくないんで、  
ローカルＰＣのフォルダーに　著者名、作品名で　フォルダー切った方がいいかだぜ？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　Windows べったりな発想だな。MAUI に　テンポラリー・フォルダーの概念　無いのかだぜ？  
👇　調べろだぜ」  

📖 [How can I save temporary file in .NET MAUI?](https://stackoverflow.com/questions/70164497/how-can-i-save-temporary-file-in-net-maui)  
📖 [File system helpers](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/file-system-helpers?tabs=windows)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　MAUI の流儀では　テンポラリー（Temporary；一時的な）と呼ばず　キャッシュ（Cache；隠し場）って呼ぶんだな」  

（カタ　カタ　カタ　カタ）

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　C# で Windows のファイル・エクスプローラーを開く方法も調べとこう」  

📖 [How can I open a folder in Windows Explorer?](https://stackoverflow.com/questions/32395163/how-can-i-open-a-folder-in-windows-explorer)  

![202307_maui_23-1843--accessDenied.png](https://crieit.now.sh/upload_images/f6c016e7378b72b60483b0b1218fa57964bcf68dd298c.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　キャッシュ・ディレクトリーを開こうとしたら　アクセスを拒否されたぜ」  

（カタ　カタ　カタ　カタ）

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あれっ？　隠しフォルダーではないフォルダーからも　アクセスを拒否されたぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　MAUI で ファイル・エクスプローラーを開けることが　イレギュラーなんじゃないか？」  

![202307_maui_23-1922--cacheDirectory-o2o0.png](https://crieit.now.sh/upload_images/a6ce4d2e16c6d7580e2a4f641008084964bcff9de2ac9.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　とりあえず　キャッシュ・ディレクトリーの場所だけ示すことにした」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　キャッシュ・ディレクトリーの下は　どのように使うかといった流儀は　あるのかだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　MAUI より、 UWP を調べた方がいいんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　分からん。　とりあえず　勝手に使って、文句出たら　変えよう」  

![202307_maui_23-1945--longDirectoryName-o2o0.png](https://crieit.now.sh/upload_images/c02b0882ff588a40f75355a4d1cd7ef564bd04ed8912c.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　１７０文字ぐらいあるが、だいたい　２５５文字しか入らないのに大丈夫か？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　まあ　とりあえず　それで」  

# 📅 （2023-07-24 mon）フォルダーなの？　ディレクトリーなの？

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　フォルダーなの？　ディレクトリーなの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　どっちでもいい」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　お父んのソースコードでは　`ディレクトリ`　という単語は０件で、　`フォルダ`　で統一されているぜ。  
しかし　C# の入出力ライブラリーのプロパティ名は　`Folder`　ではなく　`Directory`　だぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　プログラマーは　この表記ゆれを気にしないの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ファイルで連想する対になるものは　フォルダーだぜ。  
ディレクトリーは　歴史的な名称だぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　ネギラーメンは　フォルダーで統一するかだぜ」  

## Tileset フォルダーなの？ Tilesets フォルダーなの？

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　Tileset フォルダーなの？ Tilesets フォルダーなの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　タイルセットが　いくつか入っていることを期待するから　`Tilesets`　でフォルダーだぜ。  
うおお。　名前の付け直しだ」  

## タイルセットのサムネイル画像の仕様を決めようぜ？

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そもそも　タイルセット画像って　小さいんだよな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　そのまま　並べりゃどうなの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　とりあえず、タイルセット画像を、そのまま複製して　キャッシュ・フォルダーの下に置くプログラムを考えてみようぜ？」  

![202307_maui_24-2142--image-copy-o2o0.png](https://crieit.now.sh/upload_images/6eddf616ca327e07de7ef420671cfb1564be71ee22186.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　前に似たプログラムを書いたので参考にしよう」  

![202307_maui_24-2203--thumbnails.png](https://crieit.now.sh/upload_images/ccce1d3d194936ea99f94c50c6012d5464be76e0af206.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ファイルのコピーは　できたが、  
サイズを調整したいぜ。 MAUI ではできないから、 SkiaSharp でやるかな」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　SkiaSharp で作ったビットマップを保存する方法が分からん」  

📖 [Using SkiaSharp, how to save a SKBitmap ?](https://social.msdn.microsoft.com/Forums/en-US/25fe8438-8afb-4acf-9d68-09acc6846918/using-skiasharp-how-to-save-a-skbitmap-?forum=xamarinforms)  

![202307_maui_24-2247--resizeImage-o2o0.png](https://crieit.now.sh/upload_images/ff256391313ce4fab6a33da994ee7aae64be812c99e39.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　画像サイズの変更もでけた」  

## ファイル名は UUID にするか？

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　インターネット上の　すべてのタイルセット画像のファイル名が　被らないようにする対策は　してんの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ガチでやるなら　ファイル名は　UUID　にするか」  

![202307_maui_24-2255--rename-file.png](https://crieit.now.sh/upload_images/9af9cb3296d4a38852c53bbd8597ad9764be83071f786.png)  

```plaintext
"adventure_field" ----> "86A25699-E391-4D61-85A5-356BA8049881"
"map-tileset-format-8x19.png" ----> "E7911DAD-15AC-44F4-A95D-74AB940A19FB"
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　こういう名称変更をするぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　名前って　何だったんだろな？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ローカルで通じればいいのが　名前よ」  

![202307_maui_24-2321--uuidAndTitle.png](https://crieit.now.sh/upload_images/1f5ceb28ccf9c3291675867eac7ceb6964be891476234.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　UUID と　タイトルだけあれば　いいのでは？」  

![202307_maui_24-2326--tileTitle-o2o0.png](https://crieit.now.sh/upload_images/926c3bff0c64c451a55215ed9458359664be8a2a9c05b.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　タイル名も、　タイル タイトル　に名前を変えたろ」  

![202307_maui_25-0005--collectionView.png](https://crieit.now.sh/upload_images/ac5330f87268476cca7fff2b5191221764be937d77033.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　サムネイル画像を出すところまでは　できたぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　やったな！」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　やったわね！」  

# 📅 （2023-07-25 tue）UUIDを勝手に付けてくれだぜ

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　UUID って、どうやって生成するんだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　勝手にファイル名を　UUID　に置き換えてほしいよな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　👇　C# は GUID という名前になってて、UUID とは別物らしいわよ？」

📖 [How can I generate UUID in C#](https://stackoverflow.com/questions/8477664/how-can-i-generate-uuid-in-c-sharp)  

```csharp
String UUID = Guid.NewGuid().ToString();
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これで作れるらしい。試してみようぜ」  

```plaintext
be6c25e9-2bca-40ba-84b4-b9f24bef6df3
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　小文字かあ。大文字にしてやるかな」  

```plaintext
95DE5BEE-A51E-41D1-B068-C6F436603AD4
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　いいんじゃないか？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　📂 `Tilesets` フォルダーの下に 📄 `{名前}.png` 画像を放り込んでおけば、  
名前が UUID じゃないとき　UUID にしてくれだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そんなことしていいのかな……」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　対応する TOML ファイルも勝手に作ってくれたら便利じゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　画像ファイルを　コピー貼り付けしたら、勝手に　UUID　振られる　地獄のフォルダーになるぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　地獄のフォルダーなのよ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ユーザーの知らない所で　元ファイルを変更すると　ユーザーのストレスになるケースもあるから、  
`タイルセット一覧画面`　で　『ファイルを変更しますか？』　か何かのメッセージを出してもいいかもしらん」  

![202307_maui_25-1933--test-images.png](https://crieit.now.sh/upload_images/da1e1c01c723bb12da77084e602dc1ea64bfa5389a946.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　うそ画像を　水増しして　`Tilesets`　フォルダーに放り込もう」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ひどい……」  

![202307_maui_25-1950--collection-view.png](https://crieit.now.sh/upload_images/f7e9eb5b595c479375a8727cd8e7359464bfa9327aebe.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　こんな風に出てくるのか、嬉しくないな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　もっと　詰めて表示しなきゃ　サムネイル画像を小さくした意味がなくない？」  

![202307_maui_25-2023--horizontal.png](https://crieit.now.sh/upload_images/3021e00f8b005c21cc9bca7efc98e10b64bfb0dd3563e.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`ItemsLayout` 属性を `HorizontalList` にすると　１段になってしまうんだな」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　UUID が横に長い！」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ファイル名が画像に被ってない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　UUID は３行に折り曲げたろかな？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　UUID なんか表示しなくてよくない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そういう考えもあるか」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　Collection View には　Flex Layout　が無いらしいぜ」  

📖 [[Enhancement] WrapLayout support in the CollectionView #1808](https://github.com/dotnet/maui/issues/1808)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　どうすんだぜ？」  

# 📅 （2023-07-26 wed）妥協しようぜ？

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　MAUI のパターンから外れると　つらそうなので　妥協しようぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　と思ったが `HorizontalGrid, 2` が機能しない。なぜだかは分からない」  

![202307_maui_26-1943--VerticalGrid10.png](https://crieit.now.sh/upload_images/9a32e21052cf480d5ef2c147a0ffa41564c0f90c52172.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`VerticalGrid, 10` 。　静止画だと　できているように見えるが、  
ウィンドウを広げても　ずっと　１０列」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ウィンドウを広げるという概念は、 MAUI が大好きな Android アプリには無いのでは？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　MAUI は　デスクトップ・アプリ向けではないのよ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　どうすんだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　妥協しようと思ったが　その妥協ラインでもダメだった　手詰まりだぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　他のビューを調べろだぜ」  

![202307_maui_26-2010--ListView.png](https://crieit.now.sh/upload_images/36add7dccbed490ab173526743000fee64c0ff332850b.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　リスト・ビューは　縦長で、なんか選べそうな色が付いてるな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　画面全体を使って　サムネイルを並べる　フラットなレイアウトは無いの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ＭＡＵＩには無いぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　CollectionView　の　`VerticalGrid`　というのは　縦長のスマホで何列にするか　という意味なのでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　それでも　`HorizontalGrid`　が機能しない理由が分からない」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　縦でも　横でもなく　ウィンドウ・サイズの面積を　最大限に使ってほしいのよ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ＭＡＵＩは　デスクトップ・アプリ向けではないんで」  

## コレクション・ビューで進めようぜ？

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　`VerticalGrid, 4`　で進めようぜ？」  

![202307_maui_26-2026--CollectionView.png](https://crieit.now.sh/upload_images/1216f2fc9f413736c9f000f237976d2e64c10303e17ad.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　コレクション・ビューは　初期状態では　項目の選択はできないみたいだな」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👇　これを読めだぜ」  

📖 [Single selection](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/collectionview/selection#single-selection)

![202307_maui_26-2037--SingleSelection.png](https://crieit.now.sh/upload_images/cf10124aee4908f3b5f53ceb875b013c64c105b3d8fab.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　こんなＵＩが良いとは思わないが　これで我慢するかだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　画像が切れてるわよ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　どうして　こんなに　欠陥だらけなのか……」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　左上のタイルのサイズに　引きずられているのでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そんなん困るぜ」  

![202307_maui_26-2047--SizeRequest.png](https://crieit.now.sh/upload_images/44f7d7d9ab45573bb51cee29c320fd2464c107f4013e0.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　固定サイズにするしかないか～」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ユーザーが　ウィンドウサイズを変更するたび、  
列数を変えることはできないの？」  

![202307_maui_26-2128--BindingItemsLayout.png](https://crieit.now.sh/upload_images/075c2afbc08c207a0cebd34fad3f523064c1118fd1437.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　コンテント・ページのサイズは取れるけど、  
コレクション・ビューの ItemsLayout プロパティに Binding した文字列が効いてないぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　文字列ではないのでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`GridItemsLayout`　クラスを使ってみるが、効かないなあ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　👇　これを読みなさい」  

📖 [CollectionView does not update when changing ItemsLayout #7747](https://github.com/dotnet/maui/issues/7747)  
📖 [[Windows] Update CollectionView changing ItemsLayout #14532](https://github.com/dotnet/maui/pull/14532)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　これで　できたという報告なんだが、わたしには　理解できないなあ」  

📖 [[Windows] Update CollectionView changing ItemsLayout #14532](https://github.com/dotnet/maui/pull/14532/commits/8097ff10dd15cd08045fb3b2195839b5bc1a87d1)  
📖 [AdaptiveCollectionView.xaml](https://github.com/dotnet/maui/blob/main/src/Controls/samples/Controls.Sample/Pages/Controls/CollectionViewGalleries/AdaptiveCollectionView.xaml)  
📖 [AdaptiveCollectionView.xaml.cs
](https://github.com/dotnet/maui/blob/main/src/Controls/samples/Controls.Sample/Pages/Controls/CollectionViewGalleries/AdaptiveCollectionView.xaml.cs)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　そのサンプルを動かせないのかだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　クラス・ライブラリの出力タイプらしくて、動かないぜ」  

![202307_maui_26-2308--Rotate.png](https://crieit.now.sh/upload_images/54dc7332980dd9c3bc796601c1f46c0664c12907ac548.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　回転したり　半透明にしたりすることはできるのに、  
１１列にするとか　８列にするとか、列数を変えることは　頑としてできない」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　要らんことは　できるのに……」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　直ってるらしいんだが、直ってないぜ」  

📖 [CollectionView with GridItemsLayout: Issues when changing Span #8387](https://github.com/dotnet/maui/issues/8387)  
📖 [[Windows] Notify changes in CollectionView Layouts #13137](https://github.com/dotnet/maui/pull/13137)  

## MAUI のバージョンは？

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　MAUI のバージョンって　どうやってチェックできる？」  

![202307_maui_26-2327--mauiVersion.png](https://crieit.now.sh/upload_images/05fc89128dc6d093a27db3421d89e9d664c12d5b2b2c4.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これは　新しいのかだぜ？　古いのかだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　公式ページに何か書いてないかしら？」  

![202307_maui_26-2344--bug.png](https://crieit.now.sh/upload_images/9bbb9a980f67aa5de219170db3ea7c4a64c1316701536.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　マイクロソフトは　直ったと言い張り、わたしの環境では　直っていないので　これは放置して次へ進むことにする」  

# 📅 （2023-07-27 thu）ページを開くときに、ページの縦横幅を知ってることはできるの？

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ページを開いたときに　ページの縦横幅を知ることができるなら、  
ページを開いた直後なら　コレクション・ビューの列数を　調整することができるんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　調べるか……。プログラマーの美徳の１つは　怠惰だしな」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　怠惰のくせに　しつこく粘るな」  

![202307_maui_27-1951--viewModel-o2o0.png](https://crieit.now.sh/upload_images/d9a469e219957f3335d2c61320b9726164c24c8535837.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ＸＡＭＬの中で　ビューモデルを埋め込んでいたが、  
このビューモデルの中で　ItemsLayout　を初期化していて、その値を　そのあと一切　上書きできないというのが  
今回の不具合だぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ＸＡＭＬの中に　ビューモデルを埋め込むのを　止めろだぜ」  

![202307_maui_27-2018--InitializeComponent.png](https://crieit.now.sh/upload_images/761557e4d2fd91b13a4877e5601e33a664c252a6460b2.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`InitializeComponent()` の中で `ItemsLayout` の読込もしてるようなので、  
それより前に `ItemsLayout` をセットしないとな」  

![202307_maui_27-2027--pageWidth.png](https://crieit.now.sh/upload_images/afdbeb53391d096e40ece637b0200a9064c254c473d06.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ページのコンストラクターでは、ページの横幅を取れないぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　MAUI は　`InitializeComponent( )`　の中で　どうやって　ページの横幅を算出するの？」  

```plaintext
[TilesetListPage.xaml.cs TilesetListPage] this.WidthRequest: -1, this.MaximumWidthRequest: ∞, this.MinimumWidthRequest: -1, this.Width: -1
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　横幅は　持ってないみたいだなあ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　親ウィンドウのサイズは取れないのかだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　`this.GetParentWindow()`　はヌルだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　このページに遷移してくる前に　横幅を渡せばいいんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　画面の遷移は自動化されていて、  
遷移前に値を渡す方法は　分からないぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そして　ページのコンストラクターは　１回呼ばれると  
画面遷移して戻ってきても　２回目のコンストラクターは呼び出されないぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ページは破棄されてないんだ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　グローバル変数を使えばいいんじゃない？」  

![202307_maui_27-2112--navigatingFrom.png](https://crieit.now.sh/upload_images/a33c348d126cb5d237d5577c4a9f8eec64c25f50d1e0b.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`NavigatingFrom`　イベントハンドラで　メイン・ページの横幅を取れたぜ」  

![202307_maui_27-2124--navigatingFrom.png](https://crieit.now.sh/upload_images/b77981efb8a4c3705f5a319a910a032d64c2620b8fb21.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これは挫折。　マイクロソフトの仕様の前に　失望し、意志を曲げ、諦めよう」  

## タイルセットをクリックして、この前作った　タイル切抜き画面へ　遷移してくれだぜ

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　タイルセットをクリックして、この前作った　タイル切抜き画面へ　遷移してくれだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　クリックして　すぐ画面遷移すると　タイルセットのデータを見れないんで、  
タイルセットを選んだあと　ボタンを押してから画面遷移するようにするぜ」  

![202307_maui_27-2227--tileset-data.png](https://crieit.now.sh/upload_images/46ff2a320ad0afc030b083b8af0cca5e64c270e5aefa8.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ファイル名を　UUID　に変えてもらいたいんだが、  
ユーザーに　どう説明して  
ファイル名を　UUID　に強制的に変えさすかな？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　『ファイル名をＵＵＩＤに変更する（言うことを聞け）』ボタンを置けだぜ」  

## UUID かどうかを判定

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ＵＵＩＤになってるか、そうでないか　判定する方法が欲しいな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　👇　これを読みなさい」  

📖 [UUIDをヒットさせる正規表現](https://qiita.com/miriwo/items/8ea80aebd113edafebb0)  

![202307_maui_27-2328--tileset-list.png](https://crieit.now.sh/upload_images/2d13132c1911bbcb37e358de36922d6a64c27f23f34c0.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　とりあえず　画面レイアウトは　こんな感じだとしようぜ？」  

## タイルセット・タイトルは　ローカライズすんの？

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　タイルセット・タイトルは　ローカライズすんの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　１つのタイルセットに　２００か国分の添付ファイルが付いてくるのとか　嫌だな……」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　添付の TOML の中にロケールを詰め込んだらどうだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　１つのファイルに　複数のロケールが入っているのは  
**使わないものが必ず付いてくる**　という　効率が最悪のケースになるかも知らん。  
ロケールのフォルダーを作って　そこに `.toml` ファイルを置いた方がマシか……？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　丁寧にいくなら　それねえ」  

# 📅 （2023-07-28 fri）タイルセットのロケールの仕組みを作れだぜ

```plaintext
　　C:\Users\むずでょ\Documents\Unity Projects
　　└── 📂 Negiramen Practice
　　　　└── 📂 Assets
　　　　　　└── 📂 Doujin Circle Negiramen
　　　　　　　　└── 📂 Negiramen Quest
　　　　　　　　　　└── 📂 Auto Generated
　　　　　　　　　　　　└── 📂 Images
　　　　　　　　　　　　　　└── 📂 Tilesets
　　　　　　　　　　　　　　　　├── 📄 86A25699-E391-4D61-85A5-356BA8049881.png
　　　　　　　　　　　　　　　　└── 📄 86A25699-E391-4D61-85A5-356BA8049881.toml
```

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👆　今　こうなっているが……」  

```plaintext
　　C:\Users\むずでょ\Documents\Unity Projects
　　└── 📂 Negiramen Practice
　　　　└── 📂 Assets
　　　　　　└── 📂 Doujin Circle Negiramen
　　　　　　　　└── 📂 Negiramen Quest
　　　　　　　　　　└── 📂 Auto Generated
　　　　　　　　　　　　└── 📂 Images
　　　　　　　　　　　　　　└── 📂 Tilesets
　　　　　　　　　　　　　　　　├── 📂 Locales
　　　　　　　　　　　　　　　　│ 　├── 📂 ja-JP
👉 　　　　　　　　　　　　　　│ 　│ 　├── 📄 86A25699-E391-4D61-85A5-356BA8049881.toml
　　　　　　　　　　　　　　　　│ 　└── 📂 en-US
👉 　　　　　　　　　　　　　　│ 　　　└── 📄 86A25699-E391-4D61-85A5-356BA8049881.toml
　　　　　　　　　　　　　　　　└── 📄 86A25699-E391-4D61-85A5-356BA8049881.png
```

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👆　こうなる想定かだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そうだな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　どの言語にも差が無い共通の設定は　`ja-JP`　に入れるの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　デフォルトが　`ja-JP`　だからな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　`en-US`　で編集したら　`ja-JP`　に保存されるみたいなことがあるの？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　編集したのなら、すべての言語ファイルに更新を掛けないと　おかしくないか？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　英語版にあって　日本語版にないと　おかしな感じだしな」  

```plaintext
　　C:\Users\むずでょ\Documents\Unity Projects
　　└── 📂 Negiramen Practice
　　　　└── 📂 Assets
　　　　　　└── 📂 Doujin Circle Negiramen
　　　　　　　　└── 📂 Negiramen Quest
　　　　　　　　　　└── 📂 Auto Generated
　　　　　　　　　　　　└── 📂 Images
　　　　　　　　　　　　　　└── 📂 Tilesets
　　　　　　　　　　　　　　　　├── 📂 Locales
　　　　　　　　　　　　　　　　│ 　├── 📂 ja-JP
　　　　　　　　　　　　　　　　│ 　│ 　├── 📄 86A25699-E391-4D61-85A5-356BA8049881.toml
　　　　　　　　　　　　　　　　│ 　└── 📂 en-US
　　　　　　　　　　　　　　　　│ 　　　└── 📄 86A25699-E391-4D61-85A5-356BA8049881.toml
　　　　　　　　　　　　　　　　├── 📄 86A25699-E391-4D61-85A5-356BA8049881.png
👉 　　　　　　　　　　　　　　└── 📄 86A25699-E391-4D61-85A5-356BA8049881.toml
```

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👆　ベースとしての設定ファイルは、ロケールとは別に　要るのでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　`Author`　とか　どうすんだぜ？  
Michael さんは　`ja-JP` では ミカエルさんになるのかだぜ？  
ベースの設定ファイルには　どっちが入ってる？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　最後に見たロケールで上書きしたらどう？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　`ja-JP` から `en-US` に切り替えてから保存したら、  
`en-US` の設定ファイルが　日本語で上書きされるのかだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　設定されていなければ　上書きしたらいいんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　要らんデータで　上書きされたくないぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　全ての翻訳可能なプロパティは　個別にロケールを記憶しておけばどうだぜ？」  

```toml
title = "冒険の荒野"
title_locale = "ja-JP"

author = "むずでょ"
author_locale = "ja-JP"
```

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👆　こんな感じで」  

```toml
[title]
value = "冒険の荒野"
locale = "ja-JP"

[author]
value = "むずでょ"
locale = "ja-JP"
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　TOML なら　こうするかな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　画面を　英語で表示して、日本語入力したい人は　どうすんの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そんなことは　するなと　言いたい」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　素直に　設定されてないロケールで見たら　空欄が楽かなあ？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　プレースホルダーに出すとかかな」  

ベース：  

```toml
[global]

uuid = "86A25699-E391-4D61-85A5-356BA8049881"
extension = ".png"
publish_date = 2023-06-26T00:00:00+09:00

[user_defined]
```

ロケールが `ja-JP`：  

```toml
[local]

title = "冒険の荒野"
author = "むずでょ"

[user_defined]

# 自由に使える欄
```

ロケールが `en-US`：  

```toml
[local]

title = "adventure field"
author = "Muzudho"

[user_defined]

# Please feel free to use here.
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　こんな感じで　どうだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　試しにやってみないと　分かんないわねぇ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　UUID ではないファイル名は　どこにセットしたらいいんだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　じゃあ　`fileStem`　というフィールドも増やすかだぜ」  

# 📅 （2023-07-29 sat）TOMLファイルを読めるようにしようぜ？

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　打ち込むか」  

（カタ　カタ　カタ　カタ）  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あっ、循環参照した。空間が成立してない」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　サークル名と、作品名は　アプリケーションが始まる最初に入力しておかないと、  
構成ファイルの場所を取得するために　構成ファイルを見る、という循環参照が起こるぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　トップ・ページは　構成ファイルを読まなくても　表示できるようにしておかないといけないのよ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　**構成ファイルの場所**　が先に在って、　**構成ファイルそれ自体**　は後に在るのか」  

![202307_maui_29-0708--config.png](https://crieit.now.sh/upload_images/aebe5d82ced43ab6a6a6b4c92f674bb464c43c873c169.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　今こんな感じだが、内部的に改修しよう」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あれっ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　すると構成画面に入る前に　毎回、サークル名と　作品名を　入力する必要が出てくるぜ？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ユーザー名と　パスワードみたいだな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　じゃあ　構成ファイルは　サークル名とパスワードのペアを　覚えておけばいいのよ」  

```toml
[[entry]]
circle = "Doujin Circle Grayscale"
work = "Negiramen Quest"

[[entry]]
circle = "Apple Banana Cherry"
work = "Dragon Fruits"

[[entry]]
circle = "Shogi Club"
work = "Furi Bisha"
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　こんな感じか」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　現在の　`configuration.toml`　ファイルは、　`Doujin Circle Grayscale/Negiramen Quest/project.toml`　みたいな名前に変えるべきでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　じゃあ　まず　ログイン画面から作るか」  

## ログイン画面を作ろうぜ？

![202307_maui_29-0729--loginPageIdea.png](https://crieit.now.sh/upload_images/802359e2cc176acbe3e4b1d55210f99a64c4424c2bab5.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　こんな感じかだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　同じのを何度も入力したくは　ないんじゃない？」  

![202307_maui_29-0735--loginPageIdea2.png](https://crieit.now.sh/upload_images/808d7b1349da9af2d7640d07d9eaa6d764c44493746a6.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　リストから選べるように工夫だぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　試しに作ってみてくれだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あっ！　だったら　Unity の Assets フォルダーの場所も　入力しておいてほしいぜ！」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　今の構成ページを、ログイン・ページに変えたらいいんじゃない？」  

## メインページより先にログインページを出すには？

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　メインページより先にログインページを出すには　どうやったらいいんだぜ？」  

![202307_maui_29-0750--AppShellXaml-o2o0.png](https://crieit.now.sh/upload_images/dad58c47b9af35c15301fa9e9c9c494c64c446876b1e4.png)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👆　試しに　AppShell.xaml　を　いじったったらどうだぜ？」  

![202307_maui_29-0753--Startup-o2o0.png](https://crieit.now.sh/upload_images/311eda52978d65ddeb7e9ea655b824cb64c447187a638.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　Ｏｋ！　これでいける！」  

## Workspace フォルダーの名前を変えたい

![202307_maui_29-0810--Workspace.png](https://crieit.now.sh/upload_images/35512c41ecbd758940ffb7f1451e0b0a64c44b01002fc.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　この Workspace フォルダーへのパスを入力させるの、難しいな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　インストール時に勝手に設定してくれないの？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ユーザーを想定して　開発者がパスの設定をするのは大変だから　ユーザーが設定してほしい」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　このフォルダーの中で作業するのではなく、開始時にデプロイする初期ファイルが入ってるだけだから  
フォルダーの名前も　`Starter Kit`　とかに変えたいぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ログイン後は　２度と使わないんじゃないの？　このディレクトリー・パス」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　`Starter Kit`　という名前にリネームしていくぜ」  

（カタ　カタ　カタ　カタ）  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　じゃあ、　`user_configuration.toml`　というファイルは　現状、　`configuration.toml`　で置き場所を設定できるようにしていたが、  
大変なので　`Starter Kit`　フォルダーの直下に置くように　固定していい？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　設定できることは少ない方が　ユーザー・サポートが楽になるぜ」  

## configuration.toml を改造したい

![202307_maui_29-0949--ConfigurationToml.png](https://crieit.now.sh/upload_images/c6fd35f76d71fbfbf5793ca248becd8564c46227bc603.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　今　こんな感じだが、　`[profile]`　テーブルを、　`[[entry]]`　テーブル配列に変えるぜ」  

（カタ　カタ　カタ　カタ）  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あっ、　次回　どの設定で　再開するか　覚えておく必要があるぜ！」  

```toml
[remember]

# あたなのサークル名
your_circle_name = "Doujin Circle Negiramen"

# あなたの作品名
your_work_name = "Negiramen Quest"
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　こんな感じのデータは　やっぱり要るか？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　変えたり　戻したり　だな」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　エントリーは　ドロップ・ダウン・リストでいいかな。 50 件も 100 件もあるようなデータじゃないだろ」  

## やっぱりログイン画面は作ろうぜ？

![202307_maui_29-1108--starterKit.png](https://crieit.now.sh/upload_images/65c485094d9599b373e91a9b6eb792fa64c475144d237.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　構成ページではなく、　別途　ログイン・ページが要ると思う。それも２ページ構成で」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　変更じゃなくて　機能追加だったのね」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　`LoginPage1` にすると `LoginPage1ViewModel` になるのが　カッコ悪いから、  
`Login1Page` にして `Login1PageViewModel` になるようにする」  

![202307_maui_29-1255--login1page.png](https://crieit.now.sh/upload_images/8e6e6fcd4e2f4b5fd06bfca97840d9ba64c48dc4077f9.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これを整形したり　動きを付けたりしないといけない　大変だ……」  

## 『君たちはどう生きるか？』

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　観てきた～」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　続きをしろだぜ」  

## ログイン画面の作成の続き

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　つら……」  

# 📅 （2023-07-30 sun）ログイン画面　はよ作れだぜ

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ログイン画面　はよ作れだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　画面制作は　つらい」  

（カタ　カタ　カタ　カタ）  

![202307_maui_30-1204--login1page.png](https://crieit.now.sh/upload_images/df368437d4455bddbee75dc104609db264c5d37631e82.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆👇　まず　レイアウトから。 MAUI はバグ放置があって　思ったように　レイアウトできない」

📖 [Picker width on Windows not filling container #6391](https://github.com/dotnet/maui/issues/6391)  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　大企業でも　進捗は　こんなものなのね」  

![202307_maui_30-1228--login2page.png](https://crieit.now.sh/upload_images/06c41b70489dc99f9ed864dc425b0fed64c5d8d4d48a2.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　２ページ目のレイアウトは　こんな感じで」  

## 文字数のカウント

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　動きを付けろだぜ」  

![202307_maui_30-1408--numberOfCharacters.png](https://crieit.now.sh/upload_images/a36109a94fd3c6853b524ed9d2d5101a64c5f05c08c31.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　文字数のカウントは付けたが、すでに登録されているかどうかで  
新規作成か　続きからかを分けるところ　作るの　めんどくさいな」  

## エントリー・リストを出す

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　１つ１つ解消していきなさい。まず　リストを出してみなさい」  

```xaml
                <Picker Grid.Row="1" Grid.Column="2"
                        VerticalOptions="Center"
                        HorizontalOptions="StartAndExpand"
                        WidthRequest="300"
                        ItemsSource="{Binding EntryList}"
                        SelectedItem="{Binding SelectedEntry}"
                        ItemDisplayBinding="{Binding PresentableTextAsStr}"/>
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`ItemDisplayBinding` 属性に　リストの要素のメソッド名を入れれば　表示文字列になってくれるのか」  

![202307_maui_30-1544--entryList.png](https://crieit.now.sh/upload_images/e95759244092495f537ab7dc2563a46864c6076b781be.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　サークル名と　作品名を　構成ファイルから取ってくるようにしたぜ」  

## リストから選んで、テキストボックスへ入れる

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　それを選んだたら　隣のテキストボックスへ入れろだぜ」  

![202307_maui_30-1641--choicePicker.png](https://crieit.now.sh/upload_images/8776c3152dbe70b4f87111772be0bc5164c614520ab22.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　隣のテキストボックスに入るようにしたぜ」  

## レイアウトを調整

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　文字数がサークル名を、　使える文字が作品名を指しているように　誤解を与えるんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　レイアウトの設計は　本当に大変」  

![202307_maui_30-1712--table.png](https://crieit.now.sh/upload_images/3b96e66d6869be4a488e3576cd3db1ed64c61bd39c446.png)  
![202307_maui_30-1713--table-en.png](https://crieit.now.sh/upload_images/22a068adb7e8fb90473f5f963cbde1f664c61bdc2563a.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　翻訳時に　伸縮変わるんで」  

## 続きから始めるボタン

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　入力したサークル名と　作品名が　既存のときは　次へボタンではなく、  
続きから始めるボタンが出ろだぜ」  

![202307_maui_30-1827--continue.png](https://crieit.now.sh/upload_images/e9f80b56e63c770543b30af5eddfb6db64c62cfb5730f.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　**続きから** ボタンは　出るようにしたぜ」  

## 次へボタン

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　次は　**次へ**　ボタンを作れだぜ」  

![202307_maui_30-1836--nextButton.png](https://crieit.now.sh/upload_images/3504451a8e7954d7cb04a38da2bc06d964c631377f586.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　作ったぜ」  

## サークル名、作品名に使える文字

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　サークル名、作品名は　フォルダ―名に使うのに、  
このままだと　長い名前を入力されるんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　フォルダー名に使うサークル名　という日本語で２回　名　が出てくるのが煩わしいぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　サークル名　じゃなくて　サークル・フォルダー　だろ」  

![202307_maui_30-1904--folderName.png](https://crieit.now.sh/upload_images/f2b24ac134d26c15a916fae15c6d5f1e64c635dc7ffdb.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　フォルダー名を入れろ、という主張を強めたぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　推奨する使える文字じゃなくて、アスキー・コードで縛った方がいいんじゃないの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　別に　うまく使ってくれりゃ　いいんだぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　あとで困る使い方を　自由にする人が　ほとんどなのに」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　バリデーション・チェックは無しで」  

## テキストボックスへ、前回使った値を入れましょう

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　何度も　サークル・フォルダ名、作品フォルダ名を入力するのが　手間だから  
前回使った値を　最初から入れておくようにしましょう」  

![202307_maui_30-2013--remember.png](https://crieit.now.sh/upload_images/cea0b1b8e1aa4e88ac6174d469e92d9f64c645f2c0bbf.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　構成ファイルから　初期値を読み取るのは作ったぜ。  
構成ファイルに保存する方は　まだだけど」  

## 新規作成のケースを作ってくれだぜ

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　新規作成のケースを作ってくれだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ページの　`Loaded`　イベント・ハンドラーって　１回実行されたら　ページは破棄されていないのか、  
２回来訪しても　呼び出されないんだな」  

# 📅 （2023-07-31 mon）ログイン画面　はよ作れだぜ（２日目）

![202307_maui_31-1734--setup-o2o0.png](https://crieit.now.sh/upload_images/84a93c3c96194ae2608eff9897f6ba7d64c772ab45cf6.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ページを最初に読みこんだときと、ページに再び訪れたタイミングで  
ピッカーの中身を入れ替えればいいんだぜ」  

## ピッカーの項目に削除ボタンを置けないの？

![202307_maui_31-1740--picker-o2o0.png](https://crieit.now.sh/upload_images/cdc442a3a7f5548a3e615648d01057a364c773f922d06.png)  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　項目多くなったら　ウザいわよね。  
リストの項目に　削除ボタンを付けれないの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　その削除ボタンは、リストから項目を削除するボタンなのか、  
それとも　プロジェクトを丸ごと消すボタンなのか？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　リスト上から消すボタンよ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　非表示の意味で　眼玉マークの方がいいのでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　一度　非表示にしたものは、どこから　再表示できるんだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　昔のＲＰＧの皮袋みたいに　要らない道具は　皮袋に詰め込めば  
預かりもの屋に　テレポーテーションすればいいのよ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　その意図を　アイコン１つで説明するのは　難しいな……」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　預かりもの屋も　作らないといけないしな。　いったん保留かな」  

## ConfigurationEntry を ProjectIdentifier へ名称変更

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あのピッカーの項目は、構成のエントリーというより、プロジェクトの識別子だよな」  

```toml
[[project]]

# あなたのサークル・フォルダ名
id.your_circle_folder_name = ""

# あなたの作品フォルダ名
id.your_work_folder_name = ""

# Unity の 📂 `Assets` フォルダ―へのパス
unity_assets_folder = ""
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　意味を強調するなら　こうしたいところ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　構造的にネストしなくても、コメントで十分では？」  

```toml
[[project]]

# （Ｉｄ）あなたのサークル・フォルダ名
your_circle_folder_name = ""

# （Ｉｄ）あなたの作品フォルダ名
your_work_folder_name = ""

# Unity の 📂 `Assets` フォルダ―へのパス
unity_assets_folder = ""
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　じゃあ　こう」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　`unity_assets_folder` は　プロジェクトの構成ファイルが　持つべきで、  
ネギラーメンの構成ファイルが持っては　いけないのでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　それもそう。じゃあ　プロジェクト構成ファイルを作るか～」  

## remember を current に変えたい

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　変数の整合性を　きつくしたいので、  
テキスト・ボックスに入れた値が　現在の設定だ、という風にするぜ」  

# 📅 （2023-08-01 tue）まだログイン画面　できてないのかだぜ？（３日目）

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　まだログイン画面　できてないのかだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　もう　つらい」  

![202308_maui_01-1839--projectIdList.png](https://crieit.now.sh/upload_images/8b61f17a4b865228dc973fb216663b0a64c8d31e0c1e8.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　このリストは　構成ファイルに記憶するのではなく、  
ディレクトリ階層を　探索するだけで良かったのでは？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ディレクトリを探索できない権限のケースもあるから、  
構成ファイルに記録するプログラムは　有るっちゃ有るぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　画面作りの何に時間がかかんの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　作ってみたあとに気づく設計が成立してない矛盾と、  
押されたくないボタンが押せてしまう状態の管理漏れ、
そしてバリデーション・チェックが通らなかったときの表示だぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ほっとくと　都合の悪い入力を通してしまうしな」  

![202308_maui_01-2026--login1page.png](https://crieit.now.sh/upload_images/a3c910fbc32430d8d3cf1df171b956b864c8ebe2c40d0.png)  

![202308_maui_01-2027--login2page.png](https://crieit.now.sh/upload_images/43ad2ddb768cc867561e61b1ce00772164c8ec4e139ff.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　とりあえず　こんなもんで勘弁しろだぜ。  
入力チェックは　パス」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　入力欄は　縦に並んでいる方が　ふつうじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　もっともだぜ」  

![202308_maui_01-2104--login1page.png](https://crieit.now.sh/upload_images/bf0de710521fee7f29f6cbfce556c68f64c8f508747e5.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これでどうだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　良くなったわね」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　しかし、　あなたのサークル・フォルダ名って何だろな？　と、ユーザーは思わないかだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　思うぜ」  

![202308_maui_01-2116--login1page.png](https://crieit.now.sh/upload_images/77476cbcedfd2289699cdab49cb94b2864c8f7c37a2bb.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　あなたのサークル・フォルダ名のテキストボックスの右端に　×ボタンが付いているんだが、  
わたしが付けたわけじゃないし、取り除く方法も分からないぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　仕方ない。ほっといて　次へ進めだぜ」  

## ログインページから始まるように、 MainPage をログインページにしようぜ？

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ログインページから始まるように、 MainPage をログインページにしようぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　なんで　最初のページの名前が　`MainPage`　なんだぜ　やめてくれだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　まあ　プログラムは昔から　`main`　から始まるけど」  

（カタ　カタ　カタ　カタ）  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　差替えたぜ。これで　ひとまず　ログイン・ページは終わりだぜ」  

# 📅 （2023-08-02 wed）初回ログイン時にスターターキットをユニティのAssetsフォルダーへコピーするようにしよう

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　まだ終わってなかった」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　サークル・フォルダ名、作品フォルダ名を作ったあとは、初回ログイン時に  
スターターキットをユニティのAssetsフォルダーへコピーするようにしよう」  

（カタ　カタ　カタ　カタ）  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　とりあえず　実装した。不具合があるかどうかは知らん」  

## タイルセット一覧画面にインポート・ボタンを付けようぜ？

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　タイルセット一覧画面にインポート・ボタンを付けようぜ？？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ドラッグ＆ドロップの方がいいかも知れないが、  
とりあえず　インポートボタンは要るかだぜ」  

![202308_maui_02-2007--importBtn-o2o0.png](https://crieit.now.sh/upload_images/4a9c7dc3b1dc99d31684432d9cf7532664ca3998e40ae.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　インポート・ボタンは　こんなんでいいかだぜ？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　こんなでかいボタン、噴飯ものだが　MAUI　だから仕方ない。良いんじゃないか？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　ファイル・ピッカーの説明が　何言ってるか分からないな」  

📖 [Microsoft　＞　ファイル ピッカー](https://learn.microsoft.com/ja-jp/dotnet/maui/platform-integration/storage/file-picker?tabs=windows)

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　サンプル・プログラムに　ファイル・ピッカーがないか　調べてみるか」  

📖 [jfversluis　＞　MauiFilePickerSample](https://github.com/jfversluis/MauiFilePickerSample)  

# 📅 （2023-08-03 thu）疲れたので寝た

 Zzz  

# 📅 （2023-08-04 fri）インポート画面の練習をしよう

![202308_maui_04-1917--filePickerSample.png](https://crieit.now.sh/upload_images/d237db0b43f7a893373fcf780203440764ccd07270013.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　前述のサンプルは　動くぜ」  

![202308_maui_04-1920--filePickerCode-o2o0.png](https://crieit.now.sh/upload_images/1d22f3ffbaf75490d2f1db9276bffaf664ccd120a2adb.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　コードはこれを真似すれば　良さそうだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ファイル・ピッカーは　コントロールではなく、ただの関数なんだな」  

## ドラッグ＆ドロップ

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ファイル・ピッカーに取り掛かる前に　コントロールの色々なことが分からないので　本番に組み込む前に　練習したいぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👇　リポジトリ―を作っておいたぜ」  

📖 [Muzudho　＞　MAUI-Control-Practice](https://github.com/muzudho/MAUI-Control-Practice)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　画像ファイルを　ネギラーメンに　ドラッグ　アンド　ドロップ　できるかな？」  

📖 [Microsoft　＞　ドラッグ アンド ドロップ ジェスチャを認識する](https://learn.microsoft.com/ja-jp/dotnet/maui/fundamentals/gestures/drag-and-drop)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　`Square` クラスなんか　どこにあるのか？」  

📖 [Microsoft　＞　Microsoft.Maui.Controls.Shapes Namespace](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.shapes?view=net-maui-7.0)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　Rectangle で十分じゃないか？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　MAUI の Rectangle は、 XAML の Rectangle 要素なんだぜ」  

![202308_maui_04-1955--DragAndDrop.png](https://crieit.now.sh/upload_images/85d26164aba5067202145c1a1d738c3664ccd943ebcad.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　矩形を　ドラッグ＆ドロップ　しようとしたら　こんな見た目になるのか」  

![202308_maui_04-2010--Drag.png](https://crieit.now.sh/upload_images/1551bf606ba0383840c72b08edfe8c7e64ccdce80cbd5.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ドラッグ・イベントの仕事は、　禁止マークとか　コピー・マークとか　表示するところにありそうだぜ」  

![202308_maui_04-2015--Xaml-o2o0.png](https://crieit.now.sh/upload_images/7aceafb63df49c40ed115ae3b5fda20064ccde26b1960.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　XAML は　こう書いて」  

![202308_maui_04-2017--CodeBehind-o2o0.png](https://crieit.now.sh/upload_images/4b3d8fb879b19214cf2b22e19c104d2964ccdec9a63d1.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　コード・ビハインドは　こう書けば良さそうだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　Windowsのファイル・エクスプローラー上の画像ファイルを　ＭＡＵＩへドラッグ＆ドロップできるの？」  

![202308_maui_04-2024--DragImageFile.png](https://crieit.now.sh/upload_images/ccf87c1cadcdb67a48666b16634d77bb64ccdff39ecd0.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　できそうな見た目をしているが、ドロップすると　強制終了するぜ」  

![202308_maui_04-2027--error.png](https://crieit.now.sh/upload_images/6bbab9883f9f6738c26f8dfc0ef704e164cce0b7899d8.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　エラーの理由は　ヌルにできないパラメーターに　ヌルを入れたかららしいが、  
分けわからん」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　MAUI にデスクトップ・アプリケーションの機能が無いのは　鉄板の話題らしい」  

📖 [Does MAUI support file drag and drop to other application (Windows, Linux, macOS)? #7508](https://github.com/dotnet/maui/discussions/7508)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　MAUI　は　WPF　より先に廃止するかもしれないな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ファイル・ピッカーで　しのぎましょう」  

## アラート

![202308_maui_04-2157--choice-file.png](https://crieit.now.sh/upload_images/ed05e90ff9b34e53412b6c2a18e715f364ccf6b320f46.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　うーん、ファイルのフル・パスは取れるのか。  
この　ダイアログボックスみたいなの　現代では　アラートと呼ぶらしいが、  
キャンセル・ボタンも出せるだろうか？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　ポップアップはなぜ　XAML　じゃないのか……」  

📖 [ポップアップの表示](https://learn.microsoft.com/ja-jp/dotnet/maui/user-interface/pop-ups)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　いろいろ　あるっちゃ　あるのか」  

![202308_maui_04-2306--alert.png](https://crieit.now.sh/upload_images/1d7e3fa65de92cd9d8407590be14c50a64cd06316e53b.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　とりあえず　こんなんでいいか……」  

## ファイルのコピー

![202308_maui_04-2316--copyFile-o2o0.png](https://crieit.now.sh/upload_images/d95a6b674a68191818fccc10f13975e664cd086427e07.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ひとまず　上図のような　ファイル・コピーをやってみるかだぜ」  

```csharp
                    System.IO.File.Copy(
                        sourceFileName: result.FullPath,
                        destFileName: tilesetPngLocation.Path.AsStr);
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ファイルのコピーだけなら　これでいけるはず」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　Windows の API 使ってるの、いいのか知らんけど」  

![202308_maui_04-2326--copyFile-o2o0.png](https://crieit.now.sh/upload_images/298abbe4c24c155db1eeca239c712c1d64cd0aa236fe4.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ファイルのコピーはでけたけど、コレクション・ビューにも追加しないと　画面に出てこないな」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　コレクション・ビューに入れるのではなく、  
アイテム・ソースに入れるんじゃないか？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そうか」  

![202308_maui_05-0051--import-o2o0.png](https://crieit.now.sh/upload_images/c4373428cc0cffd29db3f157725e6a2364cd1e8cbace3.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　インポートで　画像を放り込んだった」  

## ファイルの削除

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　要らんもの　放り込まれたら　削除したいんだけど」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　削除ボタンを　どこに置くかという　レイアウトも困るよな」  

![202308_maui_05-0110--removeBtn.png](https://crieit.now.sh/upload_images/8106e54b9e16de45156a05abf67eeb2a64cd231c5ac49.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　左下に赤いボタンを置くというのは　どうだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　危ないから間違って押したくないボタンなのに、主張が激しくて　むしろ目が　そっちに行くんじゃない？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　頻繁に　マウスカーソルが近づくとこに　削除ボタンを置いては　いけないのでは？」  

![202308_maui_05-0127--paleViolotRed.png](https://crieit.now.sh/upload_images/2d78f572b5d9e3bfb488ece8341d0ac964cd27221c3d7.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　右上に　ペール・バイオレット・レッド　のボタンを置くというのは　どうだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ロケールのピッカーの真下なのが気になるけど、ちょっと　ずらしたら　どう？」  

![202308_maui_05-0132--deleteBtn.png](https://crieit.now.sh/upload_images/09910719b4f8821d67ed55e5e21ea26864cd28214eebd.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　おかしくないか？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　誤クリック防止よ」  

## ボタンのスタイル

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あっ、この　ペール・バイオレット・レッド　のボタン、  
不活性にしても　グレーにならねーっ！」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　背景色を指定しているからな」  

![202308_maui_05-0147--buttonStyle-o2o0.png](https://crieit.now.sh/upload_images/2856912165bb029c019bf201eed2a7ef64cd2bd005eb8.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　このボタンのスタイル、　削除ボタンだけ　別のを使うようにできないのかだぜ？」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　`x:Name` 属性を使って　なんとかならないかしらねえ？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👇 `x:Key` というのが　あるそうだぜ」  

📖 [XAML を使用してアプリのスタイルを設定する](https://learn.microsoft.com/ja-jp/dotnet/maui/user-interface/styles/xaml)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　でけた」  

## キュー構造だから、任意の場所の要素を削除できねー

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　キュー構造だから、任意の場所の要素を削除できねー」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　前から入れて、後ろから出すしか　できないぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　なんで　キューなんかにしたのよ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　コンカレント処理に強ければ　なんでもいいと思ったんだぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　`ConcurrentBag` にしよかな」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　`ConcurrentBag` も要素を削除できないのか」  

📖 [How to remove a single, specific object from a ConcurrentBag<>?](https://stackoverflow.com/questions/3029818/how-to-remove-a-single-specific-object-from-a-concurrentbag)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　非同期処理をしているときには　どんなコレクションが使えるんだぜ？」  

![202308_maui_05-0302--importAndDelete.png](https://crieit.now.sh/upload_images/faaed6043ec972dcd9a68adad366e9bf64cd3d8b28804.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　連続読込は　ConcurrentBag、　UI は同期のコレクションに変えた。  
インポートと　タイルセット削除は　でけたぜ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　疲れたので　今日は　ここまで」  

# 📅 （2023-08-05 sat）たいとる１

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　`たいとる１`  って何なの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　タイルセットのタイトルを入れたいところだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　入れなさいよ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　PNG画像があって、TOMLファイルが無いといったペアがあるとき、  
TOMLファイルを自動生成する機能が要るぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　その機能を付けなさいよ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　タイミングとしては、インポート、タイルセット削除、ファイルのリネームの３か所だな」  

（カタ　カタ　カタ　カタ）  

![202308_maui_05-1428--tilesetGlobalConfig.png](https://crieit.now.sh/upload_images/fbb45c3876ab54aa62962c269d88f77264cdde1f5296b.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　万国共通の構成ファイルの方は　自動生成するようにしたが、  
画像のタイトルは　ロケール別の構成ファイルの方に入れる仕様だぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ロケール別の構成ファイルも　早よ作れだぜ」  

## ロケール用のフォルダーを作ろうぜ？

```plaintext
cultureInfo:
    DisplayName: 日本語 (日本),
    EnglishName: Japanese (Japan),
    Name: ja-JP,
    NativeName: 日本語 (日本),
    IetfLanguageTag: ja-JP,
    TwoLetterISOLanguageName: ja,
    ToString(): ja-JP

cultureInfo:
    DisplayName: 英語 (アメリカ合衆国),
    EnglishName: English (United States),
    Name: en-US,
    NativeName: English (United States),
    IetfLanguageTag: en-US,
    TwoLetterISOLanguageName: en,
    ToString(): en-US

cultureInfo:
    DisplayName: 中国語 (簡体字),
    EnglishName: Chinese (Simplified),
    Name: zh-Hans,
    NativeName: 中文（简体）,
    IetfLanguageTag: zh-Hans,
    TwoLetterISOLanguageName: zh,
    ToString(): zh-Hans
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　画面のピッカーには `NativeName` で表示した方がいいのかな……？」  

![202308_maui_05-1558--NativeName.png](https://crieit.now.sh/upload_images/e48e6444c8902db407fb272a348880d764cdf32fc8618.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`NativeName` の方が　かっこいいな　こっちにしよ」  

![202308_maui_05-1705--TilesetLocalConfig.png](https://crieit.now.sh/upload_images/15c750fce282f349c9f65edefb424e7264ce02e829524.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ひとまず　ロケール用のフォルダーと、空のファイルを作成……」  

## コレクション・ビューの選択中の色

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　コレクション・ビューの選択中の色、グレーで見にくいな！」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　スタイル　見たけど　さっぱり分からん。後回し。ダークモード消したった」  

## タイルセット削除ボタンの位置

![202308_maui_05-1739--TilesetDeleteBtn.png](https://crieit.now.sh/upload_images/3db68693fbf1a13ab7592ef34e7de8d864ce0af39cc56.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　タイルセット削除ボタンの位置を　左下に戻したった」  

## プレースホルダ―のロケール設定を分けたいぜ

```toml
[global]

uuid = "6FBD83F1-D2A3-45EE-AFC5-411CD6E50144"
extension = ".toml"

[local_placeholder]

title = "適当に作った画像"
author = "むずでょ"

[user_defined]
```

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　タイルセット・グローバル構成ファイルに　`local_placeholder`　という項目が欲しいぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　最初に思っていなかった　いろんなものを　盛り込むなあ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　英語で設定されていて、日本語で設定されていないとき、  
英語の設定を　プレースホルダーで表示してくれてもいいじゃないか、というものだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　第１言語、第２言語を選べるようにすりゃいいんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　フーム、大改造は嫌なんで　後回しにするか」  

## タイルセットのタイトル

![202308_maui_05-1859--TilesetTitle.png](https://crieit.now.sh/upload_images/abd33f51c5e1433c223b48cdff5c5eb364ce1dcc4439d.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　まず　日本語で対応を進めていこう」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　画面から　タイトルを変更できるようにしてくれだぜ」  

## エントリー（テキストボックス）で１文字入力するたびにビュー・モデルが更新されるのつらい

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　エントリー（テキストボックス）で１文字入力するたびにビュー・モデルが更新されるのつらい」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　それが　MVVM　なんじゃない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　このサイトは　細かく書いてるが　知りたい情報はあるかな？」  

📖 [TextEditBase.Text Property](https://docs.devexpress.com/MAUI/DevExpress.Maui.Editors.TextEditBase.Text)  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　👇　`Completed`　というイベントハンドラがあるのでは？」  

📖 [Microsoft　＞　入力](https://learn.microsoft.com/ja-jp/dotnet/maui/user-interface/controls/entry)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　それを使うように変えていくか～」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　なんだこれ　エンター・キー　押さないと　`Completed` しないじゃないか。  
エンター・キー押さなかったら　どうする？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　エントリーで　タブ・キー押したら　タブが入るし」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　MAUI の不具合なんだから、**エンター・キーを押せ** という運用でやるしかなくない？」  

## 画面を移動するボタンと、それ以外の機能があるボタン

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　画面の移動も、　データの編集も　同じボタンの形状なの　分かりづらいんだが」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　そんなもんだろ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　画面遷移も　スライドするトランジションが入るやつと、  
トランジションが無いやつもあるし」  

```csharp
        //
        await contentPage.Navigation.PushAsync(new ConfigurationPage());

        //
        var shellNavigationState = new ShellNavigationState("//MapExplorerPage");
        await Shell.Current.GoToAsync(shellNavigationState);
```

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　👆　やり方が２つあるのも　分けわかんないわねえ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　`Navigation` を使うやつは、戻るボタンが勝手に付くのでは？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　わざわざ　`ShellNavigationState`　を使うやり方って　メリット無いの？」  

## 日本語と　英語で　別々にタイトルを付けれる機能

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　日本語と　英語で　別々にタイトルを付けれる機能の実装を進めるぜ」  

![202308__maui__06-0034--localization-downsize2.gif](https://crieit.now.sh/upload_images/fbced500be8f0555a4b00072d6dd459f64ce6e9d18385.gif)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　付けた」  

## アンドゥとリドゥ

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そろそろ　アンドゥと　リドゥができないことに　突っ込まれそうだぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　Command Pattern を自力実装するのか、それとも　MAUI　に何か便利なフレームワークがあるのかだぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　他人が作ってくれてるけど、自力実装すりゃ　いーんじゃねーの？」  

📖 [C#でUndo/Redoを実装した](https://qiita.com/nossey/items/c59910558d5501f03ad0)  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　ＭＶＶＭは　アンドゥに従ってくれんの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　全ての操作には、逆向きの操作も用意しておく必要があるな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　テキストボックスの文字を１つ消したのも戻せるレベルで　ヒストリーを作んの？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　エントリー（テキストボックス）には　アンドゥ・リドゥ機能がもう付いていたぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　じゃあ　MAUI　に元から付いている　アンドゥ・リドゥ機能と、  
お父んが作る　アンドゥ・リドゥ機能は　ぶつからないか？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　そこらへん　どうすんだろな？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　いきなり　バイナリで作ると　バグ取りが難しいだろうから、  
ヒストリーは　TOMLファイルで書き出すかな」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　削除したリソースや、　上書きで加工してしまったリソースとか　出てくると思うが  
復元する方法は？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ネギラーメンは　リソースは編集しないぜ。  
リソースを組み立てるだけだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　インポートした素材は　削除も　上書きもされないという前提に立っているのね」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　ネギラーメンから　素材を削除するときは、アンドゥもできません、ということにするんだぜ」  

# 📅 （2023-08-06 sun）アンドゥ・リドゥ機能を作れだぜ

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　アンドゥ・リドゥ機能を作れだぜ」  

![202308_maui_06-1152--Done-o2o0.png](https://crieit.now.sh/upload_images/eced50158e1543811a537a2f5002b09664cf0b5da767c.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　コンピューター将棋と同じだぜ。　全ての操作には、進むと　戻るが　あるんだぜ」  

![202308_maui_06-1201--History-o2o0.png](https://crieit.now.sh/upload_images/b4b3caeb34f9c7c450b0e45a8456888a64cf0d1c9d3c4.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　履歴管理機能も　便利機能を付けなければ　芯は　これだけだぜ」  

![202308_maui_06-1216--EventHandler-o2o0.png](https://crieit.now.sh/upload_images/7b4da01e8824b99d89ebdaf0f94c7abc64cf10b91cc4e.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　イベントハンドラの内、自動ではなく、ユーザーの操作によって呼び出されるものが  
ヒストリーの記録対象になるぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　じゃあ　試しに　ズームを　ヒストリカルにしなさいよ」  

![202308_maui_06-1221--Redo-o2o0.png](https://crieit.now.sh/upload_images/e053ab0d78aa219040d6bd7945d1eafc64cf11f641740.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ヒストリカルにしていないということは、  
リドゥの方向のプログラムしか　書かれていないということだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　画面の更新しか　書いてなくない？　ズームの数字が　いくつから　いくつへ変更したか書いてないわよ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　コード・ビハインドだからな」  

![202308_maui_06-1315--Zoom.png](https://crieit.now.sh/upload_images/e50cabb8fd3965441c338b531452251864cf1e63b02b8.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ズームして何が起こるかは　ビュー・モデルの方に書いてある」

![202308_maui_06-1318--ZoomChange-o2o0.png](https://crieit.now.sh/upload_images/ad70323f0257024d839d8540142d204464cf1f3aea353.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　ズームすると、画像を作り直したり、グリッドを作り直したり、  
切抜きカーソルを描き直したり、カラーマップを描き直したり、  
広範囲に影響があるようだぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　ズームの変更と、ズームの変更によって起こる事象を切り分けてくれだぜ」  

![202308_maui_06-1415--ZoomProcessing-o2o0.png](https://crieit.now.sh/upload_images/db3b8995061475c572c158c85dbcd69f64cf2cbd2922a.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`IDone` インターフェースは　`IProcessing`　インターフェースに名称変更、  
`Redo`　メソッドも　`Do`　に名称変更。そして実装」  

![202308_maui_06-1422--ZoomRecursive-o2o0.png](https://crieit.now.sh/upload_images/05df4970d2bdfbb0042de5f80ce0040a64cf2e6914f9c.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　`History` クラスも `Done` メソッドを止め、 `Do` メソッドに変更、中で `Processing` クラスの `Do` メソッドを呼び出している」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　あと、コードの書き方が下手で　無駄に再帰してるから　書き直したい」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　MAUI には　キーボード操作のイベントハンドラが無いじゃないか」  

📖 [.NET MAUIでのCtrl+Enterなどのキーイベントハンドリングの難しさ](https://qiita.com/selfstudy/items/d0f9520d3b40f8946d36)  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　スマホに　キーボードは付いていませんからね」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　`[Ctrl] + [Z]`　の操作をカスタマイズできないのかだぜ、クソじゃないか」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　MAUI は　意固地にも　画面上のボタンを押せ、ということかだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　それが　**クロスプラットフォームの真実**　なのでしょう」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　♪  
真理は　避けられず  
真実は　踏み込んだ者にしか知られず  
真相は　勝手なものである」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　お父んの前で　真実に触れるなだぜ」  

## アンドゥ・リドゥ　ボタンを置こうぜ？

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　アンドゥ・リドゥ　ボタンを置こうぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　Unicode には　アンドゥ・リドゥに適した絵文字は無いようだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　`アンドゥ`、 `リドゥ` と書かれたボタンが　画面上に置いてあるのも　邪魔ねえ」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　日本語なのに `Undo`、 `Redo` というボタンを画面に配置するのも　負けた気になるしな」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　画像ボタンは作れないのか？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👇　機能はあるみたいなんで、頑張って　絵　を描くか……」  

📖 [ImageButton](https://learn.microsoft.com/ja-jp/dotnet/maui/user-interface/controls/imagebutton)  

![202308_maui_06-1508--Buttons-o2o0.png](https://crieit.now.sh/upload_images/a328de6484934a7e4aece14b5b4cded564cf39226f259.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　置くとこないし、右上でどうだぜ？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　やっつけ　わらう」  

＜書きかけ＞