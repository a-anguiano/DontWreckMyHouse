namespace DontWreckMyHouse.BLL
{
    public class Result<T>
    {
        private List<string> messages = new List<string>();
        public bool Success => messages.Count == 0;
        public List<string> Messages => new List<string>(messages);
        public T Value { get; set; }

        public void AddMessage(string message)
        {
            messages.Add(message);
        }
    }
}
