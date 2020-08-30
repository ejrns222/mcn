namespace Characters.Instructors
{
    public class CGangzi : InstructorBase
    {
        public override long Skill(long calculatedValue)
        {
            throw new System.NotImplementedException();
        }
    
        public CGangzi()
        {
            Tag = EInstructor.Gangzi;
            Desc = "종합 게임 스트리머. 엄청난 깡을 지니고 있다. 허스키한 목소리를 가지고 있으며 입이 좀 거칠다.";
            Name = "깡쥐";
            SkillName = "파쨘";
            SkillDesc = "악질시청자를 욕으로 쫒아내서 방송중 구독자 증가 폭 + 10%, 파랑킴 보유시 + 20%";
        }
    }
}
