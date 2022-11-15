namespace Assets.Scripts.Game
{
    public struct InventoryItem
    {
        public ItemId Item { get; set; }

        private int m_amount;
        public int Amount
        {
            get { return m_amount; }
            set
            {
                if (value == 0) Item = 0;
                m_amount = value;

            }
        }

        public bool IsMaxStack() => Amount >= Item.Item().maxStack;
    }
}
