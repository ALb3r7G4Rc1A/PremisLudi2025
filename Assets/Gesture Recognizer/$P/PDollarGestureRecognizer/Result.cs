namespace PDollarGestureRecognizer
{
    /// <summary>
    /// Conté el resultat d'un reconeixement de gest.
    /// </summary>
    public class Result
    {
        public string GestureClass { get; private set; }
        public float Score { get; private set; }

        public Result(string gestureClass, float score)
        {
            GestureClass = gestureClass;
            Score = score;
        }
    }
}