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

＜書きかけ＞