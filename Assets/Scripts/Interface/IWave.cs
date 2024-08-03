using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    public interface IWave
    {
        List<ObstacleTransform> GetObstacleSettings();
        float Wait();
    }
}
