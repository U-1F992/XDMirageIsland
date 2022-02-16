using PokemonPRNG.LCG32.GCLCG;

public class Program : ConsoleAppBase
{
    public static void Main(string[] args)
    {
        ConsoleApp.Run<Program>(args);
    }

    [RootCommand]
    public void Calculate
    (
        [Option(null, "Trainer ID")] UInt32 tid,
        [Option(null, "Secret ID")] UInt32 sid,
        [Option(null, "Bonsly appears")] bool bonsly = false
    )
    {
        Parallel.For(0, 0x100000000, seed =>
        {
            var pid = GetPid((UInt32)seed);
            if (pid.lid == 0x0000 && Exists((UInt32)seed, bonsly) && IsShiny(pid, (tid, sid))) Console.WriteLine(seed);
        });
    }

    // n  , 出現判定, r%3==0
    // n+1
    // n+2, ウソハチゴンベ判定, ウソハチ判定あり: (ウソハチ)r%100<30 (ゴンベ)30<=r%100<40, 判定なし: r%100<10
    // n+3
    // n+4, HID
    // n+5, LID

    /// <summary>
    /// 開始時seedから生成される個体のPIDを返す
    /// </summary>
    /// <param name="seed"></param>
    /// <returns></returns>
    (UInt32 hid, UInt32 lid) GetPid(UInt32 seed)
    {
        seed.Advance(4);
        var hid = seed.GetRand();
        var lid = seed.GetRand();
        return (hid, lid);
    }

    /// <summary>
    /// 出現判定を踏み、ゴンベやウソハチに遮られないseedか判定する
    /// 
    /// 開始時seedはXDSearch準拠 (なつき判定を考慮しない)
    /// https://twitter.com/sub_827/status/1267288264966193152
    /// </summary>
    /// <param name="seed"></param>
    /// <param name="bonsly">ウソハチ出現判定の有無</param>
    /// <returns></returns>
    bool Exists(UInt32 seed, bool bonsly)
    {
        var appear = seed.GetRand(3) == 0;
        seed.Advance();
        var disturb = seed.GetRand(100) < (bonsly ? 40 : 10);
        return appear && !disturb;
    }

    /// <summary>
    /// 色違いか判定する
    /// https://larvesta10.hatenablog.com/entry/2020/03/03/005318
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    bool IsShiny((UInt32 hid, UInt32 lid)pid, (UInt32 tid, UInt32 sid)id)
    {
        return (id.tid ^ id.sid ^ pid.hid ^ pid.lid) <= 7;
    }
}