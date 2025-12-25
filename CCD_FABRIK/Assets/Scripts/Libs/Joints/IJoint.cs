using Math;

/// <summary>
/// General interface for controllable joints in the robot.
/// Uses input to produce a <see cref="Transformation"/> bounded by a range.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IJoint<T> where T : Transformation
{
    public string PositiveMotionKeyId { get; }
    public string NegativeMotionKeyId { get; }

    public double MinRange { get; }
    public double MaxRange { get; }

    public double Delta { get; }

    public ClampedDouble t { get; set; }
    public ClampedDouble prev { get; set; }

    public void Setup()
    {
        t = new ClampedDouble(0, MinRange, MaxRange);
        prev = t;
    }

    public T GetLocalTransformation();

    public void PositiveMotionAction() =>
        t += Delta;
    public void NegativeMotionAction() =>
        t -= Delta;
}
