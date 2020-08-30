namespace Characters.Instructors
{
    public class CSsuckSso : InstructorBase
    {
        public override long Skill(long calculatedValue)
        {
            throw new System.NotImplementedException();
        }
    
        public CSsuckSso()
        {
            Tag = EInstructor.SsuckSso;
            Desc = "종합게임 크리에이터, 엄청난 현질력으로 게임을 정복한다.";
            Name = "싹쑤";
            SkillName = "고녀석";
            SkillDesc = "자동화 마우스 '고녀석'이 영상 편집을 도와준다. \n일일 편집 영상수 + 2";
        }
    }
}