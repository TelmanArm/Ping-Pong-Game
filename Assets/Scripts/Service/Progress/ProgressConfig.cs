using UnityEngine;

namespace Service.Progress
{
    [CreateAssetMenu(fileName = "Progress Config", menuName = "Config/Progress Config", order = 0)]
    public class ProgressConfig : ScriptableObject
    {
        [SerializeField] private int firstLevelProgress;
        [SerializeField] private int addProgressEachLevel;

        public int FirstLevelProgress => firstLevelProgress;
        public int AddProgressEachLevel => addProgressEachLevel;
    }
}