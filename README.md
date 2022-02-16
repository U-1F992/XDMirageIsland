# XDMirageIsland

```ps1
# Sample
.\XDMirageIsland.exe --tid 0 --sid 28552 --bonsly | %{[System.Convert]::ToString($_, 16)}
```

性格値下位16bitが0のポケスポット野生ポケモンが出現するseedを出力する。

これだけでは大量の個体がヒットするので、色違いとする。

ストーリー開始時から電池切れのRSEmはマボロシじまの出現判定を行う日替わりの乱数が0で固定されるため、この個体を手持ちに入れることでマボロシじまを出現させることができる。
