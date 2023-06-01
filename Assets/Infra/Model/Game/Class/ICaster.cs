namespace Infra.Model.Game.Class
{
    /// <summary>
    /// Block과 연결을 위한 테스트 인터페이스
    /// </summary>
    internal interface ICaster
    {
        public int Skill1Gauge { get; set; }
        public int Skill2Gauge { get; set; }
        public int Skill3Gauge { get; set; }
        public int Skill4Gauge { get; set; }
    }
}