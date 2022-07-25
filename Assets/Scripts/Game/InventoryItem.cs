namespace Assets.Scripts.Game
{
    public struct InventoryItem
    {
        public ItemId Item { get; set; }
        public int Amount
        {
            get { return Amount; }
            set
            {
                if (value == 0) Item = 0;
                Amount = value;

            }
        }

        public bool IsMaxStack() => Amount >= Item.Item().maxStack;
    }
}
