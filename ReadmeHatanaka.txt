現在、制作に使用しているプロジェクトファイルです。

このReadmeには畑中龍之介がこだわっている箇所を記載しようと思います。


〇キャラクターのクラス設計について
　・クラスの説明
　CharacterBase.csファイルのCharacterBaseクラスであたり判定を行います。
　
　・こだわりの関数：hitAllyParent,hitAllyHenchman,HitEnemyParent,HitEnemyHenchman,hitWildHenchman
　この関数は仮想関数で、あたり判定の際にCharacterBaseクラスで呼び出されます。
　
　・仮想関数にした訳
　あたり判定の変更をしやすくする。
　
　・ワンポイント
　処理内は「自分がどうなるのか」の処理のみすることで、オブジェクト同士の依存を極力減らしています。


〇神クラスの設計について
　・クラスの説明
　FamilyManager.csのFamilyManagerクラスは参照先にオブジェクトを渡すことが出来ます。
　
　・こだわりの変数：_henchman_list,_henchman_obj_list
　Dictonaryを使って親と子を紐づけました。
　１つの親に対して子分が複数いる設計になるので、子分をキーにして親をコンテンツにしました。

〇親の動きの設計
　・クラスの説明
　MoveCommonBase.csを継承しているクラスのMovePlayer1とMovePlayer2を作成し親のオブジェクトにアタッチする仕組みにしています。
　
　・動きだけ分けた訳
　親と子分のオブジェクトは自分に対して味方か敵かだけを見ています。そのため、動きを変えるだけでプレイヤーかどうか区別できます。
　また、敵の動きのパターンを簡単に増やすことが出来ます。
