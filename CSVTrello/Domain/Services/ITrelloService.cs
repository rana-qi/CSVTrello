using Manatee.Trello;

namespace CSVTrello.Domain.Services
{
    public interface ITrelloService
    {
        Task AuthenticateAsync(string appKey, string userToken);
        Task<ICard> GetCardAsync(string boardId, string cardName);
        Task<ICard> CreateCardAsync(string listId, string cardName);
        Task UpdateCardDescriptionAsync(ICard card, string description);
        Task MoveCardToListAsync(ICard card, string listName);
    }
}
