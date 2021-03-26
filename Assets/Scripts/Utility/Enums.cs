namespace PlantsVsZombies
{
    public class Enums
    {
        public enum ItemType : byte
        {
            Weapon = 0,
            Enemy
        }

        public enum WeaponType : byte
        {
            None = 0,
            Ninja,
            Samurai,
            ArrowTower,
            Rocket,
            Geisha,
            Tessenjutsu,
            Ronin,
            Tedate,
            PrayerTemple,
            Sorcerer
        }

        public enum EnemyType : byte
        {
            None = 0,
            Qilin,
            Oni,
            Baku,
            YukiOnna,
            Nuppeppo,
            NureOnna,
            Jorogumo,
            Tengu,
            Nuribotoko,
            AzureDragon
        }
    }
}