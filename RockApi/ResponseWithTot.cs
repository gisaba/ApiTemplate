using System;
namespace RockApi
{
    public class ResponseWithTot<T>
    {
        public ResponseWithTot()
        {
        }
        public ResponseWithTot(T data, int pNumber, int pSize, int tot)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
            PageNumber = pNumber;
            PageSize = pSize;
            NextPage = setNextPage(pNumber);
            Totale = tot;
        }

        private int setNextPage(int pNumber)
        {
            if (pNumber == 0) return 0;

            int nextPage = pNumber < 1 ? 1 : pNumber+1;

            return nextPage;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
        public int PageNumber { get; }
        public int PageSize  {  get; }
        public int NextPage { get; set; }
        public int Totale { get; set; }
    }
}