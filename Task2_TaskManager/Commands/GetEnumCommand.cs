namespace Task2_TaskManager.Commands
{
    public class GetEnumCommand<T> : ChooseEnumCommand<T> where T : Enum
    {
        public int EnumIndex => _enumIndex;

        private int _enumIndex;

        protected override void HandleEnumIndex(int enumIndex)
        {
            _enumIndex = enumIndex;
        }
    }
}