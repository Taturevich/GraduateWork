namespace AimlBotUI.Infrastructure
{
    public interface IOperations
    {
        void SaveCommand();

        void DeleteCommand();

        void UpdateCommand();

        void RefreshItemList();
    }
}
