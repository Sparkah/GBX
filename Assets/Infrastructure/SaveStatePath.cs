using UnityEngine;

namespace Infrastructure
{
    public class SaveStatePath
    {
        public static readonly string StatePath = Application.persistentDataPath + "/config/local.state";
    }
}