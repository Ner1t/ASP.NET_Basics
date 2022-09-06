namespace Entities
{
    public class Reward
    {
        private static int _rewardId = 1;
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Reward()
        {
            Id = _rewardId;
            _rewardId++;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
