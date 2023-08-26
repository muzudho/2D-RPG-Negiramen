# 動機

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　クリーン・アーキテクチャー（ `Clean Architecture` ）も  
オニオン・スキン・アーキテクチャー（ `Onion Skin Architecture` ）も学習コストが高いし　実践が面倒臭すぎる。  
もっと　学習スキルも　実践スキルも低い人が選べる　ヘタクソ向けの技術としての　モデルを提示しようぜ？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　名前は　インターフェース・フォーク・アーキテクチャー（ `Interface Fork Architecture` ）だぜ」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　長い名前だな」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　あんたが考えたアーキテクチャーを発表するのね」  

# 説明

![202308_program_26-0940--LooseDoorArchitectureAbstract.png](https://crieit.now.sh/upload_images/69727917bf9526594544bbe43c24bc0064e952c4256f2.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　１つの物は２つの側を持つぜ。屋外側と屋内側だぜ。これをドアと呼ぶとしようぜ」  

![202308_program_26-1019--DeliveryPerson.png](https://crieit.now.sh/upload_images/f05a0b38b2707a25bebf0a14ddb22dff64e953e102148.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　配達人は、配達物を　ドアの前までは　持ってきてくれるぜ」  

![202308_program_26-1028--DeliveryArea.png](https://crieit.now.sh/upload_images/64396a46b15fad4d25f66729af05ef1c64e9566292b32.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　この配達人は、１つの屋外につながるドアと、奥の部屋につながる複数のドアがある部屋に  
閉じ込められているぜ」  

![202308_program_26-0951--Indoor.png](https://crieit.now.sh/upload_images/dbaa9ade5ec6bcbc30d3853dfaf2d3b064e94eaeb893b.png)  
![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　これは　木構造　になるぜ。　ここで ...」  

![202308_program_26-1045--ForkSet.png](https://crieit.now.sh/upload_images/397e1441a5d3baa43e64795a860e0b4b64e95aaa444ad.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　配達人の動きに着目すると　フォークの集合　になっているな」  

![202308_program_26-1057--InsideOutsideFork.png](https://crieit.now.sh/upload_images/9f3a8d47e9721b2d75b438a88dc7c83364e95ccbdbf51.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　１つの　フォーク　に着目すると、  
屋外のフォークと、屋内のフォークがあるな。これの見方を逆さにすると……」  

![202308_program_26-1057--InterfaceFork.png](https://crieit.now.sh/upload_images/42faf81364d20c8cfff5babcdf6fb34b64e95dbf90571.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　１つの　フォークは　誰かの室内フォークでもあるし、　屋外フォークでもあるわけだぜ。  
これを　**インターフェース・フォーク**　と呼ぶとしようぜ？」  

![kifuwarabe-futsu.png](https://crieit.now.sh/upload_images/beaf94b260ae2602ca8cf7f5bbc769c261daf8686dbda.png)  
「　フォークと　インターフェース・フォークは　何が違う？」  

## フォークと　インターフェース・フォークの違い

![202308_program_26-1120--Adressing.png](https://crieit.now.sh/upload_images/1c0e062ef2a3e9de6f6943e4940f536e64e962715a0da.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　従来のフォークは、　上図２号室にいる人は　５号室にある宅配物を  
`E → B → C → H` とパスを辿ることで　アクセスするといったことができることを　比喩している」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　これは　よくある便利な方法なわりに、  プログラムがヘタクソな人が　プログラムを自由にヘタクソに書ける要因だぜ」  

![202308_program_26-1129--InterfaceFork.png](https://crieit.now.sh/upload_images/6d69462986f17d8aea5eed4c465f712d64e964758b26c.png)  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　👆　そこで　パス　を廃止し、 `５号室にアクセスする` といったサービスを、関連する配達員全員に持たせるんだぜ」  

![ohkina-hiyoko-futsu2.png](https://crieit.now.sh/upload_images/96fb09724c3ce40ee0861a0fd1da563d61daf8a09d9bc.png)  
「　コード量が　累積的に多くならない？」  

![ramen-tabero-futsu2.png](https://crieit.now.sh/upload_images/d27ea8dcfad541918d9094b9aed83e7d61daf8532bbbe.png)  
「　なる。　プログラミングのヘタクソさをカバーするための代償だぜ」  
