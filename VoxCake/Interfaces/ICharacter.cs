public interface ICharacter
{
    byte Team { get; set; }
    int Health { get; set; }
    float WalkSpeed { get; set; }
    float RunSpeed { get; set; }
    float JumpHeight { get; set; }
    float Gravity { get; set; }

    string HeadModel { get; set; }
    string BodyModel { get; set; }
    string LeftArmModel { get; set; }
    string RightArmModel { get; set; }
    string LeftLegModel { get; set; }
    string RightLegModel { get; set; }
}