using Panik;

public static class APKeyProgression
{
    public static RewardBoxScript.RewardKind GetRewardKind()
    {
        switch (APState.KeysCompleted)
        {
            case 0:
                return RewardBoxScript.RewardKind.DrawerKey0;
            case 1:
                return RewardBoxScript.RewardKind.DrawerKey1;
            case 2:
                return RewardBoxScript.RewardKind.DrawerKey2;
            case 3:
                return RewardBoxScript.RewardKind.DrawerKey3;
            default:
                return RewardBoxScript.RewardKind.DoorKey;
        }
    }
}
