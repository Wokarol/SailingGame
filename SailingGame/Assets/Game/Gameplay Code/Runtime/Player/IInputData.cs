using UnityEngine;

namespace Wokarol
{
    public interface IMotorInput
    {
        Vector2 Direction { get; }
        float SailPower { get; }
    }
}