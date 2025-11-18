using Domain.Shared.Responses;

namespace Domain.Shared.Requests
{
    public class QueryRequest<TResponse> : Request<TResponse> where TResponse : Response
    {
        private string Search { get; set; }
        private int Pages { get; set; }
        private int Items { get; set; }
        private bool IsActive { get; set; }

        public void SetSearch(string search)
        {
            Search = search;
        }

        public string GetSearch()
        {
            return Search;
        }

        public int GetPages()
        {
            return Pages;
        }

        public int GetItems()
        {
            return Items;
        }

        public void SetPageItems(int page, int items)
        {
            Pages = page;
            Items = items;
        }

        public int GetTotalPages(int totalItems)
        {
            if (Items == 0) return 0;

            return (int)Math.Ceiling((double)totalItems / Items);
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public bool GetIsActive()
        {
            return IsActive;
        }
    }
}
