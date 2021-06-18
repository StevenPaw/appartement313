using System.Collections.Generic;
using System.Linq;

namespace _GAMEASSETS.Scripts
{
    public static class ElevatorManager
    {
        private static PlayerController playerInElevator = null;
        private static List<Elevator> levels = new List<Elevator>();

        public static PlayerController PlayerInElevator
        {
            get => playerInElevator;
            set => playerInElevator = value;
        }

        public static bool AddLevel (Elevator elevatorIn, int levelIn)
        {
            if (GetElevatorByLevel(levelIn) == null)
            {
                levels.Add(elevatorIn);
                levels.OrderBy(o => elevatorIn.Level);
                return true;
            }
            return false;
        }

        public static Elevator GetUpElevator(int currentLevel)
        {
            if (GetElevatorByLevel(currentLevel + 1) != null)
            {
                return GetElevatorByLevel(currentLevel + 1);
            }
            else
            {
                return GetElevatorByLevel(currentLevel);
            }
        }
        
        public static Elevator GetDownElevator(int currentLevel)
        {
            if (GetElevatorByLevel(currentLevel - 1) != null)
            {
                return GetElevatorByLevel(currentLevel - 1);
            }
            else
            {
                return GetElevatorByLevel(currentLevel);
            }
        }

        private static Elevator GetElevatorByLevel(int levelIn)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].Level == levelIn)
                {
                    return levels[i];
                }
            }
            return null;
        }
    }
}