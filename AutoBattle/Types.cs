namespace AutoBattle
{
    public class Types
    {
        public struct GridBox
        {
            public int xIndex;
            public int yIndex;
            public Character character;
            public int index;

            public GridBox(int x, int y, int index)
            {
                xIndex = x;
                yIndex = y;
                this.character = null;
                this.index = index;
            }

        }
    }
}
