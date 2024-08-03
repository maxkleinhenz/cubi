namespace Assets.Scripts
{
    public class ObstacleTransform
    {
        public float XPosition { get; set; }

        public int Width { get; set; }

        public ObstacleTransform(float xPosition, int width)
        {
            this.XPosition = xPosition;
            this.Width = width;
        }

        public void CorrectPosition()
        {
            var h = (Width - 1) / 2;
            XPosition += h;
        }
    }
}
