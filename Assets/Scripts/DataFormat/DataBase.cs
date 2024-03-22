using System;

namespace Personal.Study.Data
{
    [Serializable]
    public class DataBase
    {
        public static implicit operator bool(DataBase data)
            => data != null;
    }
}