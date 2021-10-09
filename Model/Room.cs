namespace Room
{

    [System.Serializable]
    public class RoomData
    {
        public enum Roomtype
        {
            None, BlackSmith, CommendCenter, Bedroom, Laundryroom, Cockingroom
        }
        public int level = 0; //방레벨
    }
}
