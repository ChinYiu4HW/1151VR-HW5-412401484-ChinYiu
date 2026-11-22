# 1151VR-HW5-412401484


Youtube 連結︰<br>
https://youtu.be/p_F-tgneyy8


說明製作流程和相關操作 ︰<br>
只找到沒有綁骨的trainer 3D模組 -> mixamo 綁骨 + 下載扔球動畫 <br>
扔球時碰到PKM 時會播放VFX<br>
PKM 增加collider + rigibody，精靈球也加上collider<br>
把精靈球掛上script設定好collider 各種然後拖下去做成prefab<br>
新增樹、石頭、山、草地等Terrain<br>
用老師教的ScreenPointToRay 方式點擊螢幕扔球<br>
修bug 增加throwDelay 令球會在人物扔球動畫播完才產球射出<br>
命中 pkm 時收服(移除pkm 3D 模組 + 固定精靈球) 同時播放命中音效<br>
調整 Directional Light 位置與光影<br>
只找到沒有綁骨的trainer 3D模組 -> mixam@ o 綁骨 + 下載扔球動畫<br>
建立 Animator 管理 Idle 與扔球動作轉場<br>
建立精靈球產生器 +  spawnPoint<br>
