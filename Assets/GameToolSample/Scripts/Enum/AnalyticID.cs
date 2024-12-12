namespace GameToolSample.Scripts.Enum
{
    public static class AnalyticID
    {
        public enum GamePlayState
        {
            none,
            victory,
            lose,
            die,
            revive,
            quit,
            skip,
            pause,
            playing,
            replay
        }

        public enum GameEconomyState
        {
            none,
            spend,
            earn
        }

        public enum GameItemState
        {
            none,
            unlock,
            trying,
            select,
            preview
        }

        public enum UseBehaviourState
        {
            none,
            app_quit,
            uninstall
        }

        public enum GameModeName
        {
            none
        }

        // public class AdjustEventToken
        // {
        //     public static string first_open = "oyvccc";
        //     public static string inter_click = "n4fccc";
        //     public static string inter_impression = "h66ccc";
        //     public static string reward_click = "1zcccc";
        //     public static string reward_completed = "8ojccc";
        //     public static string reward_impression = "eopccc";
        // }
    }
}
