using CSVTrello.Domain.Services;
using Manatee.Trello;

namespace CSVTrello.Services
{
    public class TrelloService: ITrelloService
    {
        private ITrelloFactory _factory;
        private ILogger<TrelloService> _logger;

        public TrelloService(ITrelloFactory factory, ILogger<TrelloService> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public async Task AuthenticateAsync(string appKey, string userToken)
        {
            TrelloAuthorization.Default.AppKey = appKey;
            TrelloAuthorization.Default.UserToken = userToken;
            _logger.LogInformation("Authenticated with Trello API.");
        }

        public async Task<ICard> GetCardAsync(string boardId, string cardName)
        {
            var board = _factory.Board(boardId);
            await board.Refresh();
            var card = board.Cards.FirstOrDefault(c => c.Name == cardName);
            _logger.LogInformation($"Fetched card with name '{cardName}' from board '{boardId}'.");
            return card;
        }

        public async Task<ICard> CreateCardAsync(string listId, string cardName)
        {
            var list = _factory.List(listId);
            var card = await list.Cards.Add(cardName);
            _logger.LogInformation($"Created card with name '{cardName}' in list '{listId}'.");
            return card;
        }

        public async Task UpdateCardDescriptionAsync(ICard card, string description)
        {
            card.Description = description;
            await card.Refresh();
            _logger.LogInformation($"Updated description for card '{card.Name}'.");
        }

        public async Task MoveCardToListAsync(ICard card, string listName)
        {
            if (string.IsNullOrEmpty(listName))
            {
                throw new ArgumentException("List name cannot be null or empty", nameof(listName));
            }
            await card.Board.Refresh();
            var list = card.Board.Lists.FirstOrDefault(l => l.Name == listName);
            if (list != null)
            {
                card.List = list;
                await card.Refresh();
                _logger.LogInformation($"Moved card '{card.Name}' to list '{listName}'.");
            }
            else
            {
                _logger.LogWarning($"List '{listName}' not found on board '{card.Board.Id}'.");
                throw new ArgumentException($"List '{listName}' not found on board '{card.Board.Id}'.", nameof(listName));
            }
        }
    }
}