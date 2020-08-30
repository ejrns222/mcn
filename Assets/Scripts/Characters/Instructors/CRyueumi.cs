namespace Characters.Instructors
{
    public class CRyueumi : InstructorBase
    {
        public override long Skill(long calculatedValue)
        {
            throw new System.NotImplementedException();
        }
    
        public CRyueumi()
        {
            Tag = EInstructor.Ryueumi;
            Desc = "방송을 귀찮아하는 종합 게임 스트리머. 게임을 특이하게 플레이하는 것을 즐긴다.";
            Name = "유의미";
            SkillName = "하꼬헌터";
            SkillDesc = "초보 스트리머들을 어디에선가 데리고와서 들들볶는것을 자주 볼 수 있다. 7번,8번 교육생 일일 구독자 수 2배 증가";
        }
    }
}